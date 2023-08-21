using LoginSystemBackEnd.Data;
using LoginSystemBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LoginSystemBackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly DbContextProvider _dbContextProvider;
     
        public UserService(DbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
           
        }

        public async Task<Users> Authenticate(UserLoginModel userLoginModel)
        {
            return await _dbContextProvider.UseContextAsync(async dbContext =>
            {
                var user = await dbContext.LoginUsers.SingleOrDefaultAsync(x => x.Email == userLoginModel.Email);

                // Kullanıcı adına ait bir kullanıcı var mı kontrol edelim.
                if (user == null)
                {
                    return null; // Kullanıcı bulunamadı.
                }
            
                // Kullanıcı parolasını doğrulayalım.
                if (!VerifyPasswordHash(userLoginModel.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return null; // Parola hatalı.
                }

                // Kullanıcı doğrulandı, kullanıcı nesnesini döndürelim.
                return user;
            });

        }
      
        

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }
            return true;
        }



        public async Task<Users> Register(UserCredentials userCredentials)
        {
            if (!IsPasswordValid(userCredentials.Password))
            {
                return null; // Kullanıcıdan gelen şifre kurallara uymuyor
            }

            return await _dbContextProvider.UseContextAsync(async dbContext =>
            {
                // email tanımlı mı kontrol et
                if (await dbContext.LoginUsers.AnyAsync(x => x.Email == userCredentials.Email))
                {
                    return null; //kullanıcı adı zaten kullanılıyor
                }
              
                // Parolayı şifreleme 
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userCredentials.Password, out passwordHash, out passwordSalt);

                var newUser = new Users
                {
                    Name=userCredentials.Name,
                    Surname=userCredentials.Surname,
                    Email = userCredentials.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                dbContext.LoginUsers.Add(newUser);
                await dbContext.SaveChangesAsync();

                return newUser;
            });
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool IsPasswordValid(string password)
        {
            // Şifre en az bir büyük harf, bir sayı ve bir sembol içermeli
            bool hasUpperCase = false;
            bool hasDigit = false;
            bool hasSymbol = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUpperCase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (char.IsSymbol(c) || char.IsPunctuation(c))
                {
                    hasSymbol = true;
                }
            }

            return hasUpperCase && hasDigit && hasSymbol;
        }

    }
}

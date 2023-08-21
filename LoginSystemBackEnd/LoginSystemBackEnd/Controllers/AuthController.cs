using LoginSystemBackEnd.Models;
using LoginSystemBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystemBackEnd.Controllers
{

    public class AuthController : Controller
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserCredentials userCredentials)
        {
            if (!IsPasswordValid(userCredentials.Password))
            {
                //view tarafında modelstate ile kullanıldı
                ModelState.AddModelError("PasswordInvalid", "Şifreniz en az bir büyük harf, bir sayı ve bir sembol içermelidir.");
              
            }
            if (ModelState.IsValid)
            {
                
                var newUser = await _userService.Register(userCredentials);

                if (newUser == null)
                {
                    ModelState.AddModelError("UsernameInUse", "Bu Email zaten kullanılıyor.");
                    return View();
                }
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        private bool IsPasswordValid(string password)
        {
            // şifre en az bir büyük harf, bir sayı ve bir sembol içerecek kod fonksiyonu
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                var authenticatedUser = await _userService.Authenticate(userLoginModel);

                if (authenticatedUser == null)
                {
                    ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                    return View();
                }

                return RedirectToAction("Index", "Home");           
            }
            return View();
        }
    }
}
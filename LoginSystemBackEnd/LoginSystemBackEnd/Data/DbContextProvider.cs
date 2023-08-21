namespace LoginSystemBackEnd.Data
{
    public class DbContextProvider
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DbContextProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        //ObjectDisposedException hatasını önlemek için bu metodu yazdım.
        //hayat döngüsü yönetimi hatasından kaynaklanan problemdi.
        //singelton kullanmak için 
        //DbContextProvider yardımcı sınıfı olmadan UserServiceyi singelton kullanamıyoruz.
        //IdentityContext nesnesi üzerinde çalışacak işlevi parametre olarak alır ve sonucu TResult döndürür.
        public async Task<TResult> UseContextAsync<TResult>(Func<IdentityContext, Task<TResult>> func)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            return await func(dbContext);
        }
    }
}

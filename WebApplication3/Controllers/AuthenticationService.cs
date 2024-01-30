using System.Security.Cryptography;
using System.Text;

namespace WebApplication3.Controllers
{
    public class AuthenticationService
    {
        private readonly AppDbContext _dbContext;

        public AuthenticationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ValidateCredentials(string login, string password)
        {
            var worker = _dbContext.workers.FirstOrDefault(w => w.w_login == login);

            if (worker != null)
            {
                if(password != null)
                {
                    // Сравнение хэша введенного пароля с хэшем пароля из базы данных
                    bool passwordIsValid = VerifyPasswordHash(password, worker.w_pass);
                    return passwordIsValid;
                }
            }

            return false;
        }


        private bool VerifyPasswordHash(string password, string hashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Конвертируем байтовый массив в строку шестнадцатеричного представления
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                string hashedInputPassword = builder.ToString();

                Console.WriteLine(hashedInputPassword);
                Console.WriteLine(hashedPassword);

                // Сравниваем полученный хеш с хешем из базы данных
                return string.Equals(hashedInputPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
            }
        }

    }
}

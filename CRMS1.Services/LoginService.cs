using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Services
{
    public interface ILoginService
    {
        User Login(LoginViewModel model);
    }
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUserService _userService;
        public LoginService(LoginRepository loginRepository , IUserService userService)
        {
            _loginRepository = loginRepository;
            _userService = userService;
        }

        public User Login(LoginViewModel model)
        {
           
            model.Password = _userService.PasswordEncode(model.Password);
            var user = _loginRepository.Collection().Where(a => a.Email == model.Email && a.Password == model.Password).FirstOrDefault();
            return user;
        }
    }
}

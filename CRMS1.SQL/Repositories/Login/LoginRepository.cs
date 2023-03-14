using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.Login
{
    public class LoginRepository
    {
        public CRMSEntities context;

        public LoginRepository()
        {
            context = new CRMSEntities();
        }

        public User Login(LoginViewModel model)
        {
            var user = context.Users.Where(a => a.Email == model.Email && a.Password == model.Password).FirstOrDefault();
            return user;
        }
    }
}

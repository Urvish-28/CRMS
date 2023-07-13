using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using CRMS1.SQL.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRMS1.Services
{
    public interface IUserService
    {
        void AddUser(UserViewModel model);
        void UpdateUser(UserViewModel model);
        void DeleteUser(Guid id);
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        User GetUserByEmail(string Email);
        IEnumerable<IndexViewModel> GetUserList();
        User BindUserModel(UserViewModel model);
        UserViewModel BindUserModel(User model);
        string PasswordEncode(String Password);
        bool IsAlreadyExist(UserViewModel model, bool IsCreated = false);
        void UpdatePassword(ChangePasswordViewModel model);
        bool Checkpassword(ChangePasswordViewModel model);
    }
    public class UserService :Page, IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserRoleService _userRoleService;
        public UserService(IUsersRepository usersRepository, IUserRoleService userRoleService)
        {
            _usersRepository = usersRepository;
            _userRoleService = userRoleService;
        }
        public void AddUser(UserViewModel model)
        {
            User obj = new User();
            obj = BindUserModel(model);
            _usersRepository.Insert(obj);

            UserRoles userRoles = new UserRoles();
            userRoles.UserId = obj.Id;
            userRoles.RoleId = model.RoleId;
            _userRoleService.CreateUserRole(userRoles);
        }
        public void UpdateUser(UserViewModel model)
        {
            User userToUpdate = GetUserById(model.Id);
            if (userToUpdate != null)
            {
                userToUpdate = BindUserModel(model);
                _usersRepository.Update(userToUpdate);

                UserRoles userroles = _userRoleService.GetByUserId(userToUpdate.Id);
                userroles.UserId = model.Id;
                userroles.RoleId = model.RoleId;
                _userRoleService.UpdateUserRole(userroles);
            }
        }
        public void DeleteUser(Guid id)
        {
            User obj = _usersRepository.Find(id);
            obj.IsDelete = true;
            _usersRepository.Update(obj);

            UserRoles userroles = _userRoleService.GetByUserId(id);
            // userroles.UserId = id;
            _userRoleService.DeleteUserRole(id);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _usersRepository.Collection().Where(x => x.IsDelete == false);
        }
        public User GetUserById(Guid id)
        {
            return _usersRepository.Find(id);
        }
        public User GetUserByEmail(string Email)
        {
            return _usersRepository.FindByEmail(Email);
        }
        public IEnumerable<IndexViewModel> GetUserList()
        {
            return _usersRepository.UserList();
        }
        public User BindUserModel(UserViewModel model)
        {
            User obj = GetUserById(model.Id);
            if (obj == null)
            {
                obj = new User();
                obj.CreatedOn = DateTime.Now;
                obj.CreatedBy = (Guid)Session["UserId"];
                obj.Password = model.Password;
                obj.Password = PasswordEncode(obj.Password);
            }
            else
            {
                obj.UpdatedOn = DateTime.Now;
                obj.UpdatedBy = (Guid)Session["UserId"];
            }
            obj.Name = model.Name;
            obj.Email = model.Email;
            obj.UserName = model.UserName;
            obj.MobileNo = model.MobileNo;
            obj.Gender = model.Gender;
            return obj;
        }
        public UserViewModel BindUserModel(User model)
        {
            UserViewModel obj = new UserViewModel();
            obj.Name = model.Name;
            obj.Email = model.Email;
            obj.Password = model.Password;
            obj.UserName = model.UserName;
            obj.MobileNo = model.MobileNo;
            obj.Gender = model.Gender;
            obj.CreatedOn = DateTime.Now;
            obj.UpdatedOn = DateTime.Now;
            return obj;
        }
        public string PasswordEncode(String Password)
        {
            try
            {
                byte[] EncDataByte = new byte[Password.Length];
                EncDataByte = System.Text.Encoding.UTF8.GetBytes(Password);
                string EnCryptedPassword = Convert.ToBase64String(EncDataByte);
                return EnCryptedPassword;
            }
            catch (Exception exception)
            {
                throw new Exception("Error in Encode: " + exception.Message);
            }
        }
        /*        public string PasswordDecode(String EnCryptedPassword)
                {
                    try
                    {
                        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                        System.Text.Decoder UTF8Decode = encoder.GetDecoder();
                        byte[] DecodeByte = Convert.FromBase64String(EnCryptedPassword);
                        int CharCount = UTF8Decode.GetCharCount(DecodeByte, 0, DecodeByte.Length);
                        Char[] DecodeChar = new char[CharCount];
                        UTF8Decode.GetChars(DecodeByte, 0, DecodeByte.Length, DecodeChar, 0);
                        string DeCryptedPassword = new string(DecodeChar);
                        return DeCryptedPassword;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Error in Decode: " + exception.Message);
                    }
                }*/
        public bool IsAlreadyExist(UserViewModel model, bool IsCreated = false)
        {
            var records = GetAllUsers().Where(x => (x.Email == model.Email &&
                                                    x.MobileNo == model.MobileNo) && 
                                                    (IsCreated || x.Id != model.Id)).ToList();
            if (records.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdatePassword(ChangePasswordViewModel model)
        {
            User obj = GetUserById(model.Id);
            obj.Password = model.ConfirmPassword;
            obj.Password = PasswordEncode(obj.Password);
            _usersRepository.Update(obj);
        }
        public bool Checkpassword(ChangePasswordViewModel model)
        {
            model.Password = PasswordEncode(model.Password);
            var password = GetAllUsers().Where(x => x.Password == model.Password).ToList();
            if (password.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}


﻿using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.UserRole;
using CRMS1.SQL.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Services
{
    public interface IUserService
    {
        void AddUser(UserViewModel model);
        void UpdateUser(UserViewModel model);
        void DeleteUser(Guid id);
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        IEnumerable<IndexViewModel> GetUserList();
        User BindUserModel(UserViewModel model);
        UserViewModel BindUserModel(User model);
        string PasswordEncode(String Password);
        bool IsAlreadyExist(UserViewModel model, bool IsCreated = false);
    }
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        public UserService(IUsersRepository usersRepository , IUserRoleService userRoleService, IRoleService roleService)
        {
            _usersRepository = usersRepository;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }
        public void AddUser(UserViewModel model)
        {
            User obj = new User();
            obj = BindUserModel(model);
            _usersRepository.Insert(obj);
            _usersRepository.Commit();

            UserRoles userRoles = new UserRoles();
            userRoles.UserId = obj.Id;
            userRoles.RoleId = model.RoleId;

            _userRoleService.createUserRole(userRoles);
        }
        public void UpdateUser(UserViewModel model)
        {
            User userToUpdate = GetUserById(model.Id);
            if(userToUpdate != null)
            {
                userToUpdate = BindUserModel(model);
                _usersRepository.Update(userToUpdate);
                _usersRepository.Commit();

                UserRoles userroles = new UserRoles();
                userroles.UserId = _userRoleService.GetAllUserRoles().Where(b => b.UserId == model.Id).FirstOrDefault().UserId;
                userroles.RoleId = model.RoleId;
                _userRoleService.updateUserRole(userroles);
            }
           
        }
        public void DeleteUser(Guid id)
        {
            User obj = _usersRepository.Find(id);
            obj.IsDelete = true;
            _usersRepository.Update(obj);
            _usersRepository.Commit();

            UserRoles userroles = new UserRoles();
            userroles.UserId = id;
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

        public IEnumerable<IndexViewModel> GetUserList()
        {
            IEnumerable<User> user = GetAllUsers();
            IEnumerable<Roles> role = _roleService.GetAllRoles();
            IEnumerable<UserRoles> userrole = _userRoleService.GetAllUserRoles();

            var userlist = from u in user
                           join x in userrole on u.Id equals x.UserId
                           join r in role on x.RoleId equals r.Id
                           select new IndexViewModel()
                           {
                               Id = u.Id,
                               Name = u.Name,
                               Email = u.Email,
                               UserName = u.UserName,
                               Gender = u.Gender,
                               MobileNo = u.MobileNo,
                               Role = r.Name
                           };
            return userlist;
        }

        public User BindUserModel(UserViewModel model)
        {
            User obj = GetUserById(model.Id);
            if(obj == null)
            {
                obj = new User();
                obj.CreatedOn = DateTime.Now;
            }
            else
            {
                obj.UpdatedOn = DateTime.Now;
            }
            obj.Name = model.Name;
            obj.Email = model.Email;
            obj.Password = model.Password;
            obj.UserName = model.UserName;
            obj.MobileNo = model.MobileNo;
            obj.Gender = model.Gender;
            obj.Password = PasswordEncode(obj.Password);
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
            catch(Exception exception)
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
                                                    x.MobileNo == model.MobileNo) && (IsCreated || x.Id != model.Id)).ToList();

            if(records.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


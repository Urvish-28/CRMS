using CRMS1.Core.Models;
using CRMS1.SQL.Repositories.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Services
{
    public interface IUserRoleService
    {
        void createUserRole(UserRoles model);
        void updateUserRole(UserRoles model);
        UserRoles getByUserId(Guid userId);
        void DeleteUserRole(Guid id);
        IEnumerable<UserRoles> GetAllUserRoles();
    }
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public void createUserRole(UserRoles model)
        {
            _userRoleRepository.Insert(model);
            _userRoleRepository.Commit();
        }
        public void updateUserRole(UserRoles model)
        {
            _userRoleRepository.Update(model);
            _userRoleRepository.Commit();
        }
        public UserRoles getByUserId(Guid userId)
        {
            return _userRoleRepository.Collection().Where(x=>x.UserId == userId).FirstOrDefault();
        }
        public void DeleteUserRole(Guid id)
        {
            UserRoles obj = _userRoleRepository.Find(id);
            obj.IsDelete = true;
            _userRoleRepository.Update(obj);
            _userRoleRepository.Commit();
        }
        public IEnumerable<UserRoles> GetAllUserRoles()
        {
            return _userRoleRepository.Collection().Where(x => x.IsDelete == false);
        }
    }
}

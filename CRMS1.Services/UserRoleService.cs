using CRMS1.Core.Models;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Services
{
    public interface IUserRoleService
    {
        void CreateUserRole(UserRoles model);
        void UpdateUserRole(UserRoles model);
        UserRoles GetByUserId(Guid userId);
        void DeleteUserRole(Guid id);
        IEnumerable<UserRoles> GetAllUserRoles();
    }
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<UserRoles> _userRoleRepository;
        public UserRoleService(IRepository<UserRoles> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public void CreateUserRole(UserRoles model)
        {
            _userRoleRepository.Insert(model);
            _userRoleRepository.Commit();
        }
        public void UpdateUserRole(UserRoles model)
        {
            _userRoleRepository.Update(model);
            _userRoleRepository.Commit();
        }
        public UserRoles GetByUserId(Guid userId)
        {
            return _userRoleRepository.Collection().Where(x=>x.UserId == userId).FirstOrDefault();
        }
        public void DeleteUserRole(Guid id)
        {
            UserRoles obj = _userRoleRepository.Collection().Where(x => x.UserId == id).FirstOrDefault();
            obj.IsDelete = true;
            _userRoleRepository.Commit();
        }
        public IEnumerable<UserRoles> GetAllUserRoles()
        {
            return _userRoleRepository.Collection().Where(x => x.IsDelete == false);
        }
    }
}

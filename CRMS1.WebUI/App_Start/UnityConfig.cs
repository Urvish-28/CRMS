using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL.Repositories.Login;
using CRMS1.SQL.Repositories.Role;
using CRMS1.SQL.Repositories.Room;
using CRMS1.SQL.Repositories.SqlRepository;
using CRMS1.SQL.Repositories.UserRole;
using CRMS1.SQL.Repositories.Users;
using System;

using Unity;

namespace CRMS1.WebUI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ILoginRepository, LoginRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IUserRoleRepository, UserRoleRepository>();
            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IRepository<CommonLookups>, SqlRepository<CommonLookups>>();
            container.RegisterType<IRepository<FormMst>, SqlRepository<FormMst>>();



            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserRoleService, UserRoleService>();
            container.RegisterType<IRoomService, RoomService>();
            container.RegisterType<ICommonLookupService, CommonLookupService>();
            container.RegisterType<IFormMstService, FormMstService>();
        }
    }
}
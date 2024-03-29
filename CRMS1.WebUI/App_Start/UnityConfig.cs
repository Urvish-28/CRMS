using CRMS1.Core.Models;
using CRMS1.Services;
using CRMS1.SQL.Repositories.AuditRepository;
using CRMS1.SQL.Repositories.CommonLookUps;
using CRMS1.SQL.Repositories.FormMsts;
using CRMS1.SQL.Repositories.FormroleMapping;
using CRMS1.SQL.Repositories.Login;
using CRMS1.SQL.Repositories.Room;
using CRMS1.SQL.Repositories.SqlRepository;
using CRMS1.SQL.Repositories.TicketAttachments;
using CRMS1.SQL.Repositories.TicketComments;
using CRMS1.SQL.Repositories.Tickets;
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
            container.RegisterType<IRepository<Roles>, SqlRepository<Roles>>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IRepository<User>, SqlRepository<User>>();
            container.RegisterType<IRepository<UserRoles>, SqlRepository<UserRoles>>();
            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IRepository<CommonLookups>, SqlRepository<CommonLookups>>();
            container.RegisterType<ICommonLookUpsRepository, CommonLookUpsRepository>();
            container.RegisterType<IRepository<FormMst>, SqlRepository<FormMst>>();
            container.RegisterType<IRepository<FormRoleMapping>, SqlRepository<FormRoleMapping>>();
            container.RegisterType<IRepository<ConferenceRoom>, SqlRepository<ConferenceRoom>>();
            container.RegisterType<IRepository<Roles>, SqlRepository<Roles>>();
            container.RegisterType<IRepository<FormMst>, SqlRepository<FormMst>>();
            container.RegisterType<IFormMstRepository, FormMstRepository>();
            container.RegisterType<IFormRoleRepository, FormRoleRepository>();
            container.RegisterType<IRepository<Ticket>, SqlRepository<Ticket>>();
            container.RegisterType<ITicketRepository, TicketRepository>();
            container.RegisterType<IRepository<TicketAttachment>, SqlRepository<TicketAttachment>>();
            container.RegisterType<ITicketAttachmentRepository, TicketAttachmentRepository>();
            container.RegisterType<IRepository<TicketComment>, SqlRepository<TicketComment>>();
            container.RegisterType<ITicketCommentRepository, TicketCommentRepository>();
            container.RegisterType<IRepository<AuditLogs>, SqlRepository<AuditLogs>>();
            container.RegisterType<IAuditRepository, AuditRepository>();


            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserRoleService, UserRoleService>();
            container.RegisterType<ICommonLookupService, CommonLookupService>();
            container.RegisterType<IFormMstService, FormMstService>();
            container.RegisterType<IFormRoleMappingService, FormRoleMappingService>();
            container.RegisterType<ITicketService, TicketService>();
            container.RegisterType<ITicketAttachmentService, TicketAttachmentService>();
            container.RegisterType<ITicketCommentService, TicketCommentService>();
            container.RegisterType<IAuditLogService, AuditLogService>();
        }
    }
}
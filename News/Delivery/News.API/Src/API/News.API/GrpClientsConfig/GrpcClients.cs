namespace _4EPlatform_News.GrpClientsConfig
{

    public static class GrpcClients
    {
        #region UserManagement
        public static void AddUserManagementGrpcClients(
            this IServiceCollection services,
            string UserManagmentUrl)
        {
            services.AddGrpcServiceClient<PermissionGroupingService.PermissionGroupingServiceClient>(UserManagmentUrl);
        }
        #endregion

    }
}

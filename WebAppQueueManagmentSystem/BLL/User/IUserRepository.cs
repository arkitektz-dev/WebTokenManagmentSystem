using System.Collections.Generic;
using WebAppQueueManagmentSystem.ApiHelpers.Response;

namespace WebAppQueueManagmentSystem.BLL.User
{
    public interface IUserRepository
    {
        IList<UserListBody> GetUserList(string Role);
        IList<UserRoleBody> GetUserRole();
    }
}
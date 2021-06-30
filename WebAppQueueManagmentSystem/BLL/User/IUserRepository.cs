using System.Collections.Generic;
using WebAppQueueManagmentSystem.ApiHelpers.Response;

namespace WebAppQueueManagmentSystem.BLL.User
{
    public interface IUserRepository
    {
        IList<CashierTypeBody> GetCashierTypeList();
        IList<UserListBody> GetUserList(string Role);
        IList<UserRoleBody> GetUserRole();
        bool isUserSaved(int? CSRID, string RoleID, string UserId);
    }
}
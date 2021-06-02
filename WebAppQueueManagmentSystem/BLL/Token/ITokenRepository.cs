using WebAppQueueManagmentSystem.ApiHelpers.Response;
using WebAppQueueManagmentSystem.Models;

namespace WebAppQueueManagmentSystem.BLL.Token
{
    public interface ITokenRepository
    {
        GenerateTokenBody GenerateTicket(string CustomerType);
        Auth GenerateToken();
    }
}
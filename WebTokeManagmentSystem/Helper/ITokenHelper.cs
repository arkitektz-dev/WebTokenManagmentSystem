using WebTokenManagmentSystem.Authentication.enums;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.Helper
{
    public interface ITokenHelper
    {
        Token FindTokenByTokenNumber(string Token_Number);
        int? GenerateCounterNumber();
        Token GenerateNewToken(int? isCustomer);
        AppSetting GetStartCounter();
        int? GetTokenNumber(int? isCustomer);
        void InsertTokenStatus(string TokenNumber, GlobalEnums.Status status_number);
        bool VerifyIfTokenAlreadyAssigned(string TokeNumber);
        bool VerifyTokenById(string Token_Number);
        bool VerifyTokenExsists(string Token_Number);
        bool VerifyTokenExsistsWithSameStatus(string Token_Number, byte status);
        bool VerifyTokenStatus(byte status);
        bool VerifyUserById(string user_id);
    }
}
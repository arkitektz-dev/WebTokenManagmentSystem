using RestSharp;
using WebAppQueueManagmentSystem.Models;

namespace WebAppQueueManagmentSystem.ApiHelpers.Utility
{
    public interface IApiUtility
    {
        Auth GenerateToken();
        IRestResponse RunGetRequest(string Url);
        IRestResponse RunPostRequest<T>(T RequestBody, string Url);
    }
}
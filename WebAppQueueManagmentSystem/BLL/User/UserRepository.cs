using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebAppQueueManagmentSystem.ApiHelpers.Request;
using WebAppQueueManagmentSystem.ApiHelpers.Response;
using WebAppQueueManagmentSystem.ApiHelpers.Utility;

namespace WebAppQueueManagmentSystem.BLL.User
{
    public class UserRepository : IUserRepository
    {
        readonly IApiUtility helper;
        public UserRepository(IApiUtility _helper)
        {
            this.helper = _helper;
        }

        public IList<UserRoleBody> GetUserRole()
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest("api/User/Get-Role-List");
            JArray TokenList = JArray.Parse(response.Content);

            IList<ApiHelpers.Response.UserRoleBody> row = TokenList.Select(p => new ApiHelpers.Response.UserRoleBody
            {
             
                Id = (string)p["id"],
                UserName =  (string)p["userName"]

            }).ToList();

            var return_message = row;

            return return_message;
        }

        public IList<UserListBody> GetUserList(string Role)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest($"api/User/Get-User-List?Role={Role}");
            JArray TokenList = JArray.Parse(response.Content);

            IList<ApiHelpers.Response.UserListBody> row = TokenList.Select(p => new ApiHelpers.Response.UserListBody
            {

                Id = (string)p["id"],
                Email = (string)p["email"],
                Role = (string)p["role"]
                 

            }).ToList();

            var return_message = row;

            return return_message;
        }

        public IList<CashierTypeBody> GetCashierTypeList()
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest($"api/User/Get-Counter-Type");
            JArray TokenList = JArray.Parse(response.Content);

            IList<ApiHelpers.Response.CashierTypeBody> row = TokenList.Select(p => new ApiHelpers.Response.CashierTypeBody
            {
                ID = (int)p["id"],
                Name = (string)p["serviceName"],

            }).ToList();

            var return_message = row;

            return return_message;
        }

        public bool isUserSaved(int? CSRID, string RoleID, string UserId)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new ChangeUserRequestBody()
            {
                CSRID = CSRID,
                RoleId = RoleID,
                UserId = UserId
            };


            IRestResponse response = helper.RunPostRequest(RequestBody, "api/User/Change-User-Role");

            GenerateTokenBody row = JsonConvert.DeserializeObject<GenerateTokenBody>(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else {
                return false;
            }

        }



    }
}
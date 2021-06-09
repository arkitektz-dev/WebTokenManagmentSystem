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
using WebAppQueueManagmentSystem.BLL.Token;


namespace WebAppQueueManagmentSystem.BLL.Counter
{
    public class CounterRepository : ICounterRepository
    {

        readonly ITokenRepository token;
        readonly IApiUtility helper;
        public CounterRepository(ITokenRepository _token, IApiUtility _helper)
        {
            this.token = _token;
            this.helper = _helper;
        }


        public CounterDetailBody CounterDetail(string UserID)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new CounterDetailRequestBody()
            {
                userId = UserID
            };

     
            IRestResponse response = helper.RunPostRequest(RequestBody, "api/Counter/Get_Counter_Detail_By_UserId");

            CounterDetailBody CounterDetailResponseBody = JsonConvert.DeserializeObject<CounterDetailBody>(response.Content);

            var row = CounterDetailResponseBody;

            var return_message = new CounterDetailBody()
            {
                CounterNumber = row.CounterNumber,
                CounterService = row.CounterService,
                CounterStatus = row.CounterStatus,
                CounterID = row.CounterID
            };


            return return_message;


        }

        public CounterTokenBody AssignTokenToCounter(string TokenNumber,string UserId, int StatusId)
        {
            var counterDetail = CounterDetail(UserId);


            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new CounterTokenBody()
            {
                TokenNumber = TokenNumber,
                CounterId = counterDetail.CounterID,
                StatusId = StatusId
            };

            IRestResponse response = helper.RunPostRequest(RequestBody, "api/Token/Assign-Ticket-To-Counter");

             

            CounterTokenBody CounterTokenResponseBody = JsonConvert.DeserializeObject<CounterTokenBody>(response.Content);

            var row = CounterTokenResponseBody;

            var return_message = new CounterTokenBody()
            {
               TokenNumber = row.TokenNumber,
               CounterId = row.CounterId,
               StatusId = row.StatusId
            };


            return return_message;




        }

        public IList<ApiHelpers.Response.Counter> ListCounter()
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest("api/Counter/List-Counter");
            JArray TokenList = JArray.Parse(response.Content);

            IList<ApiHelpers.Response.Counter> row = TokenList.Select(p => new ApiHelpers.Response.Counter
            {
               CounterUserId = (string)p["counterUserId"],
               Csrid = (int)p["csrid"],
               Description = (string)p["description"],
               Id = (int)p["id"],
               Number = (int)p["number"]

            }).ToList();

            var return_message = row;

            return return_message;
        }


    }
}
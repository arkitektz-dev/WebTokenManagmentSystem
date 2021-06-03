using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebAppQueueManagmentSystem.ApiHelpers.Request;
using WebAppQueueManagmentSystem.ApiHelpers.Response;
using WebAppQueueManagmentSystem.BLL.Token;

namespace WebAppQueueManagmentSystem.BLL.Counter
{
    public class CounterRepository : ICounterRepository
    {

        readonly ITokenRepository token;
        public CounterRepository(ITokenRepository _token)
        {
            this.token = _token;
        }


        public CounterDetailBody CounterDetail(string UserID)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new CounterDetailRequestBody()
            {
                userId = UserID
            };

            var client = new RestClient($"{apiEndPoint}api/Counter/Get_Counter_Detail_By_UserId");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {token.GenerateToken().BearerToken}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(RequestBody);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

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

            var client = new RestClient($"{apiEndPoint}api/Token/Assign-Ticket-To-Counter");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {token.GenerateToken().BearerToken}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(RequestBody);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

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

    }
}
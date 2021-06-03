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
using WebAppQueueManagmentSystem.Models; 

namespace WebAppQueueManagmentSystem.BLL.Token
{
    public class TokenRepository : ITokenRepository
    {
        public Auth GenerateToken()
        {
            var email = ConfigurationManager.AppSettings["email"];
            var password = ConfigurationManager.AppSettings["password"];
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new LoginRequestBody()
            {
                email = email,
                password = password
            };

            var client = new RestClient($"{apiEndPoint}api/authenticate/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(RequestBody);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

 
            LoginUserBody LoginResponseBody = JsonConvert.DeserializeObject<LoginUserBody>(response.Content);

            var return_message = new Auth()
            {
                BearerToken = LoginResponseBody.token
            };


            return return_message;
        }

        public GenerateTokenBody GenerateTicket(string CustomerType)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new GenerateTokenRequestBody()
            {
                CustomerType = CustomerType
            };

            var client = new RestClient($"{apiEndPoint}api/Token/Generate-Customer-Token");  
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {GenerateToken().BearerToken}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(RequestBody);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            GenerateTokenBody row = JsonConvert.DeserializeObject<GenerateTokenBody>(response.Content);

            var return_message = new GenerateTokenBody()
            {
                token = row.token,
                date = row.date,
                time = row.time
            };


            return return_message;

        }


        public IList<CounterListBody> ListToken(int token_status, int customer_Type)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var client = new RestClient($"{apiEndPoint}api/Token/List-Token?token_status={token_status}&customer_Type={customer_Type}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var body = @"";
            request.AddHeader("Authorization", $"Bearer {GenerateToken().BearerToken}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            JArray TokenList = JArray.Parse(response.Content);

            IList<CounterListBody> row = TokenList.Select(p => new CounterListBody
            {
                Token = (string)p["token"],
                Date = (string)p["date"],
                Time = (string)p["time"],
                isCustomer = (bool)p["isCustomer"]
            }).ToList();


            var return_message = row;

            return return_message;

        }








    }
}
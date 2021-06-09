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
using WebAppQueueManagmentSystem.Models;
 

namespace WebAppQueueManagmentSystem.BLL.Token
{
    public class TokenRepository : ITokenRepository
    {
        readonly IApiUtility helper;
        public TokenRepository(IApiUtility _helper)
        {
            this.helper = _helper;
        }



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


            IRestResponse response = helper.RunPostRequest(RequestBody, "api/Token/Generate-Customer-Token");

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

            IRestResponse response = helper.RunGetRequest($"api/Token/List-Token?token_status={token_status}&customer_Type={customer_Type}");

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

        public SubmittedTicketBody Submitted_Token(string TokenNumber,string Comment,int ServiceOptionId,byte StatusId)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new SubmittedTicketRequestBody()
            {
               TokenNumber = TokenNumber,
               Comment = Comment,
               ServiceOptionId = ServiceOptionId,
               StatusId = StatusId
            };

            IRestResponse response = helper.RunPostRequest(RequestBody, "api/Token/Counter-Submit-Ticket");
            SubmittedTicketBody row = JsonConvert.DeserializeObject<SubmittedTicketBody>(response.Content);

            var return_message = new SubmittedTicketBody()
            {
                TokenNumber = TokenNumber,
                Comment = Comment,
                ServiceOptionId = ServiceOptionId,
                StatusId = StatusId
            };
            return return_message;
        }

        public IList<ListCounterTokenBody> ListCounterToken()
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest("api/Token/List-Counter-Token");
            JArray TokenList = JArray.Parse(response.Content);

            IList<ListCounterTokenBody> row = TokenList.Select(p => new ListCounterTokenBody
            {
                CounterName = (string)p["counterName"],
                TicketNumber = (string)p["ticketNumber"]
            }).ToList();

            var return_message = row;
            return return_message;
        }

        public TokenStatusBody GetTokenStatus(string TokenNumber)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new CounterStatusBody()
            {
                TokenNumber = TokenNumber
            };

            IRestResponse response = helper.RunPostRequest(RequestBody, "api/Token/Get-Token-Status");
            TokenStatusBody row = JsonConvert.DeserializeObject<TokenStatusBody>(response.Content);

            var return_message = new TokenStatusBody()
            {
                TokenStatus = row.TokenStatus
            };
             
            return return_message;
        }

        public IList<CounterTokenBody> ViewCounterActivity(int counterId) {

            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest($"api/Counter/View-Counter-Activity?CounterId={counterId}");
            JArray TokenList = JArray.Parse(response.Content);

            IList<CounterTokenBody> row = TokenList.Select(p => new CounterTokenBody
            {
                TokenNumber = (string)p["tokenNumber"],
                StatusId = (int)p["statusId"],
                CreatedDate = (DateTime)p["createdDate"],
                CompletedDate = (DateTime)p["completedDate"],
                ServiceType = (string)p["serviceType"],
                ServingTime = (DateTime)p["servingTime"]

            }).ToList();

            var return_message = row;
            return return_message;
        } 
 


    }
}
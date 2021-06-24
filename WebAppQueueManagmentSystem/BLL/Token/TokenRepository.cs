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

        public SubmittedTicketBody Submitted_Token(string TokenNumber, string Comment, int ServiceOptionId, byte StatusId)
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

        public IList<StatusListBody> StatusList()
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest($"api/Token/Get-Status-list");
            JArray TokenList = JArray.Parse(response.Content);

            IList<StatusListBody> row = TokenList.Select(p => new StatusListBody
            {
                Id = (int)p["id"],
                Name = (string)p["name"]

            }).ToList();

            var return_message = row;
            return return_message;
        }

        public IList<CurrentCounterTokenDto> CurrentList(DateTime TicketDate, int TicketStatus, int CustomerType)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest($"api/Token/Token-Filter?TicketDate={TicketDate}&TicketStatus={TicketStatus}&CustomerType={CustomerType}");
            JArray TokenList = JArray.Parse(response.Content);

            IList<CurrentCounterTokenDto> row = TokenList.Select(p => new CurrentCounterTokenDto
            {
                CounterId = (int)p["counterId"],
                Status = (string)p["status"],
                TicketDate = (DateTime)p["ticketDate"],
                TokenId = (int)p["tokenId"],
                TokenNumber = (string)p["tokenNumber"]
            }).ToList();

            var return_message = row;

            return return_message;

        }

        public StatusChangeBody ChangeTokenStatus(string TokenNumber, byte Status)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new StatusChangeBody()
            {
                TokenNumber = TokenNumber,
                Status = Status
                
            };

            IRestResponse response = helper.RunPostRequest(RequestBody, "api/Token/Change-Token-Status-By-Token-Number");
            StatusChangeBody row = JsonConvert.DeserializeObject<StatusChangeBody>(response.Content);

            var return_message = new StatusChangeBody()
            {
                TokenNumber = row.TokenNumber,
                Status = row.Status
            };

            return return_message;

        }

        public int GetAverageTime()
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest($"api/Token/Get-Average-Time");
             
            var return_message = response.Content;

            return Convert.ToInt32(return_message);

        }

        public AddTicketToQueueBody InsertAnncoumentInQueue(int CounterId,string TokenNumber)
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            var RequestBody = new AddTicketToQueueBody()
            {
                 CounterId = CounterId,
                 TokenNumber = TokenNumber
            };


            IRestResponse response = helper.RunPostRequest(RequestBody, "api/Token/Add-Ticket-To-Queue");

            AddTicketToQueueBody row = JsonConvert.DeserializeObject<AddTicketToQueueBody>(response.Content);

            var return_message = new AddTicketToQueueBody()
            {
                CounterId = row.CounterId,
                TokenNumber = row.TokenNumber
            };


            return return_message;
        }

        public List<CounterListResponse> GetCounterList()
        {
            var apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];

            IRestResponse response = helper.RunGetRequest($"api/Token/GetCounterList");
            JArray CounterList = JArray.Parse(response.Content);

            List<CounterListResponse> ReturnCouterList = new List<CounterListResponse>();
            foreach (var item in CounterList)
            {
                CounterListResponse row = new CounterListResponse();
                row.CounterID = (int)item;
                row.CounterNumber = $"Counter No {row.CounterID}";

                ReturnCouterList.Add(row);
            }
             
            return ReturnCouterList;
        }
    }
}
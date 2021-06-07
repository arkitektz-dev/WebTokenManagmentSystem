using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebAppQueueManagmentSystem.ApiHelpers.Request;
using WebAppQueueManagmentSystem.ApiHelpers.Response;
using WebAppQueueManagmentSystem.BLL.Token;
using WebAppQueueManagmentSystem.Models;

namespace WebAppQueueManagmentSystem.ApiHelpers.Utility
{
    public class ApiUtility : IApiUtility
    {
        string  email = ConfigurationManager.AppSettings["email"];
        string  password = ConfigurationManager.AppSettings["password"];
        string  apiEndPoint = ConfigurationManager.AppSettings["api:EndPoint"];
      



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



        public IRestResponse RunPostRequest<T>(T RequestBody, string Url) 
        {

            var client = new RestClient($"{apiEndPoint}{Url}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {GenerateToken().BearerToken}");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(RequestBody);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }


        public IRestResponse RunGetRequest(string Url)
        {
            var client = new RestClient($"{apiEndPoint}{Url}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var body = @"";
            request.AddHeader("Authorization", $"Bearer {GenerateToken().BearerToken}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }





    }
}
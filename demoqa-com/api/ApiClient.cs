using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;


namespace demoqa_com.pages.api
{
    public class ApiProvider
    {
        private RestClient _client;
        public static string endoint;
        public static string apiEndPoint = "https://demoqa.com/";
        public static string booksEndPoint = apiEndPoint +  "BookStore/v1/Books";
        public static string bookEndPoint = apiEndPoint + "BookStore/v1/Book";
        public static string authorizedEndPoint = apiEndPoint + "Account/v1/Authorized";
        public static string generateTokenEndPoint = apiEndPoint + "Account/v1/GenerateToken";
        public static string userEndPoint = apiEndPoint + "Account/v1/User";

        public ApiProvider (string endpoint)
        {
            switch (endpoint)
            {
                case "books":
                    _client = new RestClient(booksEndPoint);    
                    break;
                case "Book":
                    _client = new RestClient(bookEndPoint);
                    break;
                default:
                    throw new Exception("Provider class can not be initialized without accepted api endpoint");
            }
        }

        public IRestResponse send_get_request()
        {
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = _client.Execute(request);
            return response;
        }

        public JObject ParseJsonContent(IRestResponse response)
        {
            try
            {
                return JObject.Parse(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }

    public static class ApiBooks
    {
        private static ApiProvider api = new ApiProvider("books");
        
        public static IRestResponse GetAllBooks()
        {
            IRestResponse response = api.send_get_request();
            return response;
        }

        public static JObject DeserializeAllBooks()
        {
            IRestResponse response = api.send_get_request();
            JObject jsonJObject = api.ParseJsonContent(response);
            Console.Out.WriteLine(jsonJObject);
            return jsonJObject;
        } 
    }
}
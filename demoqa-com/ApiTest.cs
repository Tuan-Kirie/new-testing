using System;
using demoqa_com.pages.api;
using NUnit.Framework;
using RestSharp;

namespace demoqa_com.pages
{
    [TestFixture]
    public class ApiTestBooksStoreBooks
    {
        
        [Test]
        public void verify_get_returns_200ok()
        {
            IRestResponse response = ApiBooks.GetAllBooks();
            Assert.IsTrue(response.IsSuccessful, "response code are not success, actual get" + response.StatusCode);
        }

        [Test]
        public void verify_response_body_is_not_empty()
        {
            IRestResponse response = ApiBooks.GetAllBooks();
            Console.Out.WriteLine(response.Content);
            Assert.IsNotEmpty(response.Content, "Response are empty");
        }
        
        
    }
}
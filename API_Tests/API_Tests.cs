using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace API_Tests
{
    public class Tests
    {
        private const string url = "https://shorturl.etotev.repl.co/api";
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }



        [Test]
        public void Test_List_ShortURLS()
        {
            this.request = new RestRequest(url + "/urls");

            var response = this.client.Execute(request);
            var urls = JsonSerializer.Deserialize<List<URLS>>(response.Content);


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(urls.Count, Is.GreaterThan(0));
            bool allUrlsAreShort = true;

            foreach (var url in urls)
            {
               if(url.shortUrl == null)
                {
                    allUrlsAreShort = false;
                }
            }

            Assert.That(allUrlsAreShort, Is.True);
        }

        [Test]
        public void Test_Find_URL_By_ShortCode()
        {
            var shortCode = "seldev";
            this.request = new RestRequest(url + $"/urls/{shortCode}");

            var response = this.client.Execute(request);
            var content = JsonSerializer.Deserialize<URLS>(response.Content);        


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content.shortCode, Is.EqualTo($"{shortCode}"));

        }

        [Test]
        public void Test_Try_To_Search_Invalid_ShortCode()
        {
            var shortCode = "dtgfdgdseldev";
            this.request = new RestRequest(url + $"/urls/{shortCode}");

            var response = this.client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Short code not found: dtgfdgdseldev\"}"));

        }



        [Test]
        public void Test_Create_New_ShortLink()
        {
            this.request = new RestRequest(url + "/urls");
            var newShortCode = "gong";

            var body = new {url = "https://gong.bg", shortCode = $"{newShortCode}"};

            request.AddJsonBody(body);

            var response = this.client.Execute(request, Method.Post);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        [Test]
        public void Test_Delete_ShortLink_FromPreviousTest()
        {
            var shortCd = "gong";
            this.request = new RestRequest(url + $"/urls/{shortCd}");

            var response = this.client.Execute(request, Method.Delete);     

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
           Assert.That(response.Content, Is.EqualTo("{\"msg\":\"URL deleted: gong\"}"));


        }



    }
}
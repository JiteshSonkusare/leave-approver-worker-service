using EmployeeLeave.Infrastructure.Extensions;
using EmployeeLeave.Infrastructure.Managers;
using EmployeeLeave.Infrastructure.Managers.Interface;
using EmployeeLeave.Infrastructure.Routes;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.TestProject
{
   public class ApproveVacationManagerTest
    {
        HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            HttpResponseMessage httpResponce = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Accepted,
                Content = new StringContent("Accepted"),
            };
            httpResponce.Content.Headers.ContentType = new MediaTypeHeaderValue(HeaderContentExtension.HeaderContentType);

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponce);

            _httpClient = new HttpClient(httpMessageHandlerMock.Object);
            _httpClient.BaseAddress = new Uri(QueueEndPoints.MessageQueueUri);
        }

        [Test]
        public async Task TestApproveVacationRequestQueue()
        {
            string messageId = "43187a16-6fcf-43ff-9f6d-eaaed4ec341c";
             ApproveVacationManager  approveVacationManager = new ApproveVacationManager(_httpClient);

            var actualresult = await approveVacationManager.ApproveVacationRequest(messageId);

            Assert.AreEqual("Accepted", actualresult);
        }
    }
}

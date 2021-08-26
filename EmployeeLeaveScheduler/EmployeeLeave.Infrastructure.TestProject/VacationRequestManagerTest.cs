using EmployeeLeave.Infrastructure.Extensions;
using EmployeeLeave.Infrastructure.Managers;
using EmployeeLeave.Infrastructure.Managers.Interface;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using EmployeeLeave.Infrastructure.Routes;

namespace EmployeeLeave.Infrastructure.TestProject
{
    public class VacationRequestManagerTest
    {
        HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {            
            HttpResponseMessage httpResponce = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"messageId\":\"43187a16-6fcf-43ff-9f6d-eaaed4ec341c\",\"data\":{\"employeeId\":100,\"requestedDays\":10,\"availableDays\":20}}"),
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
        public async Task TestVacationRequestQueue()
        {
            string expectedmessageId = "43187a16-6fcf-43ff-9f6d-eaaed4ec341c";
            IVacationRequestManager vacationRequestManager = new VacationRequestManager(_httpClient);

            var actualresult = await vacationRequestManager.GetVacationRequest();

            Assert.AreEqual(expectedmessageId, actualresult.messageId);
        }
    }
}
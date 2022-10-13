using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace EmployerRESTAPI.Unit_Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task TestReturnMethod()
        {
            var WebAppFactory = new WebApplicationFactory<Program>();
            var client = WebAppFactory.CreateDefaultClient();

            var response = await client.GetAsync("https://gorest.co.in/public/v2/users/12121");
            var result = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.AreEqual("Hello world", result);
        }
    }
}

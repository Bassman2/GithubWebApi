namespace GithubWebApiTest
{
    [TestClass]
    public partial class GithubTest
    {
        private static string? host;
        private static string? apiKey;



        [ClassInitialize]

        public static void ClassInitialize(TestContext testContext)
        {
            host = Environment.GetEnvironmentVariable("GITHUB_HOST");
            apiKey = Environment.GetEnvironmentVariable("GITHUB_APIKEY");
        }


        //[TestMethod]
        //public void TestMethodAuthenticatedUser()
        //{
        //    using var github = new GithubWebApi() service
        //}

        //[TestMethod]
        //public void TestMethodSetSimpleOnOffError()
        //{
        //    using (HomeAutomation client = new HomeAutomation(TestSettings.Login, TestSettings.Password))
        //    {
        //        client.SetSimpleOnOff(testDevice!.Ain, OnOff.Off);
        //    }
        //}
    }
}

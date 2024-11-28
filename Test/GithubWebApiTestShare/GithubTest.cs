namespace GithubWebApiTest
{
    [TestClass]
    public partial class GithubTest
    {
        private static string? host;
        private static string? apiKey;
        private static string testUser = "Bassman2";
        private static string testRepo = "ApiTest";



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

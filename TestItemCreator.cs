using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AzureDevOpsCustomObjects.WorkItems.Tests;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.VisualStudio.Services.Common;

namespace AzureDevOpsCustomObjects
{
    public class TestItemCreator
    {
        public TestItemCreator(string uri, string personalAccessToken, string projectName)
        {
            Uri = new Uri(uri);
            PersonalAccessToken = personalAccessToken;
            ProjectName = projectName;

            var credentials = new VssBasicCredential(string.Empty, PersonalAccessToken);

            var teamProjectCollection = new TfsTeamProjectCollection(Uri, credentials);
            var testManagementService = teamProjectCollection.GetService<ITestManagementService>();
            TeamProject = testManagementService.GetTeamProject(ProjectName);
        }

        private ITestManagementTeamProject TeamProject { get; }

        private Uri Uri { get; }

        private string ProjectName { get; }

        private string PersonalAccessToken { get; }

        public int Create<T>(T azureDevOpsTestPlan) where T : AzureDevOpsTestPlan
        {
            var testPlan = TeamProject.TestPlans.Create();
            testPlan.Name = azureDevOpsTestPlan.Title;
            testPlan.Description = azureDevOpsTestPlan.Description;
            testPlan.Save();

            ITestCase testCase = null;
            var testCases = new List<ITestCase>();
            foreach (var azureDevOpsTestCase in azureDevOpsTestPlan.TestCases)
            {
                testCase = TeamProject.TestCases.Create();
                testCase.Title = azureDevOpsTestCase.Title;
                testCase.Description = azureDevOpsTestCase.Description;

                foreach (var azureDevOpsTestStep in azureDevOpsTestCase.TestSteps)
                {
                    var newStep = testCase.CreateTestStep();
                    newStep.Title = azureDevOpsTestStep.StepDescription;
                    newStep.ExpectedResult = azureDevOpsTestStep.ExpectedResult;
                    testCase.Actions.Add(newStep);
                }

                testCase.Save();
                testCases.Add(testCase);
            }

            testPlan.RootSuite.Entries.AddCases(testCases);
            testPlan.Save();

            return testPlan.Id;
        }

        public HttpStatusCode Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", PersonalAccessToken))));

                var response = client.DeleteAsync(Uri + "/" + ProjectName + "/_apis/test/testcases/" + id + "?api-version=5.0-preview.1").Result;
                
                return response.StatusCode;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOpsCustomObjects.Extensions
{
    public static class TestCaseExtension
    {
        public static string FormatAsStep(this string step)
        {
            var stepPrefix = "<parameterizedString isformatted=\"true\">" +
                             "<DIV><P>".Replace("<", "&lt;").Replace(">", "&gt;");
            var stepSuffix = "<BR/></P></DIV>".Replace("<", "&lt;").Replace(">", "&gt;") + "</parameterizedString>";
            return stepPrefix + step + stepSuffix;
        }

        public static string FormatAsExpectedResult(this string result)
        {
            var resultPrefix = "<parameterizedString isformatted=\"true\">" + "<P>&nbsp;".Replace("<", "&lt;").Replace(">", "&gt;");
            var resultSuffix = "</P>".Replace("<", "&lt;").Replace(">", "&gt;") + "</parameterizedString><description/>";
            return resultPrefix + result + resultSuffix;
        }

        public static string FormatAsCompleteStep(this string completeStep, int stepNumber)
        {
            var completeStepPrefix = $"<step id=\"{stepNumber + 1}\" type=\"ValidateStep\">";
            var completeStepSuffix = "</step>";

            return completeStepPrefix + completeStep + completeStepSuffix;
        }

        public static string FormatAsTestCase(this string testCase, int numberOfSteps)
        {
            var testCasePrefix = $"<steps id=\"0\" last=\"{numberOfSteps + 1}\">";
            var testCaseSuffix = "</steps>";

            return testCasePrefix + testCase + testCaseSuffix;
        }
    }
}

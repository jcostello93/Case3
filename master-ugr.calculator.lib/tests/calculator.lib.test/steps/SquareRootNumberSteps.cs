using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace calculator.lib.test.steps
{
    [Binding]
    public class squareRootNumberSteps
    {
        private readonly ScenarioContext _scenarioContext;
        public squareRootNumberSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the number (.*)")]
        public void WhenNumberIsChecked(int number)
        {
            _scenarioContext.Add("number", number);
        }

        [When("I calculate the square root")]
        public void ICheckIfItIsOdd()
        {
            var number = _scenarioContext.Get<int>("number");
            var squareRoot = NumberAttributter.SquareRoot(number);
            _scenarioContext.Add("squareRoot", squareRoot);
        }

        [Then("the square root should be (.*)")]
        public void ItShouldBeOdd(string expectedResult)
        {
            double expectedValue;
            if (expectedResult.Equals("NaN", StringComparison.OrdinalIgnoreCase))
                expectedValue = double.NaN;
            else
                expectedValue = double.Parse(expectedResult);

            var squareRoot = _scenarioContext.Get<double>("squareRoot");
            Assert.Equal(expectedValue, squareRoot);
        }
    }
}
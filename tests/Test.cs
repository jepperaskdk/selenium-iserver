using OpenQA.Selenium;
using Xunit;

namespace tests {
    public class Test {
        [Fact]
        public void NotFound()
        {
            CustomWebApplicationFactory<src.Startup> Factory = new CustomWebApplicationFactory<src.Startup>();
            Factory.Driver.Navigate().GoToUrl(Factory.RootUri);
            var helloWorld = Factory.Driver.FindElement(By.TagName("body")).Text;
            Assert.Equal(helloWorld, "Hello World!");
        }
    }
}
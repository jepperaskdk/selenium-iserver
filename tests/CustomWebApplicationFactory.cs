using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        IHost _host;
        public string RootUri { get; set; }
        public IWebDriver Driver { get; set; }
        public CustomWebApplicationFactory()
        {
            RootUri = "http://localhost:5000";
            var driver = new ChromeDriver("/usr/local/bin");
            Driver = driver;

            try {
                var client = CreateDefaultClient();
            } catch (InvalidCastException e) {
                // TODO: This shouldn't be necessary
            }
        }

        protected override IHost CreateHost(IHostBuilder hostBuilder)
        {
            _host = hostBuilder.ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder.UseKestrel();
                webHostBuilder.UseUrls();
            }).Build();
            _host.Start();
            return _host;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _host.Dispose();
            }
            Driver.Quit();
        }
    }
}

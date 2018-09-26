using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Superheroes.Contracts;
using Superheroes.Services;
using System;
using System.Net.Http;

namespace Superheroes.Tests.Fixtures
{
    public class TestServerFixture : IDisposable
    {
        public TestServerFixture()
        {
            var startup = new WebHostBuilder()
                           .UseStartup<Startup>()
                           .ConfigureServices(x =>
                           {
                               x.AddSingleton<ICharactersProvider>(BaseTests.CreateCharactersProvider());
                               x.AddScoped<IBattleService, BattleService>();
                           });
            var testServer = new TestServer(startup);
            HttpClient = testServer.CreateClient();
        }

        public HttpClient HttpClient { get; private set; }

        public void Dispose()
        {
            HttpClient.Dispose();
            HttpClient = null;
        }
    }
}

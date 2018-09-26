using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Superheroes.Contracts;
using Superheroes.Services;

namespace Superheroes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices(x => 
                {
                    x.AddSingleton<ICharactersProvider, CharactersProvider>();
                    x.AddScoped<IBattleService, BattleService>();
                });
    }
}

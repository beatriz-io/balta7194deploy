using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Shop
{
    public class Program
    {
        // quase nunca vamos mexer nesses métodos
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // linha mais importante, se mudarmos o startuo temos que mudar esse aqui tambem.
                    webBuilder.UseStartup<Startup>();
                });
    }
}

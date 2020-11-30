using System;
using System.IO;
using Bot.Contexts;
using Bot.Handlers;
using Bot.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace Bot
{
    class Program
    {
        static void Main()
        {
            var config = BuildConfig(new ConfigurationBuilder());

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.LogTo(Console.WriteLine);
                        options.UseSqlite(config["DB:LocalDB"]);
                    });

                    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(config["Bot:Token"]));
                    services.AddSingleton<IUpdateHandler, UpdateHandler>();
                    services.AddSingleton<ICommandHandler, CommandHandler>();
                    services.AddSingleton<IInformerBotClient, InformerBotClient>();
                })
                .Build();

            var bot = ActivatorUtilities.CreateInstance<InformerBotClient>(host.Services);
            bot.StartReceiving();
        }

        private static IConfigurationRoot BuildConfig(ConfigurationBuilder builder)
        {
            return builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}

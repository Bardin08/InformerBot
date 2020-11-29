using System;
using System.IO;
using Bot.Handlers;
using Bot.Interfaces;
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
                    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(config["Bot:Token"]));
                    services.AddSingleton<IUpdateHandler, UpdateHandler>();
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

using System.Threading;
using Bot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Bot
{
    internal sealed class InformerBotClient : IInformerBotClient
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUpdateHandler _updateHandler;

        public InformerBotClient(ITelegramBotClient botClient, IUpdateHandler updateHandler)
        {
            _botClient = botClient;
            _updateHandler = updateHandler;
        }

        public void StartReceiving()
        {
            _botClient.StartReceiving();

            _botClient.OnUpdate += UpdateReceived;

            Thread.Sleep(int.MaxValue);
        }

        public void StopReceiving()
        {
            _botClient.StopReceiving();

            _botClient.OnUpdate -= UpdateReceived;
        }

        private async void UpdateReceived(object sender, UpdateEventArgs e)
        {
            await _updateHandler.HandleUpdateAsync(e.Update);
        }
    }
}

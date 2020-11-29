using Telegram.Bot;
using Telegram.Bot.Args;

namespace Bot.Interfaces
{
    public interface IInformerBotClient
    {
        void StartReceiving();

        void StopReceiving();
    }
}

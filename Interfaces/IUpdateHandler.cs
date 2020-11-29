using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Bot.Interfaces
{
    public interface IUpdateHandler
    {
        Task HandleUpdateAsync(Update update);
    }
}

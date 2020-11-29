using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands
{
    internal sealed class StartCommand : IInformerBotCommand
    {
        public string Name => "/start";

        public async Task ExecuteAsync(object transaction, ApplicationDbContext db, ITelegramBotClient botClient)
        {
            if (transaction != null)
            {
                var t = transaction as CommandTransaction;

                var user = db.Users.FirstOrDefault(x => x.Id == t.RecepientId);

                if (user == null)
                {
                    await botClient.SendTextMessageAsync(t?.RecepientId, $"Привет, {user.RealName}.");
                }
                else
                {
                    var buttons = new List<List<InlineKeyboardButton>>
                    {
                        new List<InlineKeyboardButton> {InlineKeyboardButton.WithCallbackData("Зарегистрироваться", "/register") },
                    };

                    var keyboard = new InlineKeyboardMarkup(buttons.ToArray());

                    await botClient.SendTextMessageAsync((transaction as CommandTransaction)?.RecepientId, "Привет. Для начала тебе нужно зарегистрироваться. Нажми на кнопку ниже.", replyMarkup: keyboard);
                }
            }
        }
    }
}

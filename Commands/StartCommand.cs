using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Dictionaries;
using Bot.Interfaces;
using Bot.Models;
using Microsoft.EntityFrameworkCore;
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

                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == t.RecepientId);

                if (user != null)
                {
                    await botClient.SendTextMessageAsync(t?.RecepientId, string.Format(BotDictionary.StartCommandForRegisteredUser, user.RealName));
                }
                else
                {
                    var buttons = new List<List<InlineKeyboardButton>>
                    {
                        new List<InlineKeyboardButton> {InlineKeyboardButton.WithCallbackData("Зарегистрироваться", "/register") },
                    };

                    var keyboard = new InlineKeyboardMarkup(buttons.ToArray());

                    var msg = await botClient.SendTextMessageAsync((transaction as CommandTransaction)?.RecepientId, BotDictionary.StartCommandForUnregisteredUser, replyMarkup: keyboard);

                    t.MessageIds.Add(msg.MessageId);
                    t.IsComplete = true;
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Behaviour.PostCreatingStates
{
    internal class SendPost : IPostCreatingState
    {
        public async Task Update(Message message, PostCreationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            var buttons = new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton> {InlineKeyboardButton.WithCallbackData("Отправить", "sendpost") },
            };

            var keyboard = new InlineKeyboardMarkup(buttons.ToArray());

            await botClient.SendTextMessageAsync(transaction.RecepientId, transaction.PostText, replyMarkup: keyboard);

            transaction.PostCreatingState = new PostSent();
        }
    }
}
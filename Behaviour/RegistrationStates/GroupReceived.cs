using System.Linq;
using Bot.Contexts;
using Bot.Dictionaries;
using Bot.Interfaces;
using Bot.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Behaviour.RegistrationStates
{
    internal class GroupReceived : IRegistrationState
    {
        async void IRegistrationState.Update(Message message, RegistrationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            if (message.Text == null)
            {
                await botClient.SendTextMessageAsync(transaction.RecepientId, BotDictionary.RegistrationCommandUserSentIncorrentGroup);

                transaction.RegistationState = new AskGroup();
                transaction.RegistationState.Update(new Message(), transaction, dbContext, botClient);
            }
            else
            {
                dbContext.Users.FirstOrDefault(u => u.Id == transaction.RecepientId).Group = message.Text;
                await dbContext.SaveChangesAsync();

                var user = dbContext.Users.FirstOrDefault(u => u.Id == transaction.RecepientId);

                var msg = string.Format(BotDictionary.RegistrationCommandRegistrationComplete, user.RealName, user.Group);
                await botClient.SendTextMessageAsync(transaction.RecepientId, msg);
            }
        }
    }
}
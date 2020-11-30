using Bot.Contexts;
using Bot.Dictionaries;
using Bot.Interfaces;
using Bot.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Behaviour.RegistrationStates
{
    internal sealed class FullNameReceived : IRegistrationState
    {
        async void IRegistrationState.Update(Message message, RegistrationTransaction transaction, ApplicationDbContext dbContext, ITelegramBotClient botClient)
        {
            if (string.IsNullOrEmpty(message.Text) || string.IsNullOrWhiteSpace(message.Text))
            {
                await botClient.SendTextMessageAsync(transaction.RecepientId, BotDictionary.RegistrationCommandUserSentIncorrentFullName);

                transaction.RegistationState = new RegistrationTransactionCreate();
                transaction.RegistationState.Update(new Message(), transaction, dbContext, botClient);
            }
            else
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == transaction.RecepientId);

                if (user != null)
                {
                    user.RealName = message.Text;

                    dbContext.Users.Update(user);
                    await dbContext.SaveChangesAsync();
                }
            }

            transaction.RegistationState = new AskGroup();
            transaction.RegistationState.Update(new Message(), transaction, dbContext, botClient);
        }
    }
}
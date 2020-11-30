using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Contexts;
using Bot.Interfaces;
using Bot.Models;
using Telegram.Bot;

namespace Bot.Commands
{
    internal sealed class GroupsCommands : IInformerBotCommand
    {
        public string Name => "/groups";

        public async Task ExecuteAsync(object transaction, ApplicationDbContext db, ITelegramBotClient botClient)
        {
            var groups = db.Users.Where(u => u.Group != null).Select(x => x.Group).Distinct().ToList();

            var sb = new StringBuilder();

            sb.Append("Зарегистрированно ").Append(groups.Count).Append(" групп(-а)\n\n");

            foreach (var group in groups)
            {
                sb.Append('*').Append(group).Append(';').Append('\n');
            }

            await botClient.SendTextMessageAsync((transaction as TransactionBase).RecepientId, sb.ToString());
        }
    }
}

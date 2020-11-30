using System.Threading.Tasks;

namespace Bot.Interfaces
{
    public interface ICommandHandler
    {
        Task HandleCommandAsync(object transaction);
    }
}

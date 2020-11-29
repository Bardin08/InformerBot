namespace Bot.Interfaces
{
    public interface ICommandHandler
    {
        void HandleCommandAsync(object transaction);
    }
}

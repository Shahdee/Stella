namespace Helpers
{
    public interface IMonoRegisterMediator
    {
        void Register(IUpdatable updatable);
        
        void RegisterFixed(IFixedUpdatable updatable);
    }
}
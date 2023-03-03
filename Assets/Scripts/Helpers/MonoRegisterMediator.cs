using System.Collections.Generic;

namespace Helpers
{
    public class MonoRegisterMediator : IUpdatable, IFixedUpdatable, IMonoRegisterMediator
    {
        private readonly HashSet<IUpdatable> _updatables;
        private readonly HashSet<IFixedUpdatable> _fixedUpdatables;


        public MonoRegisterMediator()
        {
            _updatables = new HashSet<IUpdatable>();
            _fixedUpdatables = new HashSet<IFixedUpdatable>();
        }

        public void Register(IUpdatable updatable)
        {
            if (!_updatables.Contains(updatable))
                _updatables.Add(updatable);
        }
        
        public void RegisterFixed(IFixedUpdatable updatable)
        {
            if (!_fixedUpdatables.Contains(updatable))
                _fixedUpdatables.Add(updatable);
        }

        public void CustomUpdate(float deltaTime)
        {
            foreach (var updatable in _updatables)
            {
                updatable.CustomUpdate(deltaTime);
            }
        }

        public void CustomFixedUpdate(float fixedDeltaTime)
        {
            foreach (var updatable in _fixedUpdatables)
            {
                updatable.CustomFixedUpdate(fixedDeltaTime);
            }
        }
    }
}
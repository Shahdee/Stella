using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class MonoMediator : Zenject.IInitializable
    {
        private readonly List<IUpdatable> _updatables;
        private readonly List<IApplicationQuitListener> _applicationQuitListeners;
        private readonly List<IApplicationPauseListener> _applicationPauseListeners;

        private MonoManagerView _view;
        
        
        public MonoMediator(List<IUpdatable> updatables,
                            List<IApplicationQuitListener> applicationQuitListeners,
                            List<IApplicationPauseListener> applicationPauseListeners)
        {
            _updatables = updatables;
            _applicationQuitListeners = applicationQuitListeners;
            _applicationPauseListeners = applicationPauseListeners;
        }

        public void Initialize()
        {
            var gameObject = new GameObject("MonoManager");
            _view = gameObject.AddComponent<MonoManagerView>();

            _view.OnUpdate += PerformUpdate;
            _view.OnQuit += ApplicationQuit;
            _view.OnPause += ApplicationPause;
        }

        private void PerformUpdate(float deltaTime)
        {
            foreach (var listeners in _updatables)
            {
                listeners.CustomUpdate(deltaTime);
            }
        }

        private void ApplicationQuit()
        {
            foreach (var listeners in _applicationQuitListeners)
            {
                listeners.ApplicationQuit();
            }
        }
        
        private void ApplicationPause()
        {
            foreach (var listeners in _applicationPauseListeners)
            {
                listeners.ApplicationPause();
            }
        }
    }
}
using System;
using UnityEngine;

namespace Helpers
{
    public class MonoManagerView : MonoBehaviour
    {
        public event Action<float> OnUpdate;
        public event Action OnQuit;
        public event Action OnPause;
        
        private void Update()
        {
            OnUpdate?.Invoke(Time.deltaTime);
        }

        private void OnApplicationQuit()
        {
            OnQuit?.Invoke();
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            // if (!pauseStatus)
            //     OnPause?.Invoke();
        }
    }
}
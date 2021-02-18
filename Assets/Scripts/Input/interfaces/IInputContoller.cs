using UnityEngine;
using System;

public interface IInputController
{
    event Action<Vector3> OnQuickTouch;
    event Action<Vector2> OnAxisHold;
    event Action<Vector2> OnAxisRelease;
    
    bool Enabled {get;}
    void SetEnabled(bool enabled);
}
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Input
{
    public class PCInputController : AbstractInputController
    {
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";

        private DateTime _quickTouchCurrTime;
        private Vector3 _touchCurrPosition;

        private bool _horizontalInProgress;
        private bool _horizontalFinish;
        private bool _verticalInProgress;
        private Vector2 _arrowDirection = new Vector2();

        protected override void UpdateInput()
        {
            UpdateAxis();

            UpdateMouse();
        }

        private void UpdateMouse()
        {
            if (_touchInProgress && EventSystem.current.IsPointerOverGameObject())
            {
                _touchInProgress = false;
                return;
            }

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _quickTouchCurrTime = DateTime.Now;
                _touchInProgress = true;
            }

            if (!_touchInProgress) return;

            if (UnityEngine.Input.GetMouseButton(0))
            {
            }


            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                if ((DateTime.Now - _quickTouchCurrTime).Seconds < QuickTouchMaxTimeDelta)
                {
                    _touchInProgress = false;
                    QuickTouch(UnityEngine.Input.mousePosition);
                }
            }
        }

        private void UpdateButtons()
        {

        }

        // TODO send just once AxisHold / AxisRelease instead of every frame 
        private void UpdateAxis()
        {
            if (UnityEngine.Input.GetButton(Horizontal))
            {
                if (!_horizontalInProgress)
                {
                    // Debug.LogWarning("H down " + UnityEngine.Input.GetAxis(Horizontal));
                }

                _horizontalInProgress = true;

                var h = UnityEngine.Input.GetAxis(Horizontal);
                _arrowDirection.x = h;
                AxisHold(_arrowDirection);
            }
            else
            {
                if (_horizontalInProgress)
                {
                    // Debug.LogWarning("H up " + UnityEngine.Input.GetAxis(Horizontal));

                    _horizontalInProgress = false;

                    var h = UnityEngine.Input.GetAxis(Horizontal);
                    _arrowDirection.x = h;
                    AxisRelease(_arrowDirection);
                }
            }
        }
    }
}
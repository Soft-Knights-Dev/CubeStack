using GameLogic;
using UnityEngine.InputSystem;

namespace Input
{
    public class GameInput
    {
        private GameControls _controls;

        public GameInput()
        {
            _controls = new GameControls();
            SetInputActivation(true);
            
            _controls.Player.Touch.performed += TapPerformedSignal;
        }

        public void SetInputActivation(bool value)
        {
            if(value)
                _controls.Enable();
            else
                _controls.Disable();
        }

        private void TapPerformedSignal(InputAction.CallbackContext context) =>
            GameSignals.PlayerTap?.Invoke();
    }
}
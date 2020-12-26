using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Input
{
    public class InputManager
    {
        private InputState lastState = new InputState();
        public InputState HandleInput()
        {
            var eventState = new InputState();

            var state = new InputState();
            state.isRotateLeftPressed = GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q);
            state.isRotateRightPressed = GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed|| Keyboard.GetState().IsKeyDown(Keys.E);
            state.isUpPressed = GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed|| Keyboard.GetState().IsKeyDown(Keys.W);
            state.isDownPressed = GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed|| Keyboard.GetState().IsKeyDown(Keys.S);
            state.isLeftPressed = GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed|| Keyboard.GetState().IsKeyDown(Keys.A);
            state.isRightPressed = GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed|| Keyboard.GetState().IsKeyDown(Keys.D);
            state.isPausePressed = GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed|| Keyboard.GetState().IsKeyDown(Keys.Space);
            state.isHoldPressed = GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed|| Keyboard.GetState().IsKeyDown(Keys.F);

            eventState.isRotateLeftPressed = (!lastState.isRotateLeftPressed && state.isRotateLeftPressed);
            eventState.isRotateRightPressed = (!lastState.isRotateRightPressed && state.isRotateRightPressed);
            eventState.isUpPressed = (!lastState.isUpPressed && state.isUpPressed);
            eventState.isDownPressed = (!lastState.isDownPressed && state.isDownPressed);
            eventState.isLeftPressed = (!lastState.isLeftPressed && state.isLeftPressed);
            eventState.isRightPressed = (!lastState.isRightPressed && state.isRightPressed);
            eventState.isPausePressed = (!lastState.isPausePressed && state.isPausePressed);
            eventState.isHoldPressed = (!lastState.isHoldPressed && state.isHoldPressed);

            lastState = state;
            return eventState;
        }
    }
}
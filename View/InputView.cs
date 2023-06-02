using _Game_.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _Game_.Managers
{
    public static class InputView
    {
        private static bool isMovingLeft;
        private static bool isMovingRight;
        private static bool isMovingUp;
        private static bool isMovingDown;
        private static bool isPauseButtonPressed; 

        private static MouseState lastMouseState;
        private static KeyboardState lastKeyboardState;
        private static Vector2 _direction;
        public static Vector2 Direction => _direction;
        public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
        public static bool MouseClicked { get; private set; }
        public static bool MouseRightClicked { get; private set; }
        public static bool MouseLeftDown { get; private set; }
        public static bool PauseIsPressed;
        public static bool IsEnterPressed { get; private set; }
        public static bool IsStartButtonPressed { get; private set; }

        public static void Update()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            
            isMovingUp = keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W);
            isMovingDown = keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S);
            isMovingLeft = keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A);
            isMovingRight = keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D);
            isPauseButtonPressed = keyboardState.IsKeyDown(Keys.Space) &&
                                   lastKeyboardState.IsKeyUp(Keys.Space);
            IsEnterPressed = keyboardState.IsKeyDown(Keys.Enter) &&
                             lastKeyboardState.IsKeyUp(Keys.Enter);
            IsStartButtonPressed = keyboardState.IsKeyDown(Keys.Space) &&
                                   lastKeyboardState.IsKeyUp(Keys.Space);
            _direction = Vector2.Zero;

            if (!PlayerView.player.IsDead)
            {
                if (isMovingLeft)
                {
                    PlayerView.ChangePositionAndFrame(1);
                    _direction.X--;
                }
                if (isMovingRight)
                {
                    PlayerView.ChangePositionAndFrame(2);
                    _direction.X++;
                }
                if (isMovingUp)
                {
                    PlayerView.ChangePositionAndFrame(3);
                    _direction.Y--;
                }
                if (isMovingDown)
                {
                    PlayerView.ChangePositionAndFrame(0);
                    _direction.Y++;
                }
                if (isPauseButtonPressed)
                {
                    Globals.IsPaused = !Globals.IsPaused;
                }
            }

            MouseLeftDown = mouseState.LeftButton == ButtonState.Pressed;
            MouseClicked = MouseLeftDown && (lastMouseState.LeftButton == ButtonState.Released);
            MouseRightClicked = mouseState.RightButton == ButtonState.Pressed
                                && (lastMouseState.RightButton == ButtonState.Released);

            lastMouseState = Mouse.GetState();
            lastKeyboardState = Keyboard.GetState();
        }
    }
}

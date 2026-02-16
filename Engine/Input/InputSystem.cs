using Raylib_cs;
using RayVec2 = System.Numerics.Vector2;

namespace Engine.Input
{
    public static class InputSystem
    {
        internal static void OnTick()
        {
            
        }

        public static bool IsStarted(KeyCode key) => Raylib.IsKeyPressed(ToRaylibKey(key));
        public static bool IsPressed(KeyCode key) => Raylib.IsKeyDown(ToRaylibKey(key));
        public static bool IsReleased(KeyCode key) => Raylib.IsKeyReleased(ToRaylibKey(key));

        public static bool IsMouseStarted(MouseButtonCode button) => Raylib.IsMouseButtonPressed(ToRaylibMouseButton(button));
        public static bool IsMousePressed(MouseButtonCode button) => Raylib.IsMouseButtonDown(ToRaylibMouseButton(button));
        public static bool IsMouseReleased(MouseButtonCode button) => Raylib.IsMouseButtonReleased(ToRaylibMouseButton(button));

        public static RayVec2 MousePosition => Raylib.GetMousePosition();
        public static float MouseWheelDelta => Raylib.GetMouseWheelMove();

        private static KeyboardKey ToRaylibKey(KeyCode key)
        {
            return key switch
            {
                KeyCode.W => KeyboardKey.W,
                KeyCode.A => KeyboardKey.A,
                KeyCode.S => KeyboardKey.S,
                KeyCode.D => KeyboardKey.D,
                KeyCode.Q => KeyboardKey.Q,
                KeyCode.E => KeyboardKey.E,
                KeyCode.R => KeyboardKey.R,
                KeyCode.F => KeyboardKey.F,
                KeyCode.Up => KeyboardKey.Up,
                KeyCode.Down => KeyboardKey.Down,
                KeyCode.Left => KeyboardKey.Left,
                KeyCode.Right => KeyboardKey.Right,
                KeyCode.Space => KeyboardKey.Space,
                KeyCode.LeftShift => KeyboardKey.LeftShift,
                KeyCode.RightShift => KeyboardKey.RightShift,
                KeyCode.LeftControl => KeyboardKey.LeftControl,
                KeyCode.RightControl => KeyboardKey.RightControl,
                KeyCode.Escape => KeyboardKey.Escape,
                KeyCode.Enter => KeyboardKey.Enter,
                KeyCode.Tab => KeyboardKey.Tab,
                KeyCode.Backspace => KeyboardKey.Backspace,
                _ => KeyboardKey.Null
            };
        }

        private static MouseButton ToRaylibMouseButton(MouseButtonCode button)
        {
            return button switch
            {
                MouseButtonCode.Left => MouseButton.Left,
                MouseButtonCode.Right => MouseButton.Right,
                MouseButtonCode.Middle => MouseButton.Middle,
                MouseButtonCode.Side => MouseButton.Side,
                MouseButtonCode.Extra => MouseButton.Extra,
                MouseButtonCode.Forward => MouseButton.Forward,
                MouseButtonCode.Back => MouseButton.Back,
                _ => MouseButton.Left
            };
        }
    }
}
namespace Engine.Input
{
    public static class InputSystem
    {
        private static readonly HashSet<ConsoleKey> _justPressedKeys = new();
        private static readonly HashSet<ConsoleKey> _pressedKeys = new();
        private static readonly HashSet<ConsoleKey> _justReleasedKeys = new();
        
        internal static void OnTick()
        {
            _justPressedKeys.Clear();
            _justReleasedKeys.Clear();

            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                
                if (!_pressedKeys.Contains(key))
                    _justPressedKeys.Add(key);
                
                _pressedKeys.Add(key);
            }
        }
        
        public static bool IsStarted(ConsoleKey key) => _justPressedKeys.Contains(key);
        public static bool IsPressed(ConsoleKey key) => _pressedKeys.Contains(key);
        public static bool IsReleased(ConsoleKey key) => _justReleasedKeys.Contains(key);
    }
}
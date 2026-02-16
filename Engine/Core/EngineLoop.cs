using System.Diagnostics;
using Engine.Input;
using Engine.SceneManagement;

namespace Engine.Core
{
    public static class EngineLoop
    {
        private static Stopwatch _stopwatch = new();
        private static double _previousFrameTime;
        private static double _currentTime;
        private static double _deltaTime;
        private static double _accumulator;
        private static int _frameCount;
        private static bool _isRunning = true;
        
        public static void Main()
        {
            _stopwatch.Start();
            _previousFrameTime = _stopwatch.Elapsed.TotalMilliseconds;

            while (_isRunning)
            {
                CalculateTime();
                InputSystem.OnTick();
                
                while (_accumulator >= Time.FixedDeltaTime)
                {
                    _accumulator -= Time.FixedDeltaTime;
                }

                SceneManager.CurrentScene.OnTick();
                
                if (InputSystem.IsStarted(ConsoleKey.A))
                    _isRunning = false;
            }

            _stopwatch.Stop();
            Console.WriteLine($"{_stopwatch.ElapsedMilliseconds} ms");
        }
        
        private static void CalculateTime()
        {
            ++_frameCount;
            _currentTime = _stopwatch.Elapsed.TotalMilliseconds;
            _deltaTime = _currentTime - _previousFrameTime;
    
            Time.CurrentTime = _currentTime;
            Time.PreviousFrameTime = _previousFrameTime;
            Time.DeltaTime = _deltaTime;
            Time.CurrentFrame = _frameCount;
    
            _previousFrameTime = _currentTime;
            _accumulator += _deltaTime;
        }
    }
}
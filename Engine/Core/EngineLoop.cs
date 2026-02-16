using System.Diagnostics;
using Engine.Input;
using Engine.Rending;
using Engine.SceneManagement;
using Engine.World;

namespace Engine.Core
{
    public static class EngineLoop
    {
        private static readonly Stopwatch _stopwatch = new();
        private static double _previousFrameTime;
        private static double _currentTime;
        private static double _deltaTime;
        private static double _accumulator;
        private static int _frameCount;
        private static bool _isRunning;
        
        public static void Run(Scene startScene, RenderSettings? renderSettings = null)
        {
            if (startScene is null)
                throw new ArgumentNullException(nameof(startScene));

            _isRunning = true;
            _frameCount = 0;
            _accumulator = 0;
            _stopwatch.Start();
            _previousFrameTime = _stopwatch.Elapsed.TotalMilliseconds;
            
            SceneManager.LoadScene(startScene);
            Renderer.Initialize(renderSettings);

            while (_isRunning)
            {
                CalculateTime();
                InputSystem.OnTick();
                
                while (_accumulator >= Time.FixedDeltaTime)
                {
                    SceneManager.CurrentScene.OnFixedTick();
                    _accumulator -= Time.FixedDeltaTime;
                }

                SceneManager.CurrentScene.OnTick();
                Renderer.Render(SceneManager.CurrentScene);
                
                if (Renderer.ShouldClose)
                    _isRunning = false;
            }

            _stopwatch.Stop();
            Renderer.Shutdown();
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
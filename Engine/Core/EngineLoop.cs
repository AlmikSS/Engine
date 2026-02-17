using System.Diagnostics;
using Engine.Entities;
using Engine.Input;
using Engine.Physics;
using Engine.Rending;
using Engine.SceneManagement;

namespace Engine.Core
{
    public static class EngineLoop
    {
        private static readonly Stopwatch _stopwatch = new();
        private static float _previousFrameTime;
        private static float _currentTime;
        private static float _deltaTime;
        private static float _accumulator;
        private static int _frameCount;
        private static bool _isRunning;

        public static bool IsRunning => _isRunning; 
        
        public static void Run(Scene startScene, PhysicsSettings physicsSettings, RenderSettings? renderSettings = null)
        {
            if (startScene is null)
                throw new ArgumentNullException(nameof(startScene));

            _isRunning = true;
            _frameCount = 0;
            _accumulator = 0;
            _stopwatch.Start();
            _previousFrameTime = (float)_stopwatch.Elapsed.TotalSeconds;
            
            SceneManager.LoadScene(startScene);
            PhysicsEngine.Initialize(physicsSettings);
            Renderer.Initialize(renderSettings);

            startScene.OnSceneCreated();
            
            while (_isRunning)
            {
                CalculateTime();
                InputSystem.OnTick();
                
                while (_accumulator >= Time.FixedDeltaTime)
                {
                    PhysicsEngine.OnFixedTick();
                    SceneManager.CurrentScene.OnFixedTick();
                    _accumulator -= Time.FixedDeltaTime;
                }

                SceneManager.CurrentScene.OnTick();
                Renderer.Render(SceneManager.CurrentScene);
                SceneManager.CurrentScene.SpawnQueue();
                SceneManager.CurrentScene.DespawnQueue();
                
                if (Renderer.ShouldClose)
                    _isRunning = false;
            }

            _stopwatch.Stop();
            Renderer.Shutdown();
            Console.WriteLine($"{_stopwatch.Elapsed.Seconds} ms");
        }
        
        private static void CalculateTime()
        {
            ++_frameCount;
            _currentTime = (float)_stopwatch.Elapsed.TotalSeconds;
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
using Engine.Entities;
using Engine.Physics;

namespace Engine.SceneManagement
{
    public static class SceneManager
    {
        public static Scene CurrentScene { get; private set; }

        public static void LoadScene(Scene scene)
        {
            CurrentScene = scene;
            PhysicsEngine.RebuildScene();
        }
    }
}
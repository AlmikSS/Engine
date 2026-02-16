using Engine.Entities.Components;

namespace Engine.Physics
{
    public static class PhysicsEngine
    {
        private static readonly List<Rigidbody2D> _rigidbodies = new();
        private static PhysicsSettings _settings;
        
        internal static void Initialize(PhysicsSettings settings)
        {
            _settings = settings;
        }
        
        internal static void OnFixedTick()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.PhysicsTick(_settings);
            }
        }

        internal static void Register(Rigidbody2D rigidbody)
        {
            if (_rigidbodies.Contains(rigidbody))
                return;

            _rigidbodies.Add(rigidbody);
        }
    }
}
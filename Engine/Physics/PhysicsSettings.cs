using Engine.Types;

namespace Engine.Physics
{
    public class PhysicsSettings
    {
        public Vector2 Gravity { get; init; } = new(0, -9.8f);
    }
}
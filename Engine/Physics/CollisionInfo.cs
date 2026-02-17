using Engine.Types;

namespace Engine.Physics
{
    public readonly record struct CollisionInfo(
        Vector2 Normal,
        float Penetration,
        Vector2 Point
    );
}
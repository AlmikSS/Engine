using Engine.World.Components;

namespace Engine.World
{
    public class Entity
    {
        internal readonly string Id;
        public Transform Transform { get; }
        private readonly List<Component> _components = new();

        public Entity(string id, Transform transform)
        {
            Id = id;
            Transform = transform;
        }
    }
}
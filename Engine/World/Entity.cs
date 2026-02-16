using Engine.API;
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
            Transform.Owner = this;
        }

        internal void OnTick()
        {
            foreach (var component in _components)
            {
                component.Tick();
            }
        }

        internal void OnFixedTick()
        {
            foreach (var component in _components)
            {
                component.FixedTick();
            }
        }
        
        public void AddComponent<T>() where T : Component, new()
        {
            var component = new T();
            component.Owner = this;
            
            if (component is GameBehaviour gameBehaviour)
                gameBehaviour.Initialize(this);
            
            _components.Add(component);
        }

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            foreach (var c in _components)
            {
                if (c is T t)
                {
                    component = t;
                    return true;
                }
            }
            
            component = null;
            return false;
        }
    }
}
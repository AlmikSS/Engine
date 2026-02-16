using Engine.World;
using Engine.World.Components;

namespace Engine.API
{
    public class GameBehaviour : Component
    {
        public Transform Transform { get; private set; }
        public Entity Entity { get; private set; }

        internal void Initialize(Entity entity)
        {
            Entity = entity;
            Transform = entity.Transform;
            
            OnInitialize();
        }

        internal override void Tick()
        {
            OnTick();
        }

        internal override void FixedTick()
        {
            OnFixedTick();
        }

        internal void Destroy()
        {
            OnDestroy();
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnTick() { }
        protected virtual void OnFixedTick() { }
        protected virtual void OnDestroy() { }
        
    }
}
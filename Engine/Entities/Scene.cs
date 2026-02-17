using Engine.Core;
using Engine.Entities.Components;
using Engine.Physics;

namespace Engine.Entities
{
    public class Scene
    {
        public IReadOnlyList<Entity> ActiveEntities => _activeEntities;
        
        private readonly List<Entity> _activeEntities = new();
        
        private readonly Queue<Entity> _spawnQueue = new();
        private readonly Queue<Entity> _despawnQueue = new();

        public void AddEntity(Entity entity)
        {
            if (EngineLoop.IsRunning)
            {
                _spawnQueue.Enqueue(entity);
                return;
            }
            
            _activeEntities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            if (EngineLoop.IsRunning)
            {
                _despawnQueue.Enqueue(entity);
                return;
            }
            
            _activeEntities.Remove(entity);
        }

        internal void SpawnQueue()
        {
            while (_spawnQueue.Count > 0)
            {
                var entity = _spawnQueue.Dequeue();
                
                if (entity.TryGetComponent<Rigidbody2D>(out var rb))
                    PhysicsEngine.Register(rb);
                if (entity.TryGetComponent<Collider2D>(out var collider))
                    PhysicsEngine.Register(collider);
                
                _activeEntities.Add(entity);
            }
        }
        
        internal void DespawnQueue()
        {
            while (_despawnQueue.Count > 0)
            {
                var entity = _despawnQueue.Dequeue();
                _activeEntities.Remove(entity);
            }
        }
        
        internal void OnSceneCreated()
        {
            foreach (var entity in _activeEntities)
            {
                entity.OnSceneCreated();
            }
        }
        
        internal void OnTick()
        {
            foreach (var entity in _activeEntities)
            {
                entity.OnTick();
            }
        }

        internal void OnFixedTick()
        {
            foreach (var entity in _activeEntities)
            {
                entity.OnFixedTick();
            }
        }
    }
}
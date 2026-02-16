namespace Engine.World
{
    public class Scene
    {
        public IReadOnlyList<Entity> ActiveEntities => _activeEntities;
        
        private readonly List<Entity> _activeEntities = new();

        public void AddEntity(Entity entity)
        {
            _activeEntities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            _activeEntities.Remove(entity);
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
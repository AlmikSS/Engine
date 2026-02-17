using Engine.Entities.Components;

namespace Engine.Entities
{
    public static class EntityFactory
    {
        private static int _idCounter;
        
        public static Entity Create(Scene scene)
        {
            var id = "Entity_" + _idCounter;
            ++_idCounter;

            var entity = new Entity(id, new Transform());
            scene.AddEntity(entity);
            return entity;
        }
    }
}
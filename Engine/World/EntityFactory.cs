using Engine.World.Components;

namespace Engine.World
{
    public static class EntityFactory
    {
        private static int _idCounter;
        
        public static Entity Create()
        {
            var id = "Entity_" + _idCounter;
            ++_idCounter;

            return new Entity(id, new Transform());
        }
    }
}
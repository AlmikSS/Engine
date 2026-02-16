namespace Engine.World.Components
{
    public class Component
    {
        internal Entity Owner;
        
        internal virtual void Tick() { }

        internal virtual void FixedTick() { }
    }
}
namespace Engine.Entities.Components
{
    public class Component
    {
        internal Entity Owner;

        internal virtual void OnSceneCreated() {}
        internal virtual void Tick() { }

        internal virtual void FixedTick() { }
    }
}
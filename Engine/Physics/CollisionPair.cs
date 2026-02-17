namespace Engine.Physics
{
    public readonly record struct CollisionPair(int A, int B)
    {
        public static CollisionPair Create(int idA, int idB)
            => idA < idB ? new CollisionPair(idA, idB) : new CollisionPair(idB, idA);
    }
}
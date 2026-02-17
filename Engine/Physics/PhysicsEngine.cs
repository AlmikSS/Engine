using Engine.Entities.Components;
using Engine.SceneManagement;
using Engine.Types;

namespace Engine.Physics
{
    public static class PhysicsEngine
    {
        private static readonly List<Rigidbody2D> _rigidbodies = new();
        private static readonly List<Collider2D> _colliders = new();

        private static readonly HashSet<CollisionPair> _currentPairs = new(); 
        private static readonly HashSet<CollisionPair> _previousPairs = new(); 
        
        private static PhysicsSettings _settings;
        
        internal static void Initialize(PhysicsSettings settings)
        {
            _settings = settings;
        }

        internal static void RebuildScene()
        {
            _rigidbodies.Clear();
            _colliders.Clear();

            foreach (var entity in SceneManager.CurrentScene.ActiveEntities)
            {
                if (entity.TryGetComponent<Rigidbody2D>(out var rigidbody))
                    _rigidbodies.Add(rigidbody);
                if (entity.TryGetComponent<Collider2D>(out var collider))
                    _colliders.Add(collider);
            }
        }

        internal static void OnFixedTick()
        {
            foreach (var rigidbody in _rigidbodies)
                rigidbody.PhysicsTick(_settings);
            
            _currentPairs.Clear();

            for (var i = 0; i < _colliders.Count; i++)
            {
                for (var j = i + 1; j < _colliders.Count; j++)
                {
                    var a = _colliders[i];
                    var b = _colliders[j];
                    
                    //TODO Layer mask 
                    
                    if (!TryIntersectAabb(a.GetAabb(), b.GetAabb(), out var infoAB, out var infoBA))
                        continue;
                    
                    var pair = CollisionPair.Create(a.Owner.GetHashCode(), b.Owner.GetHashCode());
                    _currentPairs.Add(pair);
                    
                    var isEnter = !_previousPairs.Contains(pair);

                    if (a.IsTrigger || b.IsTrigger)
                    {
                        if (isEnter)
                        {
                            a.InvokeEnter(b, infoAB);
                            b.InvokeEnter(a, infoBA);
                        }
                        else
                        {
                            a.InvokeStay(a,  infoAB);
                            b.InvokeStay(b, infoBA);
                        }
                        continue;
                    }

                    Resolve(a, b, infoAB);
                    
                    if (isEnter)
                    {
                        a.InvokeEnter(b, infoAB);
                        b.InvokeEnter(a, infoBA);
                    }
                    else
                    {
                        a.InvokeStay(b, infoAB);
                        b.InvokeStay(a, infoBA);
                    }
                }
            }
            
            foreach (var oldPair in _previousPairs)
            {
                if (_currentPairs.Contains(oldPair))
                    continue;
            }

            _previousPairs.Clear();
            foreach (var p in _currentPairs) _previousPairs.Add(p);
        }

        private static bool TryIntersectAabb(Aabb a, Aabb b, out CollisionInfo infoAB, out CollisionInfo infoBA)
        {
            infoAB = default;
            infoBA = default;
            
            
            var overlapX = MathF.Min(a.MaxX, b.MaxX) - MathF.Max(a.MinX, b.MinX);
            if (overlapX <= 0f)
                return false;

            var overlapY = MathF.Min(a.MaxY, b.MaxY) - MathF.Max(a.MinY, b.MinY);
            if (overlapY <= 0f)
                return false;
            
            var ca = a.Center;
            var cb = b.Center;
            var delta = cb - ca;

            Vector2 normal;
            float penetration;

            if (overlapX < overlapY)
            {
                normal = new Vector2(delta.X >= 0 ? 1 : -1, 0);
                penetration = overlapX;
            }
            else
            {
                normal = new Vector2(0, delta.Y >= 0 ? 1 : -1);
                penetration = overlapY;
            }

            var contact = (ca + cb) * 0.5f;

            infoAB = new CollisionInfo(normal, penetration, contact);
            infoBA = new CollisionInfo(-normal, penetration, contact);
            return true;
        }

        private static void Resolve(Collider2D a, Collider2D b, CollisionInfo ab)
        {
            var rbA = a.AttachedRigidbody;
            var rbB = b.AttachedRigidbody;
            
            var aDynamic = rbA is { IsStatic: false };
            var bDynamic = rbB is { IsStatic: false };

            if (!aDynamic && !bDynamic)
                return;

            var correction = ab.Normal * ab.Penetration;
            
            if (aDynamic && !bDynamic)
            {
                a.Owner.Transform.Position -= correction;
                RemoveVelocityAlongNormal(rbA!, ab.Normal);
                return;
            }

            if (!aDynamic && bDynamic)
            {
                b.Owner.Transform.Position += correction;
                RemoveVelocityAlongNormal(rbB!, -ab.Normal);
                return;
            }

            var mA = MathF.Max(0.0001f, rbA!.Mass);
            var mB = MathF.Max(0.0001f, rbB!.Mass);
            var total = mA + mB;

            var moveA = mB / total;
            var moveB = mA / total;

            a.Owner.Transform.Position -= correction * moveA;
            b.Owner.Transform.Position += correction * moveB;

            RemoveVelocityAlongNormal(rbA, ab.Normal);
            RemoveVelocityAlongNormal(rbB, -ab.Normal);
        }
        
        private static void RemoveVelocityAlongNormal(Rigidbody2D rb, Vector2 normal)
        {
            var vn = rb.Velocity.X * normal.X + rb.Velocity.Y * normal.Y;
            if (vn <= 0)
                return;
            rb.Velocity -= normal * vn;
        }
    }
}
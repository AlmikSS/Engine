namespace Engine.Types
{
    public struct Vector2(float x, float y)
    {
        public float X = x;
        public float Y = y;
        public float Lenght => MathF.Sqrt(X * X + Y * Y);
        public Vector2 Normalized => new Vector2(X / Lenght, Y / Lenght);
        
        public static Vector2 Zero => new Vector2(0, 0);
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Right => new Vector2(1, 0);
        
        public static Vector2 Normalize(Vector2 original)
        {
            return new Vector2(original.X / original.Lenght, original.Y / original.Lenght);
        }

        public static Vector2 operator *(float multiplier, Vector2 vector)
        {
            return new Vector2(vector.X * multiplier, vector.Y * multiplier);
        }

        public static Vector2 operator *(Vector2 vector, float multiplier)
        {
            return new Vector2(vector.X * multiplier, vector.Y * multiplier);
        }
        
        public static Vector2 operator /(float multiplier, Vector2 vector)
        {
            return new Vector2(vector.X / multiplier, vector.Y / multiplier);
        }

        public static Vector2 operator /(Vector2 vector, float multiplier)
        {
            return new Vector2(vector.X / multiplier, vector.Y / multiplier);
        }

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static Vector2 operator +(float multiplier, Vector2 vector)
        {
            return new Vector2(vector.X + multiplier, vector.Y + multiplier);
        }
        
        public static Vector2 operator +(Vector2 vector, float multiplier)
        {
            return new Vector2(vector.X + multiplier, vector.Y + multiplier);
        }

        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }
        
        public static Vector2 operator -(float multiplier, Vector2 vector)
        {
            return new Vector2(vector.X - multiplier, vector.Y - multiplier);
        }
        
        public static Vector2 operator -(Vector2 vector, float multiplier)
        {
            return new Vector2(vector.X - multiplier, vector.Y - multiplier);
        }
    }
}
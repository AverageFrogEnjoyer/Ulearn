using Microsoft.Xna.Framework;

namespace _Game_.Entities
{
    public class BulletData
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Lifespan { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
    }
}

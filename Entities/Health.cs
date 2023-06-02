using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.Entities
{
    public class Health : Sprite
    {
        public float Lifespan;

        public Health(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Lifespan = 10f;
        }

        public void Update()
        {
            Lifespan -= Globals.TotalSeconds;
        }

        public void Collect()
        {
            Lifespan = 0;
        }
    }
}

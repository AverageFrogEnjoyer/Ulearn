using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.Entities
{
    public class Sprite
    {
        protected Texture2D texture;
        public readonly Vector2 origin;
        public Vector2 Position;
        public int Speed { get; set; }
        public float Rotation { get; set; }

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            origin = new(tex.Width / 2, tex.Height / 2);
            Speed = 300;
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Position, null, Color.White, Rotation, origin, 1, SpriteEffects.None, 1);
        }
    }
}

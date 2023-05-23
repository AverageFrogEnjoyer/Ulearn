using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game_
{
    public class Sprite
    {
        protected Texture2D texture;
        protected readonly Vector2 origin;
        public Vector2 Position; /*{ get; set; }*/
        public int Speed { get; set; }
        public float Rotation { get; set; }
        //public float Scale { get; set; }
        //public Color Color { get; set; }

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            origin = new(tex.Width / 2, tex.Height / 2);
            //Speed = 300;
            //Color = Color.White;
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Position, null, Color.White, Rotation, origin, 1/*Scale*/, SpriteEffects.None, 1);
        }
    }
}

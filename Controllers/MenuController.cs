using _Game_.Entities;
using _Game_.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace _Game_.Controllers
{
    public class MenuController
    {
        private Game1 game;
        private Texture2D SpriteBack { get; set; }
        private Texture2D SpriteText { get; set; }
        private Texture2D Manual { get; set; }

        public MenuController(Game1 game)
        {
            this.game = game;
            PlayerManager.Init();
        }

        public void Update()
        {
            if (InputManager.IsStartButtonPressed)
            {
                var gameController = new GameController(game);
                //var gameOverController = new GameOverController(game);
                game.State = GameState.Game;
                game.GameController = gameController;
                //game.GameOverController = gameOverController;
            }
        }

        public void Load()
        {
            SpriteBack = Globals.Content.Load<Texture2D>("Splash");
            SpriteText = Globals.Content.Load<Texture2D>("StartButton3");
            Manual = Globals.Content.Load<Texture2D>("Rules");
            MapManager.Load();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                SpriteBack,
                Vector2.Zero,
                Color.White);
            Globals.SpriteBatch.Draw(
                SpriteText,
                new Vector2((Globals.Bounds.X - SpriteText.Width) / 2,
                (Globals.Bounds.Y - SpriteText.Height) / 2),
                null,
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
            Globals.SpriteBatch.Draw(
                Manual,
                new Vector2((Globals.Bounds.X - Manual.Width) / 2,
                Globals.Bounds.Y - Manual.Height - 64),
                null,
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
        }

    }
}

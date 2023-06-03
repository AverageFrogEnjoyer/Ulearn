using _Game_.Entities;
using _Game_.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            if (InputController.IsStartButtonPressed)
            {
                var gameController = new GameController(game);
                game.State = GameState.Game;
                game.GameController = gameController;
            }
        }

        public void Load()
        {
            SpriteBack = Globals.Content.Load<Texture2D>("Splash");
            SpriteText = Globals.Content.Load<Texture2D>("StartButton3");
            Manual = Globals.Content.Load<Texture2D>("Rules");
            MapView.Load();
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

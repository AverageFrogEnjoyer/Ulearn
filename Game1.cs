using _Game_.Controllers;
using _Game_.Entities;
using _Game_.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;


namespace _Game_
{
    public enum GameState
    {
        Menu,
        Game,
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public GameState State;
        public MenuController MenuController;
        public GameController GameController;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.Bounds = new(1536, 1024);
            graphics.PreferredBackBufferWidth = Globals.Bounds.X;
            graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
            graphics.ApplyChanges();

            Globals.Content = Content;
            State = GameState.Menu;

            MenuController = new(this);
            GameController = new(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;

            MenuController.Load();
            GameController.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.gameTime = gameTime;
            Globals.Update();
            InputController.Update();

            switch (State)
            {
                case GameState.Menu:
                    MenuController.Update();
                    break;
                case GameState.Game:
                    GameController.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (State)
            {
                case GameState.Menu:
                    MenuController.Draw();
                    break;
                case GameState.Game:
                    GameController.Draw();
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
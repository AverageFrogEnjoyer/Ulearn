using _Game_.Entities;
using _Game_.Managers;
using Game_;
using GameShooter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;


//экземпляры классов
namespace _Game_
{
    public enum GameState
    {
        SplashScreen,
        Map1,
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static GameState State;

        //private InputManager inputManager = new();

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
            Globals.IsPaused = false;            
            State = GameState.SplashScreen;

            PlayerManager.Init();
            BulletManager.Init();
            InterfaceManager.Init();
            SwampManager.Init();
            EnemyManager.Init();
            HealthManager.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;

            MenuManager.Load();
            GameOverManager.Load();
            MapManager.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.gameTime = gameTime;
            Globals.Update();
            InputManager.Update();

            switch (State)
            {
                case GameState.SplashScreen:
                    if (InputManager.IsStartButtonPressed)
                    {
                        State = GameState.Map1;
                    }
                    break;
                case GameState.Map1:
                    if (Globals.IsPaused)
                    {
                        break;                        
                    }
                    PlayerManager.Update();
                    BulletManager.Update();
                    SwampManager.Update();
                    EnemyManager.Update();
                    HealthManager.Update();
                    if (InputManager.IsEnterPressed)
                    {
                        State = GameState.SplashScreen;
                        Restart();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        private void Restart()
        {
            BulletManager.Reset();
            EnemyManager.Reset();
            SwampManager.Reset();
            HealthManager.Reset();
            PlayerManager.Reset();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (State)
            {
                case GameState.SplashScreen:
                    MenuManager.Draw(spriteBatch);
                    break;
                case GameState.Map1:
                    MapManager.Draw(spriteBatch);
                    SwampManager.Draw();
                    HealthManager.Draw();
                    BulletManager.Draw();
                    EnemyManager.Draw();
                    PlayerManager.Draw();
                    InterfaceManager.Draw();
                    if (PlayerManager.player.IsDead)
                    {
                        GameOverManager.Draw();
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
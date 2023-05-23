using _Game_.Entities;
using _Game_.GameStates;
using _Game_.Managers;
using Game_;
using GameShooter.Entities;
using GameShooter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;


//Score
//Exp
//Bushes to destroy

namespace _Game_
{
    public enum GameState
    {
        SplashScreen,
        Map1,
        Map2,
        MapFrog,
        EndScreenDeath
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        public static GameState State = GameState.SplashScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 60);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.Bounds = new(1536, 1024);
            _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
            _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
            _graphics.ApplyChanges();

            
            Globals.Content = Content;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;
            
            var bulletTexture = Globals.Content.Load<Texture2D>("Ball");
            BulletManager.Init(bulletTexture);
            InterfaceManager.Init(bulletTexture);
            Player.PlayerSprite = Content.Load<Texture2D>("Player");
            Player.DeathSprite = Content.Load<Texture2D>("PlayerDead");

            Splashscreen.SpriteBack = Content.Load<Texture2D>("Splash");
            Splashscreen.SpriteText2 = Content.Load<Texture2D>("StartButton");
            GameOver.Sprite = Content.Load<Texture2D>("End");
            Map1.Sprite = Content.Load<Texture2D>("Grass");

            player = new(Content.Load<Texture2D>("Player"), new(Globals.Bounds.X / 2 - Player.PlayerSprite.Width / 8, Globals.Bounds.Y / 2 - Player.PlayerSprite.Height / 10));
            EnemyManager.Init();
            EnemyManager.AddEnemy();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.Update(gameTime);
            InputManager.Update();
            

            switch (State)
            {
                case GameState.SplashScreen:
                    if (keyboardState.IsKeyDown(Keys.Space))
                        State = GameState.Map1;
                    break;
                case GameState.Map1:
                    BulletManager.Update(EnemyManager.Enemies);
                    player.Update(EnemyManager.Enemies);
                    EnemyManager.Update(player);
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        State = GameState.SplashScreen;
                        Restart();
                    }
                    break;
                case GameState.Map2:
                    break;
                case GameState.MapFrog:
                    break;
                case GameState.EndScreenDeath:
                    break;

            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void Restart()
        {
            BulletManager.Reset();
            EnemyManager.Reset();
            player.Reset();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch (State)
            {
                case GameState.SplashScreen:
                    Splashscreen.Draw(spriteBatch);
                    break;
                case GameState.Map1:
                    Map1.Draw(spriteBatch);
                    BulletManager.Draw();                    
                    EnemyManager.Draw();
                    player.Draw();
                    InterfaceManager.Draw(player);
                    if (Player.Dead)
                        spriteBatch.Draw(GameOver.Sprite, new Vector2((Globals.Bounds.X - GameOver.Sprite.Width) / 2, (Globals.Bounds.Y - GameOver.Sprite.Height) / 2), null, Color.White * 0.9f, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                    break;
                case GameState.Map2:
                    break;
                case GameState.MapFrog:
                    break;
                case GameState.EndScreenDeath:
                    //spriteBatch.Draw(GameOver.Sprite, new Vector2((Globals.Bounds.X - GameOver.Sprite.Width) / 2, (Globals.Bounds.Y - GameOver.Sprite.Height) / 2), null, Color.White * 0.05f, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    //EndScreenDeath.Draw(spriteBatch);
                    break;
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
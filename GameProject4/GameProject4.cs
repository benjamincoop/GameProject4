using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameProject4
{
    public class GameProject4 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private List<CoinSprite> coins = new List<CoinSprite>();
        private Random rand = new Random();
        private ParticleEmitter _emitter = new ParticleEmitter();
        SpriteFont _font;

        // Layer textures
        private Texture2D _foreground;
        private Texture2D _midground;
        private Texture2D _background;

        // particle systems
        private RainParticleSystem _rain;
        private SparksParticleSystem _sparks;
        private FireworkParticleSystem _fireworks;

        int coinsCollected = 0;
        const int levelWidth = 14000;

        public GameProject4()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // Need to use HiDef due to the size of our textures!
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player();
            _rain = new RainParticleSystem(this, _graphics.GraphicsDevice.Viewport.Bounds);
            Components.Add(_rain);
            _sparks = new SparksParticleSystem(this, _emitter);
            Components.Add(_sparks);
            _fireworks = new FireworkParticleSystem(this, 1);
            Components.Add(_fireworks);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.LoadContent(Content);
            _font = Content.Load<SpriteFont>("Font");

            // load the layer textures
            _foreground = Content.Load<Texture2D>("foreground");
            _midground = Content.Load<Texture2D>("midground");
            _background = Content.Load<Texture2D>("background");

            SpawnCoin();
        }

        private void SpawnCoin()
        {
            Vector2 pos = new Vector2(_player.Position.X + 600, rand.Next(0, GraphicsDevice.Viewport.Height));
            CoinSprite coin = new CoinSprite(pos);
            coin.LoadContent(Content);
            coins.Add(coin);
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">An object representing time in-game</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(CoinSprite coin in coins.ToArray())
            {
                if (coin.Bounds.CollidesWith(_player.Bounds))
                {
                    _fireworks.PlaceFirework(_emitter.Position);
                    coin.Collected = true;
                    coinsCollected++;
                    coins.Remove(coin);
                    SpawnCoin();
                }
            }

            _player.Update(gameTime);
            //_emitter.Position = _player.Position;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game
        /// </summary>
        /// <param name="gameTime">An object representing time in-game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Calculate offset vector
            float playerX = MathHelper.Clamp(_player.Position.X, 300, 13600);
            float offsetX = 300 - playerX;

            string str = "Coins Collected: " + coinsCollected.ToString();
            Vector2 strSize = _font.MeasureString(str);
            Vector2 strPos = new Vector2(_background.Width - strSize.X, 0);

            Matrix transform;

            // background
            transform = Matrix.CreateTranslation(offsetX * 0.333f, 0, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_font, "Fly right and collect the coins.", Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_font, str, strPos, Color.White);
            _spriteBatch.End();

            // midground
            transform = Matrix.CreateTranslation(offsetX * 0.666f, 0, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_midground, Vector2.Zero, Color.White);
            _spriteBatch.End();

            // foreground objects
            transform = Matrix.CreateTranslation(offsetX, 0, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_foreground, Vector2.Zero, Color.White);
            _player.Draw(gameTime, _spriteBatch);
            _emitter.Position = Vector2.Transform(new Vector2(_player.Position.X + 35, _player.Position.Y + 20), transform);
            foreach (CoinSprite coin in coins)
            {
                coin.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameProject4.Collisions;

namespace GameProject4
{
    /// <summary>
    /// A class representing the player's helicopter
    /// </summary>
    public class Player
    {
        // The texture atlas for the helicopter sprite
        private Texture2D _texture;

        // The bounds of hte helicopter within the texture atlas
        private Rectangle _heliBounds = new Rectangle(0, 0, 130, 53);

        // The position of the player
        private Vector2 _position = Vector2.Zero;

        private float _rotation = 0f;

        /// <summary>
        /// The current position of the player
        /// </summary>
        public Vector2 Position => _position;

        private BoundingRectangle _bounds;

        /// <summary>
        /// The bounding volume of the sprite.
        /// </summary>
        public BoundingRectangle Bounds => _bounds;

        /// <summary>
        /// Loads the player texture atlas
        /// </summary>
        /// <param name="content">The ContentManager to use to load the content</param>
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("helicopter");
            _bounds = new BoundingRectangle(new Vector2(_position.X + 25, _position.Y), _texture.Width - 25, _texture.Height);
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.GetPressedKeyCount() == 0)
            {
                _rotation = 0f;
            } else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    _position -= Vector2.UnitY * 240 * t;
                    _rotation = 0f;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    _position += Vector2.UnitY * 480 * t;
                    _rotation = 0f;
                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    _position -= Vector2.UnitX * 400 * t;
                    _rotation = -0.07f;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    if(_position.X + _texture.Width < 9600)
                    {
                        _position += Vector2.UnitX * 400 * t;
                        _rotation = 0.07f;
                    }
                }
                _bounds.X = _position.X;
                _bounds.Y = _position.Y;
            }
        }

        /// <summary>
        /// Draws the player sprite
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        /// <param name="spriteBatch">The SpriteBatch to draw the player with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _heliBounds, Color.White, _rotation, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
}

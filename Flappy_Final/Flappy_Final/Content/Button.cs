using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bat.Content.Controls
{
    class Button : Component
    {
        #region fields
        private MouseState _currentMouse;

        private SpriteFont _font;


        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture;

        #endregion
        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text {get; set;}

        #endregion
        #region Methods
        // Constructor
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;

            PenColor = Color.Black;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (_isHovering)
                color = Color.Gray;

            spriteBatch.Draw(_texture, Rectangle, color);
            if (!string.IsNullOrEmpty(Text))
            {
                float x = Rectangle.X + (Rectangle.Width / 2.0f) - (_font.MeasureString(Text).X / 2.0f);
                float y = (Rectangle.Y + (Rectangle.Height / 2.0f)) - (_font.MeasureString(Text).Y / 2.0f);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }


            public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle mouseRectange = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if(mouseRectange.Intersects(Rectangle))
            {
                _isHovering = true;
            }

            if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, new EventArgs());
            }
        }

    }


        #endregion

    }


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TrustIssues.Controls
{
    public class Button
    {
        private Texture2D _texture;
        private SpriteFont _font;
        private Vector2 _position;
        private Rectangle _rectangle;
        private string _text;
        private float _scale;
        private Color _colour = Color.White;

        public event EventHandler Click;

        public Button(Texture2D texture, SpriteFont font, string text, Vector2 position, float scale = 3f)
        {
            _texture = texture;
            _font = font;
            _text = text;
            _position = position;
            _scale = scale;
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, (int)(_texture.Width * _scale), (int)(_texture.Height * _scale));
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            _colour = Color.White;

            if (mouseRectangle.Intersects(_rectangle))
            {
                _colour = Color.Gray; 
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, _colour, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

            if (!string.IsNullOrEmpty(_text) && _font != null)
            {
                Vector2 textSize = _font.MeasureString(_text);
                float x = _position.X + ((_rectangle.Width / 2) - (textSize.X / 2));
                float y = _position.Y + ((_rectangle.Height / 2) - (textSize.Y / 2));
                spriteBatch.DrawString(_font, _text, new Vector2(x, y), Color.Black);
            }
        }
    }
}
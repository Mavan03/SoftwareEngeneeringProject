using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TrustIssues.Controls
{
    public class Button
    {
        private Texture2D _texture;
        private SpriteFont _font; // NIEUW: Voor de tekst
        private Vector2 _position;
        private Rectangle _rectangle;
        private string _text; // NIEUW: De tekst zelf (bv "1" of "Play")
        private float _scale; // NIEUW: Hoe groot moet hij zijn?

        private Color _colour = Color.White;

        public event EventHandler Click;

        // Constructor update: We vragen nu ook om Font, Tekst en Schaal
        public Button(Texture2D texture, SpriteFont font, string text, Vector2 position, float scale = 3f)
        {
            _texture = texture;
            _font = font;
            _text = text;
            _position = position;
            _scale = scale;

            // De hitbox moet nu rekening houden met de schaal!
            // 21 breed * 3 schaal = 63 breedte hitbox
            _rectangle = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                (int)(_texture.Width * _scale),
                (int)(_texture.Height * _scale));
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            _colour = Color.White;

            if (mouseRectangle.Intersects(_rectangle))
            {
                _colour = Color.Gray; // Hover effect
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 1. Teken de knop (groot gemaakt met _scale)
            spriteBatch.Draw(_texture, _position, null, _colour, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

            // 2. Teken de tekst (als die er is)
            if (!string.IsNullOrEmpty(_text) && _font != null)
            {
                // Berekenen hoe groot de tekst is, zodat we hem kunnen centreren
                Vector2 textSize = _font.MeasureString(_text);

                // Wiskunde om het midden van de knop te vinden
                float x = _position.X + ((_rectangle.Width / 2) - (textSize.X / 2));
                float y = _position.Y + ((_rectangle.Height / 2) - (textSize.Y / 2));

                spriteBatch.DrawString(_font, _text, new Vector2(x, y), Color.Black); // Zwarte tekst
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SoftwareEngeneeringProject.Interfaces;
using SoftwareEngeneeringProject.Entities;

namespace SoftwareEngeneeringProject.States
{
    internal class PlayingState : IGameState
    {
        private Game1 game;
        private GraphicsDevice graphicsDevice;
        private Hero hero;

        public PlayingState(Game1 game,GraphicsDevice graphicsDevice)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            //laden textur
            Texture2D heroTexture = game.Content.Load<Texture2D>("Idle");
            //aanmaken held op pos
            hero = new Hero(heroTexture, new Vector2(100, 100));
        }
        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            hero.Draw(spriteBatch);
        }
    }
}

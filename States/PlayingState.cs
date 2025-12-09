using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SoftwareEngeneeringProject.Interfaces;
using SoftwareEngeneeringProject.Entities;
using SoftwareEngeneeringProject.Input;
using System.Collections.Generic;

namespace SoftwareEngeneeringProject.States
{
    internal class PlayingState : IGameState
    {
        private Game1 game;
        private GraphicsDevice graphicsDevice;
        private Hero hero;

        private InputReader inputReader;

        public PlayingState(Game1 game,GraphicsDevice graphicsDevice)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            //laden textur
            Texture2D heroTexture = game.Content.Load<Texture2D>("Idle");
            //aanmaken held op pos
            hero = new Hero(heroTexture, new Vector2(100, 100));

            inputReader = new InputReader();
        }
        public void Update(GameTime gameTime)
        {
            var commands = inputReader.ReadInput();

            foreach (var command in commands)
            {
                command.Execute(hero);
            }
            hero.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            hero.Draw(spriteBatch);
        }
    }
}

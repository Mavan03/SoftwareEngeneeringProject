using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoftwareEngeneeringProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngeneeringProject.States
{
    public class MenuState : IGameState
    {
        private Game1 game;
        private GraphicsDevice graphicsDevice;

        //game1 megeven om er me kunnen praten om van state te wisselen
        public MenuState(Game1 game,GraphicsDevice graphicsDevice)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Red);
        }

        public void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                game.ChangeState(new PlayingState(game, graphicsDevice));
            }
        }
    }
}

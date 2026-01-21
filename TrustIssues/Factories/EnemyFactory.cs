using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TrustIssues.Entities;

namespace TrustIssues.Factories
{
    public enum EnemyType { Walker, Chaser, Trap }

    public class EnemyFactory
    {
        private Texture2D _walkerTex, _walkerHit;
        private Texture2D _chaserTex, _chaserHit;
        private Texture2D _trapTex;

        public EnemyFactory(ContentManager content, GraphicsDevice graphicsDevice)
        {
            // texture voor enemy dpi
            _walkerTex = content.Load<Texture2D>("Enemy/Run (32x32)");
            _walkerHit = content.Load<Texture2D>("Enemy/Hit");
            _chaserTex = content.Load<Texture2D>("Flying (46x30)"); 
            _chaserHit = content.Load<Texture2D>("Enemy/Hit (46x30)");
            _trapTex = content.Load<Texture2D>("Idle"); 
        }

        public Enemy CreateEnemy(EnemyType type, Vector2 position)
        {

            // eneny maken textures en pos megeven dpi
            switch (type)
            {
                case EnemyType.Walker: return new WalkerEnemy(_walkerTex, _walkerHit, position);
                case EnemyType.Chaser: return new ChaserEnemy(_chaserTex, _chaserHit, position);
                case EnemyType.Trap: return new TrapEnemy(_trapTex, position);
                default: return null;
            }
        }
    }
}
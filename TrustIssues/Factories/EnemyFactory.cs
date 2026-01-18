using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TrustIssues.Entities;

namespace TrustIssues.Factories
{
    public enum EnemyType
    {
        Walker,
        Chaser,
        Trap
    }
    public class EnemyFactory
    {
        private ContentManager content;
        private Texture2D walkerTexture;
        private Texture2D walkerHit;
        private Texture2D chaserTexture;
        private Texture2D chaserHit;

        private Texture2D trapTexture;


        public EnemyFactory(ContentManager content, GraphicsDevice graphicsDevice)
        {
            walkerTexture = content.Load<Texture2D>("Enemy/Run (32x32)");
            chaserTexture = content.Load<Texture2D>("Flying (46x30)");
            trapTexture = content.Load<Texture2D>("Idle");
            chaserHit = content.Load<Texture2D>("Enemy/Hit (46x30)");
            walkerHit = content.Load<Texture2D>("Enemy/Hit");
        }

        public Enemy CreateEnemy(EnemyType type, Vector2 position)
        {
            switch (type)
            {
                case EnemyType.Walker:
                    return new WalkerEnemy(walkerTexture, walkerHit, position);
                case EnemyType.Chaser:
                    return new ChaserEnemy(chaserTexture, chaserHit, position);
                case EnemyType.Trap:
                    return new TrapEnemy(trapTexture, position);
                default:
                    return null;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TrustIssues.Entities;

namespace TrustIssues.Factories
{
    public enum EnemyType
    {
        Walker,
    }
    public class EnemyFactory
    {
        private ContentManager content;
        private Texture2D walkerTexture;
        public EnemyFactory(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            walkerTexture = new Texture2D(graphicsDevice, 40, 40);
            Color[] data = new Color[40 * 40];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Red;
            walkerTexture.SetData(data);
        }

        public Enemy CreateEnemy(EnemyType type, Vector2 position)
        {
            switch (type)
            {
                case EnemyType.Walker:
                    return new WalkerEnemy(walkerTexture, position);
                default:
                    return null;
            }
        }
    }
}

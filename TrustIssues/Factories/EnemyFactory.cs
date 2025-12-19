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
        private Texture2D chaserTexture;
        private Texture2D trapTexture;


        public EnemyFactory(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            walkerTexture = new Texture2D(graphicsDevice, 32, 32);
            Color[] dataWalker = new Color[32 * 32];
            for (int i = 0; i < dataWalker.Length; i++) dataWalker[i] = Color.Red;
            walkerTexture.SetData(dataWalker);

            chaserTexture = new Texture2D(graphicsDevice, 32, 32);
            Color[] dataChaser = new Color[32 * 32];
            for (int i = 0; i < dataChaser.Length; i++) dataChaser[i] = Color.Purple;
            chaserTexture.SetData(dataChaser);

            trapTexture = new Texture2D(graphicsDevice, 32, 32);
            Color[] dataTrap = new Color[32 * 32];
            for (int i = 0; i < dataTrap.Length; i++) dataTrap[i] = Color.Gray;
            trapTexture.SetData(dataTrap);
        }

        public Enemy CreateEnemy(EnemyType type, Vector2 position)
        {
            switch (type)
            {
                case EnemyType.Walker:
                    return new WalkerEnemy(walkerTexture, position);
                case EnemyType.Chaser:
                    return new ChaserEnemy(chaserTexture, position);
                case EnemyType.Trap:
                    return new TrapEnemy(trapTexture, position);
                default:
                    return null;
            }
        }
    }
}

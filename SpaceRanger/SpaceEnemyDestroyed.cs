using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRanger
{
    class SpaceEnemyDestroyedParticles
    {
        Rectangle[] particleList = new Rectangle[8];

        public Rectangle[] ParticleList
        {
            get { return particleList; }
            set { particleList = value; }
        }
        int moveCount;

        public int MoveCount
        {
            get { return moveCount; }
            set { moveCount = value; }
        }

        public SpaceEnemyDestroyedParticles(Rectangle enemy)
        {
            Size size = new Size(3, 3);
            moveCount = 0;

            for (int i = 0; i < particleList.Length; i++)
            {
                particleList[i++] = new Rectangle(new Point(enemy.Left, enemy.Top), size);
                particleList[i++] = new Rectangle(new Point(enemy.Right, enemy.Top), size);
                particleList[i++] = new Rectangle(new Point(enemy.Left, enemy.Bottom), size);
                particleList[i] = new Rectangle(new Point(enemy.Right, enemy.Bottom), size);
            }
        }

        public void moveParticles()
        {
            particleList[0].X -= new Random().Next(-3, 3);
            particleList[0].Y -= new Random().Next(-3, 3);

            particleList[1].X += new Random().Next(-3, 3);
            particleList[1].Y -= new Random().Next(-3, 3);

            particleList[2].X -= new Random().Next(-3, 3);
            particleList[2].Y += new Random().Next(-3, 3);

            particleList[3].X += new Random().Next(-3, 3);
            particleList[3].Y += new Random().Next(-3, 3);

            moveCount++;
                
        }
    }
}

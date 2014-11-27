using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRanger
{
    class SpaceEnemy
    {
        public int incrementX;
        public int x, y;
        private Rectangle rigidBody;

        public Rectangle RigidBody
        {
            get { return rigidBody; }
            set { rigidBody = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public SpaceEnemy(Point coordinates, Size size, int increment)
        {
            rigidBody = new Rectangle(coordinates, size);
            x = coordinates.X;
            y = coordinates.Y;
            incrementX = increment;
        }

        public void Invalidate() 
        {
            rigidBody.X = x;
        }
    }
}

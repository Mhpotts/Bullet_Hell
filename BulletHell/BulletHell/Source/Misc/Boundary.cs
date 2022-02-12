using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell
{
    public class Boundary
    {
        public readonly int minX;
        public readonly int maxX;
        public readonly int minY;
        public readonly int maxY;

        public Boundary(int minX, int minY, int maxX, int maxY)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
        }

        public bool inBounds(float x, float y)
        {
            return x > minX && x < maxX && y > minY && y < maxY;
        }

        public bool inClientPlaygroundBounds(float x)
        {
            return x < (maxX + 50);
        }

        public bool inBounds(Vector2 position)
        {
            return inBounds(position.X, position.Y);
        }
    }
}

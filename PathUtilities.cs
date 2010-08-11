using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace TSAUMDHG
{
    class PathUtilities
    {
        private List<MapElement> path;
        private Point maxPoint;

        const float NintyDegrees = (float)(Math.PI * 90 / 180.0);
        const float OneEightyDegrees = (float)(Math.PI);
        const float TwoSeventyDegrees = (float)(Math.PI * 270 / 180.0);

        public PathUtilities()
        {

        }

        public List<MapElement> GeneratePath(int pathNumber, Point maxPoint)
        {
            path = new List<MapElement>();
            this.maxPoint = maxPoint;

            switch (pathNumber)
            {
                case 0:
                    PathZero();
                    break;
                case 1:
                    PathOne();
                    break;
                case 2:
                    PathTwo();
                    break;
                case 3:
                    PathThree();
                    break;
                case 4:
                    PathFour();
                    break;
                case 5:
                    PathFive();
                    break;
                default:
                    PathZero();
                    break;
            }
        
            return path;
        }

        private void PathZero()
        {
            int x = -3;
            int y = 2;
            for (; x < 23; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 7; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 3; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 13; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x <= 8; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 10; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x <= 15; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 13; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));
            
            for (++x; x <= maxPoint.X + 2; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }
        }

        private void PathOne()
        {
            int x = 3;
            int y = -3;
            for (; y < 13; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 7; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 2; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 11; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 13; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 15; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 2; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 19; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 13; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 23; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > -3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }
        }

        private void PathTwo()
        {
            int x = maxPoint.X - 3;
            int y = maxPoint.Y + 3;

            for (; y > 13; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 20; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 11; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 11; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 8; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 9; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 14; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 2; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 7; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 6; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 16; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 8; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 20; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 23; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > -3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }
        }

        private void PathThree()
        {
            int x = maxPoint.X / 2;
            int y = maxPoint.Y + 3;

            for (; y > 13; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 6; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 15; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 2; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 8; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 15; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 14; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 20; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 7; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 23; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 15; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 6; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 3; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 12; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > -3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }
        }

        private void PathFour()
        {
            int x = maxPoint.X + 3;
            int y = maxPoint.Y - 3;

            for (; x > 13; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 6; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 15; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 8; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x > 18; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 10; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 22; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 2; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 10; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 13; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 4; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 3; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > -3; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }
        }

        private void PathFive()
        {
            int x = -3;
            int y = 2;

            for (; x < 2; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 5; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 6; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 2; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 10; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 5; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0));

            for (++x; x < 14; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--y; y > 2; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++x; x < 18; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 5; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (++x; x < 22; ++x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (++y; y < 11; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 18; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 14; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 14; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 11; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 10; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 14; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > 6; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", 0f));

            for (--y; y > 11; --y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", OneEightyDegrees));

            for (--x; x > 2; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", NintyDegrees));

            for (++y; y < 14; ++y)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", 0f));
            }

            path.Add(new MapElement(new Vector2(x, y), "bend", TwoSeventyDegrees));

            for (--x; x > -3; --x)
            {
                path.Add(new MapElement(new Vector2(x, y), "path", NintyDegrees));
            }
        }
    }
}

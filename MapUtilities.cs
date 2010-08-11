using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TSAUMDHG
{
    /// <summary>
    /// This is a class of utilities for generating or manipulating map elements.
    /// </summary>
    public class MapUtilities : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public List<MapSprite> pathList;
        public List<MapSprite> mapList;
        public Dictionary<string, Texture2D> mapTextures = new Dictionary<string, Texture2D>();
        PathUtilities pathUtil;

        Random rand = new Random();
        int TileSize;
        const float NintyDegrees = (float)(Math.PI * 90 / 180.0);
        const float OneEightyDegrees = (float)(Math.PI);
        const float TwoSeventyDegrees = (float)(Math.PI * 270 / 180.0);

        Dictionary<int, float> rotationList;

        public MapUtilities(Game game, int tileSize)
            : base(game)
        {
            pathList = new List<MapSprite>();
            TileSize = tileSize;
            rotationList = new Dictionary<int, float>();
            rotationList.Add(0, 0.0f);
            rotationList.Add(1, NintyDegrees);
            rotationList.Add(2, OneEightyDegrees);
            rotationList.Add(3, TwoSeventyDegrees);
            pathUtil = new PathUtilities();
        }

        public void LoadMapTextures()
        {
            mapTextures.Add("path", Game.Content.Load<Texture2D>(@"images\path"));
            mapTextures.Add("grass", Game.Content.Load<Texture2D>(@"images\grass"));
            mapTextures.Add("bend", Game.Content.Load<Texture2D>(@"images\bend"));
        }

        public List<MapSprite> GenerateMap(List<MapSprite> pathList)
        {
            MapSprite currentPathSprite = null;
            mapList = new List<MapSprite>();

            for (int x = -100; x < ((Game1)Game).Window.ClientBounds.Width + 100; x += 50)
            {
                for (int y = -100; y < ((Game1)Game).Window.ClientBounds.Height + 100; y += 50)
                {
                    currentPathSprite = pathList.Find(
                        delegate(MapSprite path)
                        {
                            return (path.GetPosition.X == x && path.GetPosition.Y == y);
                        });
                    if (currentPathSprite != null)
                    {
                        mapList.Add(currentPathSprite);
                    }
                    else
                    {
                        mapList.Add(new MapSprite(
                            mapTextures["grass"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                            Vector2.Zero, 50.0f / mapTextures["grass"].Width, rotationList[rand.Next(0, 4)], Color.White));
                    }
                }
            }

            return mapList;
        }

        public List<MapSprite> GeneratePath()
        {
            List<MapElement> path = pathUtil.GeneratePath(rand.Next(0, 5), 
                new Point((((Game1)Game).Window.ClientBounds.Width / TileSize), (((Game1)Game).Window.ClientBounds.Height / TileSize)));
            pathList = new List<MapSprite>();

            for (int counter = 0; counter < path.Count; counter++)
            {
                pathList.Add(new MapSprite(
                            mapTextures[path[counter].tileType], 
                            new Vector2(path[counter].position.X * TileSize, path[counter].position.Y * TileSize), 
                            Point.Zero, new Point(0, 0), new Point(0, 0), Vector2.Zero, 
                            50.0f / mapTextures[path[counter].tileType].Width, path[counter].rotation, Color.White));
            }
/*
            int x = 0;
            int y = TileSize * 2;

            for (int counter = -2; counter < 23; counter++)
            {
                x = TileSize * counter;
                
                pathList.Add(new MapSprite(
                            mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                            Vector2.Zero, 50.0f / mapTextures["path"].Width, NintyDegrees, Color.White));
            }


            x += TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, OneEightyDegrees, Color.White));

            for (int counter = 3; counter < 7; counter++)
            {
                y = counter * TileSize;
                pathList.Add(new MapSprite(
                        mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["path"].Width, 0f, Color.White));
            }

            y += TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, TwoSeventyDegrees, Color.White));

            

            for (int counter = 22; counter > 3; counter--)
            {
                x = TileSize * counter;

                pathList.Add(new MapSprite(
                            mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                            Vector2.Zero, 50.0f / mapTextures["path"].Width, NintyDegrees, Color.White));
            }

            x -= TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, NintyDegrees, Color.White));

            for (int counter = 8; counter < 13; counter++)
            {
                y = counter * TileSize;
                pathList.Add(new MapSprite(
                        mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["path"].Width, 0f, Color.White));
            }

            y += TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, 0f, Color.White));

            for (int counter = 4; counter <= 8; counter++)
            {
                x = TileSize * counter;

                pathList.Add(new MapSprite(
                            mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                            Vector2.Zero, 50.0f / mapTextures["path"].Width, NintyDegrees, Color.White));
            }

            x += TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, TwoSeventyDegrees, Color.White));

            for (int counter = 12; counter > 10; counter--)
            {
                y = counter * TileSize;
                pathList.Add(new MapSprite(
                        mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["path"].Width, 0f, Color.White));
            }
            y -= TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, NintyDegrees, Color.White));

            for (int counter = 10; counter <= 15; counter++)
            {
                x = TileSize * counter;

                pathList.Add(new MapSprite(
                            mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                            Vector2.Zero, 50.0f / mapTextures["path"].Width, NintyDegrees, Color.White));
            }

            x += TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, OneEightyDegrees, Color.White));

            for (int counter = 11; counter < 13; counter++)
            {
                y = counter * TileSize;
                pathList.Add(new MapSprite(
                        mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["path"].Width, 0f, Color.White));
            }

            y += TileSize;

            pathList.Add(new MapSprite(
                        mapTextures["bend"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0),
                        Vector2.Zero, 50.0f / mapTextures["bend"].Width, 0f, Color.White));
            

            for (int counter = 17; counter <= (((Game1)Game).Window.ClientBounds.Width / TileSize) + 2; counter++)
            {
                x = TileSize * counter;

                pathList.Add(new MapSprite(
                            mapTextures["path"], new Vector2(x, y), Point.Zero, new Point(0, 0), new Point(0, 0), 
                            Vector2.Zero, 50.0f / mapTextures["path"].Width, NintyDegrees, Color.White));
            }
            */
            #region RandomGeneration
            /*
            Point start, end, current = new Point();
            
            bool buildingPath = true;
            bool northBoarder = false;
            bool southBoarder = false;
            bool eastBoarder = false;
            bool westBoarder = false;

            //Pick a side of the map to start from as well as a corner of that side.
            //Top: 1 Bottom: 2 Left: 3 Right: 4
            int startSide = rand.Next(1, 5);
            int endSide = rand.Next(1, 5);
            int startCorner = rand.Next(1, 3);
            int endCorner = rand.Next(1, 3);

            int weightPool = 100;
            int weightPoolMax = 100;
            int northWeight = 0;
            int southWeight = 0;
            int eastWeight = 0;
            int westWeight = 0;

            int southOutput = 0;
            int northOutput = 0;
            int eastOutput = 0;
            int westOutput = 0;

            int northTilesAway;
            int southTilesAway;
            int eastTilesAway;
            int westTilesAway;

            if (startSide == endSide && startCorner == endCorner)
            {
                endCorner = (endCorner % 2) + 1;
            }

            if (startSide <= 2)
            {
                if (startCorner > 1)
                {
                    start.X = 50 * rand.Next((((Game1)Game).Window.ClientBounds.Width - (((Game1)Game).Window.ClientBounds.Width / 4)) / 50,
                                             (((Game1)Game).Window.ClientBounds.Width / 50));
                }
                else
                {
                    start.X = 50 * rand.Next(1,
                                              ((((Game1)Game).Window.ClientBounds.Width / 4) / 50) + 1);
                }
                if (startSide == 1)
                {
                    start.Y = -2 * TileSize;

                    //Go south!
                    southWeight = weightPool;
                    weightPool -= southWeight;
                    northWeight = weightPool;
                    eastWeight = weightPool;
                    westWeight = weightPool;
                }
                else
                {
                    start.Y = ((Game1)Game).Window.ClientBounds.Height + (2 * TileSize);

                    //Go north!
                    northWeight = weightPool;
                    weightPool -= northWeight;
                    southWeight = weightPool;
                    eastWeight = weightPool;
                    westWeight = weightPool;
                }

            }
            else
            {
                if (startCorner > 1)
                {
                    start.Y = 50 * rand.Next((((Game1)Game).Window.ClientBounds.Height - (((Game1)Game).Window.ClientBounds.Height / 4)) / 50,
                                             (((Game1)Game).Window.ClientBounds.Height / 50));
                }
                else
                {
                    start.Y = 50 * rand.Next(1,
                                              ((((Game1)Game).Window.ClientBounds.Height / 4) / 50) + 1);
                }
                if (startSide == 3)
                {
                    start.X = -2 * TileSize;

                    //Go east!
                    eastWeight = weightPool;
                    weightPool -= eastWeight;
                    northWeight = weightPool;
                    southWeight = weightPool;
                    westWeight = weightPool;
                }
                else
                {
                    start.X = ((Game1)Game).Window.ClientBounds.Height + (2 * TileSize);

                    //Go west!
                    westWeight = weightPool;
                    weightPool -= westWeight;
                    northWeight = weightPool;
                    eastWeight = weightPool;
                    southWeight = weightPool;
                }
            }

            //Find the end point
            if (endSide <= 2)
            {
                if (endCorner > 1)
                {
                    end.X = 50 * rand.Next((((Game1)Game).Window.ClientBounds.Width - (((Game1)Game).Window.ClientBounds.Width / 4)) / 50,
                                             (((Game1)Game).Window.ClientBounds.Width / 50));
                }
                else
                {
                    end.X = 50 * rand.Next(1,
                                              ((((Game1)Game).Window.ClientBounds.Width / 4) / 50) + 1);
                }
                if (endSide == 1)
                {
                    end.Y = -2 * TileSize;
                }
                else
                {
                    end.Y = ((Game1)Game).Window.ClientBounds.Height + (2 * TileSize);
                }
            }
            else
            {
                if (endCorner > 1)
                {
                    end.Y = 50 * rand.Next((((Game1)Game).Window.ClientBounds.Height - (((Game1)Game).Window.ClientBounds.Height / 4)) / 50,
                                             (((Game1)Game).Window.ClientBounds.Height / 50));
                }
                else
                {
                    end.Y = 50 * rand.Next(1,
                                              ((((Game1)Game).Window.ClientBounds.Height / 4) / 50) + 1);
                }
                if (endSide == 3)
                {
                    end.X = -2 * TileSize;
                }
                else
                {
                    end.X = ((Game1)Game).Window.ClientBounds.Height + (2 * TileSize);
                }
            }

            current = start;

            pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y), new Point(50, 50), 10,
                                new Point(start.X, start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

            //double mapParimeter = 2 * (((Game1)Game).Window.ClientBounds.Width + ((Game1)Game).Window.ClientBounds.Height);


            while (buildingPath)
            {
                northBoarder = false;
                southBoarder = false;
                eastBoarder = false;
                westBoarder = false;

                northTilesAway = Int32.MaxValue;
                southTilesAway = Int32.MaxValue;
                eastTilesAway = Int32.MaxValue;
                westTilesAway = Int32.MaxValue;

                if (pathList.Count != 1)
                {
                    northOutput = northWeight * rand.Next(100);
                    southOutput = southWeight * rand.Next(100);
                    eastOutput = eastWeight * rand.Next(100);
                    westOutput = westWeight * rand.Next(100);

                    weightPool = weightPoolMax;

                    northWeight = weightPool / 5;
                    southWeight = weightPool / 5;
                    eastWeight = weightPool / 5;
                    westWeight = weightPool / 5;

                    weightPool -= northWeight;
                    weightPool -= southWeight;
                    weightPool -= eastWeight;
                    weightPool -= westWeight;
                }
                else
                {
                    northOutput = northWeight * rand.Next(1, 100);
                    southOutput = southWeight * rand.Next(1, 100);
                    eastOutput = eastWeight * rand.Next(1, 100);
                    westOutput = westWeight * rand.Next(1, 100);
                }
                //Do we go north?
                if (northOutput > southOutput && northOutput > eastOutput &&
                    northOutput > westOutput)
                {
                    current = GoNorth(current, end);
                }
                //Do we go south?
                else if (southOutput > northOutput && southOutput > eastOutput &&
                         southOutput > westOutput)
                {
                    current = GoSouth(current, end);
                }
                //Do we go east?
                else if (eastOutput > northOutput && eastOutput > southOutput &&
                         eastOutput > westOutput)
                {
                    current = GoEast(current, end);
                }
                //Must be we are going west!
                else
                {
                    current = GoWest(current, end);
                }

                if (current.Equals(end) ||
                    current.X < (TileSize * -2) || current.X > (((Game1)Game).Window.ClientBounds.Width + (TileSize * 2)) ||
                    current.Y < (TileSize * -2) || current.Y > (((Game1)Game).Window.ClientBounds.Height + (TileSize * 2)))
                {
                    buildingPath = false;
                    continue;
                }

                //Lets calculate some weights!
                foreach (MapSprite tile in pathList)
                {
                    if (tile.GetCurrentFrame.X == current.X && tile.GetCurrentFrame.Y < current.Y &&
                        northTilesAway > (current.Y - tile.GetCurrentFrame.Y) / TileSize && tile.GetCurrentFrame.Y > 0)
                    {
                        northTilesAway = (current.Y - tile.GetCurrentFrame.Y) / TileSize;
                    }

                    if (tile.GetCurrentFrame.X == current.X && tile.GetCurrentFrame.Y > current.Y &&
                        southTilesAway > (tile.GetCurrentFrame.Y - current.Y) / TileSize && tile.GetCurrentFrame.Y > 0)
                    {
                        southTilesAway = (tile.GetCurrentFrame.Y - current.Y) / TileSize;
                    }

                    if (tile.GetCurrentFrame.Y == current.Y && tile.GetCurrentFrame.X > current.X &&
                        eastTilesAway > (tile.GetCurrentFrame.X - current.X) / TileSize && tile.GetCurrentFrame.X > 0)
                    {
                        eastTilesAway = (tile.GetCurrentFrame.X - current.X) / 50;
                    }

                    if (tile.GetCurrentFrame.Y == current.Y && tile.GetCurrentFrame.X < current.X &&
                        westTilesAway > (current.X - tile.GetCurrentFrame.X) / TileSize && tile.GetCurrentFrame.X > 0)
                    {
                        westTilesAway = (current.X - tile.GetCurrentFrame.X) / 50;
                    }
                }

                northWeight = FindWeight(northTilesAway, weightPoolMax, end);
                southWeight = FindWeight(southTilesAway, weightPoolMax, end);
                eastWeight = FindWeight(eastTilesAway, weightPoolMax, end);
                westWeight = FindWeight(westTilesAway, weightPoolMax, end);

                weightPool = weightPoolMax;
                weightPool -= northWeight;
                weightPool -= southWeight;
                weightPool -= eastWeight;
                weightPool -= westWeight;

                if (northWeight > 0 && end.Y <= current.Y)
                {
                    northWeight += weightPool / 2;
                }
                if (southWeight > 0 && end.Y >= current.Y)
                {
                    southWeight += weightPool / 2;
                }
                if (eastWeight > 0 && end.X >= current.X)
                {
                    eastWeight += weightPool / 2;
                }
                if (westWeight > 0 && end.X <= current.X)
                {
                    westWeight += weightPool / 2;
                }

                weightPool -= weightPool;

                //If we're near the northern boarder then we cannot go that way, unless we're near our goal, that is!
                if (current.Y / TileSize <= 0)
                {
                    if (end.X != current.X)
                    {
                        weightPool += northWeight;
                        northWeight -= northWeight;
                        northBoarder = true;
                    }
                    else
                    {
                        northWeight += southWeight;
                        northWeight += eastWeight;
                        northWeight += westWeight;

                        southWeight -= southWeight;
                        eastWeight -= eastWeight;
                        westWeight -= westWeight;
                    }
                }
                if (current.Y >= (((Game1)Game).Window.ClientBounds.Height))
                {
                    if (end.X != current.X)
                    {
                        weightPool += southWeight;
                        southWeight -= southWeight;
                        southBoarder = true;
                    }
                    else
                    {
                        southWeight += northWeight;
                        southWeight += eastWeight;
                        southWeight += westWeight;

                        northWeight -= northWeight;
                        eastWeight -= eastWeight;
                        westWeight -= westWeight;
                    }
                }
                if (current.X >= (((Game1)Game).Window.ClientBounds.Width))
                {
                    if (end.X != current.X)
                    {
                        weightPool += eastWeight;
                        eastWeight -= eastWeight;
                        eastBoarder = true;
                    }
                    else
                    {
                        eastWeight += southWeight;
                        eastWeight += northWeight;
                        eastWeight += westWeight;

                        southWeight -= southWeight;
                        northWeight -= northWeight;
                        westWeight -= westWeight;
                    }
                }
                if (current.Y <= 0)
                {
                    if (end.X != current.X)
                    {
                        weightPool += westWeight;
                        westWeight -= westWeight;
                        westBoarder = true;
                    }
                    else
                    {
                        westWeight += southWeight;
                        westWeight += eastWeight;
                        westWeight += northWeight;

                        southWeight -= southWeight;
                        eastWeight -= eastWeight;
                        northWeight -= northWeight;
                    }
                }

                //If we are on a boarder and not heading toward the end point then we cannot go that way!
                if (northBoarder)
                {
                    if (end.X < current.X && westTilesAway == Int32.MaxValue)
                    {
                        westWeight += eastWeight;
                        eastWeight -= eastWeight;
                    }
                    else if (end.X > current.X && eastTilesAway == Int32.MaxValue)
                    {
                        eastWeight += westWeight;
                        westWeight -= westWeight;
                    }
                    else if (westTilesAway != Int32.MaxValue)
                    {
                        eastWeight += westWeight;
                        westWeight -= westWeight;
                    }
                    else
                    {
                        westWeight += eastWeight;
                        eastWeight -= eastWeight;
                    }
                }
                else if (southBoarder)
                {
                    if (end.X < current.X && westTilesAway == Int32.MaxValue)
                    {
                        westWeight += eastWeight;
                        eastWeight -= eastWeight;
                    }
                    else if (end.X > current.X && eastTilesAway == Int32.MaxValue)
                    {
                        eastWeight += westWeight;
                        westWeight -= westWeight;
                    }
                    else if (westTilesAway != Int32.MaxValue)
                    {
                        eastWeight += westWeight;
                        westWeight -= westWeight;
                    }
                    else
                    {
                        westWeight += eastWeight;
                        eastWeight -= eastWeight;
                    }
                }
                else if (eastBoarder)
                {
                    if (end.Y < current.Y && northTilesAway == Int32.MaxValue)
                    {
                        northWeight += southWeight;
                        southWeight -= southWeight;
                    }
                    else if (end.Y > current.Y && southTilesAway == Int32.MaxValue)
                    {
                        southWeight += northWeight;
                        northWeight -= northWeight;
                    }
                    else if (northTilesAway != Int32.MaxValue)
                    {
                        southWeight += northWeight;
                        northWeight -= northWeight;
                    }
                    else
                    {
                        northWeight += southWeight;
                        southWeight -= southWeight;
                    }
                }
                else if (westBoarder)
                {
                    if (end.Y < current.Y && northTilesAway == Int32.MaxValue)
                    {
                        northWeight += southWeight;
                        southWeight -= southWeight;
                    }
                    else if (end.Y > current.Y && southTilesAway == Int32.MaxValue)
                    {
                        southWeight += northWeight;
                        northWeight -= northWeight;
                    }
                    else if (northTilesAway != Int32.MaxValue)
                    {
                        southWeight += northWeight;
                        northWeight -= northWeight;
                    }
                    else
                    {
                        northWeight += southWeight;
                        southWeight -= southWeight;
                    }
                }

            }
            */
            #endregion

            return pathList;
        }

        private int FindWeight(int tilesAway, int weightPoolMax, Point endPoint)
        {
            int weight = 0;

            if (tilesAway <= 2)
            {
                weight = 0;
            }
            else
            {
                weight = weightPoolMax / 5;

                if (tilesAway >= 10)
                {
                    weight += (weightPoolMax / 5) / 5;
                }
                else
                {
                    weight -= (weightPoolMax / 5) / 5;
                }
            }

            return weight;
        }
/*
        private Point GoNorth(Point current, Point end)
        {
            int counter;
            int pathLength = 0;
            int closestTile = Int32.MaxValue;
            Point oldTile = current;

            if (current.Y <= 3 && current.X == end.X)
            {
                pathLength = current.Y - end.Y;
            }
            else if (current.Y <= 3 && Math.Abs(current.X - end.X) <= 4)
            {
                pathLength = current.Y;
            }
            else if (pathList.Count != 1)
            {
                foreach (MapSprite tile in pathList)
                {
                    if (tile.GetCurrentFrame.X == current.X && tile.GetCurrentFrame.Y < current.Y &&
                        closestTile > (current.Y - tile.GetCurrentFrame.Y))
                    {
                        closestTile = current.Y - tile.GetCurrentFrame.Y;
                    }
                }

                pathLength = rand.Next(1, 5);

                if (pathLength >= closestTile)
                {
                    pathLength = closestTile - 1;
                }
                current.Y -= TileSize;
            }
            else
            {
                pathLength = 3;
                current.Y -= TileSize;
            }

            for (counter = 0; counter < pathLength; counter++)
            {
                if (counter == 0 && oldTile.Y < current.Y)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X, current.Y + 1), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, TwoSeventyDegrees, new Vector2(75, 25),
                        Color.White));
                }
                else if (counter == 0 && oldTile.Y > current.Y)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X, current.Y), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25),
                        Color.White));
                }
                else
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\path"),
                        new Vector2(current.X, current.Y), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25),
                        Color.White));
                }

                current.Y -= TileSize;
            }

            return current;
        }

        private Point GoSouth(Point current, Point end)
        {
            int counter;
            int pathLength = 0;
            int closestTile = Int32.MaxValue;
            Point oldTile = current;

            if (current.Y <= (((Game1)Game).Window.ClientBounds.Height - 3) && current.X == end.X)
            {
                pathLength = end.Y - current.Y;
            }
            else if (current.Y <= (((Game1)Game).Window.ClientBounds.Height - 3) && Math.Abs(current.X - end.X) <= 4)
            {
                pathLength = current.Y;
            }
            else if (pathList.Count > 1)
            {
                foreach (MapSprite tile in pathList)
                {
                    if (tile.GetCurrentFrame.X == current.X && tile.GetCurrentFrame.Y > current.Y &&
                        closestTile > (tile.GetCurrentFrame.Y - current.Y))
                    {
                        closestTile = tile.GetCurrentFrame.Y - current.Y;
                    }
                }

                pathLength = rand.Next(1, 5);

                if (pathLength >= closestTile)
                {
                    pathLength = closestTile - 1;
                }

                current.Y += TileSize;
            }
            else
            {
                pathLength = 3;
                current.Y += TileSize;
            }

            for (counter = 0; counter < pathLength; counter++)
            {
                if (counter == 0 && oldTile.Y < current.Y)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y + 1), new Point(50, 50), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees * 2, new Vector2(75, 75),
                        Color.White));
                }
                else if (counter == 0 && oldTile.Y > current.Y)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y), new Point(50, 50), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75),
                        Color.White));
                }
                else
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\path"),
                        new Vector2(current.X, current.Y), new Point(50, 50), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25),
                        Color.White));
                }

                current.Y += TileSize;
            }

            return current;
        }

        private Point GoEast(Point current, Point end)
        {
            int counter;
            int pathLength = 0;
            int closestTile = Int32.MaxValue;
            Point oldTile = current;

            if (current.X <= (((Game1)Game).Window.ClientBounds.Width - 3) && current.Y == end.Y)
            {
                pathLength = end.Y - current.Y;
            }
            else if (current.X <= (((Game1)Game).Window.ClientBounds.Width - 3) && Math.Abs(current.Y - end.Y) <= 4)
            {
                pathLength = current.Y;
            }
            else if (pathList.Count > 1)
            {
                foreach (MapSprite tile in pathList)
                {
                    if (tile.GetCurrentFrame.Y == current.Y && tile.GetCurrentFrame.X > current.X &&
                        closestTile > (tile.GetCurrentFrame.X - current.X))
                    {
                        closestTile = tile.GetCurrentFrame.X - current.X;
                    }
                }

                pathLength = rand.Next(1, 5);

                if (pathLength >= closestTile)
                {
                    pathLength = closestTile - 1;
                }

                current.X += TileSize;
            }
            else
            {
                pathLength = 3;
                current.X += TileSize;
            }

            for (counter = 0; counter < pathLength; counter++)
            {
                if (counter == 0 && oldTile.X < current.X)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y), new Point(50, 50), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75),
                        Color.White));
                }
                else if (counter == 0 && oldTile.X > current.X)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y), new Point(50, 50), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75),
                        Color.White));
                }
                else
                {
                    pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\path"),
                            new Vector2(current.X + 1, current.Y), new Point(50, 50), 0,
                            new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), 
                            Color.White));
                }

                current.X += TileSize;
            }

            return current;
        }

        private Point GoWest(Point current, Point end)
        {
            int counter;
            int pathLength = 0;
            int closestTile = Int32.MaxValue;
            Point oldTile = current;

            if (current.X <= 3 && current.Y == end.Y)
            {
                pathLength = end.Y - current.Y;
            }
            else if (current.X <= 3 && Math.Abs(current.Y - end.Y) <= 4)
            {
                pathLength = current.Y;
            }
            else if (pathList.Count > 1)
            {
                foreach (MapSprite tile in pathList)
                {
                    if (tile.GetCurrentFrame.Y == current.Y && tile.GetCurrentFrame.X < current.X &&
                        closestTile > (current.X - tile.GetCurrentFrame.X))
                    {
                        closestTile = current.X - tile.GetCurrentFrame.X;
                    }
                }

                pathLength = rand.Next(1, 5);

                if (pathLength >= closestTile)
                {
                    pathLength = closestTile - 1;
                }

                current.X -= TileSize;
            }
            else
            {
                pathLength = 3;
                current.X -= TileSize;
            }

            for (counter = 0; counter < pathLength; counter++)
            {
                if (counter == 0 && oldTile.X < current.X)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X, current.Y + 1), new Point(50, 50), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, TwoSeventyDegrees, new Vector2(75, 25),
                        Color.White));
                }
                else if (counter == 0 && oldTile.X > current.X)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y + 1), new Point(50, 50), 0,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees * 2, new Vector2(75, 75),
                        Color.White));
                }
                else
                {
                    pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\path"),
                            new Vector2(current.X + 1, current.Y), new Point(50, 50), 0,
                            new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75),
                            Color.White));
                }

                current.X -= TileSize;
            }

            return current;
        }
*/
    }

}
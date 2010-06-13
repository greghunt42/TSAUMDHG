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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        UserControlledSprite player;
        UnitSprite unit;
        TowerSprite tower;
        List<UserControlledSprite> spriteList = new List<UserControlledSprite>();
        List<MapSprite> mapList = new List<MapSprite>();
        List<MapSprite> pathList = new List<MapSprite>();
        const float NintyDegrees = (float)(Math.PI * 90 / 180.0);
        const float OneEightyDegrees = (float)(Math.PI);
        const float TwoSeventyDegrees = (float)(Math.PI * 270 / 180.0);
        const int TileSize = 50;
        Point MinPoint = new Point(Int32.MinValue, Int32.MinValue);
        Random rand = new Random();

        //Spawning variables
        /*
        int enemySpawnMinMilliseconds = 1000;
        int enemySpawnMaxMilliseconds = 2000;
        int enemyMinSpeed = 2;
        int enemyMaxSpeed = 6;
        int nextSpawnTime = 0;
        int likelihoodAutomated = 75;
        int likelihoodChasing = 20;
        //This variable isn't used but is here for easy reference
        //indicating that evading sprites have a 5% chance of spawning
        //int likelihoodEvading = 5;
        int nextSpawnTimeChange = 5000;
        int timeSinceLastSpawnTimeChange = 0;
         */

        //Scoring
        /*
        int automatedSpritePointValue = 10;
        int chasingSpritePointValue = 20;
        int evadingSpritePointValue = 0;
         */

        //Lives
        //List<AutomatedSprite> livesList = new List<AutomatedSprite>();

        //Powerups
        //int powerUpExpiration = 0;

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            GeneratePath();
            /*
            pathList = new List<MapSprite>();
            // TODO: Add your initialization code here
            for (int x = -100; x < ((Game1)Game).Window.ClientBounds.Width + 100; x += 50)
            {
                for (int y = -100; y < ((Game1)Game).Window.ClientBounds.Height + 100; y += 50)
                {
                    if (((x / 50) == 3 && (y / 50) < 12) ||
                        ((x / 50) == 18 && (y / 50) < 12 && (y / 50) > 7) ||
                        ((x / 50) == 6 && (y / 50) < 7 && (y / 50) > 1) ||
                        ((x / 50) == 21 && (y / 50) > 1))
                    {
                        if (x == 150 && y == -100)
                        {
                            pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(x, y), new Point(50, 50), 10,
                                new Point(x, y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));
                        }
                        else
                        {
                            pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\path"),
                            new Vector2(x, y), new Point(50, 50), 10,
                            new Point(x, y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                        }
                    }
                    else if (((y / 50) == 12 && (x / 50) > 3 && (x / 50) < 18) ||
                             ((y / 50) == 7 && (x / 50) > 6 && (x / 50) < 18) ||
                             ((y / 50) == 1 && (x / 50) > 6 && (x / 50) < 21))
                    {
                        pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\path"),
                            new Vector2(x + 1, y), new Point(50, 50), 10,
                            new Point(x, y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), false));
                    }
                    else if (((x / 50) == 3 && (y / 50) == 12) ||
                             ((x / 50) == 6 && (y / 50) == 7))
                    {
                        pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\bend"),
                            new Vector2(x, y), new Point(50, 50), 10,
                            new Point(x, y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                    }
                    else if ((x / 50) == 18 && (y / 50) == 12)
                    {
                        pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\bend"),
                            new Vector2(x, y + 1), new Point(50, 50), 10,
                            new Point(x, y), new Point(1, 1), Vector2.Zero, 1, TwoSeventyDegrees, new Vector2(75, 25), false));
                    }
                    else if (((x / 50) == 18 && (y / 50) == 7) ||
                             ((x / 50) == 21 && (y / 50) == 1))
                    {
                        pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\bend"),
                            new Vector2(x + 1, y + 1), new Point(50, 50), 10,
                            new Point(x, y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees * 2, new Vector2(75, 75), false));
                    }
                    else if ((x / 50) == 6 && (y / 50) == 1)
                    {
                        pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\bend"),
                            new Vector2(x + 1, y), new Point(50, 50), 10,
                            new Point(x, y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), false));
                    }
                }
            }
            */

            base.Initialize();

            //ResetSpawnTime();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            MapSprite currentPathSprite = null;
            Point startTile = new Point(Int32.MinValue, Int32.MinValue);

            for (int x = -100; x < ((Game1)Game).Window.ClientBounds.Width + 100; x += 50)
            {
                for (int y = -100; y < ((Game1)Game).Window.ClientBounds.Height + 100; y += 50)
                {
                    currentPathSprite = pathList.Find(
                        delegate(MapSprite path)
                        {
                            return (path.GetCurrentFrame.X == x && path.GetCurrentFrame.Y == y);
                        });
                    if (currentPathSprite != null)
                    {
                        mapList.Add(currentPathSprite);

                        if (currentPathSprite.IsStart())
                        {
                            startTile = currentPathSprite.GetCurrentFrame;
                        }
                    }
                    else
                    {
                        mapList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\grass"),
                            new Vector2(x, y), new Point(50, 50), 10,
                            new Point(x, y), new Point(1, 1), Vector2.Zero, 0, 0f, new Vector2(25, 25), false));
                    }
                }
            }
            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Images/unit"),
                new Vector2(startTile.X, startTile.Y),
                new Point(177, 139), 10, new Point(0, 0),
                new Point(1, 2), new Vector2(6, 6), 120, new Vector2(52, 69));
            player.ModifyScale(0.35f);
            spriteList.Add(player);

            unit = new UnitSprite(
                Game.Content.Load<Texture2D>(@"Images/unit"),
                new Vector2(startTile.X, startTile.Y),
                new Point(177, 139), 10, new Point(0, 0),
                new Point(1, 2), new Vector2(6, 6), 120, new Vector2(52, 69), startTile);
            unit.ModifyScale(0.35f);

            /*tower = new TowerSprite(
                Game.Content.Load<Texture2D>(@"Images/tower"),
                new Vector2(50 * 4, 50 * 6),
                new Point(144, 144), 10, new Point(0, 0),
                new Point(9, 0), new Vector2(6, 6), 1200, 300f, 30f, 10, new Vector2(72, 72));
            tower.ModifyScale(0.4f);*/

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Time to spawn enemy?
            /*
            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (nextSpawnTime < 0)
            {
                SpawnEnemy();

                // Reset spawn timer
                ResetSpawnTime();
            }
            */
            UpdateSprites(gameTime);

            //AdjustSpawnTimes(gameTime);

            //CheckPowerUpExpiration(gameTime);

            base.Update(gameTime);
        }

        protected void UpdateSprites(GameTime gameTime)
        {
            // Update player
            player.Update(gameTime, Game.Window.ClientBounds, pathList);

            //tower.Update(gameTime, Game.Window.ClientBounds, spriteList);

            // Update all non-player sprites
            /*for (int i = 0; i < spriteList.Count; ++i)
            {
                Sprite s = spriteList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
            
                // Check for collisions
                if (s.collisionRect.Intersects(player.collisionRect))
                {
                    // Play collision sound
                    if (s.collisionCueName != null)
                        ((Game1)Game).PlayCue(s.collisionCueName);

                    //Remove a life from the player
                    if (s is AutomatedSprite)
                    {
                        if (livesList.Count > 0)
                        {
                            livesList.RemoveAt(livesList.Count - 1);
                            --((Game1)Game).NumberLivesRemaining;
                        }
                    }
                    else if (s.collisionCueName == "pluscollision")
                    {
                        // Collided with plus - start plus power-up
                        powerUpExpiration = 5000;
                        player.ModifyScale(2);
                    }
                    else if (s.collisionCueName == "skullcollision")
                    {
                        // Collided with skull - start skull power-up
                        powerUpExpiration = 5000;
                        player.ModifySpeed(.5f);
                    }
                    else if (s.collisionCueName == "boltcollision")
                    {
                        // Collided with bolt - start bolt power-up
                        powerUpExpiration = 5000;
                        player.ModifySpeed(2);
                    }


                    // Remove collided sprite from the game
                    spriteList.RemoveAt(i);
                    --i;
                }

                // Remove object if it is out of bounds
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    ((Game1)Game).AddScore(spriteList[i].scoreValue);
                    spriteList.RemoveAt(i);
                    --i;
                }
            }
            */
            //Update the lives left sprites
            //foreach (Sprite sprite in livesList)
            //sprite.Update(gameTime, Game.Window.ClientBounds);
        }

        public void GeneratePath()
        {
            pathList = new List<MapSprite>();
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
                if(current.Y / TileSize <= 0)
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
                if ( counter == 0 && oldTile.Y < current.Y)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X, current.Y + 1), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, TwoSeventyDegrees, new Vector2(75, 25), false));
                }
                else if ( counter == 0 && oldTile.Y > current.Y)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X, current.Y), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                }
                else
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\path"),
                        new Vector2(current.X, current.Y), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));                    
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
                        new Vector2(current.X + 1, current.Y + 1), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees * 2, new Vector2(75, 75), false));
                }
                else if (counter == 0 && oldTile.Y > current.Y)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), false));
                }
                else
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\path"),
                        new Vector2(current.X, current.Y), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
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
                        new Vector2(current.X + 1, current.Y), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), false));
                }
                else if (counter == 0 && oldTile.X > current.X)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), false));
                }
                else
                {
                    pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\path"),
                            new Vector2(current.X + 1, current.Y), new Point(50, 50), 10,
                            new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), false));
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
                        new Vector2(current.X, current.Y + 1), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, TwoSeventyDegrees, new Vector2(75, 25), false));
                }
                else if (counter == 0 && oldTile.X > current.X)
                {
                    pathList.Add(new MapSprite(
                        Game.Content.Load<Texture2D>(@"images\bend"),
                        new Vector2(current.X + 1, current.Y + 1), new Point(50, 50), 10,
                        new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees * 2, new Vector2(75, 75), false));
                }
                else
                {
                    pathList.Add(new MapSprite(
                            Game.Content.Load<Texture2D>(@"images\path"),
                            new Vector2(current.X + 1, current.Y), new Point(50, 50), 10,
                            new Point(current.X, current.Y), new Point(1, 1), Vector2.Zero, 1, NintyDegrees, new Vector2(25, 75), false));
                }

                current.X -= TileSize;
            }

            return current;
        }

        /*
        protected void CheckPowerUpExpiration(GameTime gameTime)
        {
            // Is a power-up active?
            if (powerUpExpiration > 0)
            {
                // Decrement power-up timer
                powerUpExpiration -=
                gameTime.ElapsedGameTime.Milliseconds;
                if (powerUpExpiration <= 0)
                {
                    // If power-up timer has expired, end all power-ups
                    powerUpExpiration = 0;
                    player.ResetScale();
                    player.ResetSpeed();
                }
            }
        }
*/
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                SpriteSortMode.Deferred, SaveStateMode.None);

            // Draw all sprites
            foreach (MapSprite m in mapList)
                m.Draw(gameTime, spriteBatch);

            //Draw the livesleft sprites
            //foreach (Sprite sprite in livesList)
            //    sprite.Draw(gameTime, spriteBatch);

            // Draw the player
            player.Draw(gameTime, spriteBatch);

            //tower.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        /*
                private void ResetSpawnTime()
                {
                    nextSpawnTime = ((Game1)Game).rnd.Next(
                    enemySpawnMinMilliseconds,
                    enemySpawnMaxMilliseconds);
                }

        */
        /*
        private void SpawnEnemy()
        {
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;

            // Default frame size
            Point frameSize = new Point(75, 75);

            // Randomly choose which side of the screen to place enemy,
            // then randomly create a position along that side of the screen
            // and randomly choose a speed for the enemy
            switch (((Game1)Game).rnd.Next(4))
            {
                case 0: // LEFT to RIGHT
                    position = new Vector2(
                        -frameSize.X, ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));
                    speed = new Vector2(((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed), 0);
                    break;
                case 1: // RIGHT to LEFT
                    position = new
                        Vector2(
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));

                    speed = new Vector2(-((Game1)Game).rnd.Next(
                        enemyMinSpeed, enemyMaxSpeed), 0);
                    break;
                case 2: // BOTTOM to TOP
                    position = new Vector2(((Game1)Game).rnd.Next(0,
                    Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X),
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight);

                    speed = new Vector2(0,
                        -((Game1)Game).rnd.Next(enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
                case 3: // TOP to BOTTOM
                    position = new Vector2(((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X), -frameSize.Y);
                    
                    speed = new Vector2(0,
                        ((Game1)Game).rnd.Next(enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
            }

            // Get random number between 0 and 99
            int random = ((Game1)Game).rnd.Next(100);
            if (random < likelihoodAutomated)
            {
                // Create an AutomatedSprite.
                // Get new random number to determine whether to
                // create a three-blade or four-blade sprite.
                if (((Game1)Game).rnd.Next(2) == 0)
                {
                    // Create a four-blade enemy
                    spriteList.Add(
                    new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"images\fourblades"),
                        position, new Point(75, 75), 10, new Point(0, 0),
                        new Point(6, 8), speed, "fourbladescollision", 
                        automatedSpritePointValue));
                }
                else
                {
                    // Create a three-blade enemy
                    spriteList.Add(
                    new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"images\threeblades"),
                        position, new Point(75, 75), 10, new Point(0, 0),
                        new Point(6, 8), speed, "threebladescollision", 
                        automatedSpritePointValue));
                }
            }
            else if (random < likelihoodAutomated +
            likelihoodChasing)
            {
                // Create a ChasingSprite.
                // Get new random number to determine whether
                // to create a skull or a plus sprite.
                if (((Game1)Game).rnd.Next(2) == 0)
                {
                    // Create a skull
                    spriteList.Add(
                    new ChasingSprite(
                        Game.Content.Load<Texture2D>(@"images\skullball"),
                        position, new Point(75, 75), 10, new Point(0, 0),
                        new Point(6, 8), speed, "skullcollision", this, 
                        chasingSpritePointValue));
                }
                else
                {
                    // Create a plus
                    spriteList.Add(
                    new ChasingSprite(
                        Game.Content.Load<Texture2D>(@"images\plus"),
                        position, new Point(75, 75), 10, new Point(0, 0),
                        new Point(6, 4), speed, "pluscollision", this, 
                        chasingSpritePointValue));
                }
            }
            else
            {
                // Create an EvadingSprite
                spriteList.Add(
                new EvadingSprite(
                    Game.Content.Load<Texture2D>(@"images\bolt"),
                    position, new Point(75, 75), 10, new Point(0, 0),
                    new Point(6, 8), speed, "boltcollision", this,
                    .75f, 150, evadingSpritePointValue));
            }
        }
        */
        public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }
        /*
                protected void AdjustSpawnTimes(GameTime gameTime)
                {
                    // If the spawn max time is > 500 milliseconds
                    // decrease the spawn time if it is time to do
                    // so based on the spawn-timer variables
                    if (enemySpawnMaxMilliseconds > 500)
                    {
                        timeSinceLastSpawnTimeChange += gameTime.ElapsedGameTime.Milliseconds;
                        if (timeSinceLastSpawnTimeChange > nextSpawnTimeChange)
                        {
                            timeSinceLastSpawnTimeChange -= nextSpawnTimeChange;
                            if (enemySpawnMaxMilliseconds > 1000)
                            {
                                enemySpawnMaxMilliseconds -= 100;
                                enemySpawnMinMilliseconds -= 100;
                            }
                            else
                            {
                                enemySpawnMaxMilliseconds -= 10;
                                enemySpawnMinMilliseconds -= 10;
                            }
                        }
                    }
                }
         */
    }
}
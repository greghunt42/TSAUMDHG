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
            Point start, end = new Point();
            
            bool buildingPath = true;
            //Pick a side of the map to start from as well as a corner of that side.
            Random rand = new Random();
            //Top: 1 Bottom: 2 Left: 3 Right: 4
            int startSide = rand.Next(1, 5);
            int endSide = rand.Next(1, 5);
            int startCorner = rand.Next(1, 3);
            int endCorner = rand.Next(1, 3);

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
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y), new Point(50, 50), 10,
                                new Point(start.X, start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));
                    
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y + TileSize), new Point(50, 50), 10,
                                new Point(start.X, start.Y + TileSize), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y + (2 * TileSize)), new Point(50, 50), 10,
                                new Point(start.X, start.Y + (2 * TileSize)), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                }
                else
                {
                    start.Y = ((Game1)Game).Window.ClientBounds.Height + (2* TileSize);
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y), new Point(50, 50), 10,
                                new Point(start.X, start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));
                    
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y - TileSize), new Point(50, 50), 10,
                                new Point(start.X, start.Y - TileSize), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y - (2 * TileSize)), new Point(50, 50), 10,
                                new Point(start.X, start.Y - (2 * TileSize)), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
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
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y), new Point(50, 50), 10,
                                new Point(start.X, start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));
                    
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X + TileSize, start.Y), new Point(50, 50), 10,
                                new Point(start.X + TileSize, start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X + (2 * TileSize), start.Y), new Point(50, 50), 10,
                                new Point(start.X + (2 * TileSize), start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                }
                else
                {
                    start.X = ((Game1)Game).Window.ClientBounds.Height + (2 * TileSize);
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X, start.Y), new Point(50, 50), 10,
                                new Point(start.X, start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));
                    
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X - TileSize, start.Y), new Point(50, 50), 10,
                                new Point(start.X - TileSize, start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(start.X - (2 * TileSize), start.Y), new Point(50, 50), 10,
                                new Point(start.X - (2 * TileSize), start.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
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
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y), new Point(50, 50), 10,
                                new Point(end.X, end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));
                    
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y + TileSize), new Point(50, 50), 10,
                                new Point(end.X, end.Y + TileSize), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y + (2 * TileSize)), new Point(50, 50), 10,
                                new Point(end.X, end.Y + (2 * TileSize)), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                }
                else
                {
                    end.Y = ((Game1)Game).Window.ClientBounds.Height + (2* TileSize);
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y), new Point(50, 50), 10,
                                new Point(end.X, end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));
                    
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y - TileSize), new Point(50, 50), 10,
                                new Point(end.X, end.Y - TileSize), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y - (2 * TileSize)), new Point(50, 50), 10,
                                new Point(end.X, end.Y - (2 * TileSize)), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
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
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y), new Point(50, 50), 10,
                                new Point(end.X, end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X + TileSize, end.Y), new Point(50, 50), 10,
                                new Point(end.X + TileSize, end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X + (2 * TileSize), end.Y), new Point(50, 50), 10,
                                new Point(end.X + (2 * TileSize), end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                }
                else
                {
                    end.X = ((Game1)Game).Window.ClientBounds.Height + (2 * TileSize);
                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X, end.Y), new Point(50, 50), 10,
                                new Point(end.X, end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), true));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X - TileSize, end.Y), new Point(50, 50), 10,
                                new Point(end.X - TileSize, end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));

                    pathList.Add(new MapSprite(
                                Game.Content.Load<Texture2D>(@"images\path"),
                                new Vector2(end.X - (2 * TileSize), end.Y), new Point(50, 50), 10,
                                new Point(end.X - (2 * TileSize), end.Y), new Point(1, 1), Vector2.Zero, 1, 0f, new Vector2(25, 25), false));
                }
            }

            //double mapParimeter = 2 * (((Game1)Game).Window.ClientBounds.Width + ((Game1)Game).Window.ClientBounds.Height);


            while (buildingPath && pathList.Count < 25)
            {

                if (true)
                {
                    buildingPath = false;
                }
            }
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
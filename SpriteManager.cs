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
        List<UnitSprite> spriteList = new List<UnitSprite>();
        List<MapSprite> mapList = new List<MapSprite>();
        List<MapSprite> pathList = new List<MapSprite>();
        List<UnitSprite> targetList = new List<UnitSprite>();
        List<ProjectileSprite> projectileList = new List<ProjectileSprite>();
        Dictionary<string, Texture2D> projectileTextureList = new Dictionary<string, Texture2D>();
        List<ProjectileSprite> removeProjectileList = new List<ProjectileSprite>();
        List<TowerSprite> towerList = new List<TowerSprite>();
        Dictionary<string, Texture2D> towerTextureList = new Dictionary<string, Texture2D>();
        Dictionary<string, SpriteFont> fontList = new Dictionary<string, SpriteFont>();
        Dictionary<string, Texture2D> unitTextureList = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> rangeTextureList = new Dictionary<string, Texture2D>();
        Dictionary<string, TextSprite> textList = new Dictionary<string, TextSprite>();
        List<TowerSprite> menuTowerList = new List<TowerSprite>();
        Dictionary<string, Texture2D> healthTextureList = new Dictionary<string, Texture2D>();
        BattleMenuSprite battleMenuSprite;
        BattleMenuSprite towerMenuSprite;
        Texture2D filler;

        const int TileSize = 50;
        Point MinPoint = new Point(Int32.MinValue, Int32.MinValue);
        Random rand = new Random();
        MapUtilities mapUtil;
        float unitRelease = 0.6f;
        float releaseTime = 0;
        int credits = 100;
        int unitsMissed = 0;
        int TextId = Int32.MinValue;

        SoundEffect acidHit;
        Song backgroundMusic;

        Effect desaturateEffect;

        HealthContainerSprite healthContainer;
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
            mapUtil = new MapUtilities(game, TileSize);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            MediaPlayer.Stop();
            //backgroundMusic = Game.Content.Load<Song>(@"music\bg");
            //MediaPlayer.Play(backgroundMusic);

            base.Initialize();

            //ResetSpawnTime();
        }

        protected override void LoadContent()
        {
            mapUtil.LoadMapTextures();
            pathList = mapUtil.GeneratePath();
            mapList = mapUtil.GenerateMap(pathList);
            acidHit = Game.Content.Load<SoundEffect>(@"audio\splat");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Point startTile = new Point(Int32.MinValue, Int32.MinValue);
            desaturateEffect = Game.Content.Load<Effect>(@"effects\desaturate");

            projectileTextureList.Add("acid_bomb_projectile", Game.Content.Load<Texture2D>(@"images\acid_bomb_projectile"));
            towerTextureList.Add("tower", Game.Content.Load<Texture2D>(@"images\tower"));
            towerTextureList.Add("tower_", Game.Content.Load<Texture2D>(@"images\tower_"));
            fontList.Add("arial", Game.Content.Load<SpriteFont>(@"font\arial"));
            unitTextureList.Add("unit", Game.Content.Load<Texture2D>(@"Images/unit"));
            rangeTextureList.Add("circle", Game.Content.Load<Texture2D>(@"images/rangeCircle"));
            healthTextureList.Add("container", Game.Content.Load<Texture2D>(@"images/healthContainer"));
            healthTextureList.Add("bar", Game.Content.Load<Texture2D>(@"images/menuBackground"));
            filler = Game.Content.Load<Texture2D>(@"images\menuBackground");

            menuTowerList.Add(new TowerSprite(towerTextureList["tower"], new Vector2(Game.Window.ClientBounds.Width * 0.2f, -75),
                            new Point(5, 6), new Point(0, 0), new Point(10, 0), new Vector2(6, 6), 300, 0.4f, 300f, 400f, 10,
                            Color.White, projectileTextureList["acid_bomb_projectile"], rangeTextureList["circle"], 35, 100));

            menuTowerList.Add(new TowerSprite(towerTextureList["tower_"], new Vector2(Game.Window.ClientBounds.Width * 0.5f, -75),
                            new Point(-2, -5), new Point(0, 0), new Point(10, 0), new Vector2(6, 6), 300, 0.3f, 75f, 1000f, 10,
                            Color.White, projectileTextureList["acid_bomb_projectile"], rangeTextureList["circle"], 100, 100));

            menuTowerList.Add(new TowerSprite(towerTextureList["tower"], new Vector2(Game.Window.ClientBounds.Width * 0.8f, -75),
                            new Point(23, 20), new Point(0, 0), new Point(10, 0), new Vector2(6, 6), 300, 0.65f, 600f, 200f, 10,
                            Color.Green, projectileTextureList["acid_bomb_projectile"], rangeTextureList["circle"], 15, 100));

            battleMenuSprite = new BattleMenuSprite(filler, new Vector2(0, -150),
                new Vector2(0, 0), new Point(Game.Window.ClientBounds.Width, 150), new Point(0, 0), new Point(0, 0),
                new Vector2(0, 5), new Point((Game.Window.ClientBounds.Width / filler.Width), 150 / filler.Height), 0f, new Color(0, 0, 0, 100), menuTowerList, 
                Game.Content.Load<Texture2D>(@"Images/cursor"), fontList["arial"]);

            towerMenuSprite = new BattleMenuSprite(filler, new Vector2(0, -150),
                new Vector2(0, 0), new Point(Game.Window.ClientBounds.Width, 150), new Point(0, 0), new Point(0, 0),
                new Vector2(0, 5), new Point((Game.Window.ClientBounds.Width / filler.Width), 150 / filler.Height), 0f, new Color(50, 50, 200, 100), menuTowerList,
                Game.Content.Load<Texture2D>(@"Images/cursor"), fontList["arial"]);

            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Images/cursor"),
                new Vector2(((Game1)Game).Window.ClientBounds.Width / 2, ((Game1)Game).Window.ClientBounds.Height / 2),
                new Point(0, 5), new Point(0, 0), new Point(0, 0), new Vector2(7, 7), 12, 1f, Color.White, 255);

            textList.Add("credits", new TextSprite(fontList["arial"], new Vector2(10, 10), new Vector2(0, 0), 0, "Credits: " + credits, Color.Beige));
            textList.Add("escaped", new TextSprite(fontList["arial"], new Vector2(Game.Window.ClientBounds.Width - 150, 10), new Vector2(0, 0), 0, "Escaped: " + unitsMissed, Color.Beige));

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

            BuildTower();

            //AdjustSpawnTimes(gameTime);

            //CheckPowerUpExpiration(gameTime);

            base.Update(gameTime);
        }

        public void BuildTower()
        {
            bool towerIntersect = false;
            bool mapIntersect = false;
            TowerSprite intersectingTower = null;

            foreach (TowerSprite t in towerList)
            {
                if (player.collisionRect.Intersects(t.collisionRect))
                {
                    towerIntersect = true;
                    intersectingTower = t;
                    break;
                }
            }

            foreach (MapSprite m in pathList)
            {
                if (player.collisionRect.Intersects(m.collisionRect))
                {
                    mapIntersect = true;
                    break;
                }
            }

            if (!towerIntersect && !mapIntersect && battleMenuSprite.activeSelection >= 0)
            {
                player.Activate();
                if (player.RequestTower() && (!battleMenuSprite.IsExtended() || !battleMenuSprite.IsScrolling()) &&
                    menuTowerList[battleMenuSprite.GetActiveSelection()].credits <= credits)
                {
                    TowerSprite tower = new TowerSprite(
                            menuTowerList[battleMenuSprite.GetActiveSelection()].textureImage,
                            new Vector2(player.GetPosition.X, player.GetPosition.Y),
                            menuTowerList[battleMenuSprite.GetActiveSelection()].GetCollisionOffset(),
                            menuTowerList[battleMenuSprite.GetActiveSelection()].GetCurrentFrame,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].GetSheetSize,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].GetSpeed(),
                            menuTowerList[battleMenuSprite.GetActiveSelection()].range,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].GetScale(),
                            menuTowerList[battleMenuSprite.GetActiveSelection()].reloadRate,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].numberOfTurrets,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].color,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].projectileTexture,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].rangeTexture,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].damage,
                            menuTowerList[battleMenuSprite.GetActiveSelection()].credits);

                    towerList.Add(tower);

                    credits -= 100;
                }
            }
            else if (!player.towerActive && towerIntersect && battleMenuSprite.activeSelection < 0)
            {
                player.Activate();

                if (player.RequestTower() && intersectingTower != null)
                {
                    player.TowerActiveate(intersectingTower.GetPosition, intersectingTower.frameSize);
                    towerMenuSprite.active = true;
                }
            }
            else if (player.towerActive && player.ReleaseTower())
            {
                player.TowerDeactivate();
                towerMenuSprite.active = false;
            }
            else if (player.towerActive)
            {
                player.Activate();
            }
            else
            {
                player.Deactivate();
            }
        }

        protected void UpdateSprites(GameTime gameTime)
        {
            double output = 0;
            Double.TryParse((gameTime.ElapsedGameTime.TotalSeconds).ToString(), out output);
            ProjectileSprite projectile;

            if (releaseTime > unitRelease)
            {
                float scale = (float)rand.NextDouble();

                scale = scale / 2;
                if (scale < 0.25f)
                {
                    scale = 0.25f;
                }

                int speed = (int)Math.Round(1.2 / scale);

                unit = new UnitSprite(
                    unitTextureList["unit"],
                    new Vector2(pathList[0].GetPosition.X, pathList[0].GetPosition.Y),
                    new Point((int)Math.Floor(177 * .35f), (int)Math.Floor(139 * .35f)), Point.Zero, new Point(0, 0),
                    new Point(1, 2), new Vector2(speed, speed), 120, new Vector2(177 / 2, 139 / 2),
                    new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble()),
                    scale, (int)Math.Round(400 * scale), (int)Math.Round(50 * scale), rand.Next(), new Point(0, (int)Math.Round((double)mapList[0].GetFrameSize.Y / 4) * -1),
                    healthTextureList);
                spriteList.Add(unit);

                releaseTime = 0f;
            }
            else
            {
                releaseTime += (float)output;
            }

            for (int counter = 0; counter < spriteList.Count; counter++)
            {
                spriteList[counter].Update(gameTime, Game.Window.ClientBounds, pathList);
                if (spriteList[counter].IsDone())
                {
                    unitsMissed += 1;
                    spriteList.Remove(spriteList[counter]);
                }
            }

            foreach (TowerSprite t in towerList)
            {
                projectile = t.Update(gameTime, Game.Window.ClientBounds, spriteList, pathList);

                if (projectile != null)
                {
                    projectileList.Add(projectile);
                }
            }
            foreach (ProjectileSprite p in projectileList)
            {
                p.Update(gameTime);
                if (p.GetPosition.Equals(p.target))
                {
                    if (p.GetTargetIndex() < spriteList.Count &&
                        p.GetTargetId() == spriteList[p.GetTargetIndex()].GetId())
                    {
                        spriteList[p.GetTargetIndex()].Hit(p.GetDamage());
                        if (spriteList[p.GetTargetIndex()].IsDead())
                        {
                            credits += spriteList[p.GetTargetIndex()].points;
                            TextId++;
                            textList.Add(TextId.ToString(), new TextSprite(fontList["arial"], spriteList[p.GetTargetIndex()].GetPosition, new Vector2(0, -2), 10, spriteList[p.GetTargetIndex()].points.ToString(), Color.White));
                            spriteList.Remove(spriteList[p.GetTargetIndex()]);
                        }
                    }

                    removeProjectileList.Add(p);
                }
            }

            foreach (ProjectileSprite p in removeProjectileList)
            {
                acidHit.Play();
                projectileList.Remove(p);
            }

            removeProjectileList.Clear();

            textList["credits"].SetText("Credits: " + credits);
            textList["escaped"].SetText("Escaped: " + unitsMissed);

            battleMenuSprite.Update(gameTime, Game.Window.ClientBounds);

            // Update player
            if (!battleMenuSprite.IsExtended() && !battleMenuSprite.IsScrolling())
            {
                if (battleMenuSprite.activeSelection >= 0)
                {
                    TowerSprite tower = battleMenuSprite.towerList[battleMenuSprite.activeSelection];
                    player.frameSize = tower.frameSize;
                    if (player.rangeSprite == null || !player.rangeSprite.IsSameRange(tower.range))
                    {
                        player.SetRange(tower.CloneTower());
                        player.collisionOffset = tower.collisionOffset;
                    }

                    player.Update(gameTime, Game.Window.ClientBounds, pathList);
                }
                else if (!player.towerActive)
                {
                    player.ResetFrameSize();
                    player.Update(gameTime, Game.Window.ClientBounds, pathList);
                    if (towerMenuSprite.IsExtended())
                    {
                        towerMenuSprite.active = true;
                    }
                    towerMenuSprite.Update(gameTime, Game.Window.ClientBounds);
                }
                else
                {
                    towerMenuSprite.Update(gameTime, Game.Window.ClientBounds);
                }

                
            }
            else if (battleMenuSprite.IsExtended() && !battleMenuSprite.IsScrolling() && battleMenuSprite.activeSelectionCursor.RequestTower())
            {
                battleMenuSprite.activeSelection = battleMenuSprite.selectedTower;
            }

            for (int counter = 0; counter < textList.Count; counter++)
            {
                textList.ElementAt(counter).Value.Update(gameTime, Game.Window.ClientBounds);

                if (textList.ElementAt(counter).Value.IsGone())
                {
                    textList.Remove(textList.ElementAt(counter).Key);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                SpriteSortMode.Immediate, SaveStateMode.None);

            // Draw all sprites
            foreach (MapSprite m in mapList)
                m.Draw(gameTime, spriteBatch);

            //Draw the livesleft sprites
            //foreach (Sprite sprite in livesList)
            //    sprite.Draw(gameTime, spriteBatch);

            foreach (UnitSprite u in spriteList)
                u.Draw(gameTime, spriteBatch);

            foreach (TowerSprite t in towerList)
                t.Draw(gameTime, spriteBatch);

            foreach (ProjectileSprite p in projectileList)
                p.Draw(gameTime, spriteBatch);

            for (int counter = 0; counter < textList.Count; counter++ )
                textList.ElementAt(counter).Value.Draw(spriteBatch);
            
            player.Draw(gameTime, spriteBatch);
            
            battleMenuSprite.Draw(gameTime, spriteBatch);

            towerMenuSprite.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                SpriteSortMode.Immediate, SaveStateMode.None);
            
            desaturateEffect.Begin();
            desaturateEffect.CurrentTechnique.Passes[0].Begin();

            player.DrawRangeTower(gameTime, spriteBatch);

            desaturateEffect.CurrentTechnique.Passes[0].End();
            desaturateEffect.End();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }
        
    }
}
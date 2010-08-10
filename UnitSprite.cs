using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class UnitSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        int timeSinceLastFrame = 0;
        int lastTile = 0;
        int maxHealth;
        int currentHealth;
        public int points { get; set; }
        bool isDone = false;
        int id;
        Point unitOffset;
        Dictionary<string, Texture2D> healthTextureList;
        HealthBarSprite healthBar;
        HealthContainerSprite healthContainer;
        int frameshift;
        bool isHit;


        public UnitSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, Vector2 origin, Color color, float scale, int maxHealth, int points,
            int id, Point unitOffset, Dictionary<string, Texture2D> healthTextureList)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, scale, 0f, origin, color)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            this.id = id;
            this.unitOffset = unitOffset;
            this.healthTextureList = healthTextureList;
            isHit = false;
            this.points = points;
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));

            frameshift = 1;

            float containerScale = (float)((float)frameSize.X / (float)((healthTextureList["container"].Width)));
            float barScale = (float)((float)frameSize.X / (float)((healthTextureList["bar"].Width)));

            healthContainer = new HealthContainerSprite(healthTextureList["container"],
                new Vector2(position.X, (position.Y + frameSize.Y) - healthTextureList["container"].Height), new Point(0, 0),
                new Point(0, 0), new Vector2(0, 0), 400, containerScale, new Color(255, 255, 255, 0));

            //healthContainer.SetScale((float)((float)frameSize.X / (float)((healthTextureList["container"].Width / (healthContainer.GetSheetSize.X + 1)))));
            
            //healthContainer.frameSize = new Point(base.frameSize.X, healthContainer.frameSize.Y - 4);
            healthBar = new HealthBarSprite(healthTextureList["bar"],
                new Vector2(position.X, (position.Y + frameSize.Y) - healthTextureList["container"].Height), new Point(0, 0),
                new Point(0, 0), new Vector2(0, 0), 400, barScale, new Color(0, 255, 0, 0));
            //healthBar.SetScale((float)((float)frameSize.X / (float)((healthTextureList["bar"].Width / (healthContainer.GetSheetSize.X + 1)))));
            //healthBar.frameSize = new Point(frameSize.X, healthContainer.frameSize.Y);
        }

        public int GetId()
        {
            return id;
        }

        public bool IsDone()
        {
            return isDone;
        }

        public void Hit(int damage)
        {
            currentHealth -= damage;
            isHit = true;
        }

        public bool IsDead()
        {
            if (currentHealth <= 0)
            {
                return true;
            }
            return false;
        }

        public override Vector2 GetDirection()
        {
            return direction;
        }

        public Vector2 UpdatePosition(List<MapSprite> pathList)
        {
            //MapSprite currentPathSprite = null;
            Vector2 newPosition = position;
            //float offset = 0.0f;

            if(lastTile + 1 < pathList.Count)
            {
                if (pathList[lastTile + 1].GetPosition.X > pathList[lastTile].GetPosition.X)
                {
                    newPosition.X += speed.X;

                    SetDirection(new Vector2(speed.X, 0));
                    if (newPosition.X > pathList[lastTile + 1].GetPosition.X)
                    {
                        lastTile = lastTile + 1;

                        if (lastTile == pathList.Count - 1 || pathList[lastTile + 1].GetPosition.X == pathList[lastTile].GetPosition.X)
                        {
                            newPosition.X = pathList[lastTile].GetPosition.X;
                            newPosition.Y = pathList[lastTile].GetPosition.Y;
                        }
                    }
                }
                else if (pathList[lastTile + 1].GetPosition.X < pathList[lastTile].GetPosition.X)
                {
                    newPosition.X -= speed.X;

                    SetDirection(new Vector2(-speed.X, 0));
                    if (newPosition.X <= pathList[lastTile + 1].GetPosition.X)
                    {
                        lastTile = lastTile + 1;

                        if (lastTile == pathList.Count - 1 || pathList[lastTile + 1].GetPosition.X == pathList[lastTile].GetPosition.X)
                        {
                            newPosition.X = pathList[lastTile].GetPosition.X;
                            newPosition.Y = pathList[lastTile].GetPosition.Y;
                        }
                    }

                }
                else if (pathList[lastTile + 1].GetPosition.Y > pathList[lastTile].GetPosition.Y)
                {
                    newPosition.Y += speed.Y;

                    SetDirection(new Vector2(0, speed.Y));
                    if (newPosition.Y > pathList[lastTile + 1].GetPosition.Y)
                    {
                        lastTile = lastTile + 1;

                        if (lastTile == pathList.Count - 1 || pathList[lastTile + 1].GetPosition.Y == pathList[lastTile].GetPosition.Y)
                        {
                            newPosition.X = pathList[lastTile].GetPosition.X;
                            newPosition.Y = pathList[lastTile].GetPosition.Y;
                        }
                    }

                }
                else if (pathList[lastTile + 1].GetPosition.Y < pathList[lastTile].GetPosition.Y)
                {
                    newPosition.Y -= speed.Y;

                    SetDirection(new Vector2(0, -speed.Y));
                    if (newPosition.Y <= pathList[lastTile + 1].GetPosition.Y)
                    {
                        lastTile = lastTile + 1;

                        if (lastTile == pathList.Count - 1 || pathList[lastTile + 1].GetPosition.Y == pathList[lastTile].GetPosition.Y)
                        {
                            newPosition.X = pathList[lastTile].GetPosition.X;
                            newPosition.Y = pathList[lastTile].GetPosition.Y;
                        }
                    }

                }

                if (lastTile == pathList.Count - 1)
                {
                    isDone = true;
                }
            }
            
            return newPosition;
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, List<MapSprite> pathList)
        {
            position = UpdatePosition(pathList);

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if((timeSinceLastFrame > base.GetMillisecondsPerFrame) &&
               (direction.X != 0 || direction.Y != 0))
            {
                timeSinceLastFrame = 0;
                if (Math.Abs(direction.X) > Math.Abs(direction.Y))
                {
                    SetSpriteEffect(SpriteEffects.None);
                    if (direction.X < 0)
                    {
                        SetSpriteEffect(SpriteEffects.FlipHorizontally);
                    }

                    currentFrame.X += frameshift;
                    currentFrame.Y = 2;
                    if ((currentFrame.X == sheetSize.X && frameshift > 0) || (currentFrame.X == 0 && frameshift < 0))
                    {
                        frameshift *= -1;
                    }
                }
                else
                {
                    if (direction.Y < 0)
                    {
                        currentFrame.Y = 0;
                    }
                    else
                    {
                        currentFrame.Y = 1;
                    }
                    currentFrame.X += frameshift;
                    if ((currentFrame.X == sheetSize.X && frameshift > 0) || (currentFrame.X == 0 && frameshift < 0))
                    {
                        frameshift *= -1;
                    }
                }
            }

            if (isHit)
            {
                int i = 3;
                i += 3;
            }

            healthContainer.Update(gameTime,
                new Vector2(position.X, ((position.Y + frameSize.Y) - healthTextureList["container"].Height)), isHit);
            healthBar.Update(gameTime,
                new Vector2(position.X, ((position.Y + frameSize.Y) - healthTextureList["container"].Height)), isHit,
                (float)((float)currentHealth / (float)maxHealth));

            if (isHit)
            {
                isHit = false;
            }
            //base.Update(gameTime, clientBounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            spriteBatch.Draw(base.textureImage,
                new Rectangle(((int)position.X + unitOffset.X), ((int)position.Y + unitOffset.Y), frameSize.X, frameSize.Y),
                new Rectangle((currentFrame.X) * (textureImage.Width / (base.GetSheetSize.X + 1)),
                    (currentFrame.Y) * (textureImage.Height / (base.GetSheetSize.Y + 1)),
                    (textureImage.Width / (base.GetSheetSize.X + 1)), (textureImage.Height / (base.GetSheetSize.Y + 1))),
                color, rotation, origin, GetSpriteEffect(), (1 / (position.Y + frameSize.Y)));

            healthContainer.Draw(gameTime, spriteBatch, unitOffset);
            healthBar.Draw(gameTime, spriteBatch, unitOffset);
        }
    }
}

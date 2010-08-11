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
    class TowerSprite : Sprite
    {
        public float range { get; set; }
        public float reloadRate { get; set; }
        public float reloadTimer = 0.0f;
        float turrentAngle;
        public int numberOfTurrets { get; set; }
        int timeSinceLastFrame = 0;
        UnitSprite targetSprite;
        public Texture2D projectileTexture { get; set; }
        public Texture2D rangeTexture { get; set; }
        public int damage { get; set; }
        public int credits { get; private set; }


        public TowerSprite(Texture2D textureImage, Vector2 position,
            Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, float range, float scale, float reloadRate, int numberOfTurrets,
            Color color, Texture2D projectileTexture, Texture2D rangeTexture, int damage, int credits)
            : base(textureImage, position, Point.Zero, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, scale, 0f, Vector2.Zero, color)
        {
            this.range = range;
            this.reloadRate = reloadRate;
            this.numberOfTurrets = numberOfTurrets;
            this.turrentAngle = 360 / numberOfTurrets;
            this.projectileTexture = projectileTexture;
            this.rangeTexture = rangeTexture;
            this.damage = damage;
            this.credits = credits;
            this.reloadTimer = 0.0f;
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
        }
        public TowerSprite(Texture2D textureImage, Vector2 position,
            Point collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, 
            int millisecondsPerFrame, float scale, float range, float reloadRate, int numberOfTurrets,
            Color color, Texture2D projectileTexture, Texture2D rangeTexture, int damage, int credits)
            : base(textureImage, position, Point.Zero, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, scale, 0f, Vector2.Zero, color)
        {
            this.range = range;
            this.reloadRate = reloadRate;
            this.numberOfTurrets = numberOfTurrets;
            this.turrentAngle = 360 / numberOfTurrets;
            this.projectileTexture = projectileTexture;
            this.rangeTexture = rangeTexture;
            this.damage = damage;
            this.credits = credits;
            this.reloadTimer = 0.0f;
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
        }

        public TowerSprite CloneTower()
        {
            return new TowerSprite(base.textureImage,base.position, GetCollisionOffset(), base.currentFrame, base.sheetSize, base.speed, base.millisecondsPerFrame, base.scale, range, 
                reloadRate, numberOfTurrets, base.color, projectileTexture, rangeTexture, damage, credits) ;
        }

        public Point GetCollisionOffset()
        {
            return base.collisionOffset;
        }

        public Vector2 GetSpeed()
        {
            return base.speed;
        }

        public override Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                (int)Math.Round(position.X),
                (int)Math.Round(position.Y),
                (int)Math.Round((double)(frameSize.X)),
                (int)Math.Round((double)frameSize.Y));
            }
        }

        public override Vector2 GetDirection()
        {
            //Return direction based on input from mouse and gamepad
            Vector2 inputDirection = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                inputDirection.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                inputDirection.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                inputDirection.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                inputDirection.Y += 1;

            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            if (gamepadState.ThumbSticks.Left.X != 0)
                inputDirection.X += gamepadState.ThumbSticks.Left.X;
            if (gamepadState.ThumbSticks.Left.Y != 0)
                inputDirection.Y += gamepadState.ThumbSticks.Left.Y * -1;
            return inputDirection * speed;
        }

        public ProjectileSprite Update(GameTime gameTime, Rectangle clientBounds, List<UnitSprite> spriteList, List<MapSprite> pathList)
        {
            targetSprite = null;
            ProjectileSprite projectile = null;
            int maxDistance = 0;
            float result;
            Vector2 spriteVector;
            Vector2 normalizedPosition = new Vector2(0, 0);
            int targetIndex = 0;
            //normalizedPosition.Normalize();

            //if(reloadRate % gameTime.
            if (reloadTimer <= 0.0f)
            {
                for (int index = 0; index < spriteList.Count; index++)
                {
                    result = Vector2.Distance(position, spriteList[index].GetPosition);

                    if (result <= range && spriteList[index].GetPosition.X > 0 && spriteList[index].GetPosition.Y > 0)
                    {
                        for (int counter = maxDistance; counter < pathList.Count; counter++)
                        {
                            if (!spriteList[index].IsDead() &&
                                pathList[counter].GetPosition.X == (spriteList[index].GetPosition.X - (spriteList[index].GetPosition.X % pathList[counter].GetFrameSize.X)) &&
                                pathList[counter].GetPosition.Y == (spriteList[index].GetPosition.Y - (spriteList[index].GetPosition.Y % pathList[counter].GetFrameSize.Y)))
                            {
                                maxDistance = counter;
                                targetSprite = spriteList[index];
                                targetIndex = index;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                reloadTimer -= gameTime.ElapsedGameTime.Milliseconds;
            }

            //Update animation frame
            if (targetSprite != null)
            {
                spriteVector = targetSprite.GetPosition;
                spriteVector.X += (targetSprite.GetFrameSize.X / 4);
                //spriteVector.Y += (targetSprite.GetFrameSize.Y / 4);
                normalizedPosition = position;
                //spriteVector.Normalize();
                normalizedPosition.Normalize();

                //float perpDot = normalizedPosition.X * spriteVector.Y - normalizedPosition.Y * spriteVector.X;
                //float dotproduct;// = Vector2.Dot(spriteVector, normalizedPosition);
                //double angle = (float)Math.Atan2(perpDot, Vector2.Dot(normalizedPosition, spriteVector));
                //-(float)Math.Atan2((normalizedPosition.X - spriteVector.X), (normalizedPosition.Y - spriteVector.Y));
                //double angle = Math.Acos((float)dotproduct);
                //position.Normalize();
                //v2.Normalize();
                //float dotproduct = Vector2.Dot(spriteVector, position);
                //double angle = Math.Acos((float)dotproduct);
                //float angle = -(float)Math.Atan2((position.X - spriteVector.X), (position.Y - spriteVector.Y));
                float angle = GetDegreeToTarget(position.X + ((frameSize.X) / 8),
                                                position.Y,
                                                spriteVector.X,
                                                spriteVector.Y);

                if (angle < 0)
                {
                    angle = 360 + angle;
                }

                
                
                timeSinceLastFrame = 0;

                result = angle / turrentAngle;
                currentFrame.X = (int)Math.Round(result + 1);
                //++currentFrame.X;
                if (currentFrame.X >= base.GetSheetSize.X)
                {
                    currentFrame.X = 0;
                }

                projectile = new ProjectileSprite(projectileTexture,
                    new Vector2(this.position.X + ((frameSize.X) / 8), this.position.Y),
                    new Point(0, 0), new Point(0, 0), Point.Zero, new Point(4, 0), new Vector2(20, 20),
                    Color.White, spriteVector, 50, position, targetIndex, angle, 1f, damage, targetSprite.GetId());

                //if (reloadTimer > 0)
                //{
                //    currentFrame.X = 0;
                //}

                reloadTimer = reloadRate;
            }
            else if (reloadTimer > (reloadRate - 100))
            {
                currentFrame.X = 0;
            }

            return projectile;
            //base.Update(gameTime, clientBounds);
        }

        public void SetPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch, (1 / (position.Y + frameSize.Y)));
        }
    }

}

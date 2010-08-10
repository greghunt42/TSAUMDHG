using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class UserControlledSprite: Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        public int timeSinceLastFrame { get; set; }
        public int colorFade { get; set; }
        public int maxFade { get; private set; }
        private int maxAlpha;
        public RangeSprite rangeSprite { get; set; }
        //public TowerSprite 
        public bool active { get; private set; }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, Color color, int maxFade)
            : base(textureImage, position, Point.Zero, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, 1f, 0f, Vector2.Zero, color)
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
            rangeSprite = null;
            active = false;

            this.maxFade = maxFade;
            this.colorFade = -8;

            timeSinceLastFrame = 0;
        }
        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, float scale, Color color, int maxFade)
            : base(textureImage, position, Point.Zero, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, scale, 0f, Vector2.Zero, color)
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
            rangeSprite = null;
            base.SetLayerDepth(0.0f);
            active = false;

            this.maxFade = maxFade;
            this.colorFade = -8;

            timeSinceLastFrame = 0;
        }

        public override Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                (int)Math.Round(position.X + (collisionOffset.X * -1)),
                (int)Math.Round(position.Y + (frameSize.Y / 2) + (collisionOffset.Y * -1)),
                (int)Math.Round((double)(frameSize.X)),
                (int)Math.Round((double)(frameSize.Y / 2)));
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }

        public void Activate()
        {
            active = true;
            color = Color.Lime;

            if (rangeSprite != null)
            {
                rangeSprite.color = Color.Lime;
            }
        }

        public void Deactivate()
        {
            active = false;
            color = Color.DarkGray;

            if (rangeSprite != null)
            {
                rangeSprite.color = Color.DarkGray;
            }
        }
        public void ResetFrameSize()
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            ResetRangeSprite();
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
                inputDirection.Y += gamepadState.ThumbSticks.Left.Y*-1;
            return inputDirection * speed;
        }

        public bool RequestMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightTrigger))
            {
                return true;
            }
            return false;
        }

        public bool RequestTower()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                return true;
            }
            return false;
        }

        public Vector2 UpdatePosition(List<MapSprite> pathList)
        {
            MapSprite currentPathSprite = null;
            Vector2 newPosition = position;
            float offset = 0.0f;

            if (direction.X < 0 && position.Y % pathList[0].GetFrameSize.Y == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    offset = ((position.X + direction.X) - (pathList[index].GetPosition.X /*+ pathList[index].GetFrameSize.X*/));

                    if (((position.X - (position.X % pathList[index].GetFrameSize.X)) - pathList[index].GetFrameSize.X) == pathList[index].GetPosition.X &&
                          (position.Y - (position.Y % pathList[index].GetFrameSize.Y)) == pathList[index].GetPosition.Y)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (position.Y % pathList[0].GetFrameSize.Y == 0)
                {

                }
                if (currentPathSprite != null)
                {
                    newPosition.X += direction.X;
                }
                else
                {
                    offset = position.X % pathList[0].GetFrameSize.X;

                    if (offset > Math.Abs(direction.X))
                    {
                        newPosition.X += direction.X;
                    }
                    else
                    {
                        newPosition.X -= (position.X % pathList[0].GetFrameSize.X);
                    }

                }
            }

            if (direction.X > 0 && position.Y % pathList[0].GetFrameSize.Y == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    offset = ((position.X + pathList[index].GetFrameSize.X + direction.X) - (pathList[index].GetPosition.X /*+ pathList[index].GetFrameSize.X*/));

                    if (((position.X - (position.X % pathList[index].GetFrameSize.X)) + pathList[index].GetFrameSize.X) == pathList[index].GetPosition.X &&
                          (position.Y - (position.Y % pathList[index].GetFrameSize.Y)) == pathList[index].GetPosition.Y)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (currentPathSprite != null)
                {
                    newPosition.X += direction.X;
                }
                else
                {
                    offset = position.X % pathList[0].GetFrameSize.X;

                    if (offset > Math.Abs(direction.X))
                    {
                        newPosition.X += direction.X;
                    }
                    else
                    {
                        newPosition.X -= (position.X % pathList[0].GetFrameSize.X);
                    }

                }
            }

            if (direction.Y < 0 && position.X % pathList[0].GetFrameSize.X == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    if (((position.Y - (position.Y % pathList[index].GetFrameSize.Y)) - pathList[index].GetFrameSize.Y) == pathList[index].GetPosition.Y &&
                          (position.X - (position.X % pathList[index].GetFrameSize.X)) == pathList[index].GetPosition.X)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (currentPathSprite != null)
                {
                    newPosition.Y += direction.Y;
                }
                else
                {
                    offset = position.Y % pathList[0].GetFrameSize.Y;

                    if (offset > Math.Abs(direction.Y))
                    {
                        newPosition.Y += direction.Y;
                    }
                    else
                    {
                        newPosition.Y -= (position.Y % pathList[0].GetFrameSize.Y);
                    }

                }
            }

            if (direction.Y > 0 && position.X % pathList[0].GetFrameSize.X == 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    if (((position.Y - (position.Y % pathList[index].GetFrameSize.Y)) + pathList[index].GetFrameSize.Y) == pathList[index].GetPosition.Y &&
                          (position.X - (position.X % pathList[index].GetFrameSize.X)) == pathList[index].GetPosition.X)
                    {
                        currentPathSprite = pathList[index];
                    }
                }

                if (currentPathSprite != null)
                {
                    newPosition.Y += direction.Y;
                }
                else
                {
                    offset = (position.Y) % pathList[0].GetFrameSize.Y;



                    if (offset > Math.Abs(direction.Y))
                    {
                        newPosition.Y += direction.Y;
                    }
                    else
                    {
                        newPosition.Y -= offset;
                    }

                }
            }

            return newPosition;
        }

        public void UpdateFade(GameTime gameTime)
        {
            if (maxFade != 255)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    if (color.R + colorFade > 255)
                    {
                        colorFade *= -1;
                        color.R = (byte)(255);
                        color.G = (byte)(255);
                        color.B = (byte)(255);
                    }

                    if (color.R <= maxFade)
                    {
                        colorFade *= -1;
                        color.R = (byte)(maxFade);
                        color.G = (byte)(maxFade);
                        color.B = (byte)(maxFade);
                    }
                    color.R += (byte)(colorFade);
                    color.G += (byte)(colorFade);
                    color.B += (byte)(colorFade);

                    timeSinceLastFrame = 0;
                }
            }
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, List<MapSprite> pathList)
        {
            //position = UpdatePosition(pathList);
            Vector2 direction = GetDirection();

            position += direction;

            if(direction.X <= 0 && position.X - (frameSize.X / 2) <= 0)
            {
                position.X = frameSize.X / 2;
            }
            else if(direction.X > 0 && (position.X + (frameSize.X / 2)) > clientBounds.Width)
            {
                position.X = clientBounds.Width - (frameSize.X / 2);
            }

            if (direction.Y <= 0 && position.Y - (frameSize.Y / 2) <= 0)
            {
                position.Y = (frameSize.Y / 2);
            }
            else if (direction.Y > 0 && (position.Y + (frameSize.Y / 2)) > clientBounds.Height)
            {
                position.Y = clientBounds.Height - (frameSize.Y / 2);
            }

            if (rangeSprite != null)
            {
                rangeSprite.Update(gameTime, clientBounds, position);
            }

        }

        public void ResetRangeSprite()
        {
            rangeSprite = null;
        }

        public void SetRangeSprite(Texture2D rangeTexture, float range)
        {
            maxAlpha = 200;
            rangeSprite = new RangeSprite(rangeTexture, this.position, new Point(0, 0), new Point(0, 0),
                new Vector2(0, 0), 0, 1f, new Color((byte)0,(byte)255,(byte)0,(byte)maxAlpha), 2);
            rangeSprite.SetScale(range / ((rangeTexture.Height / (rangeSprite.GetSheetSize.X + 1)) / 2));
            rangeSprite.frameSize = new Point((int)Math.Round(rangeSprite.frameSize.X * rangeSprite.GetScale()),
                                              (int)Math.Round(rangeSprite.frameSize.Y * rangeSprite.GetScale()));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            if (rangeSprite != null)
            {
                rangeSprite.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);

        }

    }
}

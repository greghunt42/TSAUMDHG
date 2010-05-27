using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class TowerSprite : Sprite
    {
        float range;
        float reloadRate;
        float reloadTimer = 0.0f;
        float turrentAngle;
        int numberOfTurrets;
        int timeSinceLastFrame = 0;
        UserControlledSprite closestSprite;

        public TowerSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, float range, float reloadRate, int numberOfTurrets, Vector2 origin)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, 0f, origin)
        {
            this.range = range;
            this.reloadRate = reloadRate;
            this.numberOfTurrets = numberOfTurrets;
            this.turrentAngle = 360 / numberOfTurrets;
        }
        public TowerSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, float range, float reloadRate, int numberOfTurrets, Vector2 origin)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, 0f, origin)
        {
            this.range = range;
            this.reloadRate = reloadRate;
            this.numberOfTurrets = numberOfTurrets;
            this.turrentAngle = 360 / numberOfTurrets;
        }

        public override Vector2 direction
        {
            get
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
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, List<UserControlledSprite> spriteList)
        {
            closestSprite = null;
            float closestDistance = float.MaxValue;
            float result;
            Vector2 spriteVector;
            Vector2 normalizedPosition = new Vector2(0, 0);
            //normalizedPosition.Normalize();
            foreach(UserControlledSprite sprite in spriteList)
            {
                result = Vector2.Distance(position, sprite.GetPosition);

                if (result <= range &&
                    result <= closestDistance)
                {
                    closestDistance = result;
                    closestSprite = sprite;
                }
            }

            //Update animation frame
            if (closestSprite != null)
            {
                spriteVector = closestSprite.GetPosition;
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
                float angle = GetDegreeToTarget(position.X, position.Y, spriteVector.X, spriteVector.Y);

                if (angle < 0)
                {
                    angle = 360 + angle;
                }

                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > base.GetMillisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;

                    result = angle / turrentAngle;
                    currentFrame.X = (int)Math.Round(result);
                    //++currentFrame.X;
                    if (currentFrame.X >= base.GetSheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                }
            }
            else
            {
                currentFrame.X = 0;
            }

            //base.Update(gameTime, clientBounds);
        }

        public void UpdateReload()
        {

        }
    }
}

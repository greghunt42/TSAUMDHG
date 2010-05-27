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
        int timeSinceLastFrame = 0;

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, Vector2 origin)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, 0f, origin)
        {
        }
        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, Vector2 origin)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, 0f, origin)
        {
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
                    inputDirection.Y += gamepadState.ThumbSticks.Left.Y*-1;
                return inputDirection * speed;
            }
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, List<MapSprite> pathList)
        {
            MapSprite currentPathSprite = null;
            Vector2 newPosition = position;
            float offset = 0.0f;
            bool pathFound = false;

            //Remove all 
            
            /*Point north = pathList.Find(
                delegate(Point point)
                {
                    /*
                    if (((Math.Floor(position.X / 50) * 50)) == (xPosFloor * 50) && (yPosFloor * 50) == ((Math.Floor(position.Y / 50) * 50)))
                    {
                        return point.X == (xPosFloor * 50) && point.Y == ((yPosFloor - 1) * 50);
                    }
                     */
                    //return point.X == (xPosFloor * 50) && point.Y == (yPosFloor * 50);
                    //return (point.X >= (xPosFloor * 50) && point.X <= (xPosCeiling * 50)) && point.Y == ((yPosFloor - 1) * 50);
                //});

            /*Point south = pathList.Find(
                delegate(Point point)
                {
                    /*
                    if (((Math.Floor(position.X / 50) * 50)) == (xPosFloor * 50) && (yPosCeiling * 50) == ((Math.Floor(position.Y / 50) * 50) ))
                    {
                        return point.X == (xPosFloor * 50) && point.Y == ((yPosCeiling + 1) * 50);
                    }
                     */
                    //return (point.X >= (xPosFloor * 50) && point.X <= (xPosCeiling * 50)) && point.Y == ((yPosFloor + 1) * 50);
                //});

            /*Point east = pathList.Find(
                delegate(Point point)
                {
                    /*
                    if (((Math.Floor(position.X / 50) * 50)) == (xPosCeiling * 50) && (yPosFloor * 50) == ((Math.Floor(position.Y / 50) * 50)))
                    {
                        return point.X == ((xPosCeiling + 1) * 50) && point.Y == (yPosFloor * 50);
                    }
                     */
                    //return point.X == ((xPosFloor + 1) * 50) && (point.Y >= (yPosFloor * 50) && point.Y <= (yPosCeiling * 50));
                //});

            /*Point west = pathList.Find(
                delegate(Point point)
                {
                    /*
                    if (((Math.Floor(position.X / 50) * 50)) == (xPosFloor * 50) && (yPosFloor * 50) == ((Math.Floor(position.Y / 50) * 50)))
                    {
                        return point.X == ((xPosFloor - 1) * 50) && point.Y == (yPosFloor * 50);
                    }
                     */
                    //return point.X == ((xPosFloor - 1) * 50) && point.Y == (yPosFloor * 50);
                //});
            

            // Move the sprite according to the direction property
            //
            //Commented out mouse support to force user to use keyboard
            //
            //// If the mouse moved, set the position of the sprite to the mouse position
            //MouseState currMouseState = Mouse.GetState();

            //if (currMouseState.X != prevMouseState.X ||
            //currMouseState.Y != prevMouseState.Y)
            //{
            //    position = new Vector2(currMouseState.X, currMouseState.Y);
            //}
            //prevMouseState = currMouseState;

            // If the sprite is off the screen, put it back in play
            /*
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.X > clientBounds.Width - frameSize.X)
            {
                position.X = clientBounds.Width - frameSize.X;
            }
            if (position.Y > clientBounds.Height - frameSize.Y)
            {
                position.Y = clientBounds.Height - frameSize.Y;
            }
             */
            /*if (north.Equals(new Point(0, 0)) && position.Y < (yPosCeiling) * 50)
            {
                position.Y = ((yPosCeiling) * 50);
            }
            if (south.Equals(new Point(0, 0)) && position.Y > (yPosFloor) * 50)
            {
                position.Y = ((yPosFloor) * 50);
            }
            if (east.Equals(new Point(0, 0)) && position.X > (xPosCeiling) * 50)
            {
                position.X = ((xPosCeiling) * 50);
            }
            if (west.Equals(new Point(0, 0)) && position.X < (xPosFloor) * 50)
            {
                position.X = ((xPosCeiling) * 50);
            }*/

            if (direction.X != 0.0f)
            {
                int i = 3;
                i += 3;
            }

            if (direction.X < 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    offset = ((position.X + direction.X) - (pathList[index].GetCurrentFrame.X /*+ pathList[index].GetFrameSize.X*/));

                    if (((position.X - (position.X % pathList[index].GetFrameSize.X)) - pathList[index].GetFrameSize.X) == pathList[index].GetCurrentFrame.X &&
                          (position.Y - (position.Y % pathList[index].GetFrameSize.Y)) == pathList[index].GetCurrentFrame.Y)
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

            if (direction.X > 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    offset = ((position.X + pathList[index].GetFrameSize.X + direction.X) - (pathList[index].GetCurrentFrame.X /*+ pathList[index].GetFrameSize.X*/));
                    
                    if (((position.X - (position.X % pathList[index].GetFrameSize.X)) + pathList[index].GetFrameSize.X) == pathList[index].GetCurrentFrame.X &&
                          (position.Y - (position.Y % pathList[index].GetFrameSize.Y)) == pathList[index].GetCurrentFrame.Y)
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
                        newPosition.X += (position.X % pathList[0].GetFrameSize.X);
                    }

                }
            }

            if (direction.Y < 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    if (((position.Y - (position.Y % pathList[index].GetFrameSize.Y)) - pathList[index].GetFrameSize.Y) == pathList[index].GetCurrentFrame.Y &&
                          (position.X - (position.X % pathList[index].GetFrameSize.X)) == pathList[index].GetCurrentFrame.X)
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

            if (direction.Y > 0)
            {
                currentPathSprite = null;
                for (int index = 0; index < pathList.Count; index++)
                {
                    if (((position.Y - (position.Y % pathList[index].GetFrameSize.Y)) + pathList[index].GetFrameSize.Y) == pathList[index].GetCurrentFrame.Y &&
                          (position.X - (position.X % pathList[index].GetFrameSize.X)) == pathList[index].GetCurrentFrame.X)
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
                        newPosition.Y += (position.Y % pathList[0].GetFrameSize.Y);
                    }
                    
                }
            }

            position = newPosition;

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

                    currentFrame.X++;
                    currentFrame.Y = 2;
                    if (currentFrame.X > GetSheetSize.X)
                    {
                        currentFrame.X = 0;
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
                    currentFrame.X++;
                    if(currentFrame.X > GetSheetSize.X)
                    {
                        currentFrame.X = 0;
                    }
                }
            }
            //base.Update(gameTime, clientBounds);
        }
    }
}

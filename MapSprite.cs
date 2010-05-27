using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class MapSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;

        public MapSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, float rotation, Vector2 origin)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, rotation, origin)
        {
        }
        public MapSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, float rotation, Vector2 origin)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, rotation, origin)
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
                    inputDirection.Y += gamepadState.ThumbSticks.Left.Y;
                return inputDirection * speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move the sprite according to the direction property
            //position += direction;

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
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;
             */
            base.Update(gameTime, clientBounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            spriteBatch.Draw(base.textureImage,
                new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y),
                new Rectangle(0, 0,textureImage.Height, textureImage.Width),
                Color.White, rotation, origin, SpriteEffects.None, 0);
        }
    }
}

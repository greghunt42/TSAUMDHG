using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    public class MapSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;

        public MapSprite(Texture2D textureImage, Vector2 position,
            Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, float rotation, Color color)
            : base(textureImage, position, Point.Zero, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, 1f, rotation, Vector2.Zero, color)
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
        }

        public MapSprite(Texture2D textureImage, Vector2 position,
            Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, float scale, float rotation, Color color)
            : base(textureImage, position, Point.Zero, collisionOffset, currentFrame,
            sheetSize, speed, 0, null, 0, scale, rotation, Vector2.Zero, color)
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
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
                inputDirection.Y += gamepadState.ThumbSticks.Left.Y;
            return inputDirection * speed;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            spriteBatch.Draw(textureImage,
                new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y),
                new Rectangle((currentFrame.X) * (textureImage.Width / (GetSheetSize.X + 1)),
                    (currentFrame.Y) * (textureImage.Height / (GetSheetSize.Y + 1)),
                    (textureImage.Width / (sheetSize.X + 1)), (textureImage.Height / (sheetSize.Y + 1))),
                color, rotation, origin, SpriteEffects.None, 1);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class HealthBarSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        public int timeSinceLastFrame { get; set; }
        byte alphaShift;
        float maxHealthSize;
        Point origFrameSize;

        public HealthBarSprite(Texture2D textureImage, Vector2 position,
            Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame, float scale,
            Color color)
            : base(textureImage, position, Point.Zero, Point.Zero, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, scale, 0f, Vector2.Zero, color)
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((double)(textureImage.Height / (base.sheetSize.Y + 1)) + 3));
            //base.origin = new Vector2((((float)textureImage.Width / (float)(base.sheetSize.X + 1) / 2)), ((float)((float)textureImage.Height / (float)(base.sheetSize.Y + 1) / 2)));
            timeSinceLastFrame = 0;
            alphaShift = 0;
            maxHealthSize = frameSize.X;
            origFrameSize = new Point(frameSize.X, frameSize.Y);
        }

        public override Vector2 GetDirection()
        {
            return direction;
        }

        public void Update(GameTime gameTime, Vector2 position, bool hit, float healthPercentage)
        {
            this.position = position;

            if (hit)
            {
                alphaShift = 20;
                timeSinceLastFrame = 0;
            }

            if (alphaShift > 0)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                if (timeSinceLastFrame >= base.millisecondsPerFrame)
                {
                    if ((int)color.A - (int)alphaShift <= 0)
                    {
                        color.A = 0;
                        alphaShift = 0;
                    }
                    else
                    {
                        color.A -= alphaShift;
                    }
                }
                else if ((int)color.A + (int)alphaShift > 255)
                {
                    color.A = 255;
                }
                else
                {
                    color.A += alphaShift;
                }
            }
            int oldFrameSizeX = base.frameSize.X;
            base.frameSize = new Point((int)Math.Round((maxHealthSize * healthPercentage)), frameSize.Y);
            color.G = (byte)Math.Round(255 * healthPercentage);
            color.R = (byte)(255 - color.G);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Point unitOffset)
        {
            //Draw the sprite
            spriteBatch.Draw(base.textureImage,
                new Rectangle(((int)position.X + (unitOffset.X * 2)) - (int)Math.Floor((double)(origFrameSize.X / 2)) - 1, 
                    ((int)position.Y + (unitOffset.Y * 2) - (int)Math.Floor((double)(origFrameSize.Y / 2))), frameSize.X, frameSize.Y),
                new Rectangle((currentFrame.X) * (textureImage.Width / (base.GetSheetSize.X + 1)),
                    (currentFrame.Y) * (textureImage.Height / (base.GetSheetSize.Y + 1)),
                    (textureImage.Width / (base.GetSheetSize.X + 1)), (textureImage.Height / (base.GetSheetSize.Y + 1))),
                color, rotation, origin, GetSpriteEffect(), 0.0021f);
        }
    }
}

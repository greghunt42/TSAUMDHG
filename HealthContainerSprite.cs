using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class HealthContainerSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        public int timeSinceLastFrame { get; set; }
        byte alphaShift;

        public HealthContainerSprite(Texture2D textureImage, Vector2 position,
            Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame, float scale,
            Color color)
            : base(textureImage, position, Point.Zero, Point.Zero, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, scale, 0f, Vector2.Zero, color)
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((double)(textureImage.Height / (base.sheetSize.Y + 1)) - 6));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
            timeSinceLastFrame = 0;
            alphaShift = 0;
        }

        public override Vector2 GetDirection()
        {
            return direction;
        }

        public void Update(GameTime gameTime, Vector2 position, bool hit)
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
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Point unitOffset)
        {
            //Draw the sprite
            spriteBatch.Draw(base.textureImage,
                new Rectangle(((int)position.X + (unitOffset.X * 2)), ((int)position.Y + (unitOffset.Y * 2)), frameSize.X, frameSize.Y),
                new Rectangle((currentFrame.X) * (textureImage.Width / (base.GetSheetSize.X + 1)),
                    (currentFrame.Y) * (textureImage.Height / (base.GetSheetSize.Y + 1)),
                    (int)Math.Round(((float)textureImage.Width / (float)(base.GetSheetSize.X + 1))), (textureImage.Height / (base.GetSheetSize.Y + 1))),
                color, rotation, origin, GetSpriteEffect(), 0.0022f);
        }

    }
}

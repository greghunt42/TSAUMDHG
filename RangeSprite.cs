using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class RangeSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        public int timeSinceLastFrame { get; set; }
        public int alphaFade { get; set; }
        public int maxFade { get; private set; }
        private int maxAlpha;

        public RangeSprite(Texture2D textureImage, Vector2 position,
            Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame,
            float scale, Color color, int alphaFade)
            : base(textureImage, position, Point.Zero, Point.Zero, currentFrame,
            sheetSize, speed, millisecondsPerFrame, null, 0, scale, 0f, Vector2.Zero, color)
        {
            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));
            timeSinceLastFrame = 0;
            this.alphaFade = alphaFade;
            this.maxFade = 80;
            this.maxAlpha = color.A;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public bool IsSameRange(float range)
        {
            return frameSize.X == range * 2;
        }

        public override Vector2 GetDirection()
        {
            return direction;
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, Vector2 position)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                if (color.A + alphaFade > maxAlpha)
                {
                    alphaFade *= -1;
                    color.A = (byte)maxAlpha;
                }

                if (color.A <= maxAlpha - maxFade)
                {
                    alphaFade *= -1;
                    color.A = (byte)(maxAlpha - maxFade);
                }
                color.A += (byte)(alphaFade);
                timeSinceLastFrame = 0;
            }

            SetPosition(position);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            base.Draw(gameTime, spriteBatch, 0.9901f);
        }
    }
}

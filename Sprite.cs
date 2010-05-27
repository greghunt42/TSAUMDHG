using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TSAUMDHG
{
    abstract class Sprite
    {
        public Texture2D textureImage { get; private set; }
        protected Vector2 position;
        protected Point frameSize;
        int collisionOffset;
        protected Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 16;
        protected float rotation;
        protected Vector2 origin;
        SpriteEffects effect = SpriteEffects.None;
        
        //Speed
        protected Vector2 speed;
        Vector2 originalSpeed;

        //Scale
        protected float scale = 1;
        protected float originalScale = 1;
        
        //Audio cue name for collisions
        public string collisionCueName { get; private set; }

        //Scoring
        public int scoreValue { get; protected set; }


        public abstract Vector2 direction
        {
            get;
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                (int)(position.X + (collisionOffset * scale)),
                (int)(position.Y + (collisionOffset * scale)),
                (int)((frameSize.X - (collisionOffset * 2)) * scale),
                (int)((frameSize.Y - (collisionOffset * 2)) * scale));
            }
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            string collisionCueName, int scoreValue, float scale, float rotation, Vector2 origin)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame, collisionCueName,
            scoreValue, rotation, origin)
        {
            this.scale = scale;
        }
        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            string collisionCueName, int scoreValue, float rotation, Vector2 origin)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame, collisionCueName,
            scoreValue, rotation, origin)
        {
        }
        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, string collisionCueName, int scoreValue, float rotation, Vector2 origin)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            originalSpeed = speed;
            this.collisionCueName = collisionCueName;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.scoreValue = scoreValue;
            this.rotation = rotation;
            this.origin = origin;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Update animation frame
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, rotation, origin,
                scale, effect, 0);
        }

        public bool IsOutOfBounds(Rectangle clientRect)
        {
            if (position.X < -frameSize.X ||
            position.X > clientRect.Width ||
            position.Y < -frameSize.Y ||
            position.Y > clientRect.Height)
            {
                return true;
            }
            return false;
        }

        public Vector2 GetPosition
        {
            get { return position; }
        }

        public void SetSpriteEffect(SpriteEffects effect)
        {
            this.effect = effect;
        }

        public Point GetCurrentFrame
        {
            get { return currentFrame; }
        }

        public Point GetSheetSize
        {
            get { return sheetSize; }
        }

        public int GetMillisecondsPerFrame
        {
            get { return millisecondsPerFrame; }
        }

        public Point GetFrameSize
        {
            get { return frameSize; }
        }

        public void ModifyScale(float modifier)
        {
            scale *= modifier;
        }

        public void ResetScale()
        {
            scale = originalScale;
        }

        public void ModifySpeed(float modifier)
        {
            speed *= modifier;
        }

        public void ResetSpeed()
        {
            speed = originalSpeed;
        }

        public float GetDegreeToTarget(float xpos1, float ypos1, float xpos2, float ypos2)
        {
            float value;

            value = (float)Math.Atan2(ypos2 - ypos1, xpos2 - xpos1);
            return (float)(value * (180 / Math.PI) + 90);
        }

    }
}

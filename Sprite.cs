using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TSAUMDHG
{
    public abstract class Sprite
    {
        public Texture2D textureImage { get; private set; }
        protected Vector2 position;

        public Point collisionOffset { get; set; }
        protected Point currentFrame;
        int timeSinceLastFrame = 0;
        public int millisecondsPerFrame { get; set; }
        const int defaultMillisecondsPerFrame = 16;
        protected float rotation;
        protected Vector2 direction;
        SpriteEffects effect = SpriteEffects.None;
        public Color color = new Color();
        private float layerDepth = 1.0f;
        
        //Speed
        protected Vector2 speed;
        Vector2 originalSpeed;

        //Scale
        protected float scale = 1;
        protected float originalScale = 1;

        public void SetLayerDepth(float layerDepth)
        {
            this.layerDepth = layerDepth;
        }

        public Point frameSize { get; set; }

        public Point sheetSize { get; set; }

        public Vector2 origin { get; set; }

        //Audio cue name for collisions
        public string collisionCueName { get; private set; }

        //Scoring
        public int scoreValue { get; protected set; }

        public virtual Rectangle collisionRect
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

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            Point collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            string collisionCueName, int scoreValue, float scale, float rotation, Vector2 origin,
            Color color)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame, collisionCueName,
            scoreValue, scale, rotation, origin, color)
        {
            this.scale = scale;
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            Point collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, string collisionCueName, int scoreValue, float scale, float rotation,
            Vector2 origin, Color color)
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
            this.color = color;
            this.scale = scale;
            SetDirection(Vector2.Zero);
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
                new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y),
                new Rectangle((currentFrame.X) * (textureImage.Width / (GetSheetSize.X + 1)),
                    (currentFrame.Y) * (textureImage.Height / (GetSheetSize.Y + 1)),
                    (textureImage.Width / (sheetSize.X + 1)), (textureImage.Height / (sheetSize.Y + 1))),
                color, rotation, origin, SpriteEffects.None, (float)Math.Abs(1 / (position.Y + frameSize.Y)));
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            //Draw the sprite
            spriteBatch.Draw(textureImage,
                new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y),
                new Rectangle((currentFrame.X) * (textureImage.Width / (GetSheetSize.X + 1)),
                    (currentFrame.Y) * (textureImage.Height / (GetSheetSize.Y + 1)),
                    (textureImage.Width / (sheetSize.X + 1)), (textureImage.Height / (sheetSize.Y + 1))),
                color, rotation, origin, SpriteEffects.None, layerDepth);
        }

/*
            spriteBatch.Draw(base.textureImage,
                new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y),
                new Rectangle(0, 0, textureImage.Height, textureImage.Width),
                color, rotation, origin, SpriteEffects.None, 0);
            */
            /*spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                color, rotation, origin,
                scale, effect, 0);*/
        //}

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

        public SpriteEffects GetSpriteEffect()
        {
            return effect;
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

        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        public virtual Vector2 GetDirection()
        {
            return direction;
        }

        public void ModifyScale(float modifier)
        {
            scale *= modifier;
        }

        public void ResetScale()
        {
            scale = originalScale;
        }

        public float GetScale()
        {
            return scale;
        }

        public void SetScale(float scale)
        {
            this.scale = scale;
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
            value = (float)(value * (180 / Math.PI) + 90);

            //if (value < 0)
            //{
            //    value = 360 + value;
            //}

            return value;
        }

    }
}

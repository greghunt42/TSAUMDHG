using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class ProjectileSprite : Sprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        int timeSinceLastFrame = 0;
        public Vector2 target;
        public Vector2 tower;
        Matrix rotMatrix;
        float angle;
        int targetIndex;
        int targetId;
        int damage;
        int frameshift;


        public ProjectileSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, Color color, Vector2 target, int animationSpeed, Vector2 tower,
            int targetIndex, float angle, float scale, int damage, int targetId)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, null, 0, scale, 0f, Vector2.Zero, color)
        {
            this.target = target;
            this.tower = tower;
            this.angle = angle;
            this.targetIndex = targetIndex;
            this.damage = damage;
            this.targetId = targetId;
            this.millisecondsPerFrame = animationSpeed;

            base.frameSize = new Point((int)Math.Round((textureImage.Width / (base.sheetSize.X + 1)) * scale), (int)Math.Round((textureImage.Height / (base.sheetSize.Y + 1)) * scale));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));

            if (angle < 0)
            {
                angle = 360 + angle;    
            }

            frameshift = 1;

            rotMatrix = Matrix.CreateRotationZ(angle);
            rotation = (float)((angle + 90) * Math.PI) / 180;
            SetDirection();
        }

        public int GetTargetId()
        {
            return targetId;
        }

        public Vector2 GetTarget()
        {
            return target;
        }

        public int GetTargetIndex()
        {
            return targetIndex;
        }

        public int GetDamage()
        {
            return damage;
        }

        public void SetDirection()
        {
            direction.X = (float)(Math.Cos(((angle - 90) * Math.PI) / 180));
            direction.Y = (float)(Math.Sin(((angle - 90) * Math.PI) / 180));
            //direction.X = (float)(speed.X * ((1 - (angle * angle)) / ((1 + (angle * angle)))));
            //direction.Y = (float)(speed.Y *((2 * angle) / (1 + (angle * angle))));

            //direction = Vector2.Transform(new Vector2(0, -1), rotMatrix);
            direction.X *= speed.X;
            direction.Y *= speed.Y;
        }

        public override Vector2 GetDirection()
        {
            return direction;
        }

        public bool Update(GameTime gameTime)
        {
            bool remove = false;
            Vector2 newPosition = position;
            newPosition += direction;            
            
            if (direction.X < 0 && newPosition.X < target.X)
            {
                newPosition.X = target.X;
                remove = true;
            }
            else if (direction.X > 0 && newPosition.X > target.X)
            {
                newPosition.X = target.X;
                remove = true;
            }

            if (direction.Y < 0 && newPosition.Y < target.Y)
            {
                newPosition.Y = target.Y;
                remove = true;
            }
            else if (direction.Y > 0 && newPosition.Y > target.Y)
            {
                newPosition.Y = target.Y;
                remove = true;
            }
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > base.millisecondsPerFrame)
            {
                currentFrame.X += frameshift;
                timeSinceLastFrame = 0;
            }

            if ((currentFrame.X == sheetSize.X && frameshift > 0) || (currentFrame.X == 0 && frameshift < 0))
            {
                frameshift *= -1;
            }
            
            position = newPosition;

            return remove;
        }

    }
}

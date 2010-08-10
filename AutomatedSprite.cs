using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TSAUMDHG
{
    class AutomatedSprite : Sprite
    {
        public AutomatedSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, string collisionCueName,
            int scoreValue, Vector2 origin, Color color)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, collisionCueName, scoreValue, 0f, 0f, origin, color)
        {
        }
        public AutomatedSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, Point collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, string collisionCueName, int scoreValue, float scale, Vector2 origin,
            Color color)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, collisionCueName, scoreValue, scale, 0f, origin, color)
        {
        }

        public override Vector2 GetDirection()
        {
            return speed;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;
            base.Update(gameTime, clientBounds);
        }
    }
}

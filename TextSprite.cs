using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class TextSprite
    {
        //Mouse movement
        //MouseState prevMouseState;
        SpriteFont font;
        Vector2 position;
        Vector2 speed;
        int fadeAmount;
        Color color;
        string text;
        int shiftRemaining;


        public TextSprite(SpriteFont font, Vector2 position, Vector2 speed, int fadeAmount,
            string text, Color color)
        {
            this.font = font;
            this.position = position;
            this.speed = speed;
            this.fadeAmount = fadeAmount;
            this.color = color;
            this.text = text;

        }

        public void SetText(string text)
        {
            this.text = text;
        }

        public string GetText()
        {
            return text;
        }

        public bool IsGone()
        {
            if (color.A == 0)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (color.A - fadeAmount < 0)
            {
                color.A = 0;
            }
            else
            {
                color.A -= (byte)fadeAmount;
                position += speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.002f);
        }
    }
}

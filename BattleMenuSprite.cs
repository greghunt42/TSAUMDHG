using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TSAUMDHG
{
    class BattleMenuSprite : Sprite
    {
        private Vector2 currentSpeed;
        private Vector2 scrollPosition;
        private Vector2 startPosition;
        private Vector2 endPosition;
        public List<TowerSprite> towerList { get; set; }
        public int activeSelection { get; set; }
        public int selectedTower { get; set; }
        public UserControlledSprite activeSelectionCursor { get; set; }
        private bool scrolling;
        private bool extended;
        int timeSinceLastFrame = 300;
        new Point scale;
        private SpriteFont font;
        public bool active { get; set; }

        public BattleMenuSprite(Texture2D textureImage, Vector2 position,
            Vector2 endPosition, Point frameSize, Point currentFrame, Point sheetSize,
            Vector2 speed, Point scale, float rotation, Color color, List<TowerSprite> towerList, Texture2D cursor,
            SpriteFont font)
            : base(textureImage, position, frameSize, Point.Zero, currentFrame,
            sheetSize, speed, null, 0, 1f, rotation, Vector2.Zero, color)
        {
            this.currentSpeed = speed;
            this.scrolling = false;
            this.endPosition = endPosition;
            this.scrollPosition = endPosition;
            this.startPosition = position;
            this.towerList = towerList;
            this.activeSelection = -1;
            this.selectedTower = -1;
            this.extended = false;
            this.scale = scale;
            this.font = font;
            this.active = false;

            base.frameSize = new Point((int)Math.Round((double)(textureImage.Width / (base.sheetSize.X + 1)) * this.scale.X), (int)Math.Round((double)(textureImage.Height / (base.sheetSize.Y + 1)) * this.scale.Y));
            base.origin = new Vector2(((textureImage.Width / (base.sheetSize.X + 1) / 2)), ((textureImage.Height / (base.sheetSize.Y + 1) / 2)));

            for (int counter = 0; counter < towerList.Count; counter++)
            {
                if (counter == 0)
                {
                    towerList[counter].SetPosition(new Vector2(this.position.X + towerList[counter].frameSize.X + 200, this.position.Y + (this.frameSize.Y / 2)));
                }
                else
                {
                    towerList[counter].SetPosition(new Vector2(towerList[counter - 1].GetPosition.X + 400, towerList[counter -1].GetPosition.Y));
                }
            }

            activeSelectionCursor = new UserControlledSprite(cursor, new Vector2(Int32.MinValue, Int32.MinValue),
                Point.Zero, new Point(0, 0), new Point(0, 0), Vector2.Zero, Color.White, 100);
        }

        public int GetActiveSelection()
        {
            return activeSelection;
        }

        public bool IsScrolling()
        {
            return scrolling;
        }

        public bool IsExtended()
        {
            return extended;
        }

        public bool GetRequestMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightTrigger))
            {
                return true;
            }
            else if (active)
            {
                return true;
            }
            return false;
        }

        public bool CursorLeft()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                return true;
            }
            return false;
        }

        public bool CursorRight()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickRight))
            {
                return true;
            }
            return false;
        }

        public bool ResetSelection()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.RightShift) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.B))
            {
                return true;
            }
            return false;
        }

        public bool SelectTower()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (GetRequestMenu())
            {
                scrolling = true;
                active = false;
                if(!IsExtended())
                {
                    selectedTower = activeSelection;
                }
            }

            if (scrolling)
            {
                position += currentSpeed;

                if (currentSpeed.X > 0 && (startPosition.X <= position.X))
                {
                    position = startPosition;
                    startPosition = endPosition;
                    endPosition = startPosition;
                    scrolling = false;
                    extended = !extended;
                }
                else if (currentSpeed.Y > 0 && (startPosition.Y >= position.Y))
                {
                    position = startPosition;
                    startPosition = endPosition;
                    endPosition = startPosition;
                    scrolling = false;
                    extended = !extended;
                }
                else if (currentSpeed.X < 0 && (startPosition.X >= position.X))
                {
                    position = startPosition;
                    startPosition = endPosition;
                    endPosition = startPosition;
                    scrolling = false;
                    extended = !extended;
                }
                else if (currentSpeed.Y < 0 && (startPosition.Y <= position.Y))
                {
                    position = startPosition;
                    startPosition = endPosition;
                    endPosition = startPosition;
                    scrolling = false;
                    extended = !extended;
                }
                else if (currentSpeed.X > 0 && (endPosition.X >= position.X))
                {
                    position = endPosition;
                    endPosition = startPosition;
                    startPosition = position;
                    scrolling = false;
                    extended = !extended;
                }
                else if (currentSpeed.Y > 0 && (endPosition.Y <= position.Y))
                {
                    position = endPosition;
                    endPosition = startPosition;
                    startPosition = position;
                    scrolling = false;
                    extended = !extended;
                }
                else if (currentSpeed.X < 0 && (endPosition.X >= position.X))
                {
                    position = endPosition;
                    endPosition = startPosition;
                    startPosition = position;
                    scrolling = false;
                    extended = !extended;
                }
                else if (currentSpeed.Y < 0 && (endPosition.Y >= position.Y))
                {
                    position = endPosition;
                    endPosition = startPosition;
                    startPosition = position;
                    scrolling = false;
                    extended = !extended;
                }

                for (int counter = 0; counter < towerList.Count; counter ++)
                {
                    towerList[counter].SetPosition(towerList[counter].GetPosition + currentSpeed);
                    if (counter == selectedTower)
                    {
                        activeSelectionCursor.SetPosition(towerList[selectedTower].GetPosition + currentSpeed);
                    }
                }

                if (!scrolling)
                {
                    currentSpeed = currentSpeed * -1;
                }
            }

            timeSinceLastFrame -= (int)gameTime.ElapsedGameTime.Milliseconds;

            if (IsExtended() && selectedTower < 0)
            {
                selectedTower = 0;
                activeSelectionCursor.SetPosition(towerList[selectedTower].GetPosition);
                activeSelectionCursor.frameSize = towerList[selectedTower].GetFrameSize;
            }

            if (timeSinceLastFrame <= 0 && !scrolling && extended && CursorLeft() && selectedTower > 0)
            {
                selectedTower -= 1;
                activeSelectionCursor.SetPosition(towerList[selectedTower].GetPosition);
                activeSelectionCursor.frameSize = towerList[selectedTower].GetFrameSize;
                timeSinceLastFrame = 300;
            }

            if (timeSinceLastFrame <= 0 && !scrolling && extended && CursorRight() && selectedTower < towerList.Count - 1)
            {
                selectedTower += 1;
                activeSelectionCursor.SetPosition(towerList[selectedTower].GetPosition);
                activeSelectionCursor.frameSize = towerList[selectedTower].GetFrameSize;
                timeSinceLastFrame = 300;
            }

            if (!IsExtended() && ResetSelection())
            {
                activeSelection = -1;
            }
            else if (IsExtended() && SelectTower())
            {
                activeSelection = selectedTower;
                scrolling = true;
            }

            if (activeSelectionCursor != null)
            {
                activeSelectionCursor.UpdateFade(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            base.Draw(gameTime, spriteBatch, 0.001f);

            foreach (TowerSprite t in towerList)
            {
                t.Draw(gameTime, spriteBatch, 0.0009f);
            }

            activeSelectionCursor.Draw(gameTime, spriteBatch, 0.0008f);
        }
    }
}

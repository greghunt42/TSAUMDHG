using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TSAUMDHG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteManager spriteManager;

        //Audio
        //AudioEngine audioEngine;
        //WaveBank waveBank;
        //SoundBank soundBank;
        //Cue trackCue;

        //Random
        public Random rnd { get; private set; }

        //Scoring
        int currentScore = 0;
        //SpriteFont scoreFont;

        //Background
        Texture2D backgroundTexture;

        SpriteFont font;

        //Game states
        enum GameState { Start, InGame, GameOver };
        GameState currentGameState = GameState.Start;

        //Lives
        /*
        int numberLivesRemaining = 3;
        public int NumberLivesRemaining
        {
            get { return numberLivesRemaining; }
            set
            {
                numberLivesRemaining = value;
                if (numberLivesRemaining == 0)
                {
                    currentGameState = GameState.GameOver;
                    spriteManager.Enabled = false;
                    spriteManager.Visible = false;
                }
            }
        }
         */

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            //Initialize random variable
            rnd = new Random();

            //Set game window size
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            //Sprite manager should be disabled at start
            spriteManager.Enabled = false;
            spriteManager.Visible = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = this.Content.Load<Texture2D>(@"images\title");
            font = this.Content.Load<SpriteFont>(@"font\arial");

            //Load audio objects
            //audioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs");
            //waveBank = new WaveBank(audioEngine, @"Content\Audio\Wave Bank.xwb");
            //soundBank = new SoundBank(audioEngine, @"Content\Audio\Sound Bank.xsb");

            // Start the soundtrack audio
            //trackCue = soundBank.GetCue("track");
            //trackCue.Play();

            // Play the start sound
            //soundBank.PlayCue("start");

            //Load fonts
            //scoreFont = Content.Load<SpriteFont>(@"fonts\arial");

            //Load background
            //backgroundTexture = Content.Load<Texture2D>(@"Images\background");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Only perform certain actions based on
            // the current game state
            switch (currentGameState)
            {
                case GameState.Start:
                    //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                    //    Keyboard.GetState().IsKeyDown(Keys.Space))
                    //{
                        currentGameState = GameState.InGame;
                        spriteManager.Enabled = true;
                        spriteManager.Visible = true;
                    //}
                    break;
                case GameState.InGame:
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        Exit();
                    break;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == 
              ButtonState.Pressed)
                this.Exit();

            //Update audio
            //audioEngine.Update();

            base.Update(gameTime);
        }

         /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
                              // Only draw certain items based on
            // the current game state
            switch (currentGameState)
            {
                case GameState.Start:
                    GraphicsDevice.Clear(Color.Black);
                    
                    // Draw text for intro splash screen
                    spriteBatch.Begin();

                    spriteBatch.Draw(backgroundTexture,
                        new Rectangle(0, 0, Window.ClientBounds.Width,
                        Window.ClientBounds.Height), null,
                        Color.White, 0, Vector2.Zero,
                        SpriteEffects.None, 0);

                    string text = "Press Space to Play!";
                    spriteBatch.DrawString(font, text,
                        new Vector2(200, 500),
                            Color.PaleGoldenrod);
/*
                    text = "(Press any key to begin)";
                    spriteBatch.DrawString(scoreFont, text,
                        new Vector2((Window.ClientBounds.Width / 2)
                            - (scoreFont.MeasureString(text).X / 2),
                            (Window.ClientBounds.Height / 2)
                            - (scoreFont.MeasureString(text).Y / 2) + 30),
                            Color.SaddleBrown);
                    */
                    spriteBatch.End();
                    break;
                case GameState.InGame:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    
                    // Draw background image
                    /*spriteBatch.Draw(backgroundTexture,
                        new Rectangle(0, 0, Window.ClientBounds.Width,
                        Window.ClientBounds.Height), null,
                        Color.White, 0, Vector2.Zero,
                        SpriteEffects.None, 0);
                    */
                    // Draw fonts
                    /*
                    spriteBatch.DrawString(scoreFont,
                        "Score: " + currentScore,
                        new Vector2(10, 10), Color.DarkBlue,
                        0, Vector2.Zero,
                        1, SpriteEffects.None, 1);
                    */
                    spriteBatch.End();
                    
                    break;
                case GameState.GameOver:
                    GraphicsDevice.Clear(Color.AliceBlue);
                    
                    //Draw game over text
                    spriteBatch.Begin();
                    /*
                    string gameover = "Game Over! The blades win again!";
                    spriteBatch.DrawString(scoreFont, gameover,
                        new Vector2((Window.ClientBounds.Width / 2)
                            - (scoreFont.MeasureString(gameover).X / 2),
                            (Window.ClientBounds.Height / 2)
                            - (scoreFont.MeasureString(gameover).Y / 2)),
                        Color.SaddleBrown);

                    gameover = "Your score: " + currentScore;
                    spriteBatch.DrawString(scoreFont, gameover,
                        new Vector2((Window.ClientBounds.Width / 2)
                            - (scoreFont.MeasureString(gameover).X / 2),
                            (Window.ClientBounds.Height / 2)
                            - (scoreFont.MeasureString(gameover).Y / 2) + 30),
                        Color.SaddleBrown);

                    gameover = "(Press ENTER to exit)";
                    spriteBatch.DrawString(scoreFont, gameover,
                        new Vector2((Window.ClientBounds.Width / 2)
                            - (scoreFont.MeasureString(gameover).X / 2),
                            (Window.ClientBounds.Height / 2)
                            - (scoreFont.MeasureString(gameover).Y / 2) + 60),
                    Color.SaddleBrown);
                    */
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }

        public void PlayCue(string cueName)
        {
            //soundBank.PlayCue(cueName);
        }

        public void AddScore(int score)
        {
            currentScore += score;
        }
    }
}

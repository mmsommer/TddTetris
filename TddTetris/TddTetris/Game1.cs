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

namespace TddTetris
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D blockTexture;
        private Texture2D emptyBackgroundTexture;

        private DateTime lastUpdateTime;

        private int numberOfRows;
        private Field field;
        private InputFilter inputFilter;
        private GameMechanics gameMechanics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            lastUpdateTime = DateTime.Now;
            inputFilter = new InputFilter();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            blockTexture = Content.Load<Texture2D>("Block");

            emptyBackgroundTexture = new Texture2D(GraphicsDevice, 1, 1);
            emptyBackgroundTexture.SetData(new Color[] { Color.White });

            numberOfRows = (GraphicsDevice.Viewport.Height - 60)/ blockTexture.Height;

            field = new Field(12, numberOfRows);
            gameMechanics = new GameMechanics(field, new BlockFactory());
            gameMechanics.NextBlock();
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
            KeyboardState keyState = Keyboard.GetState();
            var pressedKeys = keyState.GetPressedKeys().ToArray();

            var newlyPressedKeys = inputFilter.Filter(pressedKeys).ToList();
            bool shouldAdvance = (DateTime.Now - lastUpdateTime).TotalMilliseconds > 500;

            // Allows the game to exit
            if (newlyPressedKeys.Contains(Keys.Escape))
            {
                this.Exit();
            }

            gameMechanics.HandleInput(newlyPressedKeys);

            if (shouldAdvance)
            {
                gameMechanics.AdvanceIfPossible();

                lastUpdateTime = DateTime.Now;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawField();

            base.Draw(gameTime);
        }

        private void DrawField()
        {
            spriteBatch.Begin();

            drawEmptyBackground( spriteBatch );

            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    Color? color = field.ColorAt(new Vector2(x, y));

                    if (color.HasValue)
                    {
                        spriteBatch.Draw(blockTexture, new Rectangle((int)x * blockTexture.Width, (int)y * blockTexture.Height, blockTexture.Width, blockTexture.Height), color.Value);
                    }
                }
            }

            spriteBatch.End();
        }

        private void drawEmptyBackground( SpriteBatch spriteBatch )
        {
            Rectangle fieldOutline = new Rectangle(0, 0, field.Width * blockTexture.Width, field.Height * blockTexture.Height);

            spriteBatch.Draw(emptyBackgroundTexture, fieldOutline, Color.Black);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using JellyJam.resources;

using System.Collections.Generic;

namespace JellyJam {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JellyJam : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprites sprites;

        private int height = 600;
        private int width = 800;

        private Vector2 greenJellyPosition;

        // Key to X, Y change
        private Dictionary<Keys, Vector2> moveTransforms;

        public JellyJam() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;

            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            moveTransforms = new Dictionary<Keys, Vector2>();
            moveTransforms[Keys.W] = new Vector2(0, -1);
            moveTransforms[Keys.A] = new Vector2(-1, 0);
            moveTransforms[Keys.S] = new Vector2(0, 1);
            moveTransforms[Keys.D] = new Vector2(1, 0);

            greenJellyPosition = new Vector2(100, 100);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sprites = new Sprites(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboard = Keyboard.GetState();
            foreach (Keys key in moveTransforms.Keys) {
                if (keyboard.IsKeyDown(key)) {
                    Vector2 transform = moveTransforms[key];
                    greenJellyPosition = new Vector2(greenJellyPosition.X + transform.X,
                        greenJellyPosition.Y + transform.Y);
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(sprites[Sprites.BLUE_JELLY], new Vector2(250, 200), Color.White);
            spriteBatch.Draw(sprites[Sprites.GREEN_JELLY], greenJellyPosition, Color.White);
            spriteBatch.Draw(sprites[Sprites.RED_JELLY], new Vector2(100, 200), Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

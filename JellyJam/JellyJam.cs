using JellyJam.Entities;
using JellyJam.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JellyJam {
    public class JellyJam : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TimeSpan timePerFrame = TimeSpan.FromSeconds(1 / 15.0);
        private AnimationLibrary animations;
        private Player player;

        public JellyJam() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            // TODO: Add your initialization logic here
            player = new Player(animations[AnimationLibrary.BLUE_JELLY], Vector2.Zero);
           
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            animations = new AnimationLibrary(Content);
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

            // TODO: Add your update logic here
            KeyboardState keyboard = Keyboard.GetState();

            float elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            player.update(elapsedTime, keyboard);

            // TODO: add clamping back in
            // position.X = MathHelper.Clamp(position.X, 0, GraphicsDevice.Viewport.Width - tx.Width);
            // position.Y = MathHelper.Clamp(position.Y, 0, GraphicsDevice.Viewport.Height - tx.Height);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Wheat);
            
            spriteBatch.Begin();
            player.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

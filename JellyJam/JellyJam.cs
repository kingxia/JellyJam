using JellyJam.Entities;
using JellyJam.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

// TODO: determine if Entity x/y is top-left or center.
// -- center is probably better since if we have sprites of changing height/width (we shouldn't)
// -- it allows us to correct position better / maintain relative positions more easily
namespace JellyJam {
    public class JellyJam : Game {
        // TODO: add support for changing viewport size.
        public static int HEIGHT = 600;
        public static int WIDTH = 800;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private AnimationLibrary animations;

        private Player player;

        private int saltDistanceFromPlayer = 50; // pixels
        private Texture2D saltCircle;
        private Vector2 saltPosition;

        public JellyJam() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.PreferredBackBufferWidth = WIDTH;

            this.IsMouseVisible = true;
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
            saltCircle = Content.Load<Texture2D>("sprites/select_circle");
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
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            float elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            player.update(elapsedTime, keyboard);

            // TODO: determine if we want to use grid system or fluid x/y system
            Vector2 playerCenter = player.getCenter();
            Vector2 saltDirection = new Vector2(mouse.X - playerCenter.X, mouse.Y - playerCenter.Y);
            saltDirection.Normalize();
            saltPosition = Vector2.Multiply(saltDirection, saltDistanceFromPlayer);
            saltPosition = Vector2.Add(saltPosition, playerCenter);
            // currently must subtract a width/height half-length to align sprite properly
            saltPosition = Vector2.Subtract(saltPosition,
                new Vector2(saltCircle.Width / 2, saltCircle.Height / 2));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Wheat);
            
            spriteBatch.Begin();
            spriteBatch.Draw(saltCircle, saltPosition, Color.White);
            player.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

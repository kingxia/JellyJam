using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace JellyJam {
    public class JellyJam : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tx;
        Vector2 position;
        TimeSpan timePerFrame = TimeSpan.FromSeconds(1 / 15.0);
        private AnimatedSprite playerAnimation;

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
            // TODO: Add your initialization logic here

            position = new Vector2(0, 0);
            base.Initialize();
        }

        class AnimatedSprite {
            Texture2D[] frames;
            int Frame = 0;
            int framecount;

            float timePerFrame;
            float elapsed = 0;

            int direction = 1;

            public AnimatedSprite(Texture2D[] frames, float timePerFrame) {
                this.timePerFrame = timePerFrame;
                this.frames = frames;
                this.framecount = frames.Length;
            }

            public void UpdateFrame(float elapsed) {
                this.elapsed += elapsed;
                if (this.elapsed > timePerFrame)  {
                    nextFrame();
                    this.elapsed -= timePerFrame;
                }
            }

            private void nextFrame() {
                // Advance the frame
                Frame = (Frame + direction) % framecount;

                // "bounce" direction the other way if we reached one of the boundary frames
                if (Frame == 0) {
                    direction = 1;
                }
                if (Frame == framecount - 1) {
                    direction = -1;
                }

                //Frame = (Frame + 1) % framecount;
            }

            public void DrawFrame(SpriteBatch batch, Vector2 screenPosition) {
                batch.Draw(frames[Frame], screenPosition, Color.White);
            }

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            string[] frameFiles = {
                "sprites/Players/Player Blue/playerBlue_walk1",
                "sprites/Players/Player Blue/playerBlue_walk2",
                "sprites/Players/Player Blue/playerBlue_walk3",
                "sprites/Players/Player Blue/playerBlue_walk4",
                "sprites/Players/Player Blue/playerBlue_walk5"
            };
            var walkTextures = frameFiles.Select(f => Content.Load<Texture2D>(f)).ToArray();
            playerAnimation = new AnimatedSprite(walkTextures, (float)timePerFrame.TotalSeconds);

            tx = Content.Load<Texture2D>("sprites/Players/Player Blue/playerBlue_walk1");
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
            KeyboardState ks = Keyboard.GetState();
            int speed = 5;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO decouple animation updates from controls.
            if (ks.IsKeyDown(Keys.W)) {
                position.Y -= speed;
                playerAnimation.UpdateFrame(elapsed);
            }
            if (ks.IsKeyDown(Keys.A)) {
                position.X -= speed;
                playerAnimation.UpdateFrame(elapsed);
            }
            if (ks.IsKeyDown(Keys.S)) {
                position.Y += speed;
                playerAnimation.UpdateFrame(elapsed);
            }
            if (ks.IsKeyDown(Keys.D)) {
                position.X += speed;
                playerAnimation.UpdateFrame(elapsed);
            }

            position.X = MathHelper.Clamp(position.X, 0, GraphicsDevice.Viewport.Width - tx.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, GraphicsDevice.Viewport.Height - tx.Height);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Wheat);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(tx, position, Color.White);
            playerAnimation.DrawFrame(spriteBatch, position);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

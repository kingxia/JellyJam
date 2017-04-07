using JellyJam.Entities;
using JellyJam.Sprites;
using JellyJam.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

// TODO: determine if Entity x/y is top-left or center.
// -- center is probably better since if we have sprites of changing height/width (we shouldn't)
// -- it allows us to correct position better / maintain relative positions more easily
namespace JellyJam {
    public class JellyJam : Game {
        public static AnimationLibrary animations;

        // TODO: add support for changing viewport size.
        public static int HEIGHT = 600;
        public static int WIDTH = 800;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Random random;

        private MusicLibrary musicLibrary;

        private Texture2D saltCircle;
        private Player player;

        private List<Pickup> items;
        private List<SaltSpot> saltSpots;
        private List<Enemy> enemies;
        private float itemSpawnRate = 3;
        private float currentTime = 0;

        private MouseState previousMouseState;

        public JellyJam() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.PreferredBackBufferWidth = WIDTH;

            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            random = new Random();
            previousMouseState = Mouse.GetState();

            // TODO: Add your initialization logic here
            player = new Player(AnimationLibrary.BLUE_JELLY, Vector2.Zero, saltCircle);
            saltSpots = new List<SaltSpot>();
            items = new List<Pickup>();
            enemies = new List<Enemy>();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            animations = new AnimationLibrary(Content);
            musicLibrary = new MusicLibrary(Content);
            saltCircle = Content.Load<Texture2D>("sprites/select_circle");

            musicLibrary.play(MusicLibrary.HEROIC_DEMISE);
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

            if (MouseClicked(mouse)) {
                SaltSpot toAdd = new SaltSpot(AnimationLibrary.SALT_SPOT, player.getSaltPosition());
                saltSpots.Add(toAdd);
            }

            UpdateSaltKills();
            UpdateItemPickup();
            
            currentTime += elapsedTime;
            if (currentTime > itemSpawnRate) {
                items.Add(createItem());
                currentTime = 0;
            }

            base.Update(gameTime);

            previousMouseState = mouse;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Wheat);
            
            spriteBatch.Begin();

            foreach (Pickup item in items) {
                item.draw(spriteBatch);
            }

            foreach (Enemy enemy in enemies) {
                enemy.draw(spriteBatch);
            }

            foreach (SaltSpot saltSpot in saltSpots) {
                saltSpot.draw(spriteBatch);
            }

            player.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateSaltKills() {
            List<Pickup> itemsToAdd = new List<Pickup>();
            List<SaltSpot> saltToRemove = new List<SaltSpot>();
            foreach (Enemy enemy in enemies) {
                foreach (SaltSpot spot in saltSpots) {
                    if (spot.getRect().Intersects(enemy.getRect())) {
                        itemsToAdd.Add(enemy.getDroppedItem());
                        saltToRemove.Add(spot);
                    }
                }
            }
        }

        /// <summary>
        /// Check intersections with items, and remove collected items.
        /// </summary>
        private void UpdateItemPickup() {
            List<Pickup> toRemove = new List<Pickup>();
                foreach(Pickup item in items) {
                    if (player.getRect().Intersects(item.getRect())) {
                        toRemove.Add(item);
                    }
            }
            items.RemoveAll(item => toRemove.Contains(item));
        }

        private Pickup createItem() {
            float xCoord = (float) random.NextDouble() * WIDTH;
            float yCoord = (float) random.NextDouble() * HEIGHT;
            return new Pickup(AnimationLibrary.RED_JELLY, new Vector2(xCoord, yCoord));
        }

        private bool MouseClicked(MouseState currentMouseState) {
            return (previousMouseState.LeftButton == ButtonState.Released
                && currentMouseState.LeftButton == ButtonState.Pressed);
            }
        }
}

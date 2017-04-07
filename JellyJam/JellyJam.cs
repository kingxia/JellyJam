using JellyJam.Entities;
using JellyJam.Entities.Behaviors;
using JellyJam.Sprites;
using JellyJam.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;

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
        private SpriteFont font;

        private MusicLibrary musicLibrary;
        private Texture2D saltCircle;

        private Player player;

        private ItemManager _itemManager;
        private float currentTime = 0;

        private Score score;
        
        // TODO: Change into managers.
        private List<Enemy> enemies;
        private List<SaltSpot> saltSpots;

        private MouseState previousMouseState;

        public JellyJam() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.PreferredBackBufferWidth = WIDTH;

            IsMouseVisible = true;
            IsFixedTimeStep = true;
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

            // TODO: Add your initialization logic here
            player = new Player(AnimationLibrary.BLUE_JELLY, Vector2.Zero, saltCircle);
            score = new Score(font);
            enemies = new List<Enemy>();
            enemies.Add(
              new Enemy(AnimationLibrary.BLUE_JELLY, new Vector2(200, 200), new Tracker(player)));
            enemies.Add(
              new Enemy(
                AnimationLibrary.BLUE_JELLY,
                new Vector2(400, 150),
                new Roamer(new Rectangle(0, 0, WIDTH, HEIGHT))));
            _itemManager = new ItemManager(AnimationLibrary.RED_JELLY, new Vector2(WIDTH, HEIGHT));
            saltSpots = new List<SaltSpot>();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to Draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            animations = new AnimationLibrary(Content);
            musicLibrary = new MusicLibrary(Content);
            saltCircle = Content.Load<Texture2D>("sprites/select_circle");

          font = Content.Load<SpriteFont>("fonts/arial");

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

            if (MouseClicked(mouse)) {
              SaltSpot salt = new SaltSpot(AnimationLibrary.SALT_SPOT, player.getSaltPosition());  
              saltSpots.Add(salt);
            }
            previousMouseState = mouse;

            float elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            player.update(elapsedTime, keyboard);
            foreach (Enemy enemy in enemies) {
                enemy.update(elapsedTime);
            }
            foreach (SaltSpot salt in saltSpots) {
                salt.update(elapsedTime);
            }
            _itemManager.Update(gameTime, player);
            
            UpdateSaltKills();
            int itemsGrabbed = _itemManager.RemoveCollisions(player.getRect());

            score.Add(itemsGrabbed);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should Draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Wheat);

            spriteBatch.Begin();

            _itemManager.Draw(spriteBatch);
            foreach (Enemy enemy in enemies) {
                enemy.Draw(spriteBatch);
            }
            foreach (SaltSpot saltSpot in saltSpots) {
                saltSpot.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateSaltKills() {
          //TODO. Remove salt.
        }

        private bool MouseClicked(MouseState currentMouseState) {
            return (previousMouseState.LeftButton == ButtonState.Released
                && currentMouseState.LeftButton == ButtonState.Pressed);
        }
    }

  public class Score {
    // TODO move me
    private readonly SpriteFont _font;
    private int score;

    public Score(SpriteFont font) {
      _font = font;
    }

    public void Add(int amount) {
      score += amount;
    }

    public void Draw(SpriteBatch spriteBatch) {
      spriteBatch.DrawString(_font, ScoreString(), new Vector2(0, 0), Color.Black);
    }

    private string ScoreString() {
      return String.Format("Score: {0}", score);
    }
  }
}


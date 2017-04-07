using JellyJam.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace JellyJam.Entities {
  public class Player : Entity {
    private Dictionary<Keys, Vector2> moveTransforms = new Dictionary<Keys, Vector2>() {
            { Keys.W, new Vector2(0, -1) },
            { Keys.S, new Vector2(0, 1) },
            { Keys.A, new Vector2(-1, 0) },
            { Keys.D, new Vector2(1, 0) },
        };

    private int speed;
    private Texture2D saltCircle;
    private Vector2 saltPosition;
    private int saltDistance; // Pixels

    public Player(
        string animation, Vector2 position, Texture2D saltCircle,
        int speed = 5, int saltDistance = 50) : base(animation, position) {
      this.saltCircle = saltCircle;
      this.speed = speed;
      this.saltDistance = saltDistance;
      updateSaltPosition();
    }

    public Vector2 getSaltPosition() {
      return saltPosition;
    }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
      spriteBatch.Draw(saltCircle, saltPosition, Color.White);
    }

    // TODO: decouple input from update, maybe with InputHandler.
    public void update(float elapsedTime, KeyboardState keyboard) {
      bool moved = false;
      foreach (Keys key in moveTransforms.Keys) {
        if (keyboard.IsKeyDown(key)) {
          position = Vector2.Add(position, Vector2.Multiply(moveTransforms[key], speed));
          moved = true;
        }
      }

      position.X = MathHelper.Clamp(position.X, 0, JellyJam.WIDTH - getAnimation().width());
      position.Y = MathHelper.Clamp(position.Y, 0, JellyJam.HEIGHT - getAnimation().height());
      updateSaltPosition();

      base.update(elapsedTime);

      // Update the animationResource after processing time elapsed so that it starts on frame 0.
      string newAction = moved ? AnimationLibrary.WALKING : DEFAULT_CURRENT_ACTION;
      if (newAction != currentAction) {
        frameIndex = 0;
        frameTime = 0;
      }
      currentAction = newAction;
    }

    private void updateSaltPosition() {
      MouseState mouse = Mouse.GetState();
      Vector2 playerCenter = getCenter();
      Vector2 saltDirection = new Vector2(mouse.X - playerCenter.X, mouse.Y - playerCenter.Y);
      saltDirection.Normalize();
      saltPosition = Vector2.Multiply(saltDirection, saltDistance);
      saltPosition = Vector2.Add(saltPosition, playerCenter);
      // currently must subtract a width/height half-length to align sprite properly
      saltPosition = Vector2.Subtract(saltPosition,
          new Vector2(saltCircle.Width / 2, saltCircle.Height / 2));
      }
    }
}

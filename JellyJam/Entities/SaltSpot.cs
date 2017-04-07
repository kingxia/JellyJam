using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyJam.Entities {
  public class SaltSpot : Entity {
    private readonly Vector2 _startPos;
    private readonly Vector2 _endPos;

    private float interval = 1f / 30;
    private float currentTime = 0;
    private Vector2 step;
    private int steps = 3;


    public SaltSpot(string animation, Vector2 startPos, Vector2 endPos) : base(animation, startPos) {
      _startPos = startPos;
      _endPos = endPos;
      step = Vector2.Subtract(_endPos, _startPos);
      step = Vector2.Divide(step, steps);
    }

    public override void Update(float elapsedTime) {
      currentTime += elapsedTime;
      if (steps > 0 && currentTime > interval) {
        position = Vector2.Add(position, step);
        steps--;
      }
      base.Update(elapsedTime);
    }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
    }
  }
}
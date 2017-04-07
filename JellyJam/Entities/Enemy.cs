
using JellyJam.Entities.Behaviors;
using Microsoft.Xna.Framework;

namespace JellyJam.Entities {
  public class Enemy : Entity {
    private Behavior behavior;
    private int speed = 4;

    public Enemy(string animation, Vector2 startPos, Behavior behavior) :
      base(animation, startPos) {
      this.behavior = behavior;
    }

    public override void Update(float elapsedTime) {
      GameAction action = behavior.getAction(position);
      if (action == GameAction.Move) {
        Vector2 direction = behavior.getMove(position);
        position = Vector2.Add(position, Vector2.Multiply(direction, speed));
      }

      base.Update(elapsedTime);
    }
  }
}
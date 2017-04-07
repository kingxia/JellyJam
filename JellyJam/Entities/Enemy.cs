using JellyJam.Entities.Behaviors;
using Microsoft.Xna.Framework;

namespace JellyJam.Entities {
  public class Enemy : Entity {
    private Behavior behavior;
    private int speed = 4;

    public Enemy(string animation, Vector2 position, Behavior behavior) :
      base(animation, position) {
      this.behavior = behavior;
    }

    public override void update(float elapsedTime) {
      GameAction action = behavior.getAction();
      if (action == GameAction.Move) {
        Vector2 destination = behavior.getMove();
        position = destination;
      }

      base.update(elapsedTime);
    }
  }
}

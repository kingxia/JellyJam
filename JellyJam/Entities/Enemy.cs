using JellyJam.Entities.Behaviors;
using Microsoft.Xna.Framework;

namespace JellyJam.Entities {
  public class Enemy : Entity {
    private Behavior behavior;

    public Enemy(string animation, Vector2 position, Behavior behavior) :
      base(animation, position) {
      this.behavior = behavior;
    }
  }
}

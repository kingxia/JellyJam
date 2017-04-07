using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

/**
 * Behavior that takes the shortest path towards a target Entity.
 */
namespace JellyJam.Entities.Behaviors {
  public class Tracker : Behavior {
    private Entity target;

    public Tracker(Entity target) {
      this.target = target;
    }

    public GameAction getAction() {
      return GameAction.Move;
    }

    public Vector2 getMove() {
      return Vector2.Zero;
    }
  }
}

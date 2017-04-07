using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

/**
 * Behavior that performs a random walk.
 */
namespace JellyJam.Entities.Behaviors {
  public class Roamer : Behavior {
    public const float DISTANCE_THRESHOLD = 5; // Distance at which to recalculate movement.
    private static Random random = new Random();

    private Rectangle area;
    private Vector2 destination;

    public Roamer(Rectangle area) {
      this.area = area;
      updateDestination();
    }

    public GameAction getAction(Vector2 location) {
      return GameAction.Move;
    }

    public Vector2 getMove(Vector2 location) {
      if (Vector2.Distance(location, destination) < DISTANCE_THRESHOLD) {
        updateDestination();
      }
      Vector2 direction = Vector2.Subtract(destination, location);
      direction.Normalize();
      return direction;
    }
    
    private void updateDestination() {
      destination = new Vector2(random.Next(area.Width + 1) + area.Left,
        random.Next(area.Height + 1) + area.Top);
    }
  }
}

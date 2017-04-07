using Microsoft.Xna.Framework;

/**
 * Interface for AI behavior.
 */
namespace JellyJam.Entities.Behaviors {
  public interface Behavior {
    GameAction getAction(Vector2 location);
    Vector2 getMove(Vector2 location);
  }
}

public enum GameAction {
  Move,
  Attack
}
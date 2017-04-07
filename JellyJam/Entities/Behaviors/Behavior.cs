using Microsoft.Xna.Framework;

/**
 * Interface for AI behavior.
 */
namespace JellyJam.Entities.Behaviors {
  public interface Behavior {
    GameAction getAction();
    Vector2 getMove();
  }
}

public enum GameAction {
  Move,
  Attack
}
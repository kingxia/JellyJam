using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JellyJam.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyJam.Entities {
  // TOOD: should this be an interface that we can describe entities with?
  // TODO: what sort of pickups do we want to have, just jellies?
  /**
   * An item which can be picked up.
   */
  public class Item : Entity {
    public Item(string animationResource, Vector2 position) : base(animationResource, position) { }
  }

  public class ItemManager {
    private readonly string _animationResource;
    private readonly Vector2 _bounds;
    private List<Item> items = new List<Item>();
    Random random = new Random();
    private float currentTime = 0;
    private float spawnRate = 1;


    public ItemManager(string animationResource, Vector2 bounds) {
      _animationResource = animationResource;
      _bounds = bounds;
    }

    public Item Spawn(Vector2 position) {
      var spawn = new Item(_animationResource, position);
      items.Add(spawn);
      return spawn;
    }

    // Spawn in a random coordinate inside the rectangle
    public void Spawn(Vector2 lowerBounds, Vector2 upperBounds, int numberOfInstances = 1) {
      for (int i = 0; i < numberOfInstances; i++) {
        var xScale = upperBounds.X - lowerBounds.X;
        var yScale = upperBounds.Y - lowerBounds.Y;
        var xCoord = (float) random.NextDouble() * xScale + lowerBounds.X;
        var yCoord = (float) random.NextDouble() * yScale + lowerBounds.Y;
        Spawn(new Vector2(xCoord, yCoord));
      }
    }

    public IEnumerable<Item> GetCollisions(Rectangle boundingBox) {
      return items.FindAll(x => x.getRect().Intersects(boundingBox));
    }

    public void RemoveCollisions(Rectangle boundingBox) {
      items = items.Except(GetCollisions(boundingBox)).ToList();
    }

    public void Update(GameTime gameTime, Player player) {
      currentTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
      if (currentTime > spawnRate) {
        Spawn(new Vector2(0, 0), _bounds);
        currentTime = 0;
      }
      RemoveCollisions(player.getRect());
    }

    public void Draw(SpriteBatch spriteBatch) {
      items.ForEach(i => i.Draw(spriteBatch));
    }
  }
}
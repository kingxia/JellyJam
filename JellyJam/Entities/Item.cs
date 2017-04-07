using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JellyJam.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// TODO keep track of number of items picked up, "score" or "inventory".
// TODO random clustered spawns around a point (one jelly dies and drops several things)

namespace JellyJam.Entities {
  public class Item : Entity {
    public Item(string animationResource, Vector2 position) : base(animationResource, position) { }
  }

  public class ItemManager {
    private readonly string _animationResource;
    private readonly Vector2 _bounds;
    private List<Item> items = new List<Item>();
    Random random = new Random();
    private float spawnRate = 1;
    private float currentTime = 0;


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

    public int RemoveCollisions(Rectangle boundingBox) {
      var collisions = GetCollisions(boundingBox);
      items = items.Except(collisions).ToList();
      return collisions.Count();
    }

    public void Update(GameTime gameTime) {
      currentTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
      if (currentTime > spawnRate) {
        Spawn(new Vector2(0, 0), _bounds);
        currentTime = 0;
      }
    }

    public void Draw(SpriteBatch spriteBatch) {
      items.ForEach(i => i.Draw(spriteBatch));
    }
  }
}
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
    private List<Item> items = new List<Item>();

    public ItemManager(string animationResource) {
      _animationResource = animationResource;
    }

    public Item Spawn(Vector2 position) {
      var spawn = new Item(_animationResource, position);
      items.Add(spawn);
      return spawn;
    }

    Random random = new Random();

    // Spawn in a random coordinate inside the rectangle
    public Item Spawn(Vector2 lowerBounds, Vector2 upperBounds) {
      float xScale = upperBounds.X - lowerBounds.X;
      float yScale = upperBounds.Y - lowerBounds.Y;
      float xCoord = (float) random.NextDouble() * xScale + lowerBounds.X;
      float yCoord = (float) random.NextDouble() * yScale + lowerBounds.Y;
      return Spawn(new Vector2(xCoord, yCoord));
    }

    public IEnumerable<Item> GetCollisions(Rectangle boundingBox) {
      return items.FindAll(x => x.getRect().Intersects(boundingBox));
    }

    public void RemoveCollisions(Rectangle boundingBox) {
      items = items.Except(GetCollisions(boundingBox)).ToList();
    }


    private float currentTime = 0;

    public void Update(GameTime gameTime, Player player) {
      RemoveCollisions(player.getRect());
    }

    public void Draw(SpriteBatch spriteBatch) {
      items.ForEach(i => i.Draw(spriteBatch));
    }
  }
}
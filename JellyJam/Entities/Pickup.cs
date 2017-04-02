using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JellyJam.Sprites;

using Microsoft.Xna.Framework;

namespace JellyJam.Entities {
    // TODO: find a better name
    // TOOD: should this be an interface that we can describe entities with?
    // TODO: what sort of pickups do we want to have, just jellies?
    /**
     * An item which can be picked up.
     */
    public class Pickup : Entity {
        public Pickup(AnimationLibrary animationLibrary, string animation, Vector2 position) :
            base(animationLibrary, animation, position) { }
    }
}

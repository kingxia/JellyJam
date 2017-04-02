using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace JellyJam.Sprites {
    public class AnimationLibrary {
        public static string BLUE_JELLY = "blue_jelly";

        private Dictionary<string, Animation> animations;

        public AnimationLibrary(ContentManager content) {
            animations = new Dictionary<string, Animation>();

            string[] blueJelly = {
                "sprites/Players/Player Blue/playerBlue_walk1",
                "sprites/Players/Player Blue/playerBlue_walk2",
                "sprites/Players/Player Blue/playerBlue_walk3",
                "sprites/Players/Player Blue/playerBlue_walk4",
                "sprites/Players/Player Blue/playerBlue_walk5"
            };
            IEnumerable<Texture2D> walkTextures = blueJelly.Select(f => content.Load<Texture2D>(f));
            animations[BLUE_JELLY] = new Animation(new List<Texture2D>(walkTextures));
        }

        public Animation this[string key] {
            get { return animations[key]; }
            set { animations[key] = value; }
        }
    }
}

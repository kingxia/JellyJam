using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace JellyJam.Sprites {
    public class AnimationLibrary {
        // Animation bases
        public static string BLUE_JELLY = "blue_jelly";

        // Animation actions
        public static string WALKING = "_walking";

        private Dictionary<string, Animation> animations;

        public AnimationLibrary(ContentManager content) {
            animations = new Dictionary<string, Animation>();

            List<Texture2D> standingBlueJelly = new List<Texture2D>() {
                content.Load<Texture2D>("sprites/Players/Player Blue/playerBlue_walk1"),
            };
            animations[BLUE_JELLY] = new Animation(standingBlueJelly);

            string[] blueJelly = {
                "sprites/Players/Player Blue/playerBlue_walk1",
                "sprites/Players/Player Blue/playerBlue_walk2",
                "sprites/Players/Player Blue/playerBlue_walk3",
                "sprites/Players/Player Blue/playerBlue_walk4",
                "sprites/Players/Player Blue/playerBlue_walk5"
            };
            IEnumerable<Texture2D> walkTextures = blueJelly.Select(f => content.Load<Texture2D>(f));
            animations[BLUE_JELLY + WALKING] = new Animation(new List<Texture2D>(walkTextures));
        }

        public Animation this[string key] {
            get { return animations[key]; }
            set { animations[key] = value; }
        }
    }
}

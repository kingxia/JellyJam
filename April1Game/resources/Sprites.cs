using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace April1Game.resources {
    public class Sprites {
        public static string RED_JELLY = "red_jelly";
        public static string BLUE_JELLY = "blue_jelly";
        public static string GREEN_JELLY = "green_jelly";

        private Dictionary<String, Texture2D> sprites;

        public Sprites(ContentManager content) {
            sprites = new Dictionary<string, Texture2D>();
            sprites[RED_JELLY] = content.Load<Texture2D>("sprites/red_jelly");
            sprites[BLUE_JELLY] = content.Load<Texture2D>("sprites/blue_jelly");
            sprites[GREEN_JELLY] = content.Load<Texture2D>("sprites/green_jelly");
        }

        public Texture2D this[string key] {
            get { return sprites[key]; }
            set { sprites[key] = value; }
        }
    }
}

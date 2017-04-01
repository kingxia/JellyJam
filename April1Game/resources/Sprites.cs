using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace April1Game.resources {
    public class Sprites {
        public static string IMAGE = "image";

        private Dictionary<String, Texture2D> sprites;

        public Sprites(ContentManager content) {
            sprites = new Dictionary<string, Texture2D>();
            sprites[IMAGE] = content.Load<Texture2D>("sprites/test_enemy");
        }

        public Texture2D this[string key] {
            get { return sprites[key]; }
            set { sprites[key] = value; }
        }
    }
}

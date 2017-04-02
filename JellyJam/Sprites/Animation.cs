using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JellyJam.Sprites {
    /**
     * An animation for a single spritesheet.
     * 
     * Specific frames should be associated with entity objects, so that individual
     */
    public class Animation {
        public static Color DEFAULT_COLOR = Color.White;

        private List<Texture2D> frames;

        public Animation(List<Texture2D> frames) {
            this.frames = frames;
        }

        public Texture2D getFrame(int frame) {
            return frames[frame];
        }

        public int length() {
            return frames.Count;
        }

        /**
         * Draw a frame at a position.
         * 
         * Does not have exception checking to ensure a valid frame is accessed.
          */
        public void drawFrame(SpriteBatch spriteBatch, Vector2 position, int frame) {
            spriteBatch.Draw(frames[frame], position, DEFAULT_COLOR);
        }
    }
}

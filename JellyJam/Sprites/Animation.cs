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

        // TODO: decide if we want this to be a set of frames or spritesheet; or either.
        private List<Texture2D> frames;

        public Animation(List<Texture2D> frames) {
            this.frames = frames;
        }

        public Texture2D getFrame(int frame) {
            return frames[frame];
        }

        public int width() {
            // Assumes all frames are of equal width.
            return width(0);
        }

        public int width(int frame) {
            return frames[frame].Width;
        }

        public int height() {
            // Assumes all frames are of equal height.
            return height(0);
        }

        public int height(int frame) {
            return frames[frame].Height;
        }

        public int count() {
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

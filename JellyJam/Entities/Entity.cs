using JellyJam.Sprites;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyJam.Entities {
    public class Entity {
        public static float DEFAULT_FRAME_LENGTH = 0.5f; // seconds

        protected Animation animation;
        protected float frameLength;
        protected float frameTime;
        protected int frameIndex;

        protected Vector2 position;


        public Entity(Animation animation, Vector2 position) {
            this.animation = animation;
            this.frameLength = DEFAULT_FRAME_LENGTH;
            this.position = position;

            frameIndex = 0;
        }

        public virtual void update(float elapsedTime) {
            frameTime += elapsedTime;
            if (frameTime > frameLength) {
                frameTime %= frameLength;
                frameIndex = frameIndex + 1 >= animation.count() ? 0 : frameIndex + 1;
            }
        }

        public void draw(SpriteBatch spriteBatch) {
            animation.drawFrame(spriteBatch, position, frameIndex);
        }
    }
}

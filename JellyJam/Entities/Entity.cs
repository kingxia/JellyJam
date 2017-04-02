using JellyJam.Sprites;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyJam.Entities {
    public class Entity {
        public static float DEFAULT_FRAME_LENGTH = 0.5f; // seconds
        public static string DEFAULT_CURRENT_ACTION = "";

        protected AnimationLibrary animationLibrary;
        protected string animation;
        protected string currentAction;

        protected float frameLength;
        protected float frameTime;
        protected int frameIndex;

        protected Vector2 position;


        public Entity(AnimationLibrary animationLibrary, string animation, Vector2 position) {
            this.animationLibrary = animationLibrary;
            this.animation = animation;
            this.position = position;

            frameLength = DEFAULT_FRAME_LENGTH;
            frameIndex = 0;
            currentAction = DEFAULT_CURRENT_ACTION;
        }

        public virtual void update(float elapsedTime) {
            frameTime += elapsedTime;
            if (frameTime > frameLength) {
                frameTime %= frameLength;
                frameIndex = frameIndex + 1 >= getAnimation().count() ? 0 : frameIndex + 1;
            }
        }

        public void draw(SpriteBatch spriteBatch) {
            getAnimation().drawFrame(spriteBatch, position, frameIndex);
        }

        public Vector2 getPosition() {
            return new Vector2(position.X, position.Y);
        }

        public Vector2 getCenter() {
            Vector2 dimensions = getDimensions();
            return new Vector2(position.X + dimensions.X / 2, position.Y + dimensions.Y / 2);
        }

        public Vector2 getDimensions() {
            return new Vector2(getAnimation().width(frameIndex), getAnimation().height(frameIndex));
        }

        public Animation getAnimation() {
            return animationLibrary[animation + currentAction];
        }
    }
}

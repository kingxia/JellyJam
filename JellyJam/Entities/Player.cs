using JellyJam.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace JellyJam.Entities {
    public class Player : Entity {
        private Dictionary<Keys, Vector2> moveTransforms = new Dictionary<Keys, Vector2>() {
            { Keys.W, new Vector2(0, -1) },
            { Keys.S, new Vector2(0, 1) },
            { Keys.A, new Vector2(-1, 0) },
            { Keys.D, new Vector2(1, 0) },
        };

        private int speed = 5;

        public Player(Animation animation, Vector2 position) : base(animation, position) { }

        // TODO: decouple input from update, maybe with InputHandler.
        public void update(float elapsedTime, KeyboardState keyboard) {
            foreach (Keys key in moveTransforms.Keys) {
                if (keyboard.IsKeyDown(key)) {
                    position = Vector2.Add(position, Vector2.Multiply(moveTransforms[key], speed));
                }
            }

            position.X = MathHelper.Clamp(position.X, 0, JellyJam.WIDTH - animation.width());
            position.Y = MathHelper.Clamp(position.Y, 0, JellyJam.HEIGHT - animation.height());

            base.update(elapsedTime);
        }
    }
}

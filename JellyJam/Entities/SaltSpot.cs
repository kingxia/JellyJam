using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyJam.Entities {
  public class SaltSpot : Entity {

    private float currentTime = 0;

    private readonly Vector2 _endPos;

    // Animation for throwing the salt
    // Time between totalThrowSteps, "speed"
    private float stepInterval = 1f / 30;
    private Vector2 stepVector;
    private int totalThrowSteps = 3;


    public SaltSpot(string animation, Vector2 startPos, Vector2 endPos) : base(animation, startPos) {
      _endPos = endPos;
      stepVector = Vector2.Subtract(_endPos, startPos);
      stepVector = Vector2.Divide(stepVector, totalThrowSteps);
    }

    public override void Update(float elapsedTime) {
      currentTime += elapsedTime;
      if (totalThrowSteps > 0 && currentTime > stepInterval) {
        base.position = Vector2.Add(base.position, stepVector);
        totalThrowSteps--;
      }
      base.Update(elapsedTime);
    }
  }
}
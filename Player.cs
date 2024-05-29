using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public float velocity = 100f;
    public override void _PhysicsProcess(double delta)
	{
		Vector2 movementVetor = new Vector2(0, 0);
		if(Input.IsActionPressed("move_up")) {
			movementVetor.Y -= 1;
		}
		if(Input.IsActionPressed("move_down")) {
			movementVetor.Y += 1;
		}
		if(Input.IsActionPressed("move_left")) {
			movementVetor.X -= 1;
		}
		if(Input.IsActionPressed("move_right")) {
			movementVetor.X += 1;
		}
		var finalVector = movementVetor.Normalized() * velocity * (float)delta;
		var collideResult = MoveAndCollide(finalVector);
	}
}

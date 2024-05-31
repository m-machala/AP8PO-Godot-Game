using Godot;
using System;

public partial class StraightMovingFood : Edible
{
	public Vector2 movementVector = new Vector2(0f, 0f);
	public float speed = 500f;
	public override void _PhysicsProcess(double delta)
	{
		Velocity = movementVector.Normalized() * speed * (float)delta;
		MoveAndSlide();
	}
}
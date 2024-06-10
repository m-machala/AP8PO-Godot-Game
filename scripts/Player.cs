using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public float velocity = 100f;
	public float size = 1f;
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
		if(Input.IsActionPressed("debug_increase_size")) {
			ChangeSize(0.001f);
		}
		if(Input.IsActionPressed("debug_decrease_size")) {
			ChangeSize(-0.001f);
		}
		var finalVector = movementVetor.Normalized() * velocity * (float)delta;
		var collideResult = MoveAndCollide(finalVector);
		if(collideResult != null) {
			var collider = collideResult.GetCollider();
			if(collider is Edible) {
				Edible edible = (Edible)collider;
				CollideWith(edible);
			}
		}
	}

	public void ChangeSize(float changeBy) {
		size += changeBy;
		if(size < 1) size = 1;
		Scale = Vector2.One * size;
	}

	public void CollideWith(Edible edible) {
		if(edible.size < size - 0.2) {
			ChangeSize(edible.size * 0.05f);
			edible.eat();
		}
		else if(edible.hostile) {
			ChangeSize(-(edible.size / 15));
		}
	}
}

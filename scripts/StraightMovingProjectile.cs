using Godot;
using System;

public partial class StraightMovingProjectile : StraightMovingFood
{
	public override void _PhysicsProcess(double delta)
	{
		var collideResult = MoveAndCollide(movementVector.Normalized() * speed * (float)delta);
		if(collideResult != null) {
			var collider = collideResult.GetCollider();
			if(collider is Player) {
				Edible me = this;
				Player player = (Player)collider;
				player.CollideWith(me);
			}
			QueueFree();
		}
	}
}

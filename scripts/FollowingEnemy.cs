using Godot;
using System;

public partial class FollowingEnemy : Edible
{
	public float speed = 50f;
	public override void _PhysicsProcess(double delta)
	{
		var movementVector = getPlayerPosition() - Position;

		if(getPlayerSize() > size + 0.2) {
			movementVector *= -1;
		}

		var collideResult = MoveAndCollide(movementVector.Normalized() * speed * (float)delta);

		if(collideResult != null) {
			var collider = collideResult.GetCollider();
			if(collider is Player) {
				Edible me = this;
				Player player = (Player)collider;
				player.CollideWith(me);
			}
		}
	}

	public Player getPlayer() {
		return ((Level)GetParent()).playerInstance;
	}

	public Vector2 getPlayerPosition() {
		return getPlayer().Position;
	}

	public float getPlayerSize() {
		return getPlayer().size;
	}
}

using Godot;
using System;

public partial class FollowingEnemy : Edible
{
	public float speed = 50f;
	public override void _PhysicsProcess(double delta)
	{
		var playerPosition = getPlayerPosition();
		var movementVector = playerPosition - Position;

		if(getPlayerSize() > size + 0.2) {
			movementVector *= -1;

			var distanceFromPlayer = Math.Sqrt(Math.Pow(movementVector.X, 2) + Math.Pow(movementVector.Y, 2));
			if(distanceFromPlayer > 250) {
				movementVector = GetViewport().GetVisibleRect().Size / 2 - Position;
			}
			else if(distanceFromPlayer > 200) {
				movementVector = Vector2.Zero;
			}
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

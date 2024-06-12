using Godot;
using System;

public partial class ShootingEnemy : Edible
{
	public double shotTimer = 0;
	public static double shotSeconds = 5;
	public float speed = 50f;
	public PackedScene projectile = null;
	public override void _PhysicsProcess(double delta)
	{
		var playerPosition = getPlayerPosition();
		LookAt(playerPosition);
		var movementVector = playerPosition - Position;

		var distanceFromPlayer = Math.Sqrt(Math.Pow(movementVector.X, 2) + Math.Pow(movementVector.Y, 2));
		if(distanceFromPlayer < 200) {
			movementVector *= -1;
		}
		else if(distanceFromPlayer < 250) {
			movementVector = Vector2.Zero;
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

	public override void _Process(double delta)
	{
		shotTimer += delta;
		if(shotTimer > shotSeconds) {
			shotTimer -= shotSeconds;
			if(projectile != null) {
				var projectileInstance = (StraightMovingProjectile)projectile.Instantiate();
				var movement = getPlayerPosition() - Position;
				projectileInstance.movementVector = movement;
				projectileInstance.hostile = true;
				projectileInstance.size = 5;
				projectileInstance.speed = 150;
				projectileInstance.Rotate(Rotation + (float)Math.PI / 2);
				projectileInstance.Position = Position + movement.Normalized() * 60;
				GetParent().AddChild(projectileInstance);
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

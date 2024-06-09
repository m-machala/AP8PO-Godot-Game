using Godot;
using System;

public partial class Level1 : Level
{
	public override void _Ready()
	{
		playerInstance = (Player)GD.Load<PackedScene>("res://Player.tscn").Instantiate();
		AddChild(playerInstance);

		var enemyInstance = (FollowingEnemy)GD.Load<PackedScene>("res://Levels/Level1/Enemy1.tscn").Instantiate();
		enemyInstance.Position = new Vector2(100, 100);
		enemyInstance.size = 2;
		AddChild(enemyInstance);
	}

	public override void _Process(double delta)
	{
		if(playerInstance.size > 4) {
			game game = (game)GetParent();
			game.NextLevel();
		}
	}
}

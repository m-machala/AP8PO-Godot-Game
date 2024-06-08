using Godot;
using System;

public partial class Level2 : Node2D
{
	Player playerInstance;	
	public override void _Ready()
	{
		playerInstance = (Player)GD.Load<PackedScene>("res://Player.tscn").Instantiate();
		AddChild(playerInstance);
	}

	public override void _Process(double delta)
	{
		if(playerInstance.size > 1.5) {
			game game = (game)GetParent();
			game.NextLevel();
		}
	}
}

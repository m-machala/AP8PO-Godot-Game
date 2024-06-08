using Godot;
using System;

public partial class Level1 : Node2D
{
	PackedScene playerScene = GD.Load<PackedScene>("res://Player.tscn");	

	public override void _Ready()
	{
		var playerInstance = playerScene.Instantiate();
		AddChild(playerInstance);
	}

	public override void _Process(double delta)
	{
	}
}

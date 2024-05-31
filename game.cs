using Godot;
using System;

public partial class game : Node2D
{
	PackedScene playerScene = GD.Load<PackedScene>("res://Player.tscn");
	PackedScene enemyScene = GD.Load<PackedScene>("res://Enemy.tscn");
	PackedScene movingFood = GD.Load<PackedScene>("res://MovingFood.tscn");
	public override void _Ready()
	{
		var playerInstance = playerScene.Instantiate();
		AddChild(playerInstance);

		var enemyInstance = (CharacterBody2D)enemyScene.Instantiate();
		enemyInstance.GlobalPosition = new Vector2(100, 100);
		var edibleInstance = (Edible)enemyInstance;
		edibleInstance.size = 0.3f;
		AddChild(enemyInstance);

		var foodInstance = (StraightMovingFood)movingFood.Instantiate();
		foodInstance.movementVector = new Vector2(1, 1);
		foodInstance.GlobalPosition = new Vector2(0, 0);
		foodInstance.size = 0.3f;
		AddChild(foodInstance);
	}

	/*public override void _Process(double delta)
	{
	}*/
}

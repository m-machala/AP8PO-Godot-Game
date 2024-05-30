using Godot;
using System;

public partial class game : Node2D
{
	PackedScene enemyScene = GD.Load<PackedScene>("res://enemy.tscn");
	public override void _Ready()
	{
		var enemyInstance = (CharacterBody2D)enemyScene.Instantiate();
		enemyInstance.GlobalPosition = new Vector2(100, 100);
		var edibleInstance = (Edible)enemyInstance;
		edibleInstance.size = 0.3f;
		AddChild(enemyInstance);

		enemyInstance = (CharacterBody2D)enemyScene.Instantiate();
		enemyInstance.GlobalPosition = new Vector2(160, 200);
		edibleInstance = (Edible)enemyInstance;
		edibleInstance.size = 0.3f;
		AddChild(enemyInstance);

		enemyInstance = (CharacterBody2D)enemyScene.Instantiate();
		enemyInstance.GlobalPosition = new Vector2(50, 40);
		edibleInstance = (Edible)enemyInstance;
		edibleInstance.size = 0.3f;
		AddChild(enemyInstance);

		enemyInstance = (CharacterBody2D)enemyScene.Instantiate();
		enemyInstance.GlobalPosition = new Vector2(10, 350);
		edibleInstance = (Edible)enemyInstance;
		edibleInstance.size = 0.3f;
		AddChild(enemyInstance);

		enemyInstance = (CharacterBody2D)enemyScene.Instantiate();
		enemyInstance.GlobalPosition = new Vector2(400, 10);
		edibleInstance = (Edible)enemyInstance;
		edibleInstance.size = 0.3f;
		AddChild(enemyInstance);
	}

	/*public override void _Process(double delta)
	{
	}*/
}

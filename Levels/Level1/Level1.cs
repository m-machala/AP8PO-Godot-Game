using Godot;
using System;
using System.Collections.Generic;

public partial class Level1 : Level
{
	public List<(PackedScene, double)> stationaryFoodScenes = new List<(PackedScene, double)>();
	public static int startingFoodCount = 10;
	public double foodTimer = 0;
	public override void _Ready()
	{
		playerInstance = (Player)GD.Load<PackedScene>("res://Player.tscn").Instantiate();
		AddChild(playerInstance);

		var mainEnemy = (FollowingEnemy)GD.Load<PackedScene>("res://Levels/Level1/Enemy1.tscn").Instantiate();
		mainEnemy.Position = new Vector2(100, 100);
		mainEnemy.size = 2;
		mainEnemy.hostile = true;
		mainEnemy.Connect("Eaten", new Callable(this, nameof(MainEnemyWasEaten)));
		AddChild(mainEnemy);

		LoadFoodScenes();
		
		for(int i = 0; i < startingFoodCount; i++) {
			SpawnFood();
		}
	}

	public override void _Process(double delta)
	{
		foodTimer += delta;
		if((int)foodTimer >= 4) {
			foodTimer -= 4;
			SpawnFood();
		}

	}

	public void LoadFoodScenes() {
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level1/Food1.tscn"), 0.5));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level1/Food2.tscn"), 0.9));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level1/Food3.tscn"), 1.1));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level1/Food4.tscn"), 1.3));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level1/Food5.tscn"), 1.5));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level1/Food6.tscn"), 1.7));
	}

	public void SpawnFood() {
		var newFoodIndex = -1;
		for(int i = 0; i < 3; i++) {
			if(newFoodIndex < 0 || stationaryFoodScenes[newFoodIndex].Item2 > playerInstance.size - 0.3) {
				newFoodIndex = rng.RandiRange(0, stationaryFoodScenes.Count - 1);
			}
		}
		var newFood = (Edible)stationaryFoodScenes[newFoodIndex].Item1.Instantiate();
		var viewportSize = GetViewport().GetVisibleRect().Size;
		newFood.Position = new Vector2(rng.RandiRange(0, (int)viewportSize.X - 1), rng.RandiRange(0, (int)viewportSize.Y - 1));
		newFood.size = (float)stationaryFoodScenes[newFoodIndex].Item2;
		AddChild(newFood);
	}

	public void MainEnemyWasEaten() {
		game game = (game)GetParent();
			game.NextLevel();
	}
}

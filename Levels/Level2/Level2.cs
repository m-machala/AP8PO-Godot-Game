using Godot;
using System;
using System.Collections.Generic;

public partial class Level2 : Level
{
	public List<(PackedScene, double)> stationaryFoodScenes = new List<(PackedScene, double)>();
	public static int startingFoodCount = 10;
	public double foodTimer = 0;
	public override void _Ready()
	{
		playerInstance = (Player)GD.Load<PackedScene>("res://Player.tscn").Instantiate();
		AddChild(playerInstance);

		var mainEnemy = (ShootingEnemy)GD.Load<PackedScene>("res://Levels/Level2/Enemy1.tscn").Instantiate();
		game parent = (game)GetParent();
		playerInstance.Connect("Died", new Callable(parent, nameof(parent.Die)));
		mainEnemy.projectile = GD.Load<PackedScene>("res://Levels/Level2/Missile.tscn");
		mainEnemy.Position = new Vector2(100, 100);
		mainEnemy.size = 4f;
		mainEnemy.hostile = true;
		AddChild(mainEnemy);

		var mainFood = (StationaryFood)GD.Load<PackedScene>("res://Levels/Level2/Food6.tscn").Instantiate();
		mainFood.Connect("Eaten", new Callable(this, nameof(MainFoodWasEaten)));
		mainFood.size = 2.5f;
		var viewportSize = GetViewport().GetVisibleRect().Size;
		mainFood.Position = new Vector2(rng.RandiRange(0, (int)viewportSize.X - 1), rng.RandiRange(0, (int)viewportSize.Y - 1));
		AddChild(mainFood);

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
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level2/Food1.tscn"), 0.5));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level2/Food2.tscn"), 0.75));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level2/Food3.tscn"), 1.2));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level2/Food4.tscn"), 1.75));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level2/Food5.tscn"), 2));
	}

	public void SpawnFood() {
		var newFoodIndex = -1;
		for(int i = 0; i < 3; i++) {
			if(newFoodIndex < 0 || stationaryFoodScenes[newFoodIndex].Item2 > playerInstance.size - 0.3) {
				newFoodIndex = rng.RandiRange(0, stationaryFoodScenes.Count - 1);
			}
		}
		var newFood = (Edible)stationaryFoodScenes[newFoodIndex].Item1.Instantiate();
		newFood.Rotate(rng.Randf() * 2 * (float)Math.PI);
		var viewportSize = GetViewport().GetVisibleRect().Size;
		newFood.Position = new Vector2(rng.RandiRange(0, (int)viewportSize.X - 1), rng.RandiRange(0, (int)viewportSize.Y - 1));
		newFood.size = (float)stationaryFoodScenes[newFoodIndex].Item2;
		AddChild(newFood);
	}

	public void MainFoodWasEaten() {
		game game = (game)GetParent();
			game.NextLevel();
	}
}

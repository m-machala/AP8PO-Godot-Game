using Godot;
using System;
using System.Collections.Generic;

public partial class Level1 : Level
{
	public List<(PackedScene, double)> stationaryFoodScenes = new List<(PackedScene, double)>();
	public static int startingFoodCount = 10;
	public double foodTimer = 0;
	FollowingEnemy mainEnemy;
	bool missionChanged = false;
	public override void _Ready()
	{
		playerInstance = (Player)GD.Load<PackedScene>("res://Player.tscn").Instantiate();
		game parent = (game)GetParent();
		playerInstance.Connect("Died", new Callable(parent, nameof(parent.Die)));
		AddChild(playerInstance);

		mainEnemy = (FollowingEnemy)GD.Load<PackedScene>("res://Levels/Level1/Enemy1.tscn").Instantiate();
		mainEnemy.Position = new Vector2(100, 100);
		mainEnemy.size = 2;
		mainEnemy.hostile = true;
		mainEnemy.Connect("Eaten", new Callable(this, nameof(MainEnemyWasEaten)));
		AddChild(mainEnemy);

		LoadFoodScenes();
		
		for(int i = 0; i < startingFoodCount; i++) {
			SpawnFood();
		}

		mission = "- Eat meteors to grow in size\n- Avoid enemy";
	}

	public override void _Process(double delta)
	{
		foodTimer += delta;
		if((int)foodTimer >= 4) {
			foodTimer -= 4;
			SpawnFood();
		}

		if(playerInstance.size >= mainEnemy.size + 0.2 && !missionChanged) {
			mission = "- Eat enemy to go to next level";
			missionChanged = true;
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
			if(newFoodIndex < 0 || stationaryFoodScenes[newFoodIndex].Item2 > playerInstance.size - 0.2) {
				newFoodIndex = rng.RandiRange(0, stationaryFoodScenes.Count - 1);
			}
		}
		var newFood = (Edible)stationaryFoodScenes[newFoodIndex].Item1.Instantiate();
		newFood.Rotate(rng.Randf() * 2 * (float)Math.PI);
		var viewportSize = GetViewport().GetVisibleRect().Size;
		newFood.Position = new Vector2(rng.RandiRange(10, (int)viewportSize.X - 11), rng.RandiRange(10, (int)viewportSize.Y - 11));
		newFood.size = (float)stationaryFoodScenes[newFoodIndex].Item2;
		AddChild(newFood);
	}

	public void MainEnemyWasEaten() {
		game game = (game)GetParent();
			game.NextLevel();
	}
}

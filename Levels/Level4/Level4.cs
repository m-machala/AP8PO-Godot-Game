using Godot;
using System;
using System.Collections.Generic;

public partial class Level4 : Level
{
	public List<(PackedScene, double)> stationaryFoodScenes = new List<(PackedScene, double)>();
	public static int startingFoodCount = 10;
	public double foodTimer = 0;
	public StationaryFood mainFood;
	public bool missionChanged = false;
	public override void _Ready()
	{
		playerInstance = (Player)GD.Load<PackedScene>("res://Player.tscn").Instantiate();
		AddChild(playerInstance);

		var mainEnemy = (ShootingEnemy)GD.Load<PackedScene>("res://Levels/Level4/Enemy1.tscn").Instantiate();
		game parent = (game)GetParent();
		playerInstance.Connect("Died", new Callable(parent, nameof(parent.Die)));
		mainEnemy.projectile = GD.Load<PackedScene>("res://Levels/Level4/Missile.tscn");
		mainEnemy.Position = new Vector2(100, 100);
		mainEnemy.size = 4f;
		mainEnemy.hostile = true;
		AddChild(mainEnemy);

		LoadFoodScenes();
		
		for(int i = 0; i < startingFoodCount; i++) {
			SpawnFood();
		}

		mission = "- Eat meteors to grow in size\n- Avoid enemy\n- Avoid missiles";
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
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level4/Food1.tscn"), 0.65));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level4/Food2.tscn"), 0.85));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level4/Food3.tscn"), 1.5));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level4/Food4.tscn"), 2));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level4/Food5.tscn"), 2.2));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level4/Food6.tscn"), 2.5));
		stationaryFoodScenes.Add((GD.Load<PackedScene>("res://Levels/Level4/Food7.tscn"), 2.8));
	}

	public void SpawnFood() {
		var newFoodIndex = -1;
		for(int i = 0; i < 5; i++) {
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



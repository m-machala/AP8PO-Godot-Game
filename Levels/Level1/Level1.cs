using Godot;
using System;
using System.Collections.Generic;

public partial class Level1 : Level
{
	public FollowingEnemy mainEnemy;
	public List<PackedScene> stationaryFoodScenes = new List<PackedScene>();
	public RandomNumberGenerator rng = new RandomNumberGenerator();
	public static int maxFoodCount = 10;
	public int foodCount = 0;
	public override void _Ready()
	{
		playerInstance = (Player)GD.Load<PackedScene>("res://Player.tscn").Instantiate();
		AddChild(playerInstance);

		mainEnemy = (FollowingEnemy)GD.Load<PackedScene>("res://Levels/Level1/Enemy1.tscn").Instantiate();
		mainEnemy.Position = new Vector2(100, 100);
		mainEnemy.size = 2;
		AddChild(mainEnemy);

		LoadFoodScenes();
	}

	public override void _Process(double delta)
	{
		RenewFood();
		if(!IsInstanceValid(mainEnemy)) {
			game game = (game)GetParent();
			game.NextLevel();
		}
	}

	public void LoadFoodScenes() {
		stationaryFoodScenes.Add(GD.Load<PackedScene>("res://Levels/Level1/Food1.tscn"));
		stationaryFoodScenes.Add(GD.Load<PackedScene>("res://Levels/Level1/Food2.tscn"));
		stationaryFoodScenes.Add(GD.Load<PackedScene>("res://Levels/Level1/Food3.tscn"));
		stationaryFoodScenes.Add(GD.Load<PackedScene>("res://Levels/Level1/Food4.tscn"));
		stationaryFoodScenes.Add(GD.Load<PackedScene>("res://Levels/Level1/Food5.tscn"));
		stationaryFoodScenes.Add(GD.Load<PackedScene>("res://Levels/Level1/Food6.tscn"));
	}

	public void FoodWasEaten() {
		foodCount -= 1;
	}

	public void RenewFood() {
		while(maxFoodCount > foodCount) {
			var newFoodIndex = rng.RandiRange(0, stationaryFoodScenes.Count);
			var newFood = (Edible)stationaryFoodScenes[newFoodIndex].Instantiate();
			newFood.Connect("Eaten", new Callable(this, nameof(FoodWasEaten)));
			var viewportSize = GetViewport().GetVisibleRect().Size;
			newFood.Position = new Vector2(rng.RandiRange(0, (int)viewportSize.X), rng.RandiRange(0, (int)viewportSize.Y));
			newFood.size = 0.5f;
			AddChild(newFood);
			foodCount++;
		}
	}
}

using Godot;
using System;
using System.Collections.Generic;

public partial class game : Node2D
{
	static int timeIncrementSeconds = 210;
	int timeLimitSeconds = 0;
	double timeElapsedSeconds = 0;
	List<PackedScene> levels = new List<PackedScene>();
	public Label clockLabel;
	int currentLevel = 1;
	Node currentLevelInstance = null;

	enum State 
	{
		Start,
		Level,
		GameOver
	};

	State gameState = State.Start;
	public override void _Ready()
	{
		clockLabel = GetNode<Label>("Clock");
		levels.Add(GD.Load<PackedScene>("res://Levels/Level1/Level1.tscn"));
		levels.Add(GD.Load<PackedScene>("res://Levels/Level2/Level2.tscn"));
		levels.Add(GD.Load<PackedScene>("res://Levels/Level3/Level3.tscn"));
		levels.Add(GD.Load<PackedScene>("res://Levels/Level4/Level4.tscn"));
	}

	public override void _Process(double delta)
	{
		switch(gameState) {
			case State.Start:
			currentLevel = 1;
			timeElapsedSeconds = 0;
			StartLevel(1);
			break;

			case State.Level:
			timeElapsedSeconds += delta;
			clockLabel.Text = GetTimeString();

			if(Input.IsActionJustPressed("debug_next_level")) {
				NextLevel();
			}
			break;

			case State.GameOver:
			break;
		}
	}

	public void StartLevel(int level) {
		if(level > levels.Count || level < 1) {
			gameState = State.GameOver;
			return;
		}
		var levelInstance = levels[level - 1].Instantiate();
		currentLevelInstance = levelInstance;
		AddChild(levelInstance);
		gameState = State.Level;
		timeLimitSeconds += timeIncrementSeconds;
	}

	public string GetTimeString() {
		var secondsRemainder = timeLimitSeconds - timeElapsedSeconds;
		int RemainingMinutes = (int)secondsRemainder / 60;
		secondsRemainder -= RemainingMinutes * 60;
		int RemainingSeconds = (int)secondsRemainder;
		secondsRemainder -= RemainingSeconds;
		
		return $"{RemainingMinutes:D1}:{RemainingSeconds:D2}.{(int)(secondsRemainder * 10):D1}";
	}

	public void NextLevel() {
		if(currentLevelInstance != null) {
			currentLevelInstance.QueueFree();
		}
		currentLevel++;
		StartLevel(currentLevel);
	}

	public void Die() {
		if(currentLevelInstance != null) {
			currentLevelInstance.QueueFree();
		}
		gameState = State.GameOver;
	}
}

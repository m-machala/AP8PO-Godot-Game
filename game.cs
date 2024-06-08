using Godot;
using System;
using System.Collections.Generic;

public partial class game : Node2D
{
	static int timeIncrementSeconds = 150;
	int timeLimitSeconds = 0;
	double timeElapsedSeconds = 0;
	Node currentLevelInstance = null;
	PackedScene level1 = GD.Load<PackedScene>("res://Levels/Level1/Level1.tscn");
	PackedScene level2 = GD.Load<PackedScene>("res://Levels/Level2/Level2.tscn");
	PackedScene level3 = GD.Load<PackedScene>("res://Levels/Level3/Level3.tscn");
	PackedScene level4 = GD.Load<PackedScene>("res://Levels/Level4/Level4.tscn");
	List<PackedScene> levels = new List<PackedScene>();
	public Label label;
	int currentLevel = 1;

	enum State 
	{
		Start,
		Level,
		GameOver
	};

	State gameState = State.Start;
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
		levels.Add(level1);
		levels.Add(level2);
		levels.Add(level3);
		levels.Add(level4);
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
			label.Text = GetTimeString();
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
		
		return $"{RemainingMinutes:D1}:{RemainingSeconds:D2}.{(int)(secondsRemainder * 1000):D3}";
	}
}

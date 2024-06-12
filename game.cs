using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class game : Node2D
{
	static int timeIncrementSeconds = 210;
	int timeLimitSeconds = 0;
	double timeElapsedSeconds = 0;
	List<PackedScene> levels = new List<PackedScene>();
	public Label clockLabel;
	public Label scoreLabel;
	public Label missionLabel;
	int currentLevel = 1;
	List<int> scores = new List<int>();
	Level currentLevelInstance = null;

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
		scoreLabel = GetNode<Label>("Score");
		missionLabel = GetNode<Label>("Mission");
		levels.Add(GD.Load<PackedScene>("res://Levels/Level1/Level1.tscn"));
		levels.Add(GD.Load<PackedScene>("res://Levels/Level2/Level2.tscn"));
		levels.Add(GD.Load<PackedScene>("res://Levels/Level3/Level3.tscn"));
		levels.Add(GD.Load<PackedScene>("res://Levels/Level4/Level4.tscn"));
		OpenStartMenu();
	}

	public override void _Process(double delta)
	{
		switch(gameState) {
			case State.Start:
			break;

			case State.Level:
			timeElapsedSeconds += delta;
			RenderTime();
			RenderScore();
			RenderMission();

			if(Input.IsActionJustPressed("debug_next_level")) {
				NextLevel();
			}
			break;

			case State.GameOver:
			break;
		}
	}

	public void StartLevel(int level) {
		if(level > levels.Count || level < 1 || timeElapsedSeconds > timeLimitSeconds) {
			gameState = State.GameOver;
			OpenEndMenu();
			return;
		}
		var levelInstance = (Level)levels[level - 1].Instantiate();
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

	public void RenderTime() {
		clockLabel.Text = GetTimeString();
	}

	public void NextLevel() {
		if(currentLevelInstance != null) {
			scores.Add(GetCurrentLevelScore());
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
		OpenEndMenu();
	}

	public int GetCurrentLevelScore() {
		double score = 0;
		if(currentLevelInstance != null) {
			score = currentLevelInstance.playerInstance.size * 1000 * currentLevel - 1000;
		}
		return (int)score;
	}

	public int GetFullScore() {
		return scores.Sum() + GetCurrentLevelScore();
	}

	public void RenderScore() {
		int score = GetFullScore();
		scoreLabel.Text = $"Score: {score}";
	}

	public void RenderMission() {
		if(currentLevelInstance != null) {
			missionLabel.Text = currentLevelInstance.mission;
		}
	}

	public void StartGame() {
		StartLevel(1);
	}

	public void RestartGame() {
		timeIncrementSeconds = 210;
		timeLimitSeconds = 0;
		timeElapsedSeconds = 0;
	    currentLevel = 1;
	    scores = new List<int>();
		OpenStartMenu();
		gameState = State.Start;
	}

	public void OpenStartMenu() {
		var UI = (Control)GD.Load<PackedScene>("res://Levels/Other/Start.tscn").Instantiate();
		AddChild(UI);
	}

	public void OpenEndMenu() {
		var UI = (End)GD.Load<PackedScene>("res://Levels/Other/End.tscn").Instantiate();
		scoreLabel.Text = "";
		clockLabel.Text = "";
		missionLabel.Text = "";
		AddChild(UI);
	}
}

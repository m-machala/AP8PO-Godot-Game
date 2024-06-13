using Godot;
using System;

public partial class End : Control
{
	private Button buttonMenu;
	private Button buttonQuit;
	public Label labelScore;

	public override void _Ready()
	{
		buttonMenu = GetNode<Button>("MarginContainer/VBoxContainer/Menu");
		buttonQuit = GetNode<Button>("MarginContainer/VBoxContainer/Quit");
		labelScore = GetNode<Label>("MarginContainer/VBoxContainer/Score");

		buttonMenu.Connect("pressed", new Callable(this, nameof(MenuPressed)));
		buttonQuit.Connect("pressed", new Callable(this, nameof(QuitPressed)));
		int score = ((game)GetParent()).GetFullScore();
		labelScore.Text = $"Score: {score}";
	}

	public void MenuPressed() {
		((game)GetParent()).RestartGame();
		QueueFree();
	}

	public void QuitPressed() {
		GetTree().Quit();
	}
}


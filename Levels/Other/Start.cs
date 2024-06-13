using Godot;
using System;

public partial class Start : Control
{
	private Button buttonStart;
	private Button buttonQuit;

	public override void _Ready()
	{
		buttonStart = GetNode<Button>("MarginContainer/VBoxContainer/Start");
		buttonQuit = GetNode<Button>("MarginContainer/VBoxContainer/Quit");

		buttonStart.Connect("pressed", new Callable(this, nameof(StartPressed)));
		buttonQuit.Connect("pressed", new Callable(this, nameof(QuitPressed)));
	}

	public void StartPressed() {
		((game)GetParent()).StartGame();
		QueueFree();
	}

	public void QuitPressed() {
		GetTree().Quit();
	}
}

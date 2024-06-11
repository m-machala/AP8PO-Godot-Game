using Godot;
using System;

public partial class Level : Node2D
{
	public Player playerInstance;
	public RandomNumberGenerator rng = new RandomNumberGenerator();
	public string mission = "";
}

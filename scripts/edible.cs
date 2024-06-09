using Godot;
using System;

public partial class Edible : CharacterBody2D
{
	public float size = 1f;
	public bool hostile = false;

	[Signal]
	public delegate void EatenEventHandler();

	public void eat() {
		EmitSignal(SignalName.Eaten);
		QueueFree();
	}
}

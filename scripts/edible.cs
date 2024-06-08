using Godot;
using System;

public partial class Edible : CharacterBody2D
{
	public float size = 1f;
	public bool hostile = false;

	public void eat() {
		QueueFree();
	}
}

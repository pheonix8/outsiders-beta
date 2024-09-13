using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	[Export] public int Speed { get; set; } = 400;

	public override void _Ready()
	{
	}
	
	public override void _PhysicsProcess(double delta)
	{
	}
}

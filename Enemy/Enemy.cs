using Godot;
using System;

public partial class Enemy : RigidBody2D
{
	[Export] private int Speed { get; set; } = 200;
	[Export] private int Health { get; set; } = 100;
	
	private int _currentHealth;

	public override void _Ready()
	{
		_currentHealth = Health;
	}

	public void TakeDamage(int damage)
	{
		GD.Print("Taking damage");
		_currentHealth -= damage;
		if (_currentHealth <= 0)
		{
			QueueFree();
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Position -=  new Vector2(Speed * (float)delta, 0);
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}

using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public int Speed { get; set; } = 400;

	public override void _PhysicsProcess(double delta)
	{
		Position +=  new Vector2(Speed * (float)delta, 0);
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
	
}

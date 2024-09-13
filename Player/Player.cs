using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public int Speed { get; set; } = 200;

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Vector2.Zero;
        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y -= 1;
        }
		

        velocity = velocity.Normalized() * Speed;
		
        Position += velocity * (float)delta;
    }
}

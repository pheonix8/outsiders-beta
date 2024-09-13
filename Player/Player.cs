using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public Vector2 ScreenSize;
    public AnimatedSprite2D Sprite;
    [Export] public int Speed { get; set; } = 200;
    
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    
    public override void _Process(double delta)
    {
        switch (Velocity.Y)
        {
            case < 0:
                Sprite.Play("up");
                break;
            case > 0:
                Sprite.Play("down");
                break;
            case 0:
                Sprite.Play("straight");
                break;
        }
    }
    
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
        
        Velocity = velocity.Normalized() * Speed;
		
        Position += Velocity * (float)delta;
        
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    }
}

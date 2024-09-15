using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private Vector2 _screenSize;
    private AnimatedSprite2D _sprite;
    private Marker2D _marker;
    private PackedScene _bulletScene;
    private Timer _shootingTimeout;
    
    [Export] private int Speed { get; set; } = 200;
    [Export] private float BulletCooldown { get; set; } = 0.1f;
    
    public override void _Ready()
    {
        _screenSize = GetViewportRect().Size;
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _marker = GetNode<Marker2D>("Marker2D");
        _shootingTimeout = GetNode<Timer>("Timer");
        _bulletScene = GD.Load<PackedScene>("res://Bullet/Bullet.tscn");
        
        Hide();
    }
    
    public override void _Process(double delta)
    {
        switch (Velocity.Y)
        {
            case < 0:
                _sprite.Play("up");
                break;
            case > 0:
                _sprite.Play("down");
                break;
            case 0:
                _sprite.Play("straight");
                break;
        }

        if (Input.IsActionPressed("shoot") && _shootingTimeout.IsStopped())
        {
            _shootingTimeout.Start(BulletCooldown);
            Bullet bullet = _bulletScene.Instantiate<Bullet>();
            bullet.GlobalPosition = _marker.GlobalPosition;
            GetParent().AddChild(bullet);
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
            x: Mathf.Clamp(Position.X, 0, _screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, _screenSize.Y)
        );
    }

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
    }
}

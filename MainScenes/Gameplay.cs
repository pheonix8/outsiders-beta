using Godot;
using System;

public partial class Gameplay : Node2D
{
	private PackedScene _enemyScene;
	private HUD _hud;
	private int _score;
	private Signals _signals;

	public override void _Ready()
	{
		_enemyScene = GD.Load<PackedScene>("res://Enemy/Enemy.tscn");
		_hud = GetNode<HUD>("HUD");
		_signals = GetNode<Signals>("/root/Signals");
		_signals.Destroyed += OnDestroyed;
	}
	
	public void NewGame()
	{
		_score = 0;
		
		_hud.UpdateScore(_score);
		
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);
		
		GetNode<Timer>("StartTimer").Start();

	}
	
	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("EnemySpawnTimer").Start();
	}

	private void OnEnemySpawnTimerTimeout()
	{
		Enemy enemy = _enemyScene.Instantiate<Enemy>();
		var spawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemyPathLocation");
		spawnLocation.ProgressRatio = GD.Randf();
		
		enemy.Position = spawnLocation.Position;
		
		enemy.StartMoving();
		AddChild(enemy);
	}

	private void OnDestroyed()
	{
		_score++;
		_hud.UpdateScore(_score);
	}

	private void OnGameOverAreaBodyEntered(Node2D body)
	{
		if (body is Enemy enemy)
		{
			GetNode<Timer>("StartTimer").Stop();
			GetNode<Timer>("EnemySpawnTimer").Stop();
			GetNode<Player>("Player").Hide();
			_score = 0;
			_hud.UpdateScore(_score);
			GetNode<Button>("HUD/Button").Show();
		}
	}
}

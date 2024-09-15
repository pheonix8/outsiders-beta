using Godot;
using System;

public partial class HUD : CanvasLayer
{
	[Signal] public delegate void StartGameEventHandler();

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
	private void OnButtonPressed()
	{
		GetNode<Button>("Button").Hide();
		EmitSignal(SignalName.StartGame);
	}
}

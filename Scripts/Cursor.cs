using Godot;
using System;

public partial class Cursor : Sprite2D
{
    [Export] private Node2D sprite;
    public override void _Process(double delta)
    {
        sprite.Position = MapManager.Instance.SnapToGrid(GetGlobalMousePosition());
    }
}
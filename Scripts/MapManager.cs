using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class MapManager : Node
{
	/*
	 * General map manager class with utility methods and storing what is where in the map. Methods:
	 * SnapToGrid: Snaps a coordinate to its grid location
	 * Pathfinder: Input current coord and target coord and it will return a path (list of points to follow)
	 */

	public static MapManager Instance {get; private set; }
	[Export] private int scale;
	[Export] private GridPathFinder _gridPathFinder;


	public override void _Ready()
	{
		Instance = this;
	}

	public Vector2I SnapToGrid(Vector2 position)
	{
		int x = Mathf.RoundToInt((position.X + scale / 2) / scale) * scale - scale / 2;
		int y = Mathf.RoundToInt((position.Y + scale / 2) / scale) * scale - scale / 2;
		return (new  Vector2I(x, y));
	}

	public Vector2I PositionToGridPosition(Vector2 position)
	{
		return new Vector2I(Convert.ToInt32(position.X / scale), Convert.ToInt32(position.Y) / scale);
	}
	public Vector2I GridPositionToPosition(Vector2 position)
	{
        return new Vector2I(Convert.ToInt32(position.X * scale), Convert.ToInt32(position.Y) * scale);
    }

	public List<Vector2> GridPathFinder(Vector2I origin, Vector2I destination)
	{
        return _gridPathFinder.GetPath(origin, destination);
    }
}

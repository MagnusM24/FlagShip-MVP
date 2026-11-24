using Godot;
using System;
using System.Collections.Generic;

public partial class GridPathFinder : Node
{
	private AStarGrid2D _star2;
	private MapManager mapManager;
	public override void _Ready()
	{
		_star2 = new AStarGrid2D();
		_star2.Region = new Rect2I(-500, -500, 1000, 1000);
		_star2.CellSize = new Vector2I(135, 135);
		_star2.DefaultComputeHeuristic = AStarGrid2D.Heuristic.Manhattan;
		_star2.DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never;
		_star2.Update();
	}

	public List<Vector2> GetPath(Vector2I origin, Vector2I destination)
	{
		Vector2I pos = MapManager.Instance.PositionToGridPosition(origin);
		Vector2I des = MapManager.Instance.PositionToGridPosition(destination);

		List<Vector2> path = new List<Vector2>();
		foreach (Vector2I point in _star2.GetIdPath(pos, des))
		{
			path.Add(MapManager.Instance.GridPositionToPosition(point));
		}
		return path;
	}
}

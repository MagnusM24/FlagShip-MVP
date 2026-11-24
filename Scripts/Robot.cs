using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Robot : CharacterBody2D
{
	[Export] private float moveSpeed = 100;
	private Vector2 direction;
	private List<Vector2> movePath;
	private int movePathIndex; // The index of the point of the movePath it is on, or leaving


	//States
	private bool isMoving = false;
	public override void _Ready()
	{
		movePath = new List<Vector2>();
		movePathIndex = 0;
		GD.Print(MapManager.Instance.PositionToGridPosition(Position));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void MoveTo(Vector2 destination)
	{
        Vector2I pos = MapManager.Instance.SnapToGrid(Position);
		Vector2I des = MapManager.Instance.SnapToGrid(destination);

		movePathIndex = 0;
		movePath.Clear();

		movePath = MapManager.Instance.GridPathFinder(pos, des);

		if (movePath.Count > 1) //If the robot isn't just staying in the same place
		{
            isMoving = true;
        }		

		GD.Print("Move path: ");
		foreach (Vector2 i in  movePath) {
			GD.Print(i);
        }
		
	}

    public override void _PhysicsProcess(double delta)
    {
		if (isMoving)
		{
			float distanceToNextPoint = Position.DistanceTo(movePath[movePathIndex + 1]);

            if (distanceToNextPoint < 5) // When it gets  close enough to the next tile, teleport to it and increase movePathIndex
            {
                GD.Print("MovePathSize: " + movePath.Count + " Velocity: " + Velocity + " Position: " + Position + " Target: " + movePath[movePathIndex + 1] + " movePathIndex: " + movePathIndex);
				Position = movePath[movePathIndex + 1];
                movePathIndex++;
            }

            if (movePathIndex + 1 > movePath.Count - 1) // if it has got to the end of the path
            {
                movePathIndex = 0;
                movePath.Clear();
                isMoving = false;
            }
			else
			{
				
                direction = movePath[movePathIndex + 1] - Position;
                //direction.X = Mathf.RoundToInt(direction.X);
                //direction.Y = Mathf.RoundToInt(direction.Y);
          
                Velocity = direction.Normalized() * moveSpeed;
            }
        }
		
    }
}
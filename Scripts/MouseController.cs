using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;
using static Godot.TextServer;

public partial class MouseController : Node2D
{
    [Export] private Robot robot;
    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("Left click"))
        {
            robot.MoveTo(GetGlobalMousePosition());
        }
    }
}

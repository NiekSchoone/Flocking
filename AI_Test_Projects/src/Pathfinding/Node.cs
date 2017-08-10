using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Node
{
    private Point coordinate;
    private bool isWalkable;
    private NodeState nodeState;
    private float gCost;
    private float hCost;
    private float fCost;

    public Node(Point coord, bool walkable)
    {
        coordinate = coord;
        isWalkable = walkable;
    }

    public Point Coordinate { get { return coordinate; } set { coordinate = value; } }
    public bool Walkable { get { return isWalkable; } set { isWalkable = value; } }
    public NodeState State { get { return nodeState; } set { nodeState = value; } }
    public float G { get { return gCost; } set { gCost = value; } }
    public float H { get { return hCost; } set { hCost = value; } }
    public float F { get { return fCost; } set { fCost = value; } }
}

public enum NodeState
{
    CHECKED,
    OPEN,
    CLOSED
}
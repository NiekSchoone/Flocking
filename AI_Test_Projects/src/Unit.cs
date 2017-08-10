using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

abstract class Unit
{
    protected Vector2 position;
    protected Vector2 velocity;
    protected Texture2D texture;
    protected Color color;

    public Unit(float x, float y)
    {
        position = new Vector2(x, y);
        velocity = new Vector2();
    }

    public Vector2 UnitPosition
    {
        get { return position; }
        set { position = value; }
    }
    public Vector2 UnitVelocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    public Texture2D UnitTexture
    {
        get { return texture; }
        set { texture = value; }
    }
    public Color UnitColor
    {
        get { return color; }
        set { color = value; }
    }
}

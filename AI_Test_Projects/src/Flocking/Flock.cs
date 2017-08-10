using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Flock
{
    private List<Boid> boids;
    private Texture2D boidTexture;

    public Flock(Texture2D texture)
    {
        boids = new List<Boid>();
        CreateBoids(50);
        boidTexture = texture;
    }

    void CreateBoids(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            Boid newBoid = new Boid(400, 300);
            boids.Add(newBoid);
        }
    }

    public void Update()
    {
        for (int i = 0; i < boids.Count; i++)
        {
            boids[i].Update(boids);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < boids.Count; i++)
        {
            Boid currentBoid = boids[i];
            spriteBatch.Draw(boidTexture, currentBoid.UnitPosition, null, null, new Vector2(16,8), currentBoid.Angle, null, currentBoid.UnitColor);
        }
    }

    public List<Boid> GetBoids
    {
        get { return boids; }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

class Boid : Unit
{
    private Vector2 acceleration;

    private float maxForce;
    private float maxSpeed;

    private float alignmentDistance;
    private float separationDistance;
    private float cohesionDistance;

    private float angle;

    public Boid(float x, float y) : base(x, y)
    {
        Random rand = new Random();

        maxForce = 0.02f;
        maxSpeed = 1.0f;

        alignmentDistance = 50;
        separationDistance = 30;
        cohesionDistance = 90;

        acceleration = new Vector2();
        velocity = new Vector2(rand.StaticNextFloat(-maxSpeed, maxSpeed), rand.StaticNextFloat(-maxSpeed, maxSpeed));
        color = Color.White;
    }

    public void Update(List<Boid> boids)
    {
        Flock(boids);
        Run();
        Border();
    }

    private void Run()
    {
        velocity = Vector2.Add(velocity, acceleration);
        velocity = Vector2.Clamp(velocity, new Vector2(-maxSpeed), new Vector2(maxSpeed));

        position = Vector2.Add(position, velocity);
        acceleration = Vector2.Multiply(acceleration, 0);

        angle = GetAngle(velocity);
    }

    private void Flock(List<Boid> boids)
    {
        Vector2 align = Alignment(boids);
        Vector2 separate = Separation(boids);
        Vector2 cohese = Cohesion(boids);

        separate = Vector2.Multiply(separate, 4);

        ApplyForce(align);
        ApplyForce(separate);
        ApplyForce(cohese);
    }

    // Apply a force Vector to the acceleration Vector
    private void ApplyForce(Vector2 force)
    {
        acceleration = Vector2.Add(acceleration, force);
    }

    // Get the rotation angle towards a vector;
    private float GetAngle(Vector2 v)
    {
        Vector2 heading = new Vector2(v.X, v.Y);
        heading.Normalize();
        return (float)Math.Atan2(Convert.ToDouble(heading.Y), Convert.ToDouble(heading.X));
    }

    private Vector2 Seek(Vector2 targetVector)
    {
        Vector2 desired = Vector2.Subtract(targetVector, position);

        desired.Normalize();
        desired = Vector2.Multiply(desired, maxSpeed);

        Vector2 steer = Vector2.Subtract(desired, velocity);
        steer = Vector2.Clamp(steer, new Vector2(-maxForce), new Vector2(maxForce));
        return steer;
    }

    // Calculate a Vector towards average heading of neighbors.
    private Vector2 Alignment(List<Boid> boids)
    {
        Vector2 alignmentVector = new Vector2();
        int count = 0;

        foreach (Boid other in boids)
        {
            float d = Vector2.Distance(position, other.UnitPosition);
            if ((d > 0) && (d < alignmentDistance))
            {
                alignmentVector = Vector2.Add(alignmentVector, other.velocity);
                count++;
            }
        }
        if (count > 0)
        {
            alignmentVector = Vector2.Divide(alignmentVector, (float)count);

            alignmentVector.Normalize();
            alignmentVector = Vector2.Multiply(alignmentVector, maxSpeed);
            Vector2 steer = Vector2.Subtract(alignmentVector, velocity);
            steer = Vector2.Clamp(alignmentVector, new Vector2(-maxForce), new Vector2(maxForce));
            return steer;
        }
        else {
            return new Vector2(0, 0);
        }
    }

    // Avoid crowding neighbors (short range repulsion rule)
    private Vector2 Separation(List<Boid> boids)
    {
        Vector2 separationVector = new Vector2();
        int count = 0;
        foreach (Boid other in boids)
        {
            float d = Vector2.Distance(position, other.UnitPosition);
            if ((d > 0) && (d < separationDistance))
            {
                Vector2 diff = Vector2.Subtract(position, other.UnitPosition);
                diff.Normalize();
                diff = Vector2.Divide(diff, d);
                separationVector = Vector2.Add(separationVector, diff);
                count++;
            }
        }
        if (count > 0)
        {
            separationVector = Vector2.Divide(separationVector, (float)count);
        }
        if (separationVector.X > 0 && separationVector.Y > 0)
        {
            separationVector.Normalize();
            separationVector = Vector2.Multiply(separationVector, maxSpeed);
            separationVector = Vector2.Subtract(separationVector, velocity);
            separationVector = Vector2.Clamp(separationVector, new Vector2(-maxForce), new Vector2(maxForce));
        }
        return separationVector;
    }

    // Calculate a Vector towards average position of neighbors (long range attraction rule)
    private Vector2 Cohesion(List<Boid> boids)
    {
        Vector2 coheseVector = new Vector2();
        int count = 0;
        foreach (Boid other in boids)
        {
            float d = Vector2.Distance(position, other.UnitPosition);
            if ((d > 0) && (d < cohesionDistance))
            {
                coheseVector = Vector2.Add(coheseVector, other.UnitPosition);
                count++;
            }
        }
        if (count > 0)
        {
            coheseVector = Vector2.Divide(coheseVector, count);
            return Seek(coheseVector);
        }
        else {
            return new Vector2(0, 0);
        }
    }


    private Vector2 Avoidance()
    {
        Vector2 avoidanceVector = new Vector2();

        return avoidanceVector;
    }

    private void Border()
    {
        float width = GraphicsDeviceManager.DefaultBackBufferWidth;
        float height = GraphicsDeviceManager.DefaultBackBufferHeight;
        if (position.X < -10) position.X = width + 2;
        if (position.Y < -10) position.Y = height + 2;
        if (position.X > width + 10) position.X = -10;
        if (position.Y > height + 10) position.Y = -10;
    }

    public float Angle
    {
        get { return angle; }
        set { angle = value; }
    }
}
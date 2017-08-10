using System;
using System.Threading;
using Microsoft.Xna.Framework;

public static class StaticRandom
{
    private static int seed;

    private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
        (() => new Random(Interlocked.Increment(ref seed)));

    static StaticRandom()
    {
        seed = Environment.TickCount;
    }

    public static Random Instance { get { return threadLocal.Value; } }
}

public static class StaticRandomExtensions
{
    public static float StaticNextFloat(this Random rand)
    {
        return (float)StaticRandom.Instance.NextDouble();
    }
    public static float StaticNextFloat(this Random rand, float min, float max)
    {
        if (max < min)
        {
            throw new ArgumentException("max cannot be less than min");
        }
        return (float)StaticRandom.Instance.NextDouble() * (max - min) + min;
    }
    public static Color StaticNextColor(this Random rand)
    {
        return Color.FromNonPremultiplied(StaticRandom.Instance.Next(0, 256), StaticRandom.Instance.Next(0, 256), StaticRandom.Instance.Next(0, 256), 255);
    }
}

public static class RandomExtensions
{
    public static float NextFloat(this Random rand)
    {
        return (float)rand.NextDouble();
    }
    public static float NextFloat(this Random rand, float min, float max)
    {
        if (max < min)
        {
            throw new ArgumentException("max cannot be less than min");
        }
        return (float)rand.NextDouble() * (max - min) + min;
    }
    public static Color NextColor(this Random rand)
    {
        return Color.FromNonPremultiplied(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256), 255);
    }
}


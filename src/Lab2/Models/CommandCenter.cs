namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class CommandCenter
{
    private readonly int _level;

    public PlanetType Type { get; }

    public CommandCenter(PlanetType type, int level)
    {
        if (level < 0 || level > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(level));
        }

        _level = level;
        Type = type;
    }
}
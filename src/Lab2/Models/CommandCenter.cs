namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class CommandCenter
{
    private readonly int _level;

    public PlanetType Type { get; }

    internal CommandCenter(PlanetType type, int level)
    {
        if (level < 0 || level > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(level));
        }

        _level = level;
        Type = type;
    }

    public int Cpu() => _level switch
    {
        0 => 1675,
        1 => 7057,
        2 => 12136,
        3 => 17215,
        4 => 21315,
        5 => 25415,
        _ => throw new ArgumentOutOfRangeException(nameof(_level)),
    };

    public int Pg() => _level switch
    {
        0 => 6000,
        1 => 9000,
        2 => 12000,
        3 => 15000,
        4 => 17000,
        5 => 19000,
        _ => throw new ArgumentOutOfRangeException(nameof(_level)),
    };
}
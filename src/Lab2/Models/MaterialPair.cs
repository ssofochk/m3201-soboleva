namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record MaterialPair
{
    public Material First { get; }

    public Material Second { get; }

    public MaterialPair(Material first, Material second)
    {
        if (string.CompareOrdinal(first.Name, second.Name) <= 0)
        {
            First = first;
            Second = second;
        }
        else
        {
            First = second;
            Second = first;
        }
    }
}
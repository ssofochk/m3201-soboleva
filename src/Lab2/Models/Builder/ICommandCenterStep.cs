using Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Builder;

public interface ICommandCenterStep
{
    IBuildingStep WithCommandCenter(CommandCenter commandCenter);
}
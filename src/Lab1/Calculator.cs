using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Contracts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Calculator
{
    private readonly Station _station;

    public Calculator(Station station)
    {
        _station = station;
    }

    public CalculationResult Calculate(Contract contract)
    {
        foreach (IMineral mineral in contract.AsteroidBelt.Ore.YieldPerPortion.Keys)
        {
            if (!contract.PriceList.ContainsKey(mineral))
            {
                return CalculationResult.Failure(
                    ContractRejectionReason.MissingMineralPrice);
            }
        }

        var testTrip = new Trip(contract.Fleet);
        if (testTrip.MineCycle(1) == 0)
        {
            return CalculationResult.Failure(
                ContractRejectionReason.NoShipCanMineInFirstCycle);
        }

        decimal flightTime = contract.AsteroidBelt.Distance / contract.Fleet.Speed;
        if (!contract.EnoughTimeForFirstTrip(flightTime))
        {
            return CalculationResult.Failure(
                ContractRejectionReason.InsufficientTermForFirstTrip);
        }

        decimal time = 0;
        int tripsCount = 0;
        decimal minedOreVolume = 0;
        int remainingOreUnits = 0;

        var minerals = new Dictionary<IMineral, int>();
        while (contract.CanStartTrip(
                   time,
                   flightTime,
                   minedOreVolume))
        {
            var trip = new Trip(contract.Fleet);

            tripsCount++;

            time += flightTime;

            int cycleNumber = 1;

            while (contract.CanStartCycle(
                       time,
                       flightTime,
                       minedOreVolume))
            {
                int minedThisCycle = trip.MineCycle(cycleNumber);

                if (minedThisCycle == 0)
                    break;

                minedOreVolume += minedThisCycle;
                time += 1;
                cycleNumber++;
            }

            time += flightTime;

            ProcessingResult processingResult = _station.Process(
                contract.AsteroidBelt.Ore,
                trip.MinedVolume,
                remainingOreUnits);

            remainingOreUnits = processingResult.RemainingOreUnits;
            foreach (KeyValuePair<IMineral, int> mineral
                     in processingResult.Minerals)
            {
                if (minerals.ContainsKey(mineral.Key))
                {
                    minerals[mineral.Key] += mineral.Value;
                }
                else
                {
                    minerals[mineral.Key] = mineral.Value;
                }
            }
        }

        decimal revenue = 0;

        foreach (KeyValuePair<IMineral, int> mineral in minerals)
        {
            revenue += mineral.Value * contract.PriceList[mineral.Key];
        }

        decimal rentalCost = contract.Fleet.CostPerHour * time;
        decimal tax = _station.TaxRate * revenue;
        decimal profit = revenue - rentalCost - tax;

        return CalculationResult.Success(
            time,
            tripsCount,
            minedOreVolume,
            minerals,
            remainingOreUnits,
            revenue,
            rentalCost,
            tax,
            profit);
    }
}
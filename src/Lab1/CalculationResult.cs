using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1;

public record CalculationResult
{
    public bool IsSuccess { get; }

    public ContractRejectionReason? RejectionReason { get; }

    public decimal Time { get; }

    public int TripsCount { get; }

    public decimal MinedOreVolume { get; }

    public IReadOnlyDictionary<IMineral, int> Minerals { get; }

    public int RemainingOreUnits { get; }

    public decimal Revenue { get; }

    public decimal RentalCost { get; }

    public decimal Tax { get; }

    public decimal Profit { get; }

    private CalculationResult(
        bool isSuccess,
        ContractRejectionReason? rejectionReason,
        decimal time,
        int tripsCount,
        decimal minedOreVolume,
        IReadOnlyDictionary<IMineral, int> minerals,
        int remainingOreUnits,
        decimal revenue,
        decimal rentalCost,
        decimal tax,
        decimal profit)
    {
        IsSuccess = isSuccess;
        RejectionReason = rejectionReason;
        Time = time;
        TripsCount = tripsCount;
        MinedOreVolume = minedOreVolume;
        Minerals = minerals;
        RemainingOreUnits = remainingOreUnits;
        Revenue = revenue;
        RentalCost = rentalCost;
        Tax = tax;
        Profit = profit;
    }

    public static CalculationResult Failure(ContractRejectionReason reason)
    {
        return new CalculationResult(
            false,
            reason,
            0,
            0,
            0,
            new Dictionary<IMineral, int>(),
            0,
            0,
            0,
            0,
            0);
    }

    public static CalculationResult Success(
        decimal time,
        int tripsCount,
        decimal minedOreVolume,
        IReadOnlyDictionary<IMineral, int> minerals,
        int remainingOreUnits,
        decimal revenue,
        decimal rentalCost,
        decimal tax,
        decimal profit)
    {
        return new CalculationResult(
            true,
            null,
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
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Contracts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.FleetPlacementStrategy;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

// Тестовые сценарии из задания (раздел «Тестовые сценарии»).
// Значения по умолчанию: флот – один Venture, стратегия «Общий трюм»;
// пояс «Пояс A» – 4 а.е., Veldspar; налог 0;
// прайс-лист – Tritanium 4, Pyerite 10, Mexallon 70, Isogen 150.
public class ContractScenarioTests
{
    [Fact(DisplayName = "Сценарий 1. Контракт на срок")]
    public void TimeContract_WithSingleVenture_IsCompleted()
    {
        var fleet = new Fleet(
            new List<IShip>
            {
                new Venture(),
            },
            new SharedHoldStrategy());

        var belt = new AsteroidBelt(
            "Пояс A",
            4m,
            new Veldspar());

        var priceList = new Dictionary<IMineral, decimal>
        {
            [new Tritanium()] = 4m,
            [new Pyerite()] = 10m,
            [new Mexallon()] = 70m,
            [new Isogen()] = 150m,
        };

        var contract = new TermContract(
            belt,
            fleet,
            priceList,
            6m);

        var station = new Station(0m);
        var calculator = new Calculator(station);

        CalculationResult result = calculator.Calculate(contract);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.TripsCount);
        Assert.Equal(6m, result.Time);
        Assert.Equal(400m, result.MinedOreVolume);
        Assert.Equal(8000, result.Minerals[new Tritanium()]);
        Assert.Equal(32000m, result.Revenue);
        Assert.Equal(6000m, result.RentalCost);
        Assert.Equal(26000m, result.Profit);
    }

    [Fact(DisplayName = "Сценарий 2. Пустой флот")]
    public void Fleet_WithoutShips_CannotBeCreated()
    {
        Assert.Throws<ArgumentException>(() =>
            new Fleet(
                Array.Empty<IShip>(),
                new SharedHoldStrategy()));
    }

    [Fact(DisplayName = "Сценарий 3. Контракт на объём")]
    public void VolumeContract_WithSingleVenture_IsCompleted()
    {
        var fleet = new Fleet(
            new List<IShip>
            {
                new Venture(),
            },
            new SharedHoldStrategy());

        var belt = new AsteroidBelt(
            "Пояс A",
            4m,
            new Veldspar());

        var priceList = new Dictionary<IMineral, decimal>
        {
            [new Tritanium()] = 4m,
            [new Pyerite()] = 10m,
            [new Mexallon()] = 70m,
            [new Isogen()] = 150m,
        };

        var contract = new VolumeContract(
            belt,
            fleet,
            priceList,
            1000m);

        var station = new Station(0m);
        var calculator = new Calculator(station);

        CalculationResult result = calculator.Calculate(contract);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.TripsCount);
        Assert.Equal(16m, result.Time);
        Assert.Equal(1000m, result.MinedOreVolume);
        Assert.Equal(20000, result.Minerals[new Tritanium()]);
        Assert.Equal(80000m, result.Revenue);
        Assert.Equal(16000m, result.RentalCost);
        Assert.Equal(0m, result.Tax);
        Assert.Equal(64000m, result.Profit);
    }

    [Fact(DisplayName = "Сценарий 4. Отклонение по сроку")]
    public void TimeContract_TooShort_IsRejected()
    {
        var fleet = new Fleet(
            new List<IShip>
            {
                new Venture(),
            },
            new SharedHoldStrategy());

        var belt = new AsteroidBelt(
            "Пояс A",
            4m,
            new Veldspar());

        var priceList = new Dictionary<IMineral, decimal>
        {
            [new Tritanium()] = 4m,
            [new Pyerite()] = 10m,
            [new Mexallon()] = 70m,
            [new Isogen()] = 150m,
        };

        var contract = new TermContract(
            belt,
            fleet,
            priceList,
            2m);

        var calculator = new Calculator(new Station(0m));

        CalculationResult result = calculator.Calculate(contract);

        Assert.False(result.IsSuccess);
        Assert.Equal(
            ContractRejectionReason.InsufficientTermForFirstTrip,
            result.RejectionReason);
    }

    [Fact(DisplayName = "Сценарий 5. Отклонение по прайс-листу")]
    public void Contract_WithIncompletePriceList_IsRejected()
    {
        var fleet = new Fleet(
            new List<IShip>
            {
                new Venture(),
            },
            new SharedHoldStrategy());

        var belt = new AsteroidBelt(
            "Пояс A",
            4m,
            new Scordite());

        var priceList = new Dictionary<IMineral, decimal>
        {
            [new Tritanium()] = 4m,

            // Pyerite специально отсутствует
            [new Mexallon()] = 70m,
            [new Isogen()] = 150m,
        };

        var contract = new TermContract(
            belt,
            fleet,
            priceList,
            6m);

        var calculator = new Calculator(new Station(0m));

        CalculationResult result = calculator.Calculate(contract);

        Assert.False(result.IsSuccess);
        Assert.Equal(
            ContractRejectionReason.MissingMineralPrice,
            result.RejectionReason);
    }
}
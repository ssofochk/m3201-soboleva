using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Contracts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.FleetPlacementStrategy;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class AdditionalTests
{
    [Fact]
    public void Fleet_SpeedIsSlowestShip_AndCostIsSum()
    {
        var fleet = new Fleet(
            new List<IShip>
            {
                new Venture(),
                new Prospect(),
                new Epithal(),
            },
            new SharedHoldStrategy());

        Assert.Equal(2, fleet.Speed);
        Assert.Equal(3000, fleet.CostPerHour);
    }

    [Fact]
    public void Prospect_Mines75InFirstCycle_And150Afterwards()
    {
        var prospect = new Prospect();

        Assert.Equal(75, prospect.MineOneCycle(1));
        Assert.Equal(150, prospect.MineOneCycle(2));
        Assert.Equal(150, prospect.MineOneCycle(3));
    }

    [Fact]
    public void Prospect_FirstCycleIsResetForEveryNewTrip()
    {
        var fleet = new Fleet(
            new List<IShip>
            {
                new Prospect(),
            },
            new SharedHoldStrategy());

        var firstTrip = new Trip(fleet);
        var secondTrip = new Trip(fleet);

        Assert.Equal(75, firstTrip.MineCycle(1));
        Assert.Equal(75, secondTrip.MineCycle(1));
    }

    [Fact]
    public void SeparateHoldStrategy_AllowsExactFit()
    {
        var ship = new ShipAndOccupancy(new Venture())
        {
            Occupancy = 300,
        };

        var state = new FleetOccupancyState(
            new List<ShipAndOccupancy>
            {
                ship,
            });

        var strategy = new SeparateHoldStrategy();

        bool result = strategy.Place(ship, 100, state);

        Assert.True(result);
        Assert.Equal(400, ship.Occupancy);
    }

    [Fact]
    public void SeparateHoldStrategy_DoesNotUseOtherShipsHold()
    {
        var miningShip = new ShipAndOccupancy(new Venture())
        {
            Occupancy = 350,
        };

        var otherShip = new ShipAndOccupancy(new Venture());

        var state = new FleetOccupancyState(
            new List<ShipAndOccupancy>
            {
                miningShip,
                otherShip,
            });

        var strategy = new SeparateHoldStrategy();

        bool result = strategy.Place(miningShip, 100, state);

        Assert.False(result);
        Assert.Equal(350, miningShip.Occupancy);
        Assert.Equal(0, otherShip.Occupancy);
    }

    [Fact]
    public void SharedHoldStrategy_UsesMiningShipFirst_ThenOtherShipsInOrder()
    {
        var miningShip = new ShipAndOccupancy(new Venture())
        {
            Occupancy = 350,
        };

        var secondShip = new ShipAndOccupancy(new Venture())
        {
            Occupancy = 390,
        };

        var thirdShip = new ShipAndOccupancy(new Epithal());

        var state = new FleetOccupancyState(
            new List<ShipAndOccupancy>
            {
                miningShip,
                secondShip,
                thirdShip,
            });

        state.TotalOccupancy = 740;

        var strategy = new SharedHoldStrategy();

        bool result = strategy.Place(miningShip, 100, state);

        Assert.True(result);

        // 50 помещается в добывающий Venture.
        Assert.Equal(400, miningShip.Occupancy);

        // Следующие 10 — во второй Venture.
        Assert.Equal(400, secondShip.Occupancy);

        // Оставшиеся 40 — в Epithal.
        Assert.Equal(40, thirdShip.Occupancy);

        Assert.Equal(840, state.TotalOccupancy);
    }

    [Fact]
    public void SharedHoldStrategy_WhenWholeVolumeDoesNotFit_DoesNotChangeState()
    {
        var firstShip = new ShipAndOccupancy(new Venture())
        {
            Occupancy = 350,
        };

        var secondShip = new ShipAndOccupancy(new Venture())
        {
            Occupancy = 390,
        };

        var state = new FleetOccupancyState(
            new List<ShipAndOccupancy>
            {
                firstShip,
                secondShip,
            });

        state.TotalOccupancy = 740;

        var strategy = new SharedHoldStrategy();

        bool result = strategy.Place(firstShip, 100, state);

        Assert.False(result);

        Assert.Equal(350, firstShip.Occupancy);
        Assert.Equal(390, secondShip.Occupancy);
        Assert.Equal(740, state.TotalOccupancy);
    }

    [Fact]
    public void Station_ProcessesOnlyFullPortions_AndLeavesOreRemainder()
    {
        var station = new Station(0m);

        ProcessingResult result = station.Process(
            new Pyroxeres(),
            100m,
            0);

        // 100 / 0.3 = 333.33... -> 333 единицы.
        // 3 полных порции и остаток 33.
        Assert.Equal(33, result.RemainingOreUnits);

        Assert.Equal(
            135,
            result.Minerals[new Pyerite()]);

        Assert.Equal(
            45,
            result.Minerals[new Mexallon()]);
    }

    [Fact]
    public void Station_UsesOreRemainderFromPreviousTrip()
    {
        var station = new Station(0m);

        ProcessingResult firstResult = station.Process(
            new Pyroxeres(),
            100m,
            0);

        // После первого рейса осталось 33 единицы.
        Assert.Equal(33, firstResult.RemainingOreUnits);

        // 20.1 / 0.3 = 67 новых единиц.
        // 33 + 67 = 100 -> ещё одна полная порция.
        ProcessingResult secondResult = station.Process(
            new Pyroxeres(),
            20.1m,
            firstResult.RemainingOreUnits);

        Assert.Equal(0, secondResult.RemainingOreUnits);
        Assert.Equal(45, secondResult.Minerals[new Pyerite()]);
        Assert.Equal(15, secondResult.Minerals[new Mexallon()]);
    }

    [Fact]
    public void TermContract_WithNonPositiveTerm_ThrowsArgumentException()
    {
        Fleet fleet = CreateDefaultFleet();
        AsteroidBelt belt = CreateDefaultBelt();

        Assert.Throws<ArgumentException>(() =>
            new TermContract(
                belt,
                fleet,
                CreateDefaultPriceList(),
                0m));
    }

    [Fact]
    public void VolumeContract_WithNonPositiveTargetVolume_ThrowsArgumentException()
    {
        Fleet fleet = CreateDefaultFleet();
        AsteroidBelt belt = CreateDefaultBelt();

        Assert.Throws<ArgumentException>(() =>
            new VolumeContract(
                belt,
                fleet,
                CreateDefaultPriceList(),
                0m));
    }

    [Fact]
    public void Station_WithTaxOutsideAllowedRange_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Station(-0.01m));
        Assert.Throws<ArgumentException>(() => new Station(1.01m));
    }

    [Fact]
    public void Station_AllowsBoundaryTaxRates()
    {
        var zeroTaxStation = new Station(0m);
        var fullTaxStation = new Station(1m);

        Assert.Equal(0m, zeroTaxStation.TaxRate);
        Assert.Equal(1m, fullTaxStation.TaxRate);
    }

    [Fact]
    public void Contract_WithOnlyCarrier_IsRejected()
    {
        var fleet = new Fleet(
            new List<IShip>
            {
                new Epithal(),
            },
            new SharedHoldStrategy());

        var contract = new TermContract(
            CreateDefaultBelt(),
            fleet,
            CreateDefaultPriceList(),
            6m);

        var calculator = new Calculator(new Station(0m));

        CalculationResult result = calculator.Calculate(contract);

        Assert.False(result.IsSuccess);
        Assert.Equal(
            ContractRejectionReason.NoShipCanMineInFirstCycle,
            result.RejectionReason);
    }

    [Fact]
    public void VolumeContract_DoesNotMinePartialCycleToMatchTargetExactly()
    {
        Fleet fleet = CreateDefaultFleet();

        var contract = new VolumeContract(
            CreateDefaultBelt(),
            fleet,
            CreateDefaultPriceList(),
            50m);

        var calculator = new Calculator(new Station(0m));

        CalculationResult result = calculator.Calculate(contract);

        Assert.True(result.IsSuccess);

        // Venture добывает 100 м³ целым циклом.
        Assert.Equal(100m, result.MinedOreVolume);
        Assert.Equal(1, result.TripsCount);
        Assert.Equal(3m, result.Time);
    }

    [Fact]
    public void CompletedContract_CanHaveNegativeProfit()
    {
        Fleet fleet = CreateDefaultFleet();

        var contract = new TermContract(
            CreateDefaultBelt(),
            fleet,
            CreateDefaultPriceList(),
            4m);

        // 100% налог допустим по условию.
        var calculator = new Calculator(new Station(1m));

        CalculationResult result = calculator.Calculate(contract);

        Assert.True(result.IsSuccess);

        Assert.Equal(16000m, result.Revenue);
        Assert.Equal(4000m, result.RentalCost);
        Assert.Equal(16000m, result.Tax);
        Assert.Equal(-4000m, result.Profit);
    }

    private static Fleet CreateDefaultFleet()
    {
        return new Fleet(
            new List<IShip>
            {
                new Venture(),
            },
            new SharedHoldStrategy());
    }

    private static AsteroidBelt CreateDefaultBelt()
    {
        return new AsteroidBelt(
            "Пояс A",
            4m,
            new Veldspar());
    }

    private static Dictionary<IMineral, decimal> CreateDefaultPriceList()
    {
        return new Dictionary<IMineral, decimal>
        {
            [new Tritanium()] = 4m,
            [new Pyerite()] = 10m,
            [new Mexallon()] = 70m,
            [new Isogen()] = 150m,
        };
    }
}
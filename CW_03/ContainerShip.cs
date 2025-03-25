namespace CW_03;

public class ContainerShip
{
    private List<Container> containers;

    private double maxV { get; set; }
    private int maxCargoNumber { get; set; }
    private double maxLoad { get; set; }
    private string Nazwa { get; set; }

    public ContainerShip(string nazwa, double maxV, int maxCargoNumber, double maxLoad)
    {
        this.Nazwa = nazwa;
        this.maxV = maxV;
        this.maxCargoNumber = maxCargoNumber;
        this.maxLoad = maxLoad;
        containers = new List<Container>();
    }

    public void LoadContainer(Container container)
    {
        if (containers.Count >= maxCargoNumber)
        {
            throw new Exception($"Statek {Nazwa} osiągnął maksymalną liczbę kontenerów ({maxCargoNumber})");
        }

        var totalWeight = (containers.Sum(c => c.weight + c.masaCargo) + container.weight + container.masaCargo) / 1000.0;
        if (totalWeight > maxLoad)
        {
            throw new Exception($"Przekroczono maksymalną wagę {maxLoad} ton dla statku {Nazwa}");
        }

        containers.Add(container);
    }

    public void LoadListShip(List<Container> newContainers)
    {
        foreach (var container in newContainers) { LoadContainer(container); }
    }

    public void UnloadShipContainer(string serialNumber)
    {
        var container = containers.FirstOrDefault(c => c.SeryjnyNR == serialNumber);
        if (container != null)
        {
            containers.Remove(container);
            Console.WriteLine($"[Rozładowano kontener {serialNumber} ze statku {Nazwa}]");
        }
        else
        {
            throw new Exception($"Nie znaleziono kontenera {serialNumber} na statku {Nazwa}");
        }
    }

    public void ReplaceContainer(string oldSerialNumber, Container newContainer)
    {
        var index = containers.FindIndex(c => c.SeryjnyNR == oldSerialNumber);
        if (index != -1)
        {
            containers[index] = newContainer;
            Console.WriteLine($"[Zamieniono kontener {oldSerialNumber} na {newContainer.SeryjnyNR}]");
        }
        else
        {
            throw new Exception($"Nie znaleziono kontenera {oldSerialNumber} na statku {Nazwa}");
        }
    }

    public void TransferContainer(string serialNumber, ContainerShip targetShip)
    {
        var container = containers.FirstOrDefault(c => c.SeryjnyNR == serialNumber);
        if (container != null)
        {
            containers.Remove(container);
            targetShip.LoadContainer(container);
            Console.WriteLine($"[Przeniesiono kontener {serialNumber} ze statku {Nazwa} na statek {targetShip.Nazwa}]");
        }
        else
        {
            throw new Exception($"Nie znaleziono kontenera {serialNumber} na statku {Nazwa}");
        }
    }
    
    public void ShipInfo()
    {
        Console.WriteLine($"[Informacje o statku {Nazwa}:]");
        Console.WriteLine($"- Maksymalna prędkość: {maxV} węzłów");
        Console.WriteLine($"- Maksymalna liczba kontenerów: {maxCargoNumber}");
        Console.WriteLine($"- Maksymalna waga: {maxLoad} ton");
        
        double totalWeight = containers.Sum(c => c.weight + c.masaCargo) / 1000.0;
        Console.WriteLine($"- Aktualna waga: {totalWeight:F2} ton");
        
        Console.WriteLine("#Lista kontenerów:");
        foreach (var container in containers)
        {
            Console.WriteLine($"- {container.SeryjnyNR}: {container.GetType().Name}, ładunek: {container.masaCargo} kg");
        }
    }
}
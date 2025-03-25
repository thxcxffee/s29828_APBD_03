namespace CW_03;

public abstract class Container
{
    private static int Id = 0;
    
    public double masaCargo { get; set; }
    public double height { get; set; }
    public double weight { get; set; }
    public double depth { get; set; }
    public string SeryjnyNR { get; set; }
    public double maxLoad { get; set; }

    protected Container(double height, double weight, double depth, double maxLoad, string container)
    {
        this.height = height;
        this.weight = weight;
        this.depth = depth;
        this.maxLoad = maxLoad;
        SeryjnyNR = GenerateSerial(container);
        masaCargo = 0;
    }

    private static string GenerateSerial(string containerType)
    {
        Id++;
        return $"KON-{containerType}-{Id}";
    }

    public virtual void Unload()
    {
        Console.WriteLine($"Rozładowywanie kontenera {SeryjnyNR}.");
        masaCargo = 0;
    }

    public virtual void Load(double w)
    {
        if (w < 0)
        {
            throw new ArgumentException($"Waga cargo nie może być ujemna. Kontener: {SeryjnyNR}");
        }
        
        if (masaCargo + w > maxLoad)
        {
            throw new OverFillException($"Przekroczono maksymalny load {maxLoad}kg dla kontenera {SeryjnyNR}. " +
                                        $"Aktualna waga: {masaCargo}kg, próba dodania: {w}kg");
        }
        
        masaCargo += w;
        Console.WriteLine($"[Załadowano {w}kg do kontenera {SeryjnyNR}. Waga: {masaCargo}kg]");
    }
    
    public virtual void PrintInfo()
    {
        Console.WriteLine($"[Informacje o kontenerze {SeryjnyNR}:]");
        Console.WriteLine($"- Typ: {GetType().Name}");
        Console.WriteLine($"- Masa ładunku: {masaCargo} kg");
        Console.WriteLine($"- Waga kontenera: {weight} kg");
        Console.WriteLine($"- Wymiary: {height}x{depth} cm");
        Console.WriteLine($"- Maksymalna ładowność: {maxLoad} kg");
        Console.WriteLine($"- Aktualne wypełnienie: {(masaCargo/maxLoad)*100:F1}%");
    }
}
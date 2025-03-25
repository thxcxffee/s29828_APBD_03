namespace CW_03;

public class LiquidContainer : Container, HazardNotify
{
    private bool Hazard { get; set; }

    public LiquidContainer(double height, double weight, double depth, double maxLoad, bool hazard) 
        : base(height, weight, depth, maxLoad, "L")
    { Hazard = hazard; }

    public override void Load(double w)
    {
        var allowedLoad = Hazard ? maxLoad * 0.5 : maxLoad * 0.9;
        
        if (masaCargo + w > allowedLoad)
        {
            NotifyHazard(
                $"Naruszenie dozwolonego wypełnienia {(Hazard ? "50%" : "90%")}",
                SeryjnyNR
            );
            throw new OverFillException($"Przekroczono dozwolone wypełnienie {SeryjnyNR}");
        }
        base.Load(w);
    }
    
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"- Hazard: {(Hazard ? "Tak" : "Nie")}");
        Console.WriteLine($"- Maksymalne dozwolone wypełnienie: {(Hazard ? "50%" : "90%")}");
    }

    public void NotifyHazard(string message, string containerNumber)
    { Console.WriteLine($"UWAGA! Kontener na płyny {containerNumber}: {message}"); }
}
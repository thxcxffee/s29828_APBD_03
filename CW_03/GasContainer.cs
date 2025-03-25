namespace CW_03;

public class GasContainer : Container, HazardNotify
{
    private double Pressure { get; set; }
    private bool Hazard { get; set; }

    public GasContainer(double height, double weight, double depth, double maxLoad, double pressure, bool hazard) 
        : base(height, weight, depth, maxLoad, "G")
    {
        Pressure = pressure;
        Hazard = hazard;
        //Console.WriteLine($"[Kontener gazowy {SeryjnyNR} | ciśnienie: {Pressure}]");
    }

    public override void Unload()
    {
        Console.WriteLine($"[Rozładowywanie kontenera gazowego {SeryjnyNR}. Aktualna waga: {masaCargo}kg]");
        masaCargo = masaCargo * 0.05;
        Console.WriteLine($"[Kontener gazowy {SeryjnyNR} rozładowany. Pozostało 5% gazu: {masaCargo}kg]");
    }

    public override void Load(double w)
    {
        if (w > maxLoad)
        { 
            NotifyHazard(
                $"Load {w}kg przekracza dopuszczalną ładowność {maxLoad}kg dla kontenera {SeryjnyNR}",
                SeryjnyNR
            );
            throw new OverFillException($"Przekroczono dopuszczalną ładowność {maxLoad}kg dla kontenera {SeryjnyNR}");
        }
        base.Load(w);
    }
    
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"- Hazard: {(Hazard ? "Tak" : "Nie")}");
        Console.WriteLine($"- Ciśnienie: {Pressure}");
    }

    public void NotifyHazard(string message, string containerNumber)
    { Console.WriteLine($"UWAGA! Kontener gazowy {containerNumber}: {message}"); }
}
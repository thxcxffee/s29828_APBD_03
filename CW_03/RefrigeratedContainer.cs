namespace CW_03;

public class RefrigeratedContainer : Container
{
    private string Type { get; set; }
    private double Temperature { get; set; }

    public RefrigeratedContainer(double height, double weight, double depth, double maxLoad, string type, double temperature) 
        : base(height, weight, depth, maxLoad, "R")
    {
        if (!ProductTemperature.ProductList.ContainsKey(type))
        { throw new ArgumentException($"Niedozwolony typ produktu: {type} dla kontenera {SeryjnyNR}"); }
        
        var requiredTemp = ProductTemperature.ProductList[type];
        if (temperature > requiredTemp)
        {
            throw new ArgumentException(
                $"Temperatura {temperature}°C jest za wysoka dla {type} w kontenerze {SeryjnyNR}. " +
                $"Wymagana temperatura: {requiredTemp}°C lub niższa"
            );
        }

        Type = type;
        Temperature = temperature;
    }

    public override void Load(double w)
    {
        if (string.IsNullOrEmpty(Type))
        { throw new ArgumentException($"ERROR: brak typu produktu dla kontenera {SeryjnyNR}"); }
        base.Load(w);
    }
    
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"- Produktu: {Type}");
        Console.WriteLine($"- Temperatura: {Temperature}°C");
        
        if (ProductTemperature.ProductList.TryGetValue(Type, out var requiredTemp))
        { Console.WriteLine($"- Wymagana temperatura: {requiredTemp}°C"); }
    }
}

public static class ProductTemperature
{
    public static readonly Dictionary<string, double> ProductList = new()
    {
        {"Bananas", 13.3},
        {"Chocolate", 18},
        {"Fish", 2},
        {"Meat", -15},
        {"Ice cream", -18},
        {"Frozen pizza", -30},
        {"Cheese", 7.2},
        {"Sausages", 5},
        {"Butter", 20.5},
        {"Eggs", 19}
    };
}
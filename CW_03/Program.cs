using CW_03;

class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var ship1 = new ContainerShip("SS Kawka", 25, 25, 50);
            var ship2 = new ContainerShip("Europa", 22, 15, 40);
            
            var liquidContainer = new LiquidContainer(250, 1000, 200, 500, true);
            var gasContainer = new GasContainer(300, 1500, 200, 300, 2.5, false);
            var refrigeratedContainer = new RefrigeratedContainer(250, 2000, 200, 400, "Bananas", 13.0);
            var refrigeratedContainer1 = new RefrigeratedContainer(150, 1000, 100, 200, "Cheese", 4.0);
            
            liquidContainer.Load(200);
            gasContainer.Load(250);
            refrigeratedContainer.Load(300);
            refrigeratedContainer1.Load(200);
            ship1.LoadListShip(new List<Container>{liquidContainer,gasContainer,refrigeratedContainer});
            ship2.LoadContainer(refrigeratedContainer1);
            
            ship1.ShipInfo();
            ship2.ShipInfo();
            liquidContainer.PrintInfo();
            gasContainer.PrintInfo();
            refrigeratedContainer.PrintInfo();
            
            ship1.ReplaceContainer(refrigeratedContainer.SeryjnyNR, refrigeratedContainer1);
            ship1.TransferContainer(gasContainer.SeryjnyNR, ship2);
            ship1.UnloadShipContainer(liquidContainer.SeryjnyNR);
            ship1.ShipInfo();
            ship2.ShipInfo();
            
            gasContainer.Unload();
            gasContainer.PrintInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}
using DesignPrinciples.YAGNI.Good;

var inventory = new InventoryService();
inventory.Adjust("SKU-COFFEE-1KG", 24);
inventory.Adjust("SKU-COFFEE-1KG", -3);

Console.WriteLine($"Available stock: {inventory.GetAvailable("SKU-COFFEE-1KG")}");

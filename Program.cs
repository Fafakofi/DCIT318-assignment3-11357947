using System;
public class Program
{
    public static void Main(string[] args)
    {
        //Question one
        Console.WriteLine("Question one");
        FinanceApp app = new FinanceApp();
        app.Run();

        //Question two
        Console.WriteLine("Question two");
        HealthSystemApp healthApp = new HealthSystemApp();
        healthApp.SeedData();
        healthApp.BuildPrescriptionMap();
        healthApp.PrintAllPatients();
        Console.Write("Enter Patient ID to view prescriptions: ");
        if (int.TryParse(Console.ReadLine(), out int patientId))
        {
            healthApp.PrintPrescriptionsForPatient(patientId);
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        //Question three   
        Console.WriteLine("Question three");

        WareHouseManager manager1 = new WareHouseManager();
        manager1.SeedData();

        Console.WriteLine("\n--- Grocery Items ---");
        manager1.PrintAllItems(manager1.GroceriesRepo);

        Console.WriteLine("\n--- Electronic Items ---");
        manager1.PrintAllItems(manager1.ElectronicsRepo);

        Console.WriteLine("\n--- Testing Exceptions ---");
         try
        {
            manager1.ElectronicsRepo.AddItem(new ElectronicItem(1, "Tablet", 5, "Apple", 12));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        manager1.RemoveItemById(manager1.GroceriesRepo, 99); 
        manager1.IncreaseStock(manager1.ElectronicsRepo, 2, -50); 
    
    }
   

        }



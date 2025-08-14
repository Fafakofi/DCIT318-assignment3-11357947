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
    }
        }



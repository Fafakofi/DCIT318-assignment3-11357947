using System;
public class Repository<T>
{
    private readonly List<T> _items = new List<T>();

    public void Add(T item)
    {
        _items.Add(item);
    }
    public List<T> GetAll()
    {
        return new List<T>(_items);
    }
    public T? GetById(Func<T, bool> predicate)
    {
        return _items.FirstOrDefault(predicate);
    }
    public bool Remove(Func<T, bool> predicate)
    {
        var item = _items.FirstOrDefault(predicate);
        if (item != null)
        {
            _items.Remove(item);
            return true;
        }
        return false;
    }
}
    public class Patient
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }
        public string Gender { get; }
        public Patient(string name, int age, int id, string gender)
        {
            Name = name;
            Age = age;
            Id = id;
            Gender = gender;
        }
    }
    public class Prescription
    {
        public int Id { get; }
        public int PatientId { get; }
        public string MedicationName { get; }
        public DateTime DateIssued { get; }
        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        {
            Id = id;
            PatientId = patientId;
            MedicationName = medicationName;
            DateIssued = dateIssued;
        }
    }
    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo = new Repository<Patient>();
        private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
        private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();
    public void SeedData()
    {
        _patientRepo.Add(new Patient("Kwame", 30, 1, "Female"));
        _patientRepo.Add(new Patient("Jojo", 21, 2, "Male"));
        _patientRepo.Add(new Patient("Alice", 15, 3, "Male"));

        _prescriptionRepo.Add(new Prescription(1, 1, "Amoxicillin", DateTime.Now.AddDays(-10)));
        _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen", DateTime.Now.AddDays(-5)));
        _prescriptionRepo.Add(new Prescription(3, 2, "Paracetamol", DateTime.Now.AddDays(-7)));
        _prescriptionRepo.Add(new Prescription(4, 3, "Cetirizine", DateTime.Now.AddDays(-2)));
        _prescriptionRepo.Add(new Prescription(5, 1, "Vitamin C", DateTime.Now));
    }

       public void BuildPrescriptionMap()
    {
        _prescriptionMap.Clear();

        foreach (var prescription in _prescriptionRepo.GetAll())
        {
            if (!_prescriptionMap.ContainsKey(prescription.PatientId))
            {
                _prescriptionMap[prescription.PatientId] = new List<Prescription>();
            }
            _prescriptionMap[prescription.PatientId].Add(prescription);
        }
    }

    public List<Prescription> GetPrescriptionsByPatientId(int patientId)
    {
        if (_prescriptionMap.TryGetValue(patientId, out var prescriptions))
        {
            return prescriptions;
        }
        return new List<Prescription>(); 
    }

     public void PrintAllPatients()
    {
        Console.WriteLine("Patients:");
        foreach (var patient in _patientRepo.GetAll())
        {
            Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}, Gender: {patient.Gender}");
        }
    }

      public void PrintPrescriptionsForPatient(int id)
    {
        var prescriptions = GetPrescriptionsByPatientId(id);
        if (prescriptions.Count == 0)
        {
            Console.WriteLine($"No prescriptions found for Patient ID {id}.");
            return;
        }

        Console.WriteLine($"Prescriptions for Patient ID {id}:");
        foreach (var prescription in prescriptions)
        {
            Console.WriteLine($"ID: {prescription.Id}, Medication: {prescription.MedicationName}, Date: {prescription.DateIssued.ToShortDateString()}");
        }
    }
    }


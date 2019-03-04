namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Patient
    {
        public Patient()
        {
            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();
            this.Prescriptions = new HashSet<PatientMedicament>();
        }

        public Patient(string firstname, string lastname, string address, string email, bool insurance)
            : base()
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Address = address;
            this.Email = email;
            this.HasInsurance = insurance;
        }

        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}
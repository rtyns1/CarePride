using System;

namespace CarePride.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int ClassId { get; set; }

        private DateTime _enrollmentDate;
        public DateTime EnrollmentDate
        {
            get => _enrollmentDate;
            set
            {
                if (value > DateTime.UtcNow)
                    throw new ArgumentException("Enrollment date cannot be in the future.");
                _enrollmentDate = value;
            }
        }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual Student Student { get; set; }
        public virtual Class Class { get; set; }
    }
}
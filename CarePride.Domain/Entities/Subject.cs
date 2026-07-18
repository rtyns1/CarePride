using System;
using System.Collections.Generic;

namespace CarePride.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Subject name cannot be empty.");
                _name = value.Trim();
            }
        }

        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Subject code cannot be empty.");
                _code = value.Trim().ToUpper();
            }
        }

        public string Description { get; set; } = string.Empty;

        // Navigation Property
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
    }
}
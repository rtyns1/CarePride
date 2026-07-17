using System;
using System.Collections.Generic;
using System.Text;

namespace CarePride.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        // field first, then now access modifiers
        // we use encapsulation via properties to control the access to the fields
        //  => means "return" in alot of cases, but not always.

        private string? _firstName; // field-- where the data is actually stored in memory
        public string? FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty.");
                _firstName = value.Trim();
            }
        }
        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty.");
                _lastName = value.Trim(); //Trim removes all leadint and trailing whitespace 
                   
            }

        }
        private string? _email; // does a student really need an email? we can use the parent's email too i guess
        public string? Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty.");
                if (!value.Contains("@") || !value.Contains("."))
                    throw new ArgumentException("Invalid email format.");
                _email = value.Trim().ToLower();

            }                
        }
        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                var age =DateTime.Today.Year - value.Year;
                if (value.Date > DateTime.Today.AddYears(-age))
                    age--;

                if (age < 2)
                    throw new ArgumentException("Student must be atleast 5 years old.");

                _dateOfBirth = value;

            }

        }
        private int? _age;
        public int Age
        {
            // age is something that we fetch, calculate from the date of birth, its not something we set.
            // so we we only use the get access modifier.
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year; 
                if (DateOfBirth.Date > today.AddYears(-age)) // this means that if the birthday has not yet occured, we minus the birthday by one
                    age--;
                return age;
            }
            
        }
        // contact details and other stuff, incuding enrolllment metadata
        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number cannot be empty.");

                // .All() checks if every single character in the string is a number
                if (!value.All(char.IsDigit))
                    throw new ArgumentException("Phone number can only contain numerals (digits).");

                _phoneNumber = value.Trim();
            }
        }
        public string? Address { get; set; }
        public string? Gender { get; set; }

        public string? EmergencyConctactName { get; set; }
        private string? _emergencyContactNumber;
        public string? EmergencyContactNumber
        {
            get => _emergencyContactNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Emergency contact number cannot be empty.");

                if (!value.All(char.IsDigit))
                    throw new ArgumentException("Emergency contact number can only contain numerals (digits).");

                _emergencyContactNumber = value.Trim();
            }
        }
        public string? EmergencyContactRelationship { get; set; } // later add a dropdown option where they can selsect, mother, sibling,etc etc.

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow; // for when the student was enrolled into the system
        public bool IsActive { get; set; } // is the studetn an active student?
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // this is for when the student was created in the system, not when enrolled
        public DateTime? UpdatedAt { get; set; } = null; // when student profile was updated, can be null
        public DateTime? LastUpdatedAt { get; set; } = null;

        // Domain is like the guard that ensures that invalid data never enter my system.
        // Domain is responsible for keeping itself valid.
        // The others :: UI, API and database should never receive invalid data.

    }
}

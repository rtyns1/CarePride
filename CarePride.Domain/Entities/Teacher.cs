using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CarePride.Domain.Entities
{
    public class Teacher
    {
        public string? TeacherId { get; set; }
        public int? UserId { get; set; } // Nullable because a Teacher might not have a login yet, check user.cs class
        private string? _firstNameTeacher;
        public string? FirstName
        {
            get => _firstNameTeacher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty");
                _firstNameTeacher = value.Trim();

            }
        }
        private string? _lastNameTeacher;
        public string? LastName
        {
            get => _lastNameTeacher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty.");
                _lastNameTeacher = value.Trim();
            }
        }
        private string? _email;
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

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                var age = DateTime.Today.Year - value.Year;
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
        public DateTime HireDate{ get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true; 
    }
}

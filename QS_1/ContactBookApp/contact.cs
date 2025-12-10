using System;

namespace ContactBookApp
{
    // Represents one contact (encapsulation via properties)
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Company   { get; set; }
        public string Email     { get; set; }
        public DateTime Birthdate { get; set; }

        // Backing field for mobile to validate input
        private string mobileNumber;
        public string MobileNumber
        {
            get => mobileNumber;
            set
            {
                // Accept only exactly 9 digits, positive and non-zero
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Mobile number cannot be empty.");

                // remove spaces if any
                string trimmed = value.Trim();
                if (trimmed.Length != 9 || !long.TryParse(trimmed, out long n) || n <= 0)
                    throw new ArgumentException("Mobile number must be a non-zero 9-digit numeric value.");

                mobileNumber = trimmed;
            }
        }

        // Constructors
        public Contact() { }

        public Contact(string firstName, string lastName, string company,
                       string mobile, string email, DateTime birthdate)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            MobileNumber = mobile;   // setter validates
            Email = email;
            Birthdate = birthdate;
        }

        // Method overloading example: GetFullName()
        public string GetFullName() => $"{FirstName} {LastName}";

        public string GetFullName(bool lastNameFirst)
            => lastNameFirst ? $"{LastName}, {FirstName}" : $"{FirstName} {LastName}";
    }
}

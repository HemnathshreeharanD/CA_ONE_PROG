using System;
using System.Text.RegularExpressions;

namespace ContactBookApp
{
    // Represents one contact (encapsulation via properties)
    public class Contact
    {
        // Non-nullable properties with default values to avoid CS8618
        public string FirstName { get; set; } = "Unknown";
        public string LastName  { get; set; } = "Unknown";
        public string Company   { get; set; } = "Imported from CSV";
        public string Email     { get; set; } = "not_provided@csv.com";

        // Backing field for mobile to validate input
        private string mobileNumber = "0000000000";
        public string MobileNumber
        {
            get => mobileNumber;
            set
            {
                string trimmed = value?.Trim() ?? "";
                // Accept only exactly 10 digits, non-zero
                if (!Regex.IsMatch(trimmed, @"^[1-9][0-9]{9}$"))
                {
                    throw new ArgumentException(
                        "Mobile number must be a non-zero 10-digit numeric value."
                    );
                }
                mobileNumber = trimmed;
            }
        }

        // Birthdate default: 18 years ago (minimum adult age)
        public DateTime Birthdate { get; set; } = DateTime.Now.AddYears(-18);

        // Parameterless constructor (used for CSV loading, defaults apply)
        public Contact() { }

        // Full constructor
        public Contact(string firstName, string lastName, string company,
                       string mobile, string email, DateTime birthdate)
        {
            FirstName = firstName ?? "Unknown";
            LastName  = lastName ?? "Unknown";
            Company   = company ?? "Imported from CSV";
            MobileNumber = mobile ?? "0000000000"; // setter validates
            Email     = email ?? "not_provided@csv.com";
            Birthdate = birthdate < DateTime.Now.AddYears(-18) ? birthdate : DateTime.Now.AddYears(-18);
        }

        // Method overloading example: GetFullName()
        public string GetFullName() => $"{FirstName} {LastName}";

        public string GetFullName(bool lastNameFirst)
            => lastNameFirst ? $"{LastName}, {FirstName}" : $"{FirstName} {LastName}";
    }
}
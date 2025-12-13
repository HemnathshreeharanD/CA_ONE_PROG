using System;
using System.Text.RegularExpressions;

namespace ContactBookApp
{
    public class Contact
    {
        public string FirstName { get; set; } = "Unknown";
        public string LastName  { get; set; } = "Unknown";
        public string Company   { get; set; } = "Imported from CSV";
        public string Email     { get; set; } = "not_provided@csv.com";

        private string mobileNumber = "0000000000";
        public string MobileNumber
        {
            get => mobileNumber;
            set
            {
                string trimmed = value?.Trim() ?? "";
                if (!Regex.IsMatch(trimmed, @"^[1-9][0-9]{9}$"))
                {
                    throw new ArgumentException(
                        "Mobile number must be a non-zero 10-digit numeric value."
                    );
                }
                mobileNumber = trimmed;
            }
        }

        public DateTime Birthdate { get; set; } = DateTime.Now.AddYears(-18);

        public Contact() { }

        public Contact(string firstName, string lastName, string company,
                       string mobile, string email, DateTime birthdate)
        {
            FirstName = firstName ?? "Unknown";
            LastName  = lastName ?? "Unknown";
            Company   = company ?? "Imported from CSV";
            MobileNumber = mobile ?? "0000000000"; 
            Email     = email ?? "not_provided@csv.com";
            Birthdate = birthdate < DateTime.Now.AddYears(-18) ? birthdate : DateTime.Now.AddYears(-18);
        }

        public string GetFullName() => $"{FirstName} {LastName}";

        public string GetFullName(bool lastNameFirst)
            => lastNameFirst ? $"{LastName}, {FirstName}" : $"{FirstName} {LastName}";
    }

}

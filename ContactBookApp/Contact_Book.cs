using System;
using System.Collections.Generic;
using System.IO;

namespace ContactBookApp
{
    public class ContactBook
    {
        private List<Contact> contacts = new List<Contact>();

        // -------- LOAD CONTACTS FROM CSV --------
        public void LoadContactsFromCSV(string csvFilePath)
        {
            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine("CSV file not found.");
                return;
            }

            var lines = File.ReadAllLines(csvFilePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("FirstName")) // skip header
                    continue;

                var fields = line.Split(',');

                try
                {
                    string firstName = fields[0];
                    string lastName = fields[1];
                    string mobile = fields[2];
                    string company = fields[3];
                    string email = fields[4];
                    DateTime birthdate = DateTime.ParseExact(fields[5], "yyyy-MM-dd", null);

                    contacts.Add(new Contact(firstName, lastName, company, mobile, email, birthdate));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading contact: {ex.Message}");
                }
            }

            Console.WriteLine("Contacts loaded from CSV successfully.");
        }

        // -------- ADD CONTACT --------
        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }

        // -------- SHOW ALL CONTACTS --------
        public void ShowAllContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                return;
            }

            Console.WriteLine("\nIndex | Name                       | Mobile");
            Console.WriteLine("-------------------------------------------------");

            for (int i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"{i + 1,5} | {contacts[i].GetFullName(),-25} | {contacts[i].MobileNumber}");
            }
        }

        // -------- SHOW DETAILS --------
        public void ShowContactDetailsByIndex(int index)
        {
            if (!IsValidIndex(index))
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            var c = contacts[index - 1];

            Console.WriteLine("\n--- Contact Details ---");
            Console.WriteLine($"Name      : {c.GetFullName()}");
            Console.WriteLine($"Company   : {c.Company}");
            Console.WriteLine($"Mobile    : {c.MobileNumber}");
            Console.WriteLine($"Email     : {c.Email}");
            Console.WriteLine($"Birthdate : {c.Birthdate:yyyy-MM-dd}");
        }

        // -------- UPDATE CONTACT --------
        public void UpdateContactByIndex(int index, Contact updated)
        {
            if (!IsValidIndex(index))
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            contacts[index - 1] = updated;
            Console.WriteLine("Contact updated successfully.");
        }

        // -------- DELETE CONTACT --------
        public void DeleteContactByIndex(int index)
        {
            if (!IsValidIndex(index))
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            contacts.RemoveAt(index - 1);
            Console.WriteLine("Contact deleted successfully.");
        }

        private bool IsValidIndex(int index)
        {
            return index >= 1 && index <= contacts.Count;
        }
    }
}
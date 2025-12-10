using System;
using System.Collections.Generic;
using System.Globalization;

namespace ContactBookApp
{
    // Manages a collection of Contact objects
    public class ContactBook
    {
        // Contact collection (object relationship: ContactBook has-a List<Contact>)
        private List<Contact> contacts = new List<Contact>();

        // Load at least 20 sample contacts (call this once at startup)
        public void LoadSampleContacts()
        {
            // Create 20 distinct sample contacts
            for (int i = 1; i <= 20; i++)
            {
                // Ensure mobile is 9 digits (simple sample values)
                string mobile = (800000000 + i).ToString(); // e.g. "800000001"
                contacts.Add(new Contact(
                    $"Sample{i}",
                    $"User{i}",
                    "Dublin Business School",
                    mobile,
                    $"sample{i}@dbs.ie",
                    new DateTime(1990 + (i % 10), 1, 1)
                ));
            }
        }

        // Add contact by object
        public void AddContact(Contact c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            contacts.Add(c);
        }

        // Show all contacts (index + name + mobile)
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
                var c = contacts[i];
                Console.WriteLine($"{i + 1,5} | {c.GetFullName(),-25} | {c.MobileNumber}");
            }
        }

        // Show detailed info by index
        public void ShowContactDetailsByIndex(int index)
        {
            if (!IsValidIndex(index))
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            var c = contacts[index - 1];
            Console.WriteLine("\n--- Contact Details ---");
            Console.WriteLine($"Name     : {c.GetFullName()}");
            Console.WriteLine($"Company  : {c.Company}");
            Console.WriteLine($"Mobile   : {c.MobileNumber}");
            Console.WriteLine($"Email    : {c.Email}");
            Console.WriteLine($"Birthdate: {c.Birthdate.ToString("yyyy-MM-dd")}");
        }

        // Update contact by index (overloaded method example: update by index or by mobile)
        public void UpdateContactByIndex(int index, Contact updated)
        {
            if (!IsValidIndex(index))
            {
                Console.WriteLine("Invalid index.");
                return;
            }
            contacts[index - 1] = updated ?? throw new ArgumentNullException(nameof(updated));
            Console.WriteLine("Contact updated successfully.");
        }

        public void UpdateContactByMobile(string mobile, Action<Contact> updater)
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].MobileNumber == mobile)
                {
                    updater(contacts[i]);
                    Console.WriteLine("Contact updated successfully.");
                    return;
                }
            }
            Console.WriteLine("Contact with provided mobile not found.");
        }

        // Delete contact by index
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

        // Helper for index validation
        private bool IsValidIndex(int index) => index >= 1 && index <= contacts.Count;

        // Count
        public int Count() => contacts.Count;
    }
}

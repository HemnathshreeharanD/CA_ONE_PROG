using System;
using System.Globalization;
using System.IO;

namespace ContactBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactBook book = new ContactBook();

            // -------- Load contacts from CSV --------
            string csvFile = "contacts.csv";
            if (File.Exists(csvFile))
            {
                try
                {
                    foreach (var line in File.ReadAllLines(csvFile))
                    {
                        var fields = line.Split(',');

                        string firstName = fields[0]?.Trim() ?? "Unknown";
                        string lastName  = fields[1]?.Trim() ?? "Unknown";
                        string mobile    = fields[2]?.Trim() ?? "0000000000";
                        string company   = fields[3]?.Trim() ?? "Imported from CSV";
                        string email     = fields[4]?.Trim() ?? "not_provided@csv.com";
                        DateTime birthdate = DateTime.TryParseExact(fields[5], "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime dt) ? dt : DateTime.MinValue;

                        try
                        {
                            book.AddContact(new Contact(firstName, lastName, company, mobile, email, birthdate));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error loading contact: {ex.Message}");
                        }
                    }
                    Console.WriteLine("Contacts loaded from CSV successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading CSV: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("CSV file not found.");
            }

            // -------- Main menu loop --------
            while (true)
            {
                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("1: Add Contact");
                Console.WriteLine("2: Show All Contacts");
                Console.WriteLine("3: Show Contact Details");
                Console.WriteLine("4: Update Contact");
                Console.WriteLine("5: Delete Contact");
                Console.WriteLine("0: Exit");
                Console.WriteLine("------------------------");
                Console.Write("Enter your choice: ");

                string? choiceInput = Console.ReadLine();
                int choice = int.TryParse(choiceInput, out int c) ? c : -1;

                switch (choice)
                {
                    case 1: // Add Contact
                        AddContact(book);
                        break;

                    case 2: // Show All Contacts
                        book.ShowAllContacts();
                        break;

                    case 3: // Show Contact Details
                        Console.Write("Enter index: ");
                        int idx = int.TryParse(Console.ReadLine(), out int i) ? i : -1;
                        book.ShowContactDetailsByIndex(idx);
                        break;

                    case 4: // Update Contact
                        UpdateContact(book);
                        break;

                    case 5: // Delete Contact
                        Console.Write("Enter index to delete: ");
                        int dIdx = int.TryParse(Console.ReadLine(), out int di) ? di : -1;
                        book.DeleteContactByIndex(dIdx);
                        break;

                    case 0: // Exit
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddContact(ContactBook book)
        {
            Console.Write("First name: ");
            string first = Console.ReadLine()!;
            Console.Write("Last name: ");
            string last = Console.ReadLine()!;
            Console.Write("Company: ");
            string comp = Console.ReadLine()!;
            string mob = ReadValidMobile();
            Console.Write("Email: ");
            string email = Console.ReadLine()!;
            DateTime birthdate = ReadBirthdate();

            book.AddContact(new Contact(first, last, comp, mob, email, birthdate));
            Console.WriteLine("Contact added successfully.");
        }

        static void UpdateContact(ContactBook book)
        {
            Console.Write("Enter index to update: ");
            int uIdx = int.TryParse(Console.ReadLine(), out int ui) ? ui : -1;
            Console.Write("New First name: ");
            string nf = Console.ReadLine()!;
            Console.Write("New Last name: ");
            string nl = Console.ReadLine()!;
            Console.Write("New Company: ");
            string nc = Console.ReadLine()!;
            string nm = ReadValidMobile();
            Console.Write("New Email: ");
            string ne = Console.ReadLine()!;
            DateTime newBd = ReadBirthdate();

            book.UpdateContactByIndex(uIdx, new Contact(nf, nl, nc, nm, ne, newBd));
            Console.WriteLine("Contact updated successfully.");
        }

        static string ReadValidMobile()
        {
            while (true)
            {
                Console.Write("Mobile (9-10 digits, non-zero): ");
                string mob = Console.ReadLine()!;
                if (long.TryParse(mob, out long n) && (mob.Length == 9 || mob.Length == 10) && n > 0)
                    return mob;
                Console.WriteLine("Invalid mobile number. Try again.");
            }
        }

        static DateTime ReadBirthdate()
        {
            Console.Write("Birthdate (yyyy-MM-dd): ");
            string bdInput = Console.ReadLine()!;
            return DateTime.TryParseExact(bdInput, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime dt) ? dt : DateTime.MinValue;
        }
    }
}
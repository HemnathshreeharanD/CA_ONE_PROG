using System;
using System.Globalization;

namespace ContactBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactBook contactBook = new ContactBook();
            contactBook.LoadSampleContacts();  // load 20 sample contacts

            while (true)
            {
                Console.WriteLine("\n===== CONTACT BOOK MENU =====");
                Console.WriteLine("1. Show all contacts");
                Console.WriteLine("2. Show contact details");
                Console.WriteLine("3. Add new contact");
                Console.WriteLine("4. Update contact by index");
                Console.WriteLine("5. Delete contact");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        contactBook.ShowAllContacts();
                        break;

                    case "2":
                        ShowDetails(contactBook);
                        break;

                    case "3":
                        AddNewContact(contactBook);
                        break;

                    case "4":
                        UpdateContact(contactBook);
                        break;

                    case "5":
                        DeleteContact(contactBook);
                        break;

                    case "6":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        // ------------------- MENU METHODS -------------------

        static void ShowDetails(ContactBook book)
        {
            Console.Write("Enter index: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                book.ShowContactDetailsByIndex(index);
            else
                Console.WriteLine("Invalid input.");
        }

        static void AddNewContact(ContactBook book)
        {
            Console.Write("First name: ");
            string fn = Console.ReadLine();

            Console.Write("Last name: ");
            string ln = Console.ReadLine();

            Console.Write("Company: ");
            string company = Console.ReadLine();

            Console.Write("Mobile (9 digits): ");
            string mobile = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Birthdate (yyyy-MM-dd): ");
            DateTime birthdate;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, DateTimeStyles.None, out birthdate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            try
            {
                book.AddContact(new Contact(fn, ln, company, mobile, email, birthdate));
                Console.WriteLine("Contact added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void UpdateContact(ContactBook book)
        {
            Console.Write("Enter index to update: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            Console.Write("New First name: ");
            string fn = Console.ReadLine();

            Console.Write("New Last name: ");
            string ln = Console.ReadLine();

            Console.Write("New Company: ");
            string company = Console.ReadLine();

            Console.Write("New Mobile (9 digits): ");
            string mobile = Console.ReadLine();

            Console.Write("New Email: ");
            string email = Console.ReadLine();

            Console.Write("New Birthdate (yyyy-MM-dd): ");
            DateTime birthdate;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, DateTimeStyles.None, out birthdate))
            {
                Console.WriteLine("Invalid date.");
                return;
            }

            try
            {
                Contact updated = new Contact(fn, ln, company, mobile, email, birthdate);
                book.UpdateContactByIndex(index, updated);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void DeleteContact(ContactBook book)
        {
            Console.Write("Enter index to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                book.DeleteContactByIndex(index);
            else
                Console.WriteLine("Invalid input.");
        }
    }
}

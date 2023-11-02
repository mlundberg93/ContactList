using System.ComponentModel;
using System.Diagnostics;

namespace dtp6_contacts
{
    class MainClass
    {
        static bool isSaved = true;
        static List<Person> contactList = new List<Person>();
        class Person
        {
            public string persname, surname, phone, address, birthdate;
            public Person(string persname, string surname, string phone, string address, string birthdate)
            {
                this.persname = persname; this.surname = surname; this.phone = phone; this.address = address; this.birthdate = birthdate;
            }
            public Person()
            {

            }
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            MenuPrinter();
            do //REPL: programmets kärna som går runt i en loop tills den avslutas.
            {
                commandLine = Input("> ").Split(' ');
                if (commandLine[0] == "quit")
                {
                    Console.WriteLine("Do you want to save before qutting?");
                    string saveFile = Console.ReadLine();
                    if (saveFile == "yes")
                    {
                        SaveToContactFile(lastFileName);
                    }
                }
                //NYI New Person, skapar person med namn
                else if (commandLine[0] == "list")
                {
                    if (commandLine.Length == 1)
                    {
                        PrintList();
                    }
                    else
                    {
                        PrintPerson();
                    }
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2)
                        lastFileName = "address.lis";
                    else
                        lastFileName = commandLine[1];
                    FileLoader(lastFileName);
                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        SaveToContactFile(lastFileName);
                    }
                    else
                    {
                        lastFileName = commandLine[1];
                        SaveToContactFile(lastFileName);
                    }
                }
                else if (commandLine[0] == "new")
                {
                    AddPerson(commandLine);
                }
                else if (commandLine[0] == "delete") //"delete person"
                {
                    DeleteFunction(commandLine);
                }
                else if (commandLine[0] == "edit")
                {
                    EditPerson();
                }
                else if (commandLine[0] == "help")
                {
                    MenuPrinter();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit"); //Här avbryts REPL, programmet stängs av.
        }

        private static void PrintPerson()
        {
            Console.WriteLine("Which name do you want print?");
            string userInput = Console.ReadLine();
            for (int idx = 0; idx < contactList.Count; idx++)
            {
                if (userInput == contactList[idx].persname)
                {
                    Console.WriteLine($"{contactList[idx].persname}, {contactList[idx].surname}, " +
                        $"{contactList[idx].phone}, {contactList[idx].address}, {contactList[idx].birthdate}");
                }
            }
        }//Metod för att printa ut ett specifikt namn

        private static void DeleteFunction(string[] commandLine)
        {
            if (commandLine.Length == 1)
            {
                contactList.Clear();
                Console.WriteLine("The list has now been deleted!");
            }
            else //"Delete person"
            {
                Console.WriteLine("Type the number of the person you want to delete");
                int userInput = int.Parse(Console.ReadLine());
                contactList.RemoveAt(userInput);
            }
            isSaved = false;
        }//Metod för att ta bort personer från listan

        private static void EditPerson()
        {
            Console.WriteLine("Enter the number of the person you want to edit");
            int userInput = int.Parse(Console.ReadLine());
            Person p = contactList[userInput];
            Console.WriteLine("Type the new name, if you don't want to change the name press enter");
            string newName = Console.ReadLine();
            if (newName != "")
            {
                p.persname = newName;
            }
            Console.WriteLine("Type the new surname, if you don't want to change it press enter");
            string newSurname = Console.ReadLine();
            if (newSurname != "")
            {
                p.surname = newSurname;
            }
            Console.WriteLine("Type the new phone number, if you don't want to change it press enter");
            string newPhone = Console.ReadLine();
            if (newPhone != "")
            {
                p.phone = newPhone;
            }
            Console.WriteLine("Type the new address, if you don't want to change it press enter");
            string newAddress = Console.ReadLine();
            if (newAddress != "")
            {
                p.address = newAddress;
            }
            isSaved = false;
        }//Metod för att redigera personer i listan

        static void PrintList()
        {
            int idx = 0;
            foreach (var p in contactList)
            {
                Console.WriteLine($"{idx}. {p.persname}, {p.surname}, {p.phone}, {p.address}, {p.birthdate}");
                idx++;
            }
        }//Metod för att printa ut listan.
        static void MenuPrinter()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  list        - prints the list with all the persons");
            Console.WriteLine("  new         - create new person");
            Console.WriteLine("  delete      - deletes the entire list");
            Console.WriteLine("  index delete    - deletes the person from the list");
            Console.WriteLine("  edit        - opens the file with an editor");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save        - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
        } //En metod för att samla ihop då den används på mer än ett ställe.

        private static void SaveToContactFile(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(lastFileName))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate}");
                }
            }
            isSaved = false;
        }//Metod som sparar kontaktlistan till fil

        private static void FileLoader(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(lastFileName))  //TODO felhantering vid fel filnamn
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] attrs = line.Split('|');
                    Person p = new Person();
                    p.persname = attrs[0];
                    p.surname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    p.phone = phones[0];
                    string[] addresses = attrs[3].Split(';');
                    p.address = addresses[0];
                    string[] birthdate = attrs[4].Split(";");
                    p.birthdate = birthdate[0];
                    contactList.Add(p);
                }
            }
            isSaved = false;
        }//Metod som laddar upp data från fil.

        static string Input(string prompt) //Metod för att korta ner rader med återkommande kod om utskrifter
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private static void AddPerson(string[] commandLine)
        {
            if (commandLine.Length < 2)
            {
                string persname = Input("Personal name: ");
                string surname = Input("Surname: ");
                string phone = Input("Phone: ");
                string address = Input("Address: ");
                string birthdate = Input("Birthdate: ");
                contactList.Add(new Person(persname, surname, phone, address, birthdate));
            }
            else
            {
                // NYI!
                Console.WriteLine("Not yet implemented: new /person/");
            }
            isSaved = false;
        }//En metod för att lägga till en ny kontakt


    }
}

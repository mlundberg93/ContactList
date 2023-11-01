using System.ComponentModel;
using System.Diagnostics;

namespace dtp6_contacts
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
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
                    // NYI!
                    Console.WriteLine("Not yet implemented: safe quit");
                }
                //NYI New Person, skapar person med namn


                //NYI List Person, skriv ut personer med samma namn-

                //NYI Delete Person, ska ta bort en person från listan.

                //NYI Save File, spara listan på angiven fil.
                //NYI Safe Quit, om filen inte är sparad, ska programmet fråga om spara innan avslut.
                else if (commandLine[0] == "list")
                {
                    PrintList();
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
                        // NYI!
                        Console.WriteLine("Not yet implemented: save /file/");
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

        private static void DeleteFunction(string[] commandLine)
        {
            if (commandLine.Length == 1)
            {
                Array.Clear(contactList);
                Console.WriteLine("The list has now been deleted!");
            }
            else //"Delete person"
            {
                Console.WriteLine("Type the number of the person you want to delete");
                int userInput = int.Parse(Console.ReadLine());
                contactList[userInput] = null;
            }
        }

        private static void EditPerson()
        {
            Console.WriteLine("Enter the number of the person you want to edit");
            int userInput = int.Parse(Console.ReadLine());
            Console.WriteLine("Type the new name, if you don't want to change the name press enter");
            string newName = Console.ReadLine();
            if (newName != "")
            {
                contactList[userInput].persname = newName;
            }
            Console.WriteLine("Type the new surname, if you don't want to change it press enter");
            string newSurname = Console.ReadLine();
            if (newSurname != "")
            {
                contactList[userInput].surname = newSurname;
            }
            Console.WriteLine("Type the new phone number, if you don't want to change it press enter");
            string newPhone = Console.ReadLine();
            if (newPhone != "")
            {
                contactList[userInput].phone = newPhone;
            }
            Console.WriteLine("Type the new address, if you don't want to change it press enter");
            string newAddress = Console.ReadLine();
            if (newAddress != "")
            {
                contactList[userInput].address = newAddress;
            }
        }

        static void PrintList()
        {
            for (int i = 0; i < contactList.Length; i++)
            {
                Person p = contactList[i];
                if (p != null)
                Console.WriteLine($"{i}. {p.persname}, {p.surname}, {p.phone}, {p.address}, {p.birthdate}");
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
                    for (int ix = 0; ix < contactList.Length; ix++)
                    {
                        if (contactList[ix] == null)
                        {
                            contactList[ix] = p;
                            break;
                        }
                    }
                }
            }
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
                Person p = new Person(persname, surname, phone, address, birthdate);
                for (int idx = 0; idx < contactList.Length; idx++)
                {
                    if (contactList[idx] == null)
                    {
                        contactList[idx] = p;
                        break;
                    }
                }
            } 
            else
            {
                // NYI!
                Console.WriteLine("Not yet implemented: new /person/");
            }
        }//En metod för att lägga till en ny kontakt


    }
}

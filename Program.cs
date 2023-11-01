namespace dtp6_contacts
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, phone, address, birthdate;
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
                //NYI List, ska skriva ut hela listan.
                //NYI List Person, skriv ut personer med samma namn-
                //NYI Delete, ska rensa hela listan.
                //NYI Delete Person, ska ta bort en person från listan.
                //NYI Edit Person, redigera en person i listan.
                //NYI Save File, spara listan på angiven fil.
                //NYI Safe Quit, om filen inte är sparad, ska programmet fråga om spara innan avslut.
                else if (commandLine[0] == "load")
                {
                    lastFileName = LoadFile(commandLine);
                }
                else if (commandLine[0] == "save")
                {
                    SaveFunction(lastFileName, commandLine);
                }
                else if (commandLine[0] == "new")
                {
                    AddPerson(commandLine);
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

            static void MenuPrinter()
            {
                Console.WriteLine("Avaliable commands: ");
                Console.WriteLine("  load        - load contact list data from the file address.lis");
                Console.WriteLine("  load /file/ - load contact list data from the file");
                Console.WriteLine("  new        - create new person");
                Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
                Console.WriteLine("  quit        - quit the program");
                Console.WriteLine("  save         - save contact list data to the file previously loaded");
                Console.WriteLine("  save /file/ - save contact list data to the file");
                Console.WriteLine();
            } //En metod för att samla ihop då den används på mer än ett ställe.

            static void SaveFunction(string lastFileName, string[] commandLine) 
            {
                if (commandLine.Length < 2)
                {
                    using (StreamWriter outfile = new StreamWriter(lastFileName))
                    {
                        foreach (Person p in contactList)
                        {
                            if (p != null)
                                outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate}");
                        }
                    }
                }
                else
                {
                    // NYI!
                    Console.WriteLine("Not yet implemented: save /file/");
                }
            }//En metod för att spara kontaktlistan
        }
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
            } //NYI: Lägg till i telefon listan
            else
            {
                // NYI!
                Console.WriteLine("Not yet implemented: new /person/");
            }
        }//En metod för att lägga till en ny kontakt

        private static string LoadFile(string[] commandLine)
        {
            string lastFileName;
            if (commandLine.Length < 2)
            {
                lastFileName = "address.lis";


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

            }
            else
            {
                lastFileName = commandLine[1];
                try
                {
                    using (StreamReader infile = new StreamReader(lastFileName))
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
                }
                catch (System.IO.FileNotFoundException exc)
                {
                    Console.WriteLine(exc.Message);
                }

            }

            return lastFileName;
        }//En metod för att ladda in listan till programmet.
    }
}

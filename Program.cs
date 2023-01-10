using CRUD_CredentialsSaver;

while (true)
{
    Console.Clear();
    Console.WriteLine("--- Main Menu ---");
    Console.WriteLine("1. Add new credential");
    Console.WriteLine("2. Update credential");
    Console.WriteLine("3. Delete credential");
    Console.WriteLine("4. Show credential");
    Console.WriteLine("5. Exit");
    Console.Write("Enter your choice: ");

    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("You selected Option 1. (INSERT)");
            CRUD.Insert();
            break;
        case "2":
            Console.Clear();
            Console.WriteLine("You selected Option 2. (UPDATE)");
            CRUD.GetAllRecords();
            CRUD.Update();
            break;
        case "3":
            Console.Clear();
            Console.WriteLine("You selected Option 3.(DELETE)");
            CRUD.GetAllRecords();
            CRUD.Delete();
            break;
        case "4":
            Console.Clear();
            Console.WriteLine("You selected Option 4.(SELECT)");
            CRUD.GetAllRecords();
            Console.ReadLine();
            break;
        case "5":
            return;
        default:
            Console.Clear();
            Console.WriteLine("Invalid input.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            break;
    }
}

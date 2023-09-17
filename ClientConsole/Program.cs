using ClientConsole;
using Models;
using Repository;

public class Program
{
    static RestService rest;
    static void Create(string entity)
    {
        if (entity == "User")
        {
            Console.Write("Enter Username: ");
            string name = Console.ReadLine();
            rest.Post(new User() { Username = name }, "users");
        }
        else if (entity == "Task")
        {
            Console.Write("Enter Task title: ");
            string title = Console.ReadLine();
            rest.Post(new Models.Task() { Title = title }, "tasks");
        }
    }
    static void List(string entity)
    {
        if (entity == "User")
        {
            List<User> Users = rest.Get<User>("users");
            foreach (var item in Users)
            {
                Console.WriteLine(item.Id + ": " + item.Username);
            }
        }
        else if (entity == "Task")
        {
            List<Models.Task> Tasks = rest.Get<Models.Task>("tasks");
            foreach (var item in Tasks)
            {
                Console.WriteLine(item.Id + ": " + item.Title);
            }
        }
        Console.ReadLine();
    }
    static void Update(string entity)
    {
        if (entity == "User")
        {
            Console.Write("Enter User's id to update: ");
            int id = int.Parse(Console.ReadLine());
            User one = rest.Get<User>(id, "users");
            Console.Write($"New name [old: {one.Username}]: ");
            string name = Console.ReadLine();
            one.Username = name;
            rest.Put(one, "users");
        }
        else if (entity == "Task")
        {
            Console.Write("Enter Task's id to update: ");
            int id = int.Parse(Console.ReadLine());
            Models.Task one = rest.Get<Models.Task>(id, "tasks");
            Console.Write($"New name [old: {one.Title}]: ");
            string title = Console.ReadLine();
            one.Title = title;
            rest.Put(one, "tasks");
        }
    }
    static void Delete(string entity)
    {
        if (entity == "User")
        {
            Console.Write("Enter User's id to delete: ");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "users");
        }
        else if (entity == "Task")
        {
            Console.Write("Enter Task's id to delete: ");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "tasks");
        }
    }
    static void Main(string[] args)
    {
        DatabaseContext db = new();
        var u = db.Users.ToList();
        var t = db.Tasks.ToList();
        foreach (var item in u)
        {
            Console.WriteLine($"{item.Id}: {item.Username}");
        }
        Console.WriteLine("Finished");
        foreach (var item in t)
        {
            Console.WriteLine($"{item.Id}: {item.Title}");
        }
        Console.WriteLine("Finished");
        rest = new RestService("http://localhost:59674/", "users");
    }
}

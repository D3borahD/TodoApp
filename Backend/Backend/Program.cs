using System.Text.Json;

public enum ImportanceLevel 
{
    Minor, 
    Medium,
    High
}

public class Todo
{
    public int Id { get; set; }
    public string Description { get; set; }
    // transformer en enum 
    public string ImportanceLevel { get; set; }
}

internal class Program
{
    public static void Main(string[] args)
    {
        string path = "/Users/deborah/Documents/dev/TodoApp/Backend/datas";
       
        List<Todo> todos = new List<Todo>();
        
        // à supprimer qd front en place
        Console.WriteLine("saisir une tache : ");
        Todo todo = new Todo()
        {
            Id = 0,
            Description = Console.ReadLine(),
            ImportanceLevel = nameof(ImportanceLevel.Minor),
        };
            
        string jsonString = JsonSerializer.Serialize(todo);
        todos.Add(todo);

        try
        {
            using (StreamWriter fileStream = File.AppendText(path))
            {
                if (jsonString != null)
                {
                    fileStream.WriteLine($"{jsonString};");
                }
            }

            // à supprimer qd front en place
            using (StreamReader streamReader = File.OpenText(path))
            {
                string str = "";
                while ((str = streamReader.ReadLine()) != null)
                { 
                    Console.WriteLine(str);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}




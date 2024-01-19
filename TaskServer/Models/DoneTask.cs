namespace TaskServer.Models;

public class DoneTask
{
    public long? Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }

    public DateOnly DoneDate { get; set; }
}
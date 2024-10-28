namespace TodoApi.Models;

public class Reserve
{
    public int Id { get; set; }
    public int VisningsId { get; set; }
    public Visning? Visning { get; set; }
    public int ReservationsCount { get; set; }
}


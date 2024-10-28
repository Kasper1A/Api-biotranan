namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SalongController : ControllerBase
{


    [HttpPost]
    public ActionResult AddSalong([FromBody] Salong salong)
    {
        using (var context = new TodoDbContext())
        {
            context.Salongs.Add(salong);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetSalong), new { id = salong.Id }, salong);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Salong> GetSalong(int id)
    {
        using (var context = new TodoDbContext())
        {
            var salong = context.Salongs.Single(s => s.Id == id);
            return Ok(salong);
        }
    }
    [HttpPut("{id}")]
    public ActionResult UpdateSalong(int id, [FromBody] Salong updatedSalong)
    {
        using (var context = new TodoDbContext())
        {
            // Hämta salongen som ska uppdateras från databasen
            var existingSalong = context.Salongs.SingleOrDefault(s => s.Id == id);

            if (existingSalong == null)
            {
                return NotFound($"Salong with ID {id} not found.");
            }

            // Uppdatera fälten med de nya värdena
            existingSalong.Name = updatedSalong.Name;
            existingSalong.MaxSeats = updatedSalong.MaxSeats;

            // Spara ändringarna i databasen
            context.SaveChanges();

            return NoContent(); // Returnera 204 No Content som indikerar att uppdateringen lyckades
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteSalong(int id)
    {
        using (var context = new TodoDbContext())
        {
            // Hämta salongen som ska tas bort från databasen
            var salong = context.Salongs.SingleOrDefault(s => s.Id == id);

            if (salong == null)
            {
                return NotFound($"Salong with ID {id} not found.");
            }

            // Ta bort salongen från databasen
            context.Salongs.Remove(salong);
            context.SaveChanges();

            // Returnera 204 No Content som indikerar att borttagningen lyckades
            return NoContent();
        }
    }


}

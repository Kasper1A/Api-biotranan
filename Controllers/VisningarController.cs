using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi;
using TodoApi.Models;

[Route("api/[controller]")]
[ApiController]
public class VisningarController : ControllerBase
{
    // GET: api/Visningar
    [HttpGet]
    public ActionResult<IEnumerable<Visning>> GetVisningar()
    {
        List<Visning> visningar;
        using (var context = new TodoDbContext())
        {
            // Hämta alla visningar och inkludera relaterade filmer
            visningar = context.Visnings
                .Include(v => v.Salong)
                .Include(v => v.Movie)
                .ToList();
        }
        return Ok(visningar);
    }

    // POST: api/Visningar
    [HttpPost]
    public async Task<ActionResult<Visning>> AddVisning([FromBody] CreateVisningarDto visningData)
    {
        using (var context = new TodoDbContext())
        {
            // Validera att visningData inte är null och att tiden är angiven
            if (visningData == null || visningData.Time == default)
            {
                return BadRequest("Invalid visning data. Please provide both a valid movie ID, salong ID, and time.");
            }

            // Kontrollera antalet befintliga visningar för samma film (MovieId)
            int existingShowingsCount = context.Visnings
                                               .Count(v => v.MovieId == visningData.MovieId);

            // Begränsa till max 3 visningar per film (MovieId)
            if (existingShowingsCount >= 3)
            {
                // Returnera ett felmeddelande om gränsen är nådd
                return BadRequest("Det går inte att lägga till fler än tre visningar för denna film.");
            }

            // Skapa en ny visning
            var newVisning = new Visning
            {
                Time = visningData.Time,
                SalongId = visningData.SalongId,
                MovieId = visningData.MovieId,
                ReservedSeats = 0
            };

            // Lägg till den nya visningen om gränsen inte har nåtts
            context.Visnings.Add(newVisning);
            await context.SaveChangesAsync();

            // Returnera den nya visningen med CreatedAtAction för att visa var den skapades
            return CreatedAtAction(nameof(GetVisningar), new { id = newVisning.Id }, newVisning);
        }


    }
    [HttpGet("{id}/available-seats")]
    public ActionResult<AvailableSeatsDto> GetAvailableSeats(int id)
    {
        using (var context = new TodoDbContext())
        {
            var visning = context.Visnings.Include(v => v.Salong).SingleOrDefault(v => v.Id == id);
            if (visning == null)
            {
                return NotFound();
            }
            var availableSeatsDto = new AvailableSeatsDto();
            availableSeatsDto.Seats = visning.Salong.MaxSeats - visning.ReservedSeats;

            return Ok(availableSeatsDto);
        }
    }
    // DELETE: api/Visningar/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVisning(int id)
    {
        using (var context = new TodoDbContext())
        {
            // Hitta visningen i databasen baserat på id
            var visning = await context.Visnings.FindAsync(id);

            // Om visningen inte finns, returnera NotFound
            if (visning == null)
            {
                return NotFound($"Visning med id {id} hittades inte.");
            }

            // Ta bort visningen från databasen
            context.Visnings.Remove(visning);
            await context.SaveChangesAsync();

            // Returnera NoContent för att visa att raderingen lyckades
            return NoContent();
        }
    }

}
public class AvailableSeatsDto
{
    public int Seats { get; set; }
}

public class CreateVisningarDto
{
    public int MovieId { get; set; }
    public int SalongId { get; set; }
    public DateTime Time { get; set; }
}
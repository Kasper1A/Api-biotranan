using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ReserveController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> ReserveVisning([FromBody] ReserveDto dto)
    {
        using (var context = new TodoDbContext())
        {
            var reserve = new Reserve();
            reserve.VisningsId = dto.VisningsId;
            reserve.ReservationsCount = dto.Seats;

            var visning = await context.Visnings.FirstOrDefaultAsync<Visning>(v => v.Id == dto.VisningsId);
            if (visning == null)
            {
                return NotFound("Visning not found");
            }
            visning.ReservedSeats += reserve.ReservationsCount;

            context.Reservs.Add(reserve);
            context.SaveChanges();
            return Ok(reserve);
        }
    }

    [HttpGet]
    public List<Reserve> getAllReserves()
    {
        using (var context = new TodoDbContext())
        {
            return context.Reservs.ToList<Reserve>();
        }
    }
}

public class ReserveDto
{
    public int VisningsId { get; set; }
    public int Seats { get; set; }
}
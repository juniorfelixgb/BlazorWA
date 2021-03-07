using Microsoft.AspNetCore.Mvc;
using HotelReservations.Shared.Models;
using System.Collections.Generic;
using HotelReservations.Shared.Repository;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;

namespace HotelReservations.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IRepository _repo;
        public ReservationsController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return _repo.Reservations;
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int Id)
        {
            if (Id == 0)
                return BadRequest("Value must be passed in the request body");
            return Ok(_repo[Id]);
        }

        [HttpPost]
        public Reservation Post([FromBody] Reservation reservation) =>
            _repo.AddReservation(new Reservation 
            {
                Name = reservation.Name,
                StartLocation = reservation.StartLocation,
                EndLocation = reservation.EndLocation
            });
        
        [HttpPut]
        public Reservation Put([FromBody] Reservation reservation) => _repo.UpdateReservation(reservation);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int Id, [FromBody]JsonPatchDocument<Reservation> patch)
        {
            var res = (Reservation)((OkObjectResult)Get(Id).Result).Value;
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _repo.DeleteReservation(id);
    }
}
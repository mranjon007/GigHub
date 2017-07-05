using System.Linq;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Dtos;

namespace GigHub.Controllers
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a=>a.AttendeeId == userId && a.GigId == dto.GigId))
            {
                return BadRequest("User Already exists");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId(),
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}

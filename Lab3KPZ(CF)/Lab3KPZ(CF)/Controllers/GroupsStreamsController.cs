using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab3KPZ_CF_.Controllers
{
   /* [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Обмеження доступу тільки для адміністраторів
    public class GroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(GroupViewModel model)
        {
            var group = new Group
            {
                GroupName = model.GroupName,
                StreamID = model.StreamID
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return Ok("Group created successfully.");
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Обмеження доступу тільки для адміністраторів
    public class StreamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StreamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStream(StreamViewModel model)
        {
            var stream = new Lab3KPZ_CF_.Data.Stream
            {
                StreamName = model.StreamName,
                Year = model.Year,
                CourseID = model.CourseID
            };

            _context.Streams.Add(stream);
            await _context.SaveChangesAsync();

            return Ok("Stream created successfully.");
        }
    }
   */
}

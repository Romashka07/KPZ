using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3KPZ_CF_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GroupsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Groups
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> GetGroups([FromQuery] int? streamId = null)
        {
            // Фільтруємо групи за StreamID, якщо параметр передано
            var groupsQuery = _context.Groups.Include(g => g.Stream).AsQueryable();

            if (streamId.HasValue)
            {
                groupsQuery = groupsQuery.Where(g => g.StreamID == streamId);
            }

            var groups = await groupsQuery.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<GroupViewModel>>(groups));
        }


        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            var group = await _context.Groups.Include(g => g.Stream).FirstOrDefaultAsync(g => g.GroupID == id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GroupViewModel>(group));
        }

        // POST: api/Groups
        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> PostGroup(GroupViewModel groupViewModel)
        {
            var group = _mapper.Map<Group>(groupViewModel);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroup), new { id = group.GroupID }, _mapper.Map<GroupViewModel>(group));
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, GroupViewModel groupViewModel)
        {
            if (id != groupViewModel.GroupID)
            {
                return BadRequest();
            }

            var group = _mapper.Map<Group>(groupViewModel);
            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.GroupID == id);
        }
    }
}

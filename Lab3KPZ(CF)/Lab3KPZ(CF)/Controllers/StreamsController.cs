using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stream = Lab3KPZ_CF_.Data.Stream;

namespace Lab3KPZ_CF_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StreamsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Streams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StreamViewModel>>> GetStreams([FromQuery] int? courseId)
        {
            IQueryable<Stream> query = _context.Streams;

            // Якщо переданий courseId, додаємо фільтр
            if (courseId.HasValue)
            {
                query = query.Where(s => s.CourseID == courseId);
            }

            var streams = await query.ToListAsync();

            // Мапимо результати в StreamViewModel
            return Ok(_mapper.Map<IEnumerable<StreamViewModel>>(streams));
        }



        // GET: api/Streams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StreamViewModel>> GetStream(int id)
        {
            var stream = await _context.Streams.Include(s => s.Course).FirstOrDefaultAsync(s => s.StreamID == id);
            if (stream == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StreamViewModel>(stream));
        }

        // POST: api/Streams
        [HttpPost]
        public async Task<ActionResult<StreamViewModel>> PostStream(StreamViewModel streamViewModel)
        {
            if (streamViewModel.CourseID == null)
            {
                return BadRequest("Курс не вказано.");
            }

            var stream = _mapper.Map<Stream>(streamViewModel);
            _context.Streams.Add(stream);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStream), new { id = stream.StreamID }, _mapper.Map<StreamViewModel>(stream));

        }

        // PUT: api/Streams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStream(int id, StreamViewModel streamViewModel)
        {
            if (id != streamViewModel.StreamID)
            {
                return BadRequest();
            }

            var stream = _mapper.Map<Stream>(streamViewModel);
            _context.Entry(stream).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreamExists(id))
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

        // DELETE: api/Streams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStream(int id)
        {
            var stream = await _context.Streams.FindAsync(id);
            if (stream == null)
            {
                return NotFound();
            }

            _context.Streams.Remove(stream);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StreamExists(int id)
        {
            return _context.Streams.Any(e => e.StreamID == id);
        }

        // GET: api/Streams/GroupedByYear
        [HttpGet("GroupedByYear")]
        public async Task<ActionResult<IEnumerable<object>>> GetStreamsGroupedByYear(int courseId)
        {
            var streams = await _context.Streams
                .Where(s => s.CourseID == courseId)
                .OrderBy(s => s.Year)
                .GroupBy(s => s.Year)
                .Select(group => new
                {
                    Year = group.Key,
                    Streams = group.Select(s => new
                    {
                        s.StreamID,
                        s.StreamName
                    }).ToList()
                })
                .ToListAsync();

            return Ok(streams);
        }

    }
}
using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3KPZ_CF_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CoursesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseViewModel>>> GetCourses()
        {
            var courses = await _context.Courses
                .Select(c => new CourseViewModel
                {
                    CourseID = c.CourseID,
                    CourseName = c.CourseName
                })
                .ToListAsync();

            return Ok(courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseViewModel>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Where(c => c.CourseID == id)
                .Select(c => new CourseViewModel
                {
                    CourseID = c.CourseID,
                    CourseName = c.CourseName
                })
                .FirstOrDefaultAsync();

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseViewModel>> PostCourse(CourseViewModel courseViewModel)
        {
            var course = _mapper.Map<Course>(courseViewModel);
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseID }, _mapper.Map<CourseViewModel>(course));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseViewModel courseViewModel)
        {
            if (id != courseViewModel.CourseID)
            {
                return BadRequest("ID mismatch between URL and body.");
            }

            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            _mapper.Map(courseViewModel, existingCourse);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving course: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

            return NoContent();
        }


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }
    }
}

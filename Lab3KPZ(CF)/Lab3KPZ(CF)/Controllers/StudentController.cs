using AutoMapper;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3KPZ_CF_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> GetStudents([FromQuery] int? groupId = null)
        {
            var studentsQuery = _context.Students
                .Include(s => s.User)        // Завантажуємо інформацію про User
                .Include(s => s.Group)       // Завантажуємо групу для кожного студента
                .AsQueryable();

            // Фільтрація по групі, якщо параметр groupId передано
            if (groupId.HasValue)
            {
                studentsQuery = studentsQuery.Where(s => s.GroupID == groupId.Value);
            }

            var students = await studentsQuery.ToListAsync();

            var studentViewModels = _mapper.Map<IEnumerable<StudentViewModel>>(students);

            // Додаємо статус і назву групи в кожен студентський ViewModel
            foreach (var student in studentViewModels)
            {
                student.Status = students.FirstOrDefault(s => s.StudentID == student.StudentID)?.Status;
                student.GroupName = students.FirstOrDefault(s => s.StudentID == student.StudentID)?.Group?.GroupName;
            }

            return Ok(studentViewModels);
        }




        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudent(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentViewModel>(student));
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<StudentViewModel>> PostStudent(StudentViewModel studentViewModel)
        {
            var student = _mapper.Map<Student>(studentViewModel);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentID }, _mapper.Map<StudentViewModel>(student));
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.StudentID)
            {
                return BadRequest();
            }

            var student = _mapper.Map<Student>(studentViewModel);
            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}

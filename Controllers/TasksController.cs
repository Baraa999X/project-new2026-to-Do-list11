using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project2026ToDoList.Models;

namespace project2026ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TasksController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }
        [HttpPost]
        public async Task<ActionResult<TodoTask>> CreateTask([FromBody] TodoTask newTask)
        {
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllTasks), new { id = newTask.Id }, newTask);
        }

        // 3. حذف مهمة معينة من الـ SQL باستخدام الـ Id (DELETE api/tasks/{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            // البحث عن المهمة داخل جدول الـ Tasks باستخدام الـ Id
            var task = await _context.Tasks.FindAsync(id);
            // إذا لم يجد المهمة، يرجع 404 Not Found
            if (task == null)
            {
                return NotFound();
            }
            // حذف المهمة من الجدول
            _context.Tasks.Remove(task);
            // حفظ التغييرات نهائياً بالـ SQL
            await _context.SaveChangesAsync();

            return NoContent(); // تعني تمت العملية بنجاح وبدون إرجاع بيانات
        }
        // 4. تشطيب المهمة أو إلغاء التشطيب (PUT api/tasks/{id}/toggle)
        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleTaskStatus(int id)
        {
            // البحث عن المهمة بالـ ID
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            // قلب الحالة السحرية: إذا كانت false بتصير true، وإذا true بتصير false
            task.IsCompleted = !task.IsCompleted;

            // حفظ التعديل في قاعدة البيانات
            await _context.SaveChangesAsync();

            return Ok(task);
        }

    }
}
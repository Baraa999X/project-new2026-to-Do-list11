using Microsoft.EntityFrameworkCore;

namespace project2026ToDoList.Models
{
    public class AppDbContext : DbContext
    {
        // هاد الكود بيمرّر نص الاتصال (mycon) تبعك لقاعدة البيانات
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // هاد السطر هو اللي بيحجزلنا جدول اسمه Tasks بالـ SQL
        public DbSet<TodoTask> Tasks { get; set; }
    }
}
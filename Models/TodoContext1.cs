using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoApi1.Models
{
    public class TodoContext1 : DbContext
    {
        public TodoContext1(DbContextOptions<TodoContext1> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=d:/mydb1.db");    //创建文件夹的位置        
        }
        public TodoContext1() { }
        public DbSet<TodoItem1> TodoItems { get; set; }
    }
}
using System;

namespace Todos.Entities {
    public class Todo {
        public virtual int Id { get; set; }
        public virtual string TaskDescirption { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime? DueDate { get; set; }
    }
}

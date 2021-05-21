using FluentNHibernate.Mapping;

using Todos.Entities;

namespace Todos.HIbernate.Mappings {
    public class TodosMap: ClassMap<Todo> {
        public TodosMap() {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.Id).Generated.Insert();
            Map(x => x.TaskDescirption);
            Map(x => x.Title);
            Map(x => x.DueDate);
        }
    }
}

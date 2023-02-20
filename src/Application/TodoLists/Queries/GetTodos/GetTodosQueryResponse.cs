using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Application.TodoLists.Queries.GetTodos
{
    public class GetTodosItemQueryResponse
    {
        public GetTodosItemQueryResponse()
        {
            //Items = new List<TodoItemDto>();
        }

        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Colour { get; set; }

        //public IList<TodoItemDto> Items { get; set; }
    }
}

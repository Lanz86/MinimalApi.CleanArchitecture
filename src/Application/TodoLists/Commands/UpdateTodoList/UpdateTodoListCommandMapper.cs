using FastEndpoints;
using Mapster;
using MapsterMapper;
using MinimalApi.CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandMapper : Mapper<UpdateTodoListCommand, object, TodoList>
    {
        public override TodoList UpdateEntity(UpdateTodoListCommand r, TodoList e)
        {
            r.Adapt(e);
            return e;
        }
    }
}

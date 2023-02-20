using MinimalApi.CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public Colour Colour { get; set; } = Colour.White;

}

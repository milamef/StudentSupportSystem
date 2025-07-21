using ExercisesDAL;
using System;
using System.Collections.Generic;

namespace StudentSupportDAL;

public partial class Major : StudentSupportEntity
{
    public string? MajorName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

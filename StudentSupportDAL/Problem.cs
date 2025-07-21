using ExercisesDAL;
using System;
using System.Collections.Generic;

namespace StudentSupportDAL;

public partial class Problem : StudentSupportEntity
{
    public string? Description { get; set; }
}

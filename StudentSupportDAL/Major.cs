using System;
using System.Collections.Generic;

namespace StudentSupportDAL;

public partial class Major
{
    public int Id { get; set; }

    public string? MajorName { get; set; }

    public byte[] Timer { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

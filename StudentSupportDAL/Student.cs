using System;
using System.Collections.Generic;

namespace StudentSupportDAL;

public partial class Student
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public int MajorId { get; set; }

    public bool? IsTech { get; set; }

    public byte[]? Picture { get; set; }

    public byte[] Timer { get; set; } = null!;

    public virtual Major Major { get; set; } = null!;
}

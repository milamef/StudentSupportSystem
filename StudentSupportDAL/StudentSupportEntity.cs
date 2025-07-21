using System.ComponentModel.DataAnnotations;
namespace ExercisesDAL
{
    public class StudentSupportEntity
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[]? Timer { get; set; }
    }
}
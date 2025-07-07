using Microsoft.EntityFrameworkCore;
using StudentSupportDAL;
using System.Diagnostics;
using System.Reflection;
namespace StudentSupportDAL
{
    public class StudentDAO
    {
        public async Task<Student> GetByEmail(string email)
        {
            Student? selectedStudent;
            try
            {
                StudentSupportContext _db = new();
                selectedStudent = await _db.Students.FirstOrDefaultAsync(stu => stu.Email ==
                email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return selectedStudent!;
        }


        public async Task<Student> GetById(int id)
        {
            Student? selectedStudent;
            try
            {
                StudentSupportContext _db = new();
                selectedStudent = await _db.Students.FindAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return selectedStudent!;
        }


        public async Task<List<Student>> GetAll()
        {
            List<Student> allStudents;
            try
            {
                StudentSupportContext _db = new();
                allStudents = await _db.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allStudents;
        }


        public async Task<int> Add(Student newStudent)
        {
            try
            {
                StudentSupportContext _db = new();
                await _db.Students.AddAsync(newStudent);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return newStudent.Id;
        }


        public async Task<int> Update(Student updatedStudent)
        {
            int studentUpdated = -1;
            try
            {
                StudentSupportContext _db = new();
                Student? currentStudent =
                             await _db.Students.FirstOrDefaultAsync(stu => stu.Id == updatedStudent.Id);
                _db.Entry(currentStudent!).CurrentValues.SetValues(updatedStudent);
                studentUpdated = await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return studentUpdated;
        }


        public async Task<int> Delete(int? id)
        {
            int studentsDeleted = -1;
            try
            {
                StudentSupportContext _db = new();
                Student? selectedStudent = await _db.Students.FirstOrDefaultAsync(stu => stu.Id == id);
                _db.Students.Remove(selectedStudent!);
                studentsDeleted = await _db.SaveChangesAsync();  // returns # of rows removed 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return studentsDeleted;
        }


        public async Task<Student> GetByPhoneNumber(string phoneNo)
        {
            Student? selectedStudent;
            try
            {
                StudentSupportContext _db = new();
                selectedStudent = await _db.Students.FirstOrDefaultAsync(stu => stu.PhoneNo ==
                phoneNo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return selectedStudent!;
        }
    }
}
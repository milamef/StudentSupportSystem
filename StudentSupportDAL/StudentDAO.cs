using Microsoft.EntityFrameworkCore;
using StudentSupportDAL;
using System.Diagnostics;
using System.Reflection;
namespace StudentSupportDAL
{
    public class StudentDAO
    {
        readonly IRepository<Student> _repo;
        public StudentDAO()
        {
            _repo = new StudentSupportRepository<Student>();
        }


        public async Task<Student> GetByEmail(string email)
        {
            Student? selectedStudent;
            try
            {
                selectedStudent = await _repo.GetOne(stu => stu.Email == email);
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
                selectedStudent = await _repo.GetOne(stu => stu.Id == id);
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
                allStudents = await _repo.GetAll();
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
                newStudent = await _repo.Add(newStudent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return newStudent.Id;
        }


        public async Task<UpdateStatus> Update(Student updatedStudent)
        {
            UpdateStatus status;
            try
            {
                status = await _repo.Update(updatedStudent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return status;
        }


        public async Task<int> Delete(int? id)
        {
            int studentsDeleted = -1;
            try
            {
                studentsDeleted = await _repo.Delete((int)id!);
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
                selectedStudent = await _repo.GetOne(stu => stu.PhoneNo == phoneNo);
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
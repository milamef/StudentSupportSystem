using StudentSupportDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupportTests
{
    public class DAOTests
    {
        [Fact]
        public async Task Student_GetByEmailTest()
        {
            StudentDAO dao = new();
            Student selectedStudent = await dao.GetByEmail("am@gmail.com");
            Assert.NotNull(selectedStudent);
        }


        [Fact]
        public async Task Student_GetByIdTest()
        {
            StudentDAO dao = new();
            Student selectedStudent = await dao.GetById(1);
            Assert.NotNull(selectedStudent);
        }


        [Fact]
        public async Task Student_GetAllTest()
        {
            StudentDAO dao = new();
            List<Student> allStudents = await dao.GetAll();
            Assert.True(allStudents.Count > 0);
        }


        [Fact]
        public async Task Student_AddTest()
        {
            StudentDAO dao = new();
            Student newStudent = new()
            {
                FirstName = "Test",
                LastName = "Testing",
                PhoneNo = "(111) 111-1111",
                Title = "Ms.",
                MajorId = 100,
                Email = "tt@test.com"
            };
            Assert.True(await dao.Add(newStudent) > 0); 
        }


        [Fact]
        public async Task Student_UpdateTest()
        {
            StudentDAO dao = new();
            Student? studentForUpdate = await dao.GetById(12);
            if (studentForUpdate != null)
            {
                string oldPhoneNo = studentForUpdate.PhoneNo!;
                string newPhoneNo = oldPhoneNo == "(111) 111-1111" ? "(222) 222-2222" : "(111) 111-1111";
                studentForUpdate!.PhoneNo = newPhoneNo;
            }
            Assert.True(await dao.Update(studentForUpdate!) == 1);
        }


        [Fact]
        public async Task Student_DeleteTest()
        {
            StudentDAO dao = new();
            Student? studentForDelete = await dao.GetById(12);
            if (studentForDelete != null)
            {
                int result = await dao.Delete(studentForDelete.Id);
                Assert.True(result == 1);
            }
        }


        [Fact]
        public async Task Student_GetByPhoneNumberTest()
        {
            StudentDAO dao = new();
            Student selectedStudent = await dao.GetByPhoneNumber("(226) 700-4823");
            Assert.NotNull(selectedStudent);
        }
    }
}

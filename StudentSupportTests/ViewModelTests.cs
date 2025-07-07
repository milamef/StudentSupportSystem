using StudentSupportViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupportTests
{
    public class ViewModelTests
    {
        [Fact]
        public async Task Student_GetByEmailTest()
        {
            StudentViewModel vm = new() { Email = "am@gmail.com" };
            await vm.GetByEmail();
            Assert.NotNull(vm.Email);
        }


        [Fact]
        public async Task Student_GetByIdTest()
        {
            StudentViewModel vm = new() { Id = 1 };
            await vm.GetById();
            Assert.NotNull(vm.Id);
        }


        [Fact]
        public async Task Student_GetAllTest()
        {
            List<StudentViewModel> allStudentVms;
            StudentViewModel vm = new();
            allStudentVms = await vm.GetAll();
            Assert.True(allStudentVms.Count > 0);
        }


        [Fact]
        public async Task Student_AddTest()
        {
            StudentViewModel vm;
            vm = new()
            {
                Title = "Mr.",
                Firstname = "Test",
                Lastname = "Testing",
                Email = "tt@test.com",
                Phoneno = "(111)111-1111",
                MajorId = 200
            };
            await vm.Add();
            Assert.True(vm.Id > 0);
        }


        [Fact]
        public async Task Student_UpdateTest()
        {
            StudentViewModel vm = new() { Email = "tt@test.com" };
            await vm.GetByEmail();
            vm.Phoneno = vm.Phoneno == "(111)111-1111" ? "(222)222-2222" : "(111)111-1111";
            Assert.True(await vm.Update() == 1);
        }


        [Fact]
        public async Task Student_DeleteTest()
        {
            StudentViewModel vm = new() { Email = "tt@test.com" };
            await vm.GetByEmail();
            Assert.True(await vm.Delete() == 1);
        }


        [Fact]
        public async Task Student_GetByPhoneNumberTest()
        {
            StudentViewModel vm = new() { Phoneno = "(123) 123-1111" };
            await vm.GetByPhoneNumber();
            Assert.NotNull(vm.Firstname);
        }
    }
}

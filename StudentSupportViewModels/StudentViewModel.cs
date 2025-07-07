using StudentSupportDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupportViewModels
{
    public class StudentViewModel
    {
        readonly private StudentDAO _dao;

        public string? Title { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phoneno { get; set; }
        public string? Timer { get; set; }
        public int MajorId { get; set; }
        public string? MajorName { get; set; }
        public int? Id { get; set; }
        public bool? IsTech { get; set; }
        public string? Picture64 { get; set; }

        // constructor 
        public StudentViewModel()
        {
            _dao = new StudentDAO();
        }


        public async Task GetByEmail()
        {
            try
            {
                Student stu = await _dao.GetByEmail(Email!);
                Title = stu.Title;
                Firstname = stu.FirstName;
                Lastname = stu.LastName;
                Phoneno = stu.PhoneNo;
                Email = stu.Email;
                Id = stu.Id;
                MajorId = stu.MajorId;
                if (stu.Picture != null)
                {
                    Picture64 = Convert.ToBase64String(stu.Picture);
                }
                Timer = Convert.ToBase64String(stu.Timer);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                Email = "not found";
            }
            catch (Exception ex)
            {
                Email = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }


        public async Task GetById()
        {
            try
            {
                Student stu = await _dao.GetById((int)Id!);
                Title = stu.Title;
                Firstname = stu.FirstName;
                Lastname = stu.LastName;
                Phoneno = stu.PhoneNo;
                Email = stu.Email;
                Id = stu.Id;
                MajorId = stu.MajorId;
                if (stu.Picture != null)
                {
                    Picture64 = Convert.ToBase64String(stu.Picture);
                }
                Timer = Convert.ToBase64String(stu.Timer);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }


        public async Task<List<StudentViewModel>> GetAll()
        {
            List<StudentViewModel> allVms = new();
            try
            {
                List<Student> allStudents = await _dao.GetAll();
                foreach (Student stu in allStudents)
                {
                    StudentViewModel stuVm = new()
                    {
                        Title = stu.Title,
                        Firstname = stu.FirstName,
                        Lastname = stu.LastName,
                        Phoneno = stu.PhoneNo,
                        Email = stu.Email,
                        Id = stu.Id,
                        MajorId = stu.MajorId,
                        MajorName = stu.Major.MajorName,
                        Timer = Convert.ToBase64String(stu.Timer)
                    };
                    allVms.Add(stuVm);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allVms;
        }


        public async Task Add()
        {
            Id = -1;
            try
            {
                Student stu = new()
                {
                    Title = Title,
                    FirstName = Firstname,
                    LastName = Lastname,
                    PhoneNo = Phoneno,
                    Email = Email,
                    MajorId = MajorId,
                    Picture = Picture64 != null ? Convert.FromBase64String(Picture64!) : null
                };
                Id = await _dao.Add(stu);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }


        public async Task<int> Update()
        {
            int updateStatus;
            try
            {
                Student stu = new()
                {
                    Title = Title,
                    FirstName = Firstname,
                    LastName = Lastname,
                    PhoneNo = Phoneno,
                    Email = Email,
                    Id = (int)Id!,
                    MajorId = MajorId,
                    Picture = Picture64 != null ? Convert.FromBase64String(Picture64!) : null,
                    Timer = Convert.FromBase64String(Timer!)
                };

                updateStatus = -1;
                updateStatus = Convert.ToInt16(await _dao.Update(stu));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return updateStatus;
        }


        public async Task<int> Delete()
        {
            try
            {
                return await _dao.Delete(Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }


        public async Task GetByPhoneNumber()
        {
            try
            {
                Student stu = await _dao.GetByPhoneNumber(Phoneno!);
                Title = stu.Title;
                Firstname = stu.FirstName;
                Lastname = stu.LastName;
                Phoneno = stu.PhoneNo;
                Email = stu.Email;
                Id = stu.Id;
                MajorId = stu.MajorId;
                if (stu.Picture != null)
                {
                    Picture64 = Convert.ToBase64String(stu.Picture);
                }
                Timer = Convert.ToBase64String(stu.Timer!);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                Phoneno = "not found";
            }
            catch (Exception ex)
            {
                Phoneno = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }



    }
}

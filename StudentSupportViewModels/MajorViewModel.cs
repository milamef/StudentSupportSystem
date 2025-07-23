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
    public class MajorViewModel
    {
        private readonly MajorDAO _dao;

        public int? Id { get; set; }

        public string? MajorName { get; set; }


        // constructor 
        public MajorViewModel()
        {
            _dao = new MajorDAO();
        }


        public async Task<List<MajorViewModel>> GetAll()
        {
            List<MajorViewModel> allVms = new();
            try
            {
                List<Major> allMajors = await _dao.GetAll();
                foreach (Major mjr in allMajors)
                {
                    MajorViewModel mjrVm = new()
                    {
                        Id = mjr.Id,
                        MajorName = mjr.MajorName
                    };
                    allVms.Add(mjrVm);
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
    }
}

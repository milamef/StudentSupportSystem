using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupportDAL
{
    public class MajorDAO
    {
        readonly IRepository<Major> _repo;
        public MajorDAO()
        {
            _repo = new StudentSupportRepository<Major>();
        }


        public async Task<List<Major>> GetAll()
        {
            List<Major> allMajors;
            try
            {
                allMajors = await _repo.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allMajors;
        }
    }
}

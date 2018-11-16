using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Models
{
    class ProjectBusiness
    {
        private ManagerContext _db;
        private IEnumerable<Projects> _projects;

        public ProjectBusiness()
        {
            _db = new ManagerContext();
            _projects = _db.Projects.ToList();
        }

        public IEnumerable<Projects> GetProjects()
        {
            return _db.Projects.ToList();
        }

        public Projects GetProject(Projects selectedObject)
        {
            return _projects.FirstOrDefault(x => x.Id == selectedObject.Id);
        }
    }
}

using System.Collections.Generic;

namespace OrderManager.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeOfImplementation { get; set; }
        public double PlannedBudget { get; set; }
        public double RealBudget { get; set; }
        public string ProjectStatus { get; set; }
        public int IdCustomer { get; set; }
        public int IdResponsibleExecutor { get; set; }
        
    }
}

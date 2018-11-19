using System.Collections.Generic;

namespace OrderManager.Models
{
    public class Customers
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string AdressInformation { get; set; }
        public int IdProject { get; set; }
    }
}

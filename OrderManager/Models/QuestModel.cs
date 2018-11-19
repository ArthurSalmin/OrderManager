namespace OrderManager.Models
{
    public class Quests
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string TimeOfImplementation { get; set; }
        public int IdExecutor { get; set; }
        public int IdProject { get; set; }
        public string Status { get; set; }
    }
}

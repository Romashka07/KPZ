namespace Lab3KPZ_CF_.ViewModels
{
    public class GroupViewModel
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int? StreamID { get; set; }  // Foreign key to Stream

    }
}

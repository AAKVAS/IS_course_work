namespace WpfApp1.Models.DTO
{
    public class LoginedWorkerRights
    { 
        public int Id { get; set; }
        public int RightId { get; set; }
        public string RightTitle { get; set; } = null!;
        public int SectionId { get; set; }
        public string SectionTitle { get; set; } = null!;
        public int? SectionParentId { get; set; }
        public int PostId { get; set; }
        public string SectionKey { get; set; } = null!;

    }
}

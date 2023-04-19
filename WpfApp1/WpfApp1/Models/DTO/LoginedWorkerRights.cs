namespace WpfApp1.Models.DTO
{
    /// <summary>
    /// Объект передачи данных, отображающий право на раздел вошедшего в систему сотрудника.
    /// </summary>
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

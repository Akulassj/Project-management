namespace JIRA.Server.Domain.Entity
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; } // Навигационное свойство для связи с таблицей Job
    }
}

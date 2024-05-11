﻿namespace JIRA.Shared.Entity
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
      
        public DateTime UploadedAt { get; set; }
        public Guid ProjectTaskId { get; set; }
        public byte[] FileData { get; set; }

    }
}

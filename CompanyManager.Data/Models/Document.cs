namespace CompanyManager.Data.Models
{
    public class Document
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string Name { get; set; }

        public string FileExtension { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }
    }
}

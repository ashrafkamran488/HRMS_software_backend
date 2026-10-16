namespace HrOne_Rep.DTOs.Note
{
    public class CreateNoteDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}

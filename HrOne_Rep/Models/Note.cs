using System.ComponentModel.DataAnnotations;

namespace HrOne_Rep.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}

using HrOne_Rep.DTOs;
using HrOne_Rep.DTOs.Department;
using HrOne_Rep.DTOs.Note;
using HrOne_Rep.Models;

namespace HrOne_Rep.IRepository
{
    public interface INoteRepository
    {
        Task<List<NoteDto>> GetNoteAsync();
        Task<NoteDetailDto?> GetNoteByIdAsync(int id);
        Task<Note> CreateNoteAsync(CreateNoteDto note);
        Task UpdateNoteAsync(int id, UpdateNoteDto department);
    }
}

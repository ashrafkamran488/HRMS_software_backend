using HrOne_Rep.Data;
using HrOne_Rep.DTOs.Department;
using HrOne_Rep.DTOs.Note;
using HrOne_Rep.IRepository;
using HrOne_Rep.Models;
using Microsoft.EntityFrameworkCore;

namespace HrOne_Rep.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NoteDto>> GetNoteAsync()
        {
            return await _context.Note
                .Select(d => new NoteDto
                {
                    NoteId = d.NoteId,
                    Description = d.Description,
                    Title = d.Title
                }).ToListAsync();
        }
        public async Task<NoteDetailDto?> GetNoteByIdAsync(int id)
        {
            return await _context.Note
                .Select(d => new NoteDetailDto
                {
                    Title = d.Title,
                    Description = d.Description
                }).FirstOrDefaultAsync();
        }

        public async Task<Note> CreateNoteAsync(CreateNoteDto dto)
        {
            var note = new Note
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate

            };
            await _context.Note.AddAsync(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task UpdateNoteAsync(int id, UpdateNoteDto note)
        {
            var existingDepartment = await _context.Note.FirstOrDefaultAsync(x => x.NoteId == id);

            if (existingDepartment != null)
            {
                existingDepartment.Description = note.Description;
                existingDepartment.Title = note.Title;
                await _context.SaveChangesAsync();
            }
        }
    }
}

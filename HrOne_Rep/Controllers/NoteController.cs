using HrOne_Rep.DTOs.Department;
using HrOne_Rep.DTOs.Note;
using HrOne_Rep.IRepository;
using HrOne_Rep.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HrOne_Rep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noterepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noterepository = noteRepository;
        }

        [HttpGet("GetNoteList")]
        public async Task<IActionResult> GetNoteList()
        {
            var notes = await _noterepository.GetNoteAsync();

            return Ok(notes);
        }

        [HttpGet("GetNoteById/{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {
            var note = await _noterepository.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddNote(CreateNoteDto note)
        {
            await _noterepository.CreateNoteAsync(note);
            return Ok(new
            {
                Message = "Note Added Successfully"
            });
        }

        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateNoteDto dto)
        {
            await _noterepository.UpdateNoteAsync(id, dto);
            return Ok(new
            {
                Message = "Department Updated Successfully"
            });
        }
    }
}

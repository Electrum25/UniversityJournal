using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Linq;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Server.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        #region Subject Management (Управление предметами)

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromBody] CreateSubjectUseCase.CreateSubjectRequest request,
            [FromServices] CreateSubjectUseCase createUseCase)
        {
            try
            {
                var id = await createUseCase.Handle(request);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("teacher/{teacherId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetByTeacher(
    Guid teacherId,
    [FromServices] GetSubjectsByTeacherUseCase getUseCase)
        {
            var subjects = await getUseCase.Handle(teacherId);
            return Ok(subjects);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateSubjectUseCase.UpdateSubjectRequest request,
            [FromServices] UpdateSubjectUseCase updateUseCase)
        {
            try
            {
                await updateUseCase.Handle(request);
                return Ok("Предмет обновлен");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromServices] DeleteSubjectUseCase deleteUseCase,
            [FromQuery] bool deleteWithRelated = false)
        {
            try
            {
                await deleteUseCase.Handle(id, deleteWithRelated);
                return Ok("Предмет удален");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Student Assignments (Привязка студентов к предметам)

        [HttpPost("enroll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnrollStudent(
            [FromBody] CreateStudentSubjectUseCase.CreateStudentSubjectRequest request,
            [FromServices] CreateStudentSubjectUseCase enrollUseCase)
        {
            try
            {
                await enrollUseCase.Handle(request);
                return Ok("Студент успешно записан на предмет");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("unlink")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnlinkStudent(
            [FromBody] UnlinkStudentSubjectUseCase.UnlinkStudentSubjectRequest request,
            [FromServices] UnlinkStudentSubjectUseCase unlinkUseCase)
        {
            try
            {
                await unlinkUseCase.Handle(request);
                return Ok("Связь удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> GetAll()
        {
            var subjects = await _subjectRepository.GetAll();

            var dtos = subjects.Select(s => new SubjectDTO(s)).ToList();

            return Ok(dtos);
        }

        [HttpPut("final-grade")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> UpdateFinalGrade(
            [FromBody] UpdateStudentSubjectUseCase.UpdateStudentSubjectRequest request,
            [FromServices] UpdateStudentSubjectUseCase updateGradeUseCase)
        {
            try
            {
                await updateGradeUseCase.Handle(request);
                return Ok("Финальная оценка обновлена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("enrolled-students/{subjectId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetEnrolledStudents(Guid subjectId, [FromServices] IStudentRepository studentRepository, [FromServices] IStudentSubjectRepository studentSubjectRepository)
        {
            try
            {
                var assignments = await studentSubjectRepository.GetBySubject(subjectId);
                if (assignments == null || !assignments.Any())
                    return Ok(new List<StudentDTO>());

                var studentIds = assignments.Select(a => a.StudentId).ToList();

                var allStudents = await studentRepository.GetAll();
                var enrolledStudents = allStudents
                    .Where(s => studentIds.Contains(s.StudentId))
                    .Select(s => new StudentDTO(s)) 
                    .ToList();

                return Ok(enrolledStudents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> GetByStudent(Guid studentId, [FromServices] IStudentSubjectRepository studentSubjectRepository)
        {
            try
            {
                var assignments = await studentSubjectRepository.GetByStudent(studentId);

                var allSubjects = await _subjectRepository.GetAll();
                var studentSubjectIds = assignments.Select(a => a.SubjectId).ToList();

                var studentSubjects = allSubjects
                    .Where(s => studentSubjectIds.Contains(s.SubjectId))
                    .Select(s => new SubjectDTO(s))
                    .ToList();

                return Ok(studentSubjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
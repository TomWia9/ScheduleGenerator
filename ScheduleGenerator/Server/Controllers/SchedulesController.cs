using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Server.Repositories;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;
        private readonly ISchedulesRepository _schedulesRepository;
        private readonly IMapper _mapper;

        public SchedulesController(ISchedulesRepository schedulesRepository, IDbRepository dbRepository, IMapper mapper)
        {
            _schedulesRepository = schedulesRepository;
            _dbRepository = dbRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleDto>> NewSchedule(ScheduleForCreationDto schedule)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (await _schedulesRepository.ScheduleExistsAsync(userId, schedule.Name))
                {
                    return Conflict();
                }

                var newSchedule = _mapper.Map<Schedule>(schedule);
                newSchedule.UserId = userId;

                _dbRepository.Add(newSchedule);

                if (await _dbRepository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetSchedule), new { scheduleId = newSchedule.Id }, _mapper.Map<ScheduleDto>(newSchedule));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpGet("{scheduleId}")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedule(int scheduleId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                var schedule = await _schedulesRepository.GetScheduleAsync(userId, scheduleId);

                if (schedule != null)
                {
                    return Ok(_mapper.Map<ScheduleDto>(schedule));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedules()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                var schedules = await _schedulesRepository.GetSchedulesAsync(userId);

                if (schedules != null)
                {
                    return Ok(_mapper.Map<IEnumerable<ScheduleDto>>(schedules));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpPut("{scheduleId}")]
        public async Task<IActionResult> UpdateSchedule(int scheduleId, ScheduleForUpdateDto schedule)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (await _schedulesRepository.ScheduleExistsAsync(userId, schedule.Name))
                {
                    return Conflict();
                }

                var scheduleFromRepo = await _schedulesRepository.GetScheduleAsync(userId, scheduleId);

                if (scheduleFromRepo == null)
                {
                    return NotFound();
                }

                _mapper.Map(schedule, scheduleFromRepo);
                _schedulesRepository.UpdateSchedule(scheduleFromRepo);

                if (await _dbRepository.SaveChangesAsync())
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();

        }

        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                var scheduleToRemove = await _schedulesRepository.GetScheduleAsync(userId, scheduleId);

                if (scheduleToRemove == null)
                {
                    return NotFound();
                }

                _dbRepository.Remove(scheduleToRemove);

                if (await _dbRepository.SaveChangesAsync())
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();

        }

    }
}

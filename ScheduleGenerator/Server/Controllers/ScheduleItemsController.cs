using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Server.Repositories;
using ScheduleGenerator.Shared.Dto;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace ScheduleGenerator.Server.Controllers
{
    [Authorize]
    [Route("api/schedules/{scheduleId}/[controller]")]
    [ApiController]
    public class ScheduleItemsController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;
        private readonly ISchedulesRepository _schedulesRepository;
        private readonly IScheduleItemsRepository _scheduleItemsRepository;
        private readonly IMapper _mapper;

        public ScheduleItemsController(IDbRepository dbRepository, IScheduleItemsRepository scheduleItemsRepository, ISchedulesRepository schedulesRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _schedulesRepository = schedulesRepository;
            _scheduleItemsRepository = scheduleItemsRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleItemDto>> NewScheduleItem(int scheduleId, ScheduleItemForCreationDto scheduleItem)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (!await _schedulesRepository.ScheduleExistsAsync(userId, scheduleId))
                {
                    return NotFound();
                }

                if (!_scheduleItemsRepository.AreDatesCorrect(scheduleItem.StartTime, scheduleItem.EndTime))
                {
                    return BadRequest(new { message = "The difference between the start and end times should be at least 15 minutes " });
                }

                if (await _scheduleItemsRepository.DatesConflictAsync(scheduleId, scheduleItem.DayOfWeek,
                    scheduleItem.StartTime, scheduleItem.EndTime))
                {
                    return Conflict();
                }

                var newScheduleItem = _mapper.Map<ScheduleItem>(scheduleItem);
                newScheduleItem.Color = _scheduleItemsRepository.GetScheduleItemColor(scheduleItem.TypeOfClasses);
                newScheduleItem.ScheduleId = scheduleId;
                
                _dbRepository.Add(newScheduleItem);

                if (await _dbRepository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetScheduleItem),
                        new { scheduleId, scheduleItemId = newScheduleItem.Id }, _mapper.Map<ScheduleItemDto>(newScheduleItem));
                }
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleItemDto>>> GetScheduleItems(int scheduleId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (!await _schedulesRepository.ScheduleExistsAsync(userId, scheduleId))
                {
                    return NotFound();
                }
                
                var scheduleItems = await _scheduleItemsRepository.GetScheduleItemsAsync(scheduleId);
                
                if (scheduleItems != null)
                {
                    return Ok(_mapper.Map<IEnumerable<ScheduleItemDto>>(scheduleItems));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpGet("{scheduleItemId}")]
        public async Task<ActionResult<ScheduleItemDto>> GetScheduleItem(int scheduleId, int scheduleItemId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (!await _schedulesRepository.ScheduleExistsAsync(userId, scheduleId))
                {
                    return NotFound();
                }

                var scheduleItem = await _scheduleItemsRepository.GetScheduleItemAsync(scheduleId, scheduleItemId);

                if (scheduleItem != null)
                {
                    return Ok(_mapper.Map<ScheduleItemDto>(scheduleItem));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpPut("{scheduleItemId}")]
        public async Task<IActionResult> UpdateScheduleItem(int scheduleId, int scheduleItemId, ScheduleItemForUpdateDto scheduleItem)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (!await _schedulesRepository.ScheduleExistsAsync(userId, scheduleId))
                {
                    return NotFound();
                }

                if (! _scheduleItemsRepository.AreDatesCorrect(scheduleItem.StartTime, scheduleItem.EndTime))
                {
                    return BadRequest(new { message = "The difference between the start and end times should be at least 15 minutes " });
                }

                if (await _scheduleItemsRepository.DatesConflictAsync(scheduleId, scheduleItem.DayOfWeek,
                    scheduleItem.StartTime, scheduleItem.EndTime, scheduleItemId))
                {
                    return Conflict();
                }

                var scheduleItemFromRepo = await _scheduleItemsRepository.GetScheduleItemAsync(scheduleId, scheduleItemId);

                if (scheduleItemFromRepo == null)
                {
                    return NotFound();
                }

                _mapper.Map(scheduleItem, scheduleItemFromRepo);
                scheduleItemFromRepo.Color = _scheduleItemsRepository.GetScheduleItemColor(scheduleItem.TypeOfClasses);
                _scheduleItemsRepository.UpdateScheduleItem(scheduleItemFromRepo);

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

        [HttpPatch("{scheduleItemId}")]
        public async Task<IActionResult> PartiallyUpdateScheduleItem(int scheduleId, int scheduleItemId, JsonPatchDocument<ScheduleItemForUpdateDto> patchDocument)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (!await _schedulesRepository.ScheduleExistsAsync(userId, scheduleId))
                {
                    return NotFound();
                }

                var scheduleItemFromRepo = await _scheduleItemsRepository.GetScheduleItemAsync(scheduleId, scheduleItemId);

                if (scheduleItemFromRepo == null)
                {
                    return NotFound();
                }

                var scheduleItemToPatch = _mapper.Map<ScheduleItemForUpdateDto>(scheduleItemFromRepo);
                patchDocument.ApplyTo(scheduleItemToPatch, ModelState);

                var isValid = TryValidateModel(scheduleItemToPatch);

                if (!isValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_scheduleItemsRepository.AreDatesCorrect(scheduleItemToPatch.StartTime, scheduleItemToPatch.EndTime))
                {
                    return BadRequest(new { message = "The difference between the start and end times should be at least 15 minutes " });
                }

                if (await _scheduleItemsRepository.DatesConflictAsync(scheduleId, scheduleItemToPatch.DayOfWeek,
                    scheduleItemToPatch.StartTime, scheduleItemToPatch.EndTime, scheduleItemId))
                {
                    return Conflict();
                }

                _mapper.Map(scheduleItemToPatch, scheduleItemFromRepo);
                scheduleItemFromRepo.Color = _scheduleItemsRepository.GetScheduleItemColor(scheduleItemToPatch.TypeOfClasses);
                _scheduleItemsRepository.UpdateScheduleItem(scheduleItemFromRepo);

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

        [HttpDelete("{scheduleItemId}")]
        public async Task<IActionResult> DeleteTodo(int scheduleId, int scheduleItemId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("id").Value);

                if (!await _schedulesRepository.ScheduleExistsAsync(userId, scheduleId))
                {
                    return NotFound();
                }

                var scheduleItemToRemove = await _scheduleItemsRepository.GetScheduleItemAsync(scheduleId, scheduleItemId);

                if (scheduleItemToRemove == null)
                {
                    return NotFound();
                }

                _dbRepository.Remove(scheduleItemToRemove);

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

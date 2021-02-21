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
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Create a new schedule item
        /// </summary>
        /// <param name="scheduleId">The id of schedule for which to create item</param>
        /// <param name="scheduleItem">The schedule item to create</param>
        /// <returns>An ActionResult of type ScheduleItemDto</returns>
        /// <response code="201">Creates and returns the created schedule item</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

        /// <summary>
        /// Get a list of schedule items from specified schedule
        /// </summary>
        /// <param name="scheduleId">The Id of schedule you want to get items from</param>
        /// <returns>An ActionResult of type IEnumerable of ScheduleItemDto</returns>
        /// <response code="200">Returns the requested list of items from specified schedule</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Get item from specified schedule
        /// </summary>
        /// <param name="scheduleId">The Id of schedule you want to get item from</param>
        /// <param name="scheduleItemId">The Id of schedule item you want to get</param>
        /// <returns>An ActionResult of type ScheduleItemDto</returns>
        /// <response code="200">Returns the requested item from specified schedule</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Update schedule item
        /// </summary>
        /// <param name="scheduleId">The Id of schedule where you want to update item</param>
        /// <param name="scheduleItemId">The Id of item you want to update</param>
        /// <param name="scheduleItem">The schedule item with updated values</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

        /// <summary>
        /// Partially update a schedule item
        /// </summary>
        /// <param name="scheduleId">The Id of schedule where you want to partially update item</param>
        /// <param name="scheduleItemId">The Id of item you want to partially update</param>
        /// <param name="patchDocument">The set of operations to apply to the schedule item</param>
        /// <returns>An IActionResult</returns>
        /// <remarks>
        /// Sample request (this request updates the schedule item's **lecturer** field)
        ///
        ///     PATCH /schedules/{scheduleId}/scheduleItems/{scheduleItemId} 
        ///     [ 
        ///         { 
        ///             "op": "replace", 
        ///             "patch": "/lecturer", 
        ///             "value": "new value" 
        ///         } 
        ///     ]
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

        /// <summary>
        /// Delete the schedule item with given id
        /// </summary>
        /// <param name="scheduleId">The Id of schedule you want to delete item from</param>
        /// <param name="scheduleItemId">The id of item want to delete</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{scheduleItemId}")]
        public async Task<IActionResult> DeleteScheduleItem(int scheduleId, int scheduleItemId)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Shared.Modals;
using ScheduleGenerator.Shared.Dto;
using ScheduleGenerator.Shared.Enums;

namespace ScheduleGenerator.Client.Shared
{
    public class ScheduleCreateBase : ComponentBase
    {
        protected ScheduleForCreationDto ScheduleForCreation = new();
        protected CreateScheduleItemModal CreateScheduleItemModal;

        //mock data for now
        protected List<ScheduleItemDto> Items = new List<ScheduleItemDto>
        {
            new ScheduleItemDto()
            {
                Id = 1,
                Subject = "Technika mikroprocesorowa",
                RoomNumber = "E9",
                Lecturer = "Mr. Test",
                DayOfWeek = DayOfWeek.Tuesday,
                StartTime = TimeSpan.Parse("07:00"),
                EndTime = TimeSpan.Parse("08:00"),
                TypeOfClasses = TypeOfClasses.Exercises,
                Color = Color.Success,
                ScheduleId = 1
    },

            new ScheduleItemDto()
            {
                Id = 2,
                Subject = "Aplikacje internetowe",
                RoomNumber = "E12",
                Lecturer = "Mr. Test",
                DayOfWeek = DayOfWeek.Wednesday,
                StartTime = TimeSpan.Parse("09:00"),
                EndTime = TimeSpan.Parse("10:45"),
                TypeOfClasses = TypeOfClasses.Lecture,
                Color = Color.Danger,
                ScheduleId = 1
            },

            new ScheduleItemDto()
            {
                Id = 3,
                Subject = "Język C#",
                RoomNumber = "c9",
                Lecturer = "Mr. Test",
                DayOfWeek = DayOfWeek.Friday,
                StartTime = TimeSpan.Parse("07:00"),
                EndTime = TimeSpan.Parse("09:00"),
                TypeOfClasses = TypeOfClasses.Project,
                Color = Color.Secondary,
                ScheduleId = 1
            },
            new ScheduleItemDto()
            {
                Id = 3,
                Subject = "Język C#",
                RoomNumber = "c9",
                Lecturer = "Mr. Test",
                DayOfWeek = DayOfWeek.Friday,
                StartTime = TimeSpan.Parse("07:00"),
                EndTime = TimeSpan.Parse("09:00"),
                TypeOfClasses = TypeOfClasses.Project,
                Color = Color.Secondary,
                ScheduleId = 1
            },
            new ScheduleItemDto()
            {
                Id = 3,
                Subject = "Język C#",
                RoomNumber = "c9",
                Lecturer = "Mr. Test",
                DayOfWeek = DayOfWeek.Friday,
                StartTime = TimeSpan.Parse("07:00"),
                EndTime = TimeSpan.Parse("09:00"),
                TypeOfClasses = TypeOfClasses.Project,
                Color = Color.Secondary,
                ScheduleId = 1
            },
            new ScheduleItemDto()
            {
                Id = 3,
                Subject = "Język C#",
                RoomNumber = "c9",
                Lecturer = "Mr. Test",
                DayOfWeek = DayOfWeek.Thursday,
                StartTime = TimeSpan.Parse("07:00"),
                EndTime = TimeSpan.Parse("09:00"),
                TypeOfClasses = TypeOfClasses.Seminar,
                Color = Color.Warning,
                ScheduleId = 1
            },
            new ScheduleItemDto()
            {
                Id = 3,
                Subject = "Język C#",
                RoomNumber = "c9",
                Lecturer = "Mr. Test",
                DayOfWeek = DayOfWeek.Monday,
                StartTime = TimeSpan.Parse("07:00"),
                EndTime = TimeSpan.Parse("09:00"),
                TypeOfClasses = TypeOfClasses.Project,
                Color = Color.Secondary,
                ScheduleId = 1
            },
        };
        
        protected bool Loading;


        protected async Task HandleValidSubmit()
        {
            Loading = true;
            try
            {
                Console.WriteLine("Schedule created");
                Loading = false;
            }
            catch (Exception ex)
            {
              
            }
        }
    }
}

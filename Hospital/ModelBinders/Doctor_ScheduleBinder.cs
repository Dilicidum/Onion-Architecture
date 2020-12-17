using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Application.Models.DTO;
namespace API.ModelBinders
{
    public class Doctor_ScheduleBinder:IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
            {
                throw new ArgumentException(nameof(bindingContext));
            }
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }
            //var doctorIdPartValue = modelBindingContext.ValueProvider.GetValue("DoctorId");
            //var dayOfWeekProviderResult = modelBindingContext.ValueProvider.GetValue("dayOfWeek");
            //var StartTimePartValue = modelBindingContext.ValueProvider.GetValue("startTime");
            //var BreakStartTimePartValue = modelBindingContext.ValueProvider.GetValue("breakStartTime");
            //var BreakEndTimePartValue = modelBindingContext.ValueProvider.GetValue("breakEndTime");
            //var EndTimePartValue = modelBindingContext.ValueProvider.GetValue("endTime");
            //var x = modelBindingContext.HttpContext;

            //string dayOfWeekString = dayOfWeekProviderResult.FirstValue;
            //int doctorId = Int32.Parse(doctorIdPartValue.FirstValue);
            //TimeSpan StartTime = TimeSpan.Parse(StartTimePartValue.FirstValue);
            //TimeSpan BreakStart = TimeSpan.Parse(BreakStartTimePartValue.FirstValue);
            //TimeSpan BreakEnd = TimeSpan.Parse(BreakEndTimePartValue.FirstValue);
            //TimeSpan endTime = TimeSpan.Parse(EndTimePartValue.FirstValue);

            //DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeekString);
            //bindingContext.Result = ModelBindingResult.Success(dayOfWeek);
            //Doctor_ScheduleDTO doctor_ScheduleDTO = new Doctor_ScheduleDTO()
            //{
            //    DoctorId = doctorId,
            //    DayOfWeek = dayOfWeek,
            //    StartTime = StartTime,
            //    BreakTimeStart = BreakStart,
            //    BreakEndTime = BreakEnd,
            //    EndTime = endTime
            //};

            bindingContext.Result = ModelBindingResult.Success(modelName);
            return Task.CompletedTask;
        }
    }
}

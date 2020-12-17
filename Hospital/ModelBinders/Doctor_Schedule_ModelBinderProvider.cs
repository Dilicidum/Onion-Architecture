using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Application.Models.DTO;
namespace API.ModelBinders
{
    public class Doctor_Schedule_ModelBinderProvider:IModelBinderProvider
    {
        private readonly IModelBinder binder = new Doctor_ScheduleBinder();

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(Doctor_ScheduleDTO) ? binder : null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using System.Threading.Tasks;
namespace Application.Interfaces.Persistence
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseGenericRepository<Doctor> DoctorsRepository { get; }
        IBaseGenericRepository<Doctor_Schedule> Doctors_SheduleRepository { get;}
        IBaseGenericRepository<Patient> PatientsRepository { get;  }
        IBaseGenericRepository<Patient_Visiting> Patients_VisitingsRepository { get; }
        Task<int> Commit();
    }
}

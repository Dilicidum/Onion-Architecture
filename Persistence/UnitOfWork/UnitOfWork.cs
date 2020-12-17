using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Persistence;
using Domain.Entities;
using Persistence.Repositories;
namespace Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;
        private IBaseGenericRepository<Doctor> doctors;
        private IBaseGenericRepository<Patient> patients;
        private IBaseGenericRepository<Patient_Visiting> patients_visitings;
        private IBaseGenericRepository<Doctor_Schedule> doctors_schedule;
        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }
        
        public IBaseGenericRepository<Doctor> DoctorsRepository { get => doctors ?? 
                (doctors = new BaseRepository<Doctor>(context)) ; }
        public IBaseGenericRepository<Doctor_Schedule> Doctors_SheduleRepository { get => doctors_schedule ??
                (doctors_schedule = new BaseRepository<Doctor_Schedule>(context));}
        public IBaseGenericRepository<Patient> PatientsRepository { get => patients ??
                (patients = new BaseRepository<Patient>(context));}
        public IBaseGenericRepository<Patient_Visiting> Patients_VisitingsRepository { get => patients_visitings ??
                (patients_visitings = new BaseRepository<Patient_Visiting>(context));}

        public async Task<int> Commit()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if(this.context == null)
            {
                return;
            }
            this.context.Dispose();
        }
    }
}

using MedicineRest.Models.Db;

namespace MedicineRest.Models
{
    public class medicineservic
    {
        ProjectDbContext context;
        public medicineservic(ProjectDbContext context)
        {
            this.context = context;
        }

        public List<Medicinedetail> GetAllMeds()
        {
            List<Medicinedetail> meds = context.Medicinedetails.ToList();
            return meds;
        }

        public Medicinedetail GetMed(int id)
        {
            var med = context.Medicinedetails.SingleOrDefault(c => c.Id == id);
            return med!;
        }
        public bool DeleteMed(int id)
        {
            var med = context.Medicinedetails.SingleOrDefault(c => c.Id == id);
            if (med != null)
            {
                context.Medicinedetails.Remove(med);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateMed(Medicinedetail md)
        {
            var med = context.Medicinedetails.SingleOrDefault(c => c.Id == md.Id);
            if (med != null)
            {
                med.Mediname = md.Mediname;
                med.Prize = md.Prize;
                context.SaveChanges();
                return true;
            }
            return false;
        }



        public int AddMed(Medicinedetail md)
        {
            context.Medicinedetails.Add(md);
            return context.SaveChanges();
        }

        public List<Medicinedetail> GetMedicinesExpiringNextMonth()
        {

            List<Medicinedetail> expiringMeds  = context.Medicinedetails.Where(m => m.Expirydate >= DateTime.Now && m.Expirydate <= DateTime.Now.AddMonths(1)).ToList();
            return expiringMeds;
        } 
        public List<Medicinedetail> GetMedicinesReachingCriticalStock(int criticalStockLevel)
        {
            var medicines = context.Medicinedetails
                .Where(m => m.Stocklvl <= criticalStockLevel)
                .ToList();

            return medicines;
        }

    }
}
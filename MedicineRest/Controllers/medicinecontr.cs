using MedicineRest.Models;
using MedicineRest.Models.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace MedicineRest.Controllers
{
    [ApiController]
    public class medicinecontr : ControllerBase
    {
        public medicineservic service;
        public medicinecontr(medicineservic svc)
        {
            service = svc;
        }
        //[AllowAnonymous]
        [HttpGet]
        [Route("Medicinedetails")]
        public ObjectResult GetAllMeds()
        {
            var list = service.GetAllMeds();
            return Ok(list);
        }
        //[Authorize]
        [HttpGet]
        [Route("Medicinedetails/{id}")]
        public IActionResult GetMeds(int id)
        {
            Medicinedetail md = service.GetMed(id);
            if (md == null)
                return NotFound();
            else
                return Ok(md);
        }
        [HttpPost]
        [Route("add")]
        public IActionResult postMed(Medicinedetail medicine)
        {
            int result = service.AddMed(medicine);
            if (result == 1) return Ok();
            else
                return new StatusCodeResult(501);
        }
        /*[Authorize]
        [HttpPost]
        [Route("addquery")]
        public IActionResult PostMed3([FromQuery] MedicineInventory medicine)
        {
            int result = service.AddMed(medicine);
            if (result == 1) return Ok();
            else
                return new StatusCodeResult(501); //HttpStatusCode
        } */

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteMed(int id)
        {
            bool result = service.DeleteMed(id);
            if (result == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateMed(Medicinedetail md)
        {
            bool result = service.UpdateMed(md);
            if (result == true)
            {
                return Ok();
            }
            else
                return NotFound();
        }
        //[Authorize]
       /* [HttpGet]
        [Route("MedicinesExpiringNextMonth")]
        public ObjectResult GetMedicinesExpiringNextMonth()
        {
            var expiringMeds = service.GetMedicinesExpiringNextMonth();
            if (expiringMeds == null)
            {
                return NotFound("No medicines are expiring next month.");
            }
            return Ok(expiringMeds);
        } */
        /*public ObjectResult GetAllMeds()
        {
            var list = service.GetAllMeds();
            return Ok(list);
        }*/ 

        //[Authorize]
        [HttpGet]
        [Route("MedicineInventories/CriticalStock/{criticalStockLevel}")]
        public IActionResult GetMedicinesReachingCriticalStock(int criticalStockLevel)
        {
            var medicines = service.GetMedicinesReachingCriticalStock(criticalStockLevel);
            return Ok(medicines);
        }
        /*[HttpGet]
        [Route("MedicinesReachingCriticalStock/{criticalStockLevel}")]
        public IActionResult GetMedicinesReachingCriticalStock()
        {
            Console.Write("Enter the critical stock level: ");
            if (!int.TryParse(Console.ReadLine(), out int criticalStockLevel))
            {
                return NotFound("Invalid input for critical stock level");
               
            }
            //int criticalStockLevel = 10; // Define your critical stock level here
            var criticalMeds = service.GetMedicinesReachingCriticalStockLevel(criticalStockLevel);
            if (criticalMeds == null || criticalMeds.Count == 0)
            {
                return NotFound("No medicines are reaching critical stock levels.");
            }
            return Ok(criticalMeds);
        }*/

    }
}


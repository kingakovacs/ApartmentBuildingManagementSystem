using ApartmentBuildingManagementSystem.Models;
using ApartmentBuildingManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentBuildingManagementSystem.Controllers
{
    public class ConsumptionController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        public ConsumptionController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("OverviewOfConsumptions")]
        public async Task<IActionResult> GetAll()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("UploadConsumption")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("UploadConsumption")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,DateCreated,ConsumptionType,Consumption,Price")] ConsumptionDetails cd)
        {
            if (ModelState.IsValid)
            {
                cd.Id = Guid.NewGuid();
                cd.DateCreated = DateTime.UtcNow;
                await _cosmosDbService.AddItemAsync(cd);
                return RedirectToAction("OverviewOfConsumptions");
            }

            return View(cd);
        }

        [HttpPost]
        [ActionName("EditConsumption")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,DateCreated,ConsumptionType,Consumption,Price")] ConsumptionDetails cd)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateItemAsync(cd.Id.ToString(), cd);
                return RedirectToAction("OverviewOfConsumptions");
            }

            return View(cd);
        }

        [ActionName("EditConsumption")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ConsumptionDetails item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetItemAsync(id));
        }
    }
}

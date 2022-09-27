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
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,Price,Consumption")] ConsumptionDetails item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                await _cosmosDbService.AddItemAsync(item);
                return RedirectToAction("OverviewOfConsumptions");
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

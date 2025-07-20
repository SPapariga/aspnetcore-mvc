using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;
        private Repository<Ingredient> _ingredientRepository;

        public IngredientController(ApplicationDbContext context)
        {
            ingredients = new Repository<Ingredient>(context);
            _ingredientRepository = new Repository<Ingredient>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await ingredients.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var options = new QueryOptions<Ingredient>
            {
                Includes = "ProductIngredients,ProductIngredients.Product"
            };

            var ingredient = await _ingredientRepository.GetByIdAsync(id, options);

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngredientId, Name")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await ingredients.AddAsync(ingredient);
                return RedirectToAction("Index");
            }
            
            return View(ingredient);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient> { Includes = "ProductIngredients.Product" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Ingredient ingredient)
        {
            await ingredients.DeleteAsync(ingredient.IngredientId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        { 
            return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient> { Includes = "ProductIngredients.Product" }));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await ingredients.UpdateAsync(ingredient);
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }


    }
}

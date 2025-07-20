using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;
        private Repository<Ingredient> _ingredientRepository;


        //public IngredientController(ApplicationDbContext context) 
        //{
        //    ingredients = new Repository<Ingredient>(context);
        //}
        public IngredientController(ApplicationDbContext context)
        {
            ingredients = new Repository<Ingredient>(context);
            _ingredientRepository = new Repository<Ingredient>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await ingredients.GetAllAsync());
        }
        //public async Task<IActionResult> Details(int id) 
        //{


        //    return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient>() {Includes="ProductIngredients, Product"}));
        //}

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
    }
}

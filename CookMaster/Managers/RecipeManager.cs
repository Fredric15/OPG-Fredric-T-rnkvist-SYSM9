using CookMaster.Model;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Managers
{
    public class RecipeManager : ViewModelBase
    {   
        private readonly UserManager _userManager;
        private readonly List<Recipe>? _recipes;

        public RecipeManager(UserManager userManager)
        {
            _userManager = userManager;
            _recipes = new List<Recipe>();
            SeedDefaultRecipes();
        }

        private void SeedDefaultRecipes()
        {
            _recipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager.CurrentUser, Date = DateTime.Now });
            _recipes.Add(new Recipe {Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager.CurrentUser, Date = DateTime.Now});
        }

        public void AddRecipe(Recipe recipe)
        { }
        public void RemoveRecipe(Recipe recipe) 
        { }
        public void GetAllRecipes()
        { }
        public void GetByUser(User user)
        { }
        public void Filter(string criteria)
        { }
        public void UpdateRecipe(Recipe recipe)
        { }
    }
}

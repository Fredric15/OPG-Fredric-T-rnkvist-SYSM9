using CookMaster.Model;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Managers
{
    public class RecipeManager : ViewModelBase
    {   
        private readonly UserManager _userManager;
        public List<Recipe>? AllRecipes { get; set; }
        public ObservableCollection<Recipe> FilteredRecipes { get; set; }
        


        public RecipeManager(UserManager userManager)
        {
            _userManager = userManager;
            //AllRecipes = new List<Recipe>();
            //AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            //AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[1], Date = DateTime.Now });
            //SeedDefaultRecipes();
        }

        private void SeedDefaultRecipes()
        {
            //AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0] , Date = DateTime.Now});
            //AllRecipes.Add(new Recipe {Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[1], Date = DateTime.Now});
        }

        public void AddRecipe(Recipe recipe)
        { }
        public void RemoveRecipe(Recipe recipe) 
        { }
        public void GetAllRecipes()
        { }
        public void GetByUser(User user)
        {
            var filtrerade = AllRecipes.Where(recipe => recipe.CreatedBy == user);
            FilteredRecipes = new ObservableCollection<Recipe>(filtrerade);

        }
        public void Filter(string criteria)
        { }
        public void UpdateRecipe(Recipe recipe)
        { }
    }
}

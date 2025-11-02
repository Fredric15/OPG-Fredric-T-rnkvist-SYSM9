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
        private ObservableCollection<Recipe> _allRecipes;

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set 
            { 
                _selectedRecipe = value; 
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Recipe> AllRecipes
        {
            get { return _allRecipes; }
            set { _allRecipes = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Recipe> _userFilteredRecipe;

        public ObservableCollection<Recipe> UserFilteredRecipe
        {
            get { return _userFilteredRecipe; }
            set 
            { 
                _userFilteredRecipe = value; 
                OnPropertyChanged();
            }
        }




        public RecipeManager()//UserManager userManager)
        {
            //_userManager = userManager;
            AllRecipes = new ObservableCollection<Recipe>();
            //AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            //AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[1], Date = DateTime.Now });
        }
        public RecipeManager(UserManager userManager)
        {
            _userManager = userManager;

        }

        /*public void SeedDefaultRecipes()
        {
            
            AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0] , Date = DateTime.Now});
            AllRecipes.Add(new Recipe {Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[1], Date = DateTime.Now});
        }*/

        public void AddRecipe(Recipe recipe)
        {
            AllRecipes.Add(recipe);
        }
        public void RemoveRecipe(Recipe recipe) 
        { 
            AllRecipes.Remove(recipe);

        }
        public ObservableCollection<Recipe> GetAllRecipes()
        {
            var allRecipes = AllRecipes;

            return UserFilteredRecipe = new ObservableCollection<Recipe>(allRecipes);
        }
        public ObservableCollection<Recipe> GetByUser(User user)
        {
            var filtrerade = AllRecipes.Where(recipe => recipe.CreatedBy == user);
            return UserFilteredRecipe = new ObservableCollection<Recipe>(filtrerade);

        }
        public void Filter(string criteria)
        { }
        public void UpdateRecipe(Recipe recipe)
        {
            
        
        }
    }
}

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
        
        //Sparar markerat recept i RecipeListViewModel
        //Injekteras sedan in i RecipeDetailViewModel konstruktorn
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
        
        //Innehåller alla användares recept
        private ObservableCollection<Recipe> _allRecipes;
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




        public RecipeManager()
        {
            AllRecipes = new ObservableCollection<Recipe>();

        }
        public void AddRecipe(Recipe recipe)
        {
            AllRecipes.Add(recipe);
        }
        public void RemoveRecipe(Recipe recipe) 
        { 
            AllRecipes.Remove(recipe);

        }
        public ObservableCollection<Recipe> GetByUser(User user)
        {
            //om inloggade användaren är en Admin så skickar metoden alla recepten
            //annars filtrerar den ut alla recept för den inloggade användaren
            if (user is AdminUser)
            {
                return UserFilteredRecipe = new ObservableCollection<Recipe>(AllRecipes);
            }
            else
            {
                var filtrerade = AllRecipes.Where(recipe => recipe.CreatedBy == user);
                return UserFilteredRecipe = new ObservableCollection<Recipe>(filtrerade);
            }
        }
    }
}

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




        public RecipeManager()
        {
            AllRecipes = new ObservableCollection<Recipe>();

        }
        /*public RecipeManager(UserManager userManager)
        {
            _userManager = userManager;

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
            if (user is AdminUser)
            {
                var allRecipes = AllRecipes;

                return UserFilteredRecipe = new ObservableCollection<Recipe>(allRecipes);
            }
            else
            {
                var filtrerade = AllRecipes.Where(recipe => recipe.CreatedBy == user);
                return UserFilteredRecipe = new ObservableCollection<Recipe>(filtrerade);
            }


        }
        public void Filter(string criteria)
        { }
        public void UpdateRecipe(Recipe recipe)
        {
            
        
        }
    }
}

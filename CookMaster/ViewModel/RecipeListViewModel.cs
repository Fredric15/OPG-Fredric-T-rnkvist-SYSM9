using CookMaster.Managers;
using CookMaster.Model;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;

namespace CookMaster.ViewModel
{
    public class RecipeListViewModel : ViewModelBase
    {
        public event Action<string> ErrorMessage;
        private readonly UserManager _userManager;
        private readonly RecipeManager _recipeManager;
        public ObservableCollection<Recipe> FilteredRecipeList { get; set; }
        public ObservableCollection<Recipe> FilterRecipeList { get; set; }

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set { _selectedRecipe = value; OnPropertyChanged(); }
        }


        private DateTime _dateTime = DateTime.Now;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set 
            { 
                _dateTime = value; OnPropertyChanged();
            }
        }

        public string FormattedTime => _dateTime.ToString("HH:mm:ss");
        
        public ICommand RemoveCommand { get;}


        public RecipeListViewModel(UserManager userManager)
        {
            _userManager = userManager;
            FilteredRecipeList = new ObservableCollection<Recipe>();

            FilteredRecipeList.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            FilteredRecipeList.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            ShowRecipes();
            RemoveCommand = new RelayCommand(execute => RemoveRecipes());
        }
        public void ShowRecipes()
        {
            var filtrerade = FilteredRecipeList.Where(recipe => recipe.CreatedBy == _userManager.CurrentUser);
            FilterRecipeList = new ObservableCollection<Recipe>(filtrerade);
            
            
        }

        

        public void RemoveRecipes() 
        { 
            if(SelectedRecipe != null)
            {
                FilterRecipeList.Remove(SelectedRecipe);
            }
            else 
            {
                ErrorMessage?.Invoke("Du måste markera ett recept.");
            }
        
        }
        public void FilterRecipes()
        { }
        
    }
}

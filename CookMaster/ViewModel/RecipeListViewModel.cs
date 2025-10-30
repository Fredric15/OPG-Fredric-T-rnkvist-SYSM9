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

        public ICommand UserDetailsCommand { get;}
        public ICommand AddCommand { get;}
        public ICommand RemoveCommand { get;}
        public ICommand DetailsCommand { get;}
        public ICommand InfoCommand { get;}
        public ICommand SignOutCommand {  get;}

        public event Action<string> ErrorMessage;
        public event Action RequestUserDetails;
        public RecipeListViewModel(UserManager userManager)
        {
            _userManager = userManager;
            FilteredRecipeList = new ObservableCollection<Recipe>();

            FilteredRecipeList.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            FilteredRecipeList.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            ShowRecipes();
            UserDetailsCommand = new RelayCommand(execute => UserDetails());
            AddCommand = new RelayCommand(execute => AddRecipes());
            RemoveCommand = new RelayCommand(execute => RemoveRecipes());
            DetailsCommand = new RelayCommand(execute => RecipeDetails());
            InfoCommand = new RelayCommand(execute => CookMasterInfo());
            SignOutCommand = new RelayCommand(execute => SignOut());
        }

        private void SignOut()
        {
            _userManager.CurrentUser = null;
        }

        private void CookMasterInfo()
        {
            throw new NotImplementedException();
        }

        private void RecipeDetails()
        {
            throw new NotImplementedException();
        }

        private void AddRecipes()
        {
            throw new NotImplementedException();
        }

        private void UserDetails()
        {
            RequestUserDetails?.Invoke();
        }

        public void ShowRecipes()
        {
            var filtrerade = FilteredRecipeList.Where(recipe => recipe.CreatedBy == _userManager.CurrentUser);
            FilterRecipeList = new ObservableCollection<Recipe>(filtrerade);
            
            
        }

        

        private void RemoveRecipes() 
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

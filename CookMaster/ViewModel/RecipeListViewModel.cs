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
        public ObservableCollection<Recipe> UserFilteredRecipeList { get; set; }

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

        public event Action RequestAddRecipe;
        public event Action<string> InfoMessage;
        public event Action RequestUserDetails;
        public event Action LogOut;
        public RecipeListViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            _userManager = userManager;
            _recipeManager = recipeManager;
            FilteredRecipeList = new ObservableCollection<Recipe>();

            //FilteredRecipeList.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            //FilteredRecipeList.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });

            SeedDefaultRecipes();
            /*if (_userManager.CurrentUser.UserName is "admin")
            {
                FilterRecipeList = new ObservableCollection<Recipe>();
                FilterRecipeList = _recipeManager.GetAllRecipes();
                
            }*/
            
            
            ShowRecipes();
            UserDetailsCommand = new RelayCommand(execute => UserDetails());
            AddCommand = new RelayCommand(execute => AddRecipes());
            RemoveCommand = new RelayCommand(execute => RemoveRecipes());
            DetailsCommand = new RelayCommand(execute => RecipeDetails());
            InfoCommand = new RelayCommand(execute => CookMasterInfo());
            SignOutCommand = new RelayCommand(execute => SignOut());
        }

        private void SeedDefaultRecipes()
        {
            var currentuser = _userManager.CurrentUser;
            bool userAlreadyHasRecipes = _recipeManager.AllRecipes.Any(r => r.CreatedBy == currentuser );
            if (userAlreadyHasRecipes)
            {
                return;
            }
            _recipeManager.AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
            _recipeManager.AllRecipes.Add(new Recipe { Title = "Köttfärssås", Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", Instructions = "Blanda allt", Category = "Kött", CreatedBy = _userManager._users[0], Date = DateTime.Now });
        }

        private void SignOut()
        {
            _userManager.Logout();
            LogOut?.Invoke();
        }

        private void CookMasterInfo()
        {
            InfoMessage?.Invoke("Välkommen till CookMaster.\n" +
                "Ett nystartat företag som vill göra det enkelt för dig att spara och hantera dina favoritrecept.\n" +
                "Du kan lägga till recept via knappen \"Add\".\n" +
                "Ta bort recpet genom att markera ett recapt och sedan trycka på \"Delete\".\n" +
                "Läsa Receptdetaljer genom att markera ett recept och sedan trycka på \"Details\".\n" +
                "Ändra användarinfo genom att trydcka på ditt \"Namn\".");
        }

        private void RecipeDetails()
        {
            throw new NotImplementedException();
        }

        private void AddRecipes()
        {
            RequestAddRecipe?.Invoke();
        }

        private void UserDetails()
        {
            RequestUserDetails?.Invoke();
        }

        public void ShowRecipes()
        {
            //var filtrerade = FilteredRecipeList.Where(recipe => recipe.CreatedBy == _userManager.CurrentUser);
            UserFilteredRecipeList = new ObservableCollection<Recipe>();
            UserFilteredRecipeList = _recipeManager.GetByUser(_userManager.CurrentUser);
            
            
        }

        

        private void RemoveRecipes() 
        { 
            if(SelectedRecipe != null)
            {
                _recipeManager.RemoveRecipe(SelectedRecipe);
                UserFilteredRecipeList.Remove(SelectedRecipe);
                SelectedRecipe = null;
                //ShowRecipes();
                
            }
            else 
            {
                InfoMessage?.Invoke("Du måste markera ett recept.");
            }
        
        }
        public void FilterRecipes()
        { }
        
    }
}

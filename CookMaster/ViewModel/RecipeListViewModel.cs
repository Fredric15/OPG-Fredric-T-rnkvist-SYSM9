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
using System.Windows;

namespace CookMaster.ViewModel
{
    public class RecipeListViewModel : ViewModelBase
    {

        private readonly UserManager _userManager;
        private readonly RecipeManager _recipeManager;

        //En lista som är bunden till Admins combobox och innehåller alla registrerade användare
        public ObservableCollection<User> AllUsersList { get; set; }
        
        //Använder denna för att sedan kunna filtrera mellan de olika kategorierna
        public ObservableCollection<Recipe> RecipeByCategory { get; set; }
        
        //Receptlista som är bunden till datagriden
        public ObservableCollection<Recipe> VisibleRecipeList { get; set; } = new ObservableCollection<Recipe>();


        public ObservableCollection<Recipe> AllRecipesByUsers { get; set; }

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set { _selectedRecipe = value; OnPropertyChanged(); }
        }

        public List<string> Category { get; set; } = new List<string> { "Visa alla", "Kött", "Fisk och Skaldjur", "Vegetariskt", "Dessert" };
        
        private string _selectedCategory = "Visa alla";
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                    FilterRecipes();
                }
            }
        }
        
        //
        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                OnPropertyChanged();
                FilterByUser();
            }
        }



        //Readonly commands som endast kan läsas av utanför konstruktorn
        public ICommand UserDetailsCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand InfoCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand ResetFilterCommand { get; }

        //Events som fönstret kan prenumerar på
        public event Action RequestRecipeDetails;
        public event Action RequestAddRecipe;
        public event Action<string> InfoMessage;
        public event Action RequestUserDetails;
        public event Action LogOut;
        public RecipeListViewModel(UserManager userManager, RecipeManager recipeManager)
        {

            _userManager = userManager;
            _recipeManager = recipeManager;

            //Skapar en lista på alla användare som Admin sedan kan filtrerar mellan i sin Combobox
            AllUsersList = new ObservableCollection<User>(_userManager.GetAllUsersList());

            //Anropar metoden för att skapa default recipes samt anroper metoden för att visa recepten i vyn
            SeedDefaultRecipes();
            ShowRecipes();

            //Här sätter jag alla Commandsen när programmet startar eftersom jag gjort dom read-only utanför konstruktorn
            UserDetailsCommand = new RelayCommand(execute => UserDetails());
            AddCommand = new RelayCommand(execute => AddRecipes());
            RemoveCommand = new RelayCommand(execute => RemoveRecipes());
            DetailsCommand = new RelayCommand(execute => RecipeDetails());
            InfoCommand = new RelayCommand(execute => CookMasterInfo());
            SignOutCommand = new RelayCommand(execute => SignOut());
            ResetFilterCommand = new RelayCommand(execute => ResetFilter());
        }

        private void ResetFilter()
        {
            //Sätter SelectedUser till null när Admin trycker på "Reset Filter" button
            SelectedUser = null;
            SelectedCategory = "Visa alla";
        }

        private void SeedDefaultRecipes()
        {
            //Skapar ett par defaultrecept
            //Samt innehåller logik för att recepten endast ska skapas en gång när appen startar
            
            bool userAlreadyHasRecipes = _recipeManager.AllRecipes.Any(r => r.Title == "Köttfärssås" || r.Title == "Pannkakor");
            if (userAlreadyHasRecipes)
            {
                return;
            }
            else
            {
                _recipeManager.AllRecipes.Add
                    (new KöttRecipe 
                    { 
                        Title = "Köttfärssås", 
                        Ingredients = "Nötfärs, Tomatsås, Lök, Vitlök, Spaghetti", 
                        Instructions = "Blanda allt", 
                        Category = "Kött",
                        Time = "60",
                        CreatedBy = _userManager._users[0], 
                        Date = DateTime.Now.ToString("yyyy/MM/dd") });
                _recipeManager.AllRecipes.Add
                    (new DessertRecipe {
                        Title = "Pannkakor",
                        Ingredients = "Mjöl, ägg, mjölk, vatten",
                        Instructions = "Blanda allt",
                        Time = "30",
                        Category = "Dessert",
                        CreatedBy = _userManager._users[0],
                        Date = DateTime.Now.ToString("yyyy/MM/dd") });
            }
        }

        private void SignOut()
        {
            //Anropar först _userManagerns Logout-metod för att sätta CurrentUser till null och sedan anropa stängning av fönster
            _userManager.Logout();
            LogOut?.Invoke();
        }

        private void CookMasterInfo()
        {
            //Öppnar en Popup med information om appen
            InfoMessage?.Invoke(
                "Välkommen till CookMaster.\n" +
                "Ett nystartat företag som vill göra det enkelt för dig att spara och hantera dina favoritrecept.\n" +
                "Du kan lägga till recept via knappen \"Add\".\n" +
                "Ta bort recpet genom att markera ett recapt och sedan trycka på \"Delete\".\n" +
                "Läsa Receptdetaljer genom att markera ett recept och sedan trycka på \"Details\".\n" +
                "Ändra användarinfo genom att trydcka på ditt \"Namn\".");
        }

        private void RecipeDetails()
        {
            //Sparar ett markerat recpet in i _recipeManager propertyn så detta sedan kan injiceras i RecipeDetail konstruktorn
            //Öppnar RecipeDetailWindow om ett recept är markerat
            //Annars poppar ett meddelande upp
            if (SelectedRecipe != null)
            {

                _recipeManager.SelectedRecipe = SelectedRecipe;
                RequestRecipeDetails?.Invoke();
                SelectedRecipe = null;


            }
            else
            {
                InfoMessage?.Invoke("Du måste markera ett recept.");

            }
        }

        private void AddRecipes()
        {
            //Öppnar AddRecipeWindow
            RequestAddRecipe?.Invoke();
        }

        private void UserDetails()
        {
            //Öppnar UserDetailsWindow
            RequestUserDetails?.Invoke();
        }

        public void ShowRecipes()
        {
            //Hämtar receptlistan genom att skicka CurrentUser till en metod hos RecipeManager
            //Metoden returnerar listan och sparas i en ObservableCollection som är bunden till vyn
            VisibleRecipeList = _recipeManager.GetByUser(_userManager.CurrentUser);
        }



        private void RemoveRecipes()
        {
            //Om SelectedRecipe är null dyker ett felmedelande upp när man trycker på "Delete"
            //Annars raderas valt recept och sedan sätter Selectedrecipe till null igen så inte nytt recept markeras automatiskt
            if (SelectedRecipe != null)
            {
                _recipeManager.RemoveRecipe(SelectedRecipe);
                VisibleRecipeList.Remove(SelectedRecipe);
                SelectedRecipe = null;


            }
            else
            {
                InfoMessage?.Invoke("Du måste markera ett recept.");
            }
        }
        public void FilterRecipes()
        {
            //Denna metoden filtrerar recepten på vald kategori i en combobox

            RecipeByCategory = _recipeManager.GetByUser(_userManager.CurrentUser);
            VisibleRecipeList.Clear();

            switch (SelectedCategory)
            {
                case "Kött":
                    foreach (var recipe in RecipeByCategory)
                    {
                        if (recipe is KöttRecipe)
                        {
                            VisibleRecipeList?.Add(recipe);
                        }
                    }
                    break;
                case "Fisk och Skaldjur":
                    foreach (var recipe in RecipeByCategory)
                    {
                        if (recipe is FiskochSkaldjurRecipe)
                        {
                            VisibleRecipeList?.Add(recipe);
                        }
                    }
                    break;
                case "Vegetariskt":
                    foreach (var recipe in RecipeByCategory)
                    {
                        if (recipe is VegetarisktRecipe)
                        {
                            VisibleRecipeList?.Add(recipe);
                        }
                    }
                    break;
                case "Dessert":
                    foreach (var recipe in RecipeByCategory)
                    {
                        if (recipe is DessertRecipe)
                        {
                            
                            VisibleRecipeList?.Add(recipe);
                        }
                    }
                    break;
                case "Visa alla":
                    RecipeByCategory = _recipeManager.GetByUser(_userManager.CurrentUser);
                    foreach (var recipe in RecipeByCategory)
                    {
                        VisibleRecipeList?.Add(recipe);
                    }
                    break;
            }
        }
        private void FilterByUser()
        {
            //Denna metoden är till Admins combobox
            //Om Admin inte valt en användare så läggs alla recept till från befintliga användare
            //Här samlar jag först alla recept i en lista som sedan filtreras ut på SelectedUser
            RecipeByCategory = _recipeManager.GetByUser(_userManager.CurrentUser);

            VisibleRecipeList.Clear();
            if (SelectedUser == null)
            {
                foreach (var recipe in RecipeByCategory)
                {
                    VisibleRecipeList.Add(recipe);
                }
            }
            else
            {
                foreach (var recipe in RecipeByCategory)
                {

                    if (recipe.CreatedBy == SelectedUser)
                    {
                        VisibleRecipeList?.Add(recipe);
                    }
                }
            }
        }
    }
}

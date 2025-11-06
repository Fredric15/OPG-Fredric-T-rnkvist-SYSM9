using CookMaster.Managers;
using CookMaster.Model;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookMaster.ViewModel
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly RecipeManager _recipeManager;
        private readonly Recipe _recipe;

        private string _editTitle;
        public string EditTitle
        {
            get { return _editTitle; }
            set 
            { 
                _editTitle = value;
                OnPropertyChanged();
            }
        }
        
        private string _editIngredients;
        public string EditIngredients
        {
            get { return _editIngredients; }
            set 
            { 
                _editIngredients = value;
                OnPropertyChanged();
            }
        }
        
        private string _editInstructions;
        public string EditInstructions
        {
            get { return _editInstructions; }
            set 
            { 
                _editInstructions = value;
                OnPropertyChanged();
            }
        }

        private string _editCategory;
        public string EditCategory
        {
            get { return _editCategory; }
            set 
            { 
                _editCategory = value; 
                OnPropertyChanged();
            }
        }

        private string _editTime;
        public string EditTime
        {
            get { return _editTime; }
            set
            {
                _editTime = value;
                OnPropertyChanged();
            }
        }

        private bool _editable = false;
        public bool Editable
        {
            get { return _editable; }
            set 
            { 
                _editable = value; 
                OnPropertyChanged();
            }
        }
        private string _errorText;

        public string ErrorText
        {
            get { return _errorText; }
            set 
            { 
                _errorText = value;
                OnPropertyChanged();
            }
        }


        public List<string> Category { get; set; } = new List<string> { "Kött", "Fisk och Skaldjur", "Vegetariskt", "Dessert" };

        public ICommand SaveCommand { get;}
        public ICommand EditCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CopyCommand { get; }

        public event Action<string> RecipeSaved;
        public event Action RequestClose;
        public event Action RequestCopy;
        public RecipeDetailViewModel(RecipeManager recipemanager)
        {
            _recipeManager = recipemanager;

            //Fyller i alla fälten med valt recepts properties 
            EditTitle = _recipeManager.SelectedRecipe.Title!;
            EditIngredients = _recipeManager.SelectedRecipe.Ingredients!;
            EditInstructions = _recipeManager.SelectedRecipe.Instructions!;
            EditCategory = _recipeManager.SelectedRecipe.Category!;
            EditTime = _recipeManager.SelectedRecipe.Time!;

            SaveCommand = new RelayCommand(execute => SaveDetails());
            EditCommand = new RelayCommand(execute => EditDetails());
            CancelCommand = new RelayCommand(execute => CancelDetails());
            CopyCommand = new RelayCommand(execute => CopyDetails());
        }

        private void CopyDetails()
        {
            RequestCopy?.Invoke();
        }

        private void CancelDetails()
        {
            RequestClose?.Invoke();
        }

        private void EditDetails()
        {
            //Gör alla fälten editable när man trycker på Edit-button
            
            Editable = true;
        }

        private void SaveDetails()
        {
            //Uppdaterar receptet med ändringar som man fyllt i
            if (!string.IsNullOrWhiteSpace(EditTitle) && !string.IsNullOrWhiteSpace(EditIngredients) && !string.IsNullOrWhiteSpace(EditInstructions) && !string.IsNullOrWhiteSpace(EditTime))
            {
                _recipeManager.SelectedRecipe.Title = EditTitle;
                _recipeManager.SelectedRecipe.Ingredients = EditIngredients;
                _recipeManager.SelectedRecipe.Instructions = EditInstructions;
                _recipeManager.SelectedRecipe.Time = EditTime;
                RecipeSaved?.Invoke("Dina ändringar är sparade.");
                RequestClose?.Invoke();
            }
            else
            {
                ErrorText = "Alla fält måste vara ifyllda.";
            }
        }
    }
}

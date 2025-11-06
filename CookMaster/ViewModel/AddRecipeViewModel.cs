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
    public class AddRecipeViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly RecipeManager _recipeManager;
		private readonly Recipe _recipe;
        
		private string _inputTitle;
		public string InputTitle
		{
			get { return _inputTitle; }
			set 
			{ 
				_inputTitle = value; 
				OnPropertyChanged();
			}
		}
		private string _inputIngredients;

		public string InputIngredients
		{
			get { return _inputIngredients; }
			set 
			{ 
				_inputIngredients = value;
				OnPropertyChanged();
			}
		}
		private string _inputInstructions;

		public string InputInstructions
        {
			get { return _inputInstructions; }
			set 
			{ 
				_inputInstructions = value;
				OnPropertyChanged();
			}
		}
		public List<string> Category { get; set; } = new List<string> {"Kött","Fisk och Skaldjur","Vegetariskt","Dessert"};
		private string _inputSelectedCategory;

		public string SelectedCategory
		{
			get { return _inputSelectedCategory; }
			set 
			{ 
				_inputSelectedCategory = value;
				OnPropertyChanged();
			}
		}

		private string _time;
		public string Time
		{
			get { return _time; }
			set 
			{ 
				_time = value; 
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

		public ICommand AddRecipeCommand {  get;}
		public ICommand CancelCommand { get;}

		//Events som stänger fönster samt skickar bekräftelsemeddelande
		public event Action RequestClose;
		public event Action<string> ConfirmAddRecipe;

		public AddRecipeViewModel(UserManager usermanager, RecipeManager recipemanager)
        {
			_userManager = usermanager;
			_recipeManager = recipemanager;

			AddRecipeCommand = new RelayCommand(execute => AddRecipe());
			CancelCommand = new RelayCommand(execute => Cancel());
        }

        //En konstruktor som används när man kopierar ett recept från RecipeDetailsWindow så att samtliga fält fylls i när AppRecipe öppnas
        public AddRecipeViewModel(UserManager usermanager, RecipeManager recipemanager, Recipe recipe)
		{
            _userManager = usermanager;
            _recipeManager = recipemanager;
            _recipe = recipe;

			InputTitle = recipe.Title;
			InputIngredients = recipe.Ingredients;
			InputInstructions = recipe.Instructions;
			SelectedCategory = recipe.Category;
			Time = recipe.Time;

            AddRecipeCommand = new RelayCommand(execute => AddRecipe());
            CancelCommand = new RelayCommand(execute => Cancel());
        }

        private void Cancel()
        {
            RequestClose?.Invoke();
        }

        private void AddRecipe()
        {
			//Om alla fält är i fyllda så skapas det ett nytt recept i vald kategori
			//Annars ett felmeddelande
			if(!string.IsNullOrWhiteSpace(InputTitle) && !string.IsNullOrWhiteSpace(InputIngredients) && !string.IsNullOrWhiteSpace(InputInstructions) && SelectedCategory != null && !string.IsNullOrWhiteSpace(Time))
			{
				switch (SelectedCategory)
				{
					case "Kött":
						Recipe köttrecipe = new KöttRecipe
						{
							Title = InputTitle,
							Ingredients = InputIngredients,
							Instructions = InputInstructions,
							Category = SelectedCategory,
							CreatedBy = _userManager.CurrentUser,
							Date = DateTime.Now.ToString("yyyy/MM/dd")
						};
						_recipeManager.AddRecipe(köttrecipe);
						ConfirmAddRecipe?.Invoke("Ditt recept är sparat.");
						RequestClose?.Invoke();
						break;

					case "Fisk och Skaldjur":
						Recipe fiskrecipe = new FiskochSkaldjurRecipe
						{
							Title = InputTitle,
							Ingredients = InputIngredients,
							Instructions = InputInstructions,
							Category = SelectedCategory,
							CreatedBy = _userManager.CurrentUser,
							Date = DateTime.Now.ToString("yyyy/MM/dd")
						};
						_recipeManager.AddRecipe(fiskrecipe);
						ConfirmAddRecipe?.Invoke("Ditt recept är sparat.");
						RequestClose?.Invoke();
						break;
					case "Vegetariskt":
						Recipe vegrecipe = new VegetarisktRecipe
						{
							Title = InputTitle,
							Ingredients = InputIngredients,
							Instructions = InputInstructions,
							Category = SelectedCategory,
							CreatedBy = _userManager.CurrentUser,
							Date = DateTime.Now.ToString("yyyy/MM/dd")
						};
						_recipeManager.AddRecipe(vegrecipe);
						ConfirmAddRecipe?.Invoke("Ditt recept är sparat.");
						RequestClose?.Invoke();
						break;

					case "Dessert":
						Recipe dessertrecipe = new DessertRecipe
						{
							Title = InputTitle,
							Ingredients = InputIngredients,
							Instructions = InputInstructions,
							Category = SelectedCategory,
							CreatedBy = _userManager.CurrentUser,
							Date = DateTime.Now.ToString("yyyy/MM/dd")
						};
						_recipeManager.AddRecipe(dessertrecipe);
						ConfirmAddRecipe?.Invoke("Ditt recept är sparat.");
						RequestClose?.Invoke();
						break;
					default:
						break;
				}
			} 
			else
			{
				ErrorText = "Alla fält måste vara ifyllda.";
			}
		}
    }
}

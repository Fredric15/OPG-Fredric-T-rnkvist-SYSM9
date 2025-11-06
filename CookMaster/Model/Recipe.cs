using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Model
{
    public abstract class Recipe
    {
        private string? _title;
        private string? _ingredients;
        private string? _instructions;
        private string? _category;
        private string? _time;
        private string? _date;
        private User? _createdBy;

        public string? Title { get => _title; set => _title = value; }
        public string? Ingredients { get => _ingredients; set => _ingredients = value; }
        public string? Instructions { get => _instructions; set => _instructions = value; }
        public string? Category { get => _category; set => _category = value; }
        public string? Time { get => _time; set => _time = value; }
        public string? Date { get => _date; set => _date = value; }
        public User? CreatedBy { get => _createdBy; set => _createdBy = value; }

    }
}

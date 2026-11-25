using System;

namespace BudgetBuddy.Models
{
    public class Category
    {
        private int _categoryId;
        private string _name;
        private bool _isPreset;

        public int CategoryId
        {
            get => _categoryId;
            set => _categoryId = value;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Category name cannot be empty.");
                _name = value.Trim();
            }
        }

        public bool IsPreset
        {
            get => _isPreset;
            set => _isPreset = value;
        }

        public Category()
        {
            _name = "General";
            _isPreset = false;
        }

        public Category(int categoryId, string name, bool isPreset)
        {
            CategoryId = categoryId;
            Name = name;
            IsPreset = isPreset;
        }

        public override string ToString() => Name;
    }
}

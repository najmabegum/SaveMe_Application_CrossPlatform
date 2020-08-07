using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class Category
    {
        private string _categoryName;

        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
            }
        }
    }
}

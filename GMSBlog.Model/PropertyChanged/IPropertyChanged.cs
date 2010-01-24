using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMSBlog.Model.PropertyChanged
{
    interface IPropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
}

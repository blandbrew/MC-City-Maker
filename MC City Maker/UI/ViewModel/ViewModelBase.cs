using MC_City_Maker.Grid_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.UI.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected static int Property_SizeOfTheGrid;
        protected static Coordinate StartCoordinate = new Coordinate();




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            //Console.WriteLine("Changed: " + propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            //Console.WriteLine("Changed: " + propertyName);
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}

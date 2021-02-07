using MC_City_Maker.UI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MC_City_Maker.UI.ViewModel
{
    public class NewCity_ViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private ICommand closeWindow;

        public NewCity_ViewModel()
        {

        }

        ObservableCollection<string> GridMapSizes = new ObservableCollection<string>()
        {
            "1","4","9","16","25","36","49","64","81","100","121","144","169","196"
        };
        //public ObservableCollection<UIRectangle> RectItems { get; set; }

        public ObservableCollection<string> GridSizes
        {
            get { return GridMapSizes; }
            set { GridMapSizes = value; }
        }
 
        public int SelectedGridSize
        {
            get { return Property_SizeOfTheGrid; }
            set
            {
                Property_SizeOfTheGrid = value;
                
            }
        }

        private int _xVal;
        public int xVal
        {
            get { return StartCoordinate.x; }
            set
            {
                StartCoordinate.x = value;
            }
        }
        private int _yVal;
        public int yVal
        {
            get { return StartCoordinate.y; }
            set
            {
                StartCoordinate.y = value;
            }
        }

        private int _zVal;
        public int zVal
        {
            get { return StartCoordinate.z; }
            set
            {
                StartCoordinate.z = value;
            }
        }
    }
}

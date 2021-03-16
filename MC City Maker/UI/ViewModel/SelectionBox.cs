using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MC_City_Maker.UI.ViewModel
{
    public class SelectionBox : INotifyPropertyChanged
    {
        public Rectangle selectionBox { get; set; }

        private double _selectionBoxX;
        public double selectionBoxX
        {
            get { return _selectionBoxX; }
            set { _selectionBoxX = value; RaisePropertyChanged(nameof(selectionBoxX)); }
        }

        private double _selectionBoxY;
        public double selectionBoxY
        {
            get { return _selectionBoxY; }
            set { _selectionBoxY = value; RaisePropertyChanged(nameof(selectionBoxY)); }
        }

        private double _selectionBoxWidth;
        public double selectionBoxWidth
        {
            get { return _selectionBoxWidth; }
            set { _selectionBoxWidth = value; RaisePropertyChanged(nameof(selectionBoxWidth)); }
        }

        private double _selectionBoxHeight;
        public double selectionBoxHeight
        {
            get { return _selectionBoxHeight; }
            set { _selectionBoxHeight = value; RaisePropertyChanged(nameof(selectionBoxHeight)); }
        }

        private Visibility _selectionBoxVisibility;
        public Visibility selectionBoxVisibility
        {
            get { return _selectionBoxVisibility; }
            set { _selectionBoxVisibility = value; RaisePropertyChanged(nameof(selectionBoxVisibility)); }
        }

        private SolidColorBrush _Brush;
        public SolidColorBrush Brush
        {
            get { return _Brush; }
            set { _Brush = value; RaisePropertyChanged(nameof(Brush)); }
        }

        public SelectionBox(double x, double y, double width, double height, SolidColorBrush aBrush)
        {
            
            selectionBoxX = x;
            selectionBoxY = y;
            selectionBoxWidth = width;
            selectionBoxHeight = height;
            Brush = aBrush;
        }










        public event PropertyChangedEventHandler PropertyChanged;
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

using MC_City_Maker.Constants;
using MC_City_Maker.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MC_City_Maker.UI
{
    class _UI_GridContainer : GridMap_ViewModel
    {
        public (int,int) ContainerArrayCoordinate { get; set; }
        bool selected { get; set; }

        private double _X;
        public double X
        {
            get { return _X; }
            set { _X = value; RaisePropertyChanged(nameof(X)); }
        }

        private double _Y;
        public double Y
        {
            get { return _Y; }
            set { _Y = value; RaisePropertyChanged(nameof(Y)); }
        }

        private double _Width;
        public double Width
        {
            get { return _Width; }
            set { _Width = value; RaisePropertyChanged(nameof(Width)); }
        }

        private double _Height;
        public double Height
        {
            get { return _Height; }
            set { _Height = value; RaisePropertyChanged(nameof(Height)); }
        }

        private System.Windows.Media.Color _Color;
        public System.Windows.Media.Color Color
        {
            get { return _Color; }
            set { _Color = value; RaisePropertyChanged(nameof(Color)); }
        }

        private System.Windows.Media.Color _FillColor;
        public System.Windows.Media.Color FillColor
        {
            get { return _FillColor; }
            set { _FillColor = value; RaisePropertyChanged(nameof(FillColor)); }
        }

        public _UI_GridContainer(double x, double y, Color color, Color fillcolor)
        {
            X = x;
            Y = y;
            Width = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Height = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Color = color;
            FillColor = fillcolor;
            //ClickGridContainer = new RelayCommand(new Action<object>(SelectContiner));
        }


    }
}

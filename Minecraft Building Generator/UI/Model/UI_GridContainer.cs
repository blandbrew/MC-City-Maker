using Minecraft_Building_Generator.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Minecraft_Building_Generator.UI.Model
{
    public class UI_GridContainer : GridMap_ViewModel
    {
        public Rectangle rect { get; set; }
        public (int, int) ContainerArrayCoordinate { get; private set; }

        public bool selected { get; set; } = false;
        public bool previously_selected { get; set; } = false;
        public GridSquare_Zoning zone { get; set; }

        /// <summary>
        /// Stored Grid square array in each container
        /// </summary>
        public UI_Grid_Square[,] ui_GridSquares_array { get; set; }

        public HashSet<UI_GridContainer> AdjacentContainers { get; set; }

        


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

        public UI_GridContainer(double x, double y, Color color, Color fillcolor, (int, int) containerarraycoords)
        {
            X = x;
            Y = y;
            Width = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Height = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Color = color;
            FillColor = fillcolor;
            ContainerArrayCoordinate = containerarraycoords;
            AdjacentContainers = new HashSet<UI_GridContainer>();

        }


    }
}

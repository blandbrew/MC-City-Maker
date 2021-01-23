using MC_City_Maker.Constants;
using MC_City_Maker.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MC_City_Maker.UI.Model
{
    public class UI_GridContainer : GridMap_ViewModel
    {
       
        public (int, int) ContainerArrayCoordinate { get; private set; }



        //public GridSquare_Zoning zone { get; set; }

        /// <summary>
        /// Stored Grid square array in each container
        /// </summary>
        public UI_Grid_Square[,] ui_GridSquares_array { get; set; }

        public HashSet<UI_GridContainer> AdjacentContainers { get; set; }



        //public (int, int) _ContainerArrayCoordinate;
        //public (int, int) ContainerArrayCoordinate
        //{
        //    get { return _ContainerArrayCoordinate; }
        //    set
        //    {
        //        _ContainerArrayCoordinate = value; RaisePropertyChanged(nameof(ContainerArrayCoordinate));
        //    }
        //}

        private UI_GridContainer _Selected_container;
        public UI_GridContainer Selected_container
        {
            get { return _Selected_container; }
            set { _Selected_container = value; RaisePropertyChanged(nameof(Selected_container)); }

        }

        private UI_GridContainer _PreviouslySelected_container;
        public UI_GridContainer PreviouslySelected_container
        {
            get { return _PreviouslySelected_container; }
            set { _PreviouslySelected_container = value; RaisePropertyChanged(nameof(PreviouslySelected_container)); }

        }
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

        //public void SelectedContainer(UI_GridContainer selected)
        //{
        //    //If true, reset the container.  if False, set to true and mark as selected

        //    if (PreviouslySelected_container == null)
        //    {
        //        PreviouslySelected_container = selected;
        //        selected.FillColor = UI_Constants.Selected_Container_Color;
        //        return;
        //    }

        //    //if same container clicked - clears it
        //    if(PreviouslySelected_container == selected)
        //    {
        //        selected.FillColor = UI_Constants.Unselected_grid;
        //        PreviouslySelected_container = null;
        //        return;
        //    }

        //    if(PreviouslySelected_container != selected)
        //    {
        //        PreviouslySelected_container.FillColor = UI_Constants.Unselected_grid;
        //        PreviouslySelected_container = selected;
        //        selected.FillColor = UI_Constants.Selected_Container_Color;

        //    } 
        //}
    }
}

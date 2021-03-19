using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using MC_City_Maker.Structures.Buildings.Building_Construction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Grid_Zones.Structures
{

    //public enum BuildingClass { Residential, Commercial, Industrial, Other, None}
    //public enum BuildingMaterial { Glass, Wooden, Rock, Concrete, Jewel, Other, None}

    public abstract class abstract_Building : INotifyPropertyChanged
    {


        protected Coordinate startCoordinate; //Upper left most point
        protected Coordinate EndCoordinate; //lower right most point

        //This is necessary to store larger buildings that take up more than one square
        protected Grid_Square startSquare; //upper left most square
        protected Grid_Square endSquare; //lower right most square

        //Building Description
        protected int NumberOfFloors { get; set; } = 1;
        protected int Height { get; set; } = 4;
        protected int Width { get; set; } = 1;
        protected int Length { get; set; } = 1;
        protected int SpaceBetweenFloors { get; set; } = 4;
        protected bool Windows { get; set; }

  
        protected List<Coordinate> Doors { get; set; }
        protected Block WallBlock { get; set; }
        protected Roof RoofType { get; set; }

        protected Direction Direction {get; set;}

        protected BuildingClass building_Class;
        //protected BuildingType building_Type;



        //protected ObservableCollection<string> building_class { get; private set; } = new ObservableCollection<string>()
        //{
        //    "1","4","9","16","25","36","49","64","81","100","121","144","169","196"
        //};

        //protected ObservableCollection<string> building_type { get; private set; } = new ObservableCollection<string>()
        //{
        //    "1","4","9","16","25","36","49","64","81","100","121","144","169","196"
        //};






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




        //Subterranian Features
        //bool isUnderground;
        //int maxDepth;

        //BuildingProperties
        //block type
        //block color
        //door
        //windows
        //roof





    }
}

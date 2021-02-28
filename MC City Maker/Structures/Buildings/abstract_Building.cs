using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using MC_City_Maker.Structures.Buildings.Building_Construction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Structures
{

    //public enum BuildingClass { Residential, Commercial, Industrial, Other, None}
    //public enum BuildingMaterial { Glass, Wooden, Rock, Concrete, Jewel, Other, None}

    public abstract class abstract_Building : INotifyPropertyChanged
    {


        protected Coordinate startCoordinate; //Upper left most point
        protected Coordinate EndCoordinate; //lower right most point
        //Building Description
        protected int NumberOfFloors { get; set; }
        protected int Height { get; set; }
        protected int Width { get; set; }
        protected int SpaceBetweenFloors { get; set; } = 4;
        protected bool Windows { get; set; }

  
        protected List<Coordinate> Doors { get; set; }
        protected Block WallBlock { get; set; }
        protected Roof RoofType { get; set; }

        protected Direction Direction {get; set;}

        protected BuildingClass building_Class;
        protected BuildingType building_Type;



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

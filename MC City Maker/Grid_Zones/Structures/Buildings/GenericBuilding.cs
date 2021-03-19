using MC_City_Maker.Command_Generator;
using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Grid_Zones.Structures
{
    public class GenericBuilding : abstract_Building, IBuilding
    {
        //inherited variables
        //protected int NnumberOfFloors { get; }
        //protected int MaxHeight { get; }
        //protected int MaxWidth { get; set; } = 13;
        //protected int SpaceBetweenFloors { get; set; } = 4;

        string buildingType;
        string color;
        string blockType;


        public ObservableCollection<string> UISelectionLength { get; private set; } = new ObservableCollection<string>()
        {
             "1","2","3","4","5","6","7","8","9","10","11","12","13"
        };

        public ObservableCollection<string> UISelectionWidth { get; private set; } = new ObservableCollection<string>()
        {
            "1","2","3","4","5","6","7","8","9","10","11","12","13"
        };
        public ObservableCollection<string> UISpaceBetweenFloors { get; private set; } = new ObservableCollection<string>()
        {
            "3","4"
        };
        public ObservableCollection<string> UINumberOfFloors { get; private set; } = new ObservableCollection<string>()
        {
            "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23",
            "24","25","26","27","28","29","30","31","32","33","34","35","36","37","38","39","40","41","42","43","44",
            "45","46","47","48","49","50","51","52","53","54","55","56","57","58","59","60"

        };

        public ObservableCollection<string> UISelectionRooftop { get; private set; } = new ObservableCollection<string>()
        {
             "Flat","Overhang","A-Frame","Pyramid",
        };

        //"13","26","39","52","65","78","91","104","117","130","143","156","169"

        public GenericBuilding(int height, BuildingClass buildingClass)
        {
            Height = height;
        }

        public GenericBuilding(int height)
        {

        }


        public GenericBuilding()
        {
            
        }


        public string TemplateLabelTest { get; set; }
        
        //How to access protected values
        public int width
        {
            get { return Width; }
            set { Width = value;
                  RaisePropertyChanged(nameof(width)); }
        }

        public int height
        {
            get { return Height; }
            set { Height = value; RaisePropertyChanged(nameof(height)); }
        }
        public int length
        {
            get { return Length; }
            set { Length = value; RaisePropertyChanged(nameof(length)); }
        }

        public int numberOfFloors
        {
            get { return NumberOfFloors; }
            set {
                    NumberOfFloors = value;
                    height = (NumberOfFloors * spaceBetweenFloors) + spaceBetweenFloors + 1;
                    RaisePropertyChanged(nameof(numberOfFloors)); }
        }

        public int spaceBetweenFloors
        {
            get { return SpaceBetweenFloors; }
            set { SpaceBetweenFloors = value;
                  height = (numberOfFloors * SpaceBetweenFloors) + SpaceBetweenFloors + 1; 
                  RaisePropertyChanged(nameof(spaceBetweenFloors)); }
        }


        /// <summary>
        /// Converts UI Selected width or length and multiplies it by 12 because there are 13 blocks [0-12]
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        private int ConvertUISquareToGrid(int selectedValue)
        {
            return selectedValue * 12;
        }












        /*BUILD Methods*/







        public void Building_OutsideWalls(Grid_Square square)
        {

            
            //Coordinate endPoint = new Coordinate(startingPoint.x + Width, startingPoint.y + Height, startingPoint.z + Width);

            //adds hollow command
            Generate_Commands.Add_Command($"fill {square.startCoordinate.x} {square.startCoordinate.y+Shared_Constants.FLAT_WORLD_STARTING_Y} {square.startCoordinate.z} {square.endCoordinate.x} {square.endCoordinate.y + Height} {square.endCoordinate.z} glass 1 hollow");

            //throw new NotImplementedException();
        }

        public void Building_Floor(Grid_Square square)
        {
            NumberOfFloors = Height / 4; //adds an additional floor onto the building height...may need to reconsider this
            for (int i = 0; i < NumberOfFloors; i++)
            {

                //Coordinate endPoint = new Coordinate(startingPoint.x + Width, startingPoint.y, startingPoint.z + Width );
                square.startCoordinate.y += 4;
                square.endCoordinate.y += 4;
                Generate_Commands.Add_Command($"fill {square.startCoordinate.x} {square.startCoordinate.y} {square.startCoordinate.z} {square.endCoordinate.x} {square.endCoordinate.y} {square.endCoordinate.z} stone");
                
            }
        }

        public void Building_Lighting()
        {
            throw new NotImplementedException();
        }

        public void Building_Door(string direction)
        {
            throw new NotImplementedException();
        }

        public void Building_Ladder()
        {
            throw new NotImplementedException();
        }

        public void Building_Windows()
        {
            throw new NotImplementedException();
        }

        public void Building_Rooftop(Grid_Square square)
        {
            //builds a pyramid top
            //need to create properties definitions for various building structure types, frames, architecture
            //Coordinate endPoint = new Coordinate(startPoint.x + Width, Height, startPoint.z + Width);

            for(int i = 0; i < Width/2; i++)
            {
                Generate_Commands.Add_Command($"fill {square.startCoordinate.x+i} {Height+i} {square.startCoordinate.z+i} {square.endCoordinate.x-i} {Height+i} {square.endCoordinate.z-i} concrete 9");
            }

            //throw new NotImplementedException();
        }

        public void Building_Direction()
        {
            throw new NotImplementedException();
        }
    }
}

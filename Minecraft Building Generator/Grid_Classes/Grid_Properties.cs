using Minecraft_Building_Generator.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Grid_Classes
{
    public abstract class Grid_Properties
    {

        /*Variables*/
        protected Coordinate StartCoordinate;
        public Coordinate startCoordinate
        {
            get { return StartCoordinate; }   
            set { StartCoordinate = value; }                               
        }


        protected Coordinate EndCoordinate;
        public Coordinate endCoordinate   
        {
            get { return EndCoordinate; }   
            set { EndCoordinate = value; }                              
        }


        protected Coordinate Centerblock;
        public Coordinate centerblock  
        {
            get { return Centerblock; }   
            set { Centerblock = value; }                               
        }


        protected int Padding;
        public int padding
        {
            get { return Padding; }
            set { Padding = value; }
        }


        protected int Offset;
        public int offset
        {
            get { return Offset; }
            set { Offset = value; }
        }

        protected int Margin;
        public int margin
        {
            get { return Margin; }
            set { Margin = value; }
        }

        protected bool IsValid;
        public bool isValid
        {
            get { return IsValid; }
            set { IsValid = value; }                               
        }


    }
}

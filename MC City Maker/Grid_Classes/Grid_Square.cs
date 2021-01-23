//This class defines a 13x13 grid square.
//each can have specific properties such ash
//marking adjacent grids, offsets for roads, subterannian features, etc.

using MC_City_Maker.UI;
using MC_City_Maker.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace MC_City_Maker.Grid_Classes
{
    public class Grid_Square : Grid_Properties, IGrid_Square
    {

        public GridSquare_Zoning zone { get; set; }

        public (int, int) ParentContainerArrayCoordinate { get; private set; }
        public (int, int) SquareArrayCoordinate { get; private set; }

        public Rectangle gridSquareRectangle { get; set; }



        /*Variables*/
        /// <summary>
        /// Grid Squares that are adjacent, maximum number of 4.  Excludes Diagnal Squares
        /// </summary>
        public HashSet<Grid_Square> adjacent_Squares { get; set; }

        /// <summary>
        /// Squares that have been marked for adjacent building.
        /// </summary>
        public HashSet<Grid_Square> Joined_Squares { get; set; }

        


        /*Constructor*/
        public Grid_Square(Coordinate startPoint)
        {
            startCoordinate = startPoint;
            adjacent_Squares = new HashSet<Grid_Square>();
        }

        /// <summary>
        /// Create Grid squares and define array coordinates for the grid layout
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="parentContainerArrayCoordinate"></param>
        /// <param name="squareArrayCoordinate"></param>
        public Grid_Square(Coordinate startPoint, (int, int) parentContainerArrayCoordinate, (int,int) squareArrayCoordinate)
        {
            startCoordinate = startPoint;
            adjacent_Squares = new HashSet<Grid_Square>();
            SquareArrayCoordinate = squareArrayCoordinate;
            ParentContainerArrayCoordinate = parentContainerArrayCoordinate;
        }


        /*Methods*/
        public void Add_Adjacent_Square(Grid_Square adjacentSquare)
        {
            this.adjacent_Squares.Add(adjacentSquare);
            
        }

        public HashSet<Grid_Square> GetAll_Adjacent_Squares()
        {
            return adjacent_Squares;
            throw new NotImplementedException();
        }


    }
}

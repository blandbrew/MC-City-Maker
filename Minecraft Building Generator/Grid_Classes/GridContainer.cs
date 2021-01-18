/**Grid Container Class
 * 
 * Description:
 * Grid container class is the container of smaller grid squares. 
 * Each grid container is 169x169 blocks
 * 
 * 
 */

using Minecraft_Building_Generator.UI;
using Minecraft_Building_Generator.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Grid_Classes
{
    public class Grid_Container : Grid_Properties, IGrid_Container
    {

        /*Variables*/
        public Grid_Square[,] gridSquareMap { get; set; }
        public (int, int) ContainerArrayCoordinate { get; private set; }
        public List<Grid_Container> adjacent_Container_List { get; set; }
        
        //public UI_GridContainer[,] UI_GridPlanner { get; set; }


        /*Constructor*/
        public Grid_Container(Coordinate startPoint)
        {
            startCoordinate = startPoint;
            adjacent_Container_List = new List<Grid_Container>();
        }

        public Grid_Container(Coordinate startPoint, (int, int) ContainerArrayCoordinate)
        {
            startCoordinate = startPoint;
            adjacent_Container_List = new List<Grid_Container>();
            this.ContainerArrayCoordinate = ContainerArrayCoordinate;
        }

        /*Methods*/
        public void Add_Adjacent_Container(Grid_Container adjacentContainer)
        {
            adjacent_Container_List.Add(adjacentContainer);
        }

        public List<Grid_Container> GetAll_Adjacent_Containers()
        {
            return adjacent_Container_List;
        }

    }
}

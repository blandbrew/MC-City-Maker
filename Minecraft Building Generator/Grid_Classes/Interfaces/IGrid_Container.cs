using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Grid_Classes
{
    interface IGrid_Container
    {

        void Add_Adjacent_Container(Grid_Container adjacentContainer);

        List<Grid_Container> GetAll_Adjacent_Containers();
    }
}

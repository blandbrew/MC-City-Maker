using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Grid_Classes
{
    interface IGrid_Container
    {

        void Add_Adjacent_Container(Grid_Container adjacentContainer);

        HashSet<Grid_Container> GetAll_Adjacent_Containers();
    }
}

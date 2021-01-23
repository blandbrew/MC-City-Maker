using MC_City_Maker.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Structures.Interfaces
{
    interface IBuildingUnderground
    {
        void BuildUnderground(int depth, int width);

        void ConnectToAdjacentUnderground(Grid_Container container, Grid_Square square);
    }
}

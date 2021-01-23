using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Grid_Classes
{
    interface IGrid_Square
    {

        void Add_Adjacent_Square(Grid_Square adjacentSquare);

        HashSet<Grid_Square> GetAll_Adjacent_Squares();

    }
}

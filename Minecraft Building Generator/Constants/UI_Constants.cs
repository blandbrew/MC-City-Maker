using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Minecraft_Building_Generator.Constants
{
    public static class UI_Constants
    {
        public const int UI_Rectangle_Width = 15;
        public const int UI_Rectangle_Height = 15;

        public static Color GetZoningColor(GridSquare_Zoning zone)
        {
            //Building, Infrustructure, Scenery, Water, None
            switch (zone)
            {
                case GridSquare_Zoning.Building:
                {
                    return Colors.Gray;
                }
                case GridSquare_Zoning.Infrustructure:
                {
                    return Colors.Black;
                }
                case GridSquare_Zoning.Water:
                {
                    return Colors.Gray;
                }
                case GridSquare_Zoning.Scenery:
                {
                    return Colors.Gray;
                }
            }

            return Colors.Transparent;
        }

    }
}

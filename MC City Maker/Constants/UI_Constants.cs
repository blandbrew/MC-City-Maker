using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MC_City_Maker.Constants
{
    public static class UI_Constants
    {
        public const int UI_Rectangle_Width = 15;
        public const int UI_Rectangle_Height = 15;
        public static Color Selected_Container_Color = Colors.Red;
        public static Color Unselected_grid = Colors.White;

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
                    return Colors.Blue;
                }
                case GridSquare_Zoning.Scenery:
                {
                    return Colors.Green;
                }
                case GridSquare_Zoning.None:
                {
                    return Colors.White;
                }
            }

            return Colors.Transparent;
        }

        public static GridSquare_Zoning StringToZoneConverter(string zone)
        {
            switch (zone)
            {
                case Shared_Constants.BUILDING:
                    {
                        return GridSquare_Zoning.Building;
                    }
                case Shared_Constants.INFRUSTRUCTURE:
                    {
                        return GridSquare_Zoning.Infrustructure;
                    }
                case Shared_Constants.WATER:
                    {
                        return GridSquare_Zoning.Water;
                    }
                case Shared_Constants.SCENARY:
                    {
                        return GridSquare_Zoning.Scenery;
                    }
                default:
                    {
                        return GridSquare_Zoning.None;
                    }
            }
        }
    }
}

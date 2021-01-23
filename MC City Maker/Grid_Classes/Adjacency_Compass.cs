//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Minecraft_Building_Generator.UI
//{
//    public class Adjacency_Compass
//    {
//        public Point ContainerCoordinates { get; private set; }
//        public Point SquareCoordinates { get; private set; }

//        public UI_Grid_Planning_Container Container { get; private set; }
//        public UI_Grid_Planning_Square Square { get; private set; }

       

//        public Adjacency_Compass(int containerX, int containerY, int squareX, int squareY, UI_Grid_Planning_Container aContainer, UI_Grid_Planning_Square aSquare)
//        {
//            ContainerCoordinates = new Point(containerX, containerY);
//            SquareCoordinates = new Point(squareX, squareY);
//            Container = aContainer;
//            Square = aSquare;
//        }

//        public Adjacency_Compass(int containerX, int containerY, UI_Grid_Planning_Container aContainer)
//        {
//            ContainerCoordinates = new Point(containerX, containerY);
//            Container = aContainer;
//        }
//        public Adjacency_Compass(int squareX, int squareY, UI_Grid_Planning_Square aSquare)
//        {
//            SquareCoordinates = new Point(squareX, squareY);
//            Square = aSquare;
//        }

        
//    }
//}

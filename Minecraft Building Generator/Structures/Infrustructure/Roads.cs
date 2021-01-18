using Minecraft_Building_Generator.Command_Generator;
using Minecraft_Building_Generator.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Structures.Infrustructure
{
    public class Roads
    {
        /**
         * City Generator roads will consume 1 Grid_Square.
         * 
         */
        Coordinate StartPoint;
        int StreetWidth { get; set; } = 8;
        int SidewalkWidth { get; set; } = 2;

        Coordinate CenterlineStart;
        Coordinate CrosswalkStart;

        Direction roadDirection;
        public Roads()
        {

        }

        public void Build_Road(Coordinate startingPoint, Direction direction)
        {
            if( direction == Direction.North || direction == Direction.South)
            {
                Coordinate endPoint = new Coordinate(startingPoint.x + Shared_Constants.GRID_SQUARE_SIZE-1, startingPoint.y, startingPoint.z + Shared_Constants.GRID_SQUARE_SIZE-1);
    
                //fills entire square black
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y+Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y+Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                //draws the center yellow line
                Generate_Commands.Add_Command($"fill {startingPoint.x+6} {startingPoint.y+ Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x-7} {endPoint.y+Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete

                //draws the two sidewalks
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x - 11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete
                Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");
            } else if(direction == Direction.West || direction == Direction.East)
            {
                Coordinate endPoint = new Coordinate(startingPoint.x + Shared_Constants.GRID_SQUARE_SIZE-1, startingPoint.y, startingPoint.z + Shared_Constants.GRID_SQUARE_SIZE-1);

                //fills entire square black
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                //draws the center yellow line
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 6} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z-7} concrete 4"); //yellow concrete

                //draws the two sidewalks
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 12} concrete 8"); //grey concrete
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 11} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");
            }

        }

        public void Road_Adjacency(HashSet<Grid_Square> adjacentSquaresList, Grid_Square startSquare)
        {
            Coordinate startingPoint = startSquare.startCoordinate;
            //Coordinate endPoint = new Coordinate(startingPoint.x + Shared_Constants.GRID_SQUARE_SIZE - 1, startingPoint.y, startingPoint.z + Shared_Constants.GRID_SQUARE_SIZE - 1);
            Coordinate endPoint = startSquare.endCoordinate;
            Coordinate centerPoint = startSquare.centerblock;


            //determine if adj square is a road
            //which direction is it in?
            bool north = false;
            bool east = false;
            bool south = false;
            bool west = false;


            int intersectionCount = 0;
            List<Grid_Square> adjacentRoads = new List<Grid_Square>();
            foreach (Grid_Square adjsquare in adjacentSquaresList)
            {
                if (adjsquare.zone == GridSquare_Zoning.Infrustructure)
                {
                    if (adjsquare.SquareArrayCoordinate.Item1 < startSquare.SquareArrayCoordinate.Item1)   
                    {//if X is greater, adjsquare is above(north)

                        north = true;
                        intersectionCount++;
                    } else if(adjsquare.SquareArrayCoordinate.Item1 > startSquare.SquareArrayCoordinate.Item1)
                    {//if X is less, adjsquare is below (south)
                        south = true;
                        intersectionCount++;
                    }
                    else if (adjsquare.SquareArrayCoordinate.Item2 > startSquare.SquareArrayCoordinate.Item2)
                    {//if Y is greater, adjsquare is right(east)
                        east = true;
                        intersectionCount++;
                    }
                    else if (adjsquare.SquareArrayCoordinate.Item2 < startSquare.SquareArrayCoordinate.Item2)
                    {//if Y is less, adjsquare is left (west)
                        west = true;
                        intersectionCount++;
                    }
                }//end of if for Infrustructure test
            }

            //if this is an orphaned road
            if(intersectionCount == 0)
            {
                //fills entire square black
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                //draws the center yellow line
                Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete

                //draws the two sidewalks
                //west side
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x - 11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete

                //East Side
                Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");
                return;
            }

            if (intersectionCount == 1)
            {
                if(north || south)
                {
                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete

                    //draws the two sidewalks
                    //west side
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x-11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete
                   
                    //East Side
                    Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");

                    return;
                } else
                {
                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete

                    //draws the two sidewalks
                    //north side
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 11} concrete 8"); //grey concrete
                    
                    //south side
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 11} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");
                    return;
                }
            }

            if(intersectionCount == 2)
            {
                if(north && south)
                {
                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete

                    //draws the two sidewalks
                    //west side
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x - 11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete

                    //East Side
                    Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");

                    return;
                }
                if(north && east)
                {

                    //*
                    //**

                    //start point North west corner
                    //draw west sidewalk first startpoint X

                    //draw two squares on north east size  startpoint x + 11
                    //fill startpoint x + 11
                    //to endpoint -> startPoint X + 12 and startpoint z+1
                    //draw 2 bricks for yellow line
                    //fill startpoint x+11, z+7
                    //fill endpoint -> startpoint x+12, z+7
                    //draw final squares on south east corner
                    //fill endpoint to
                    //endpoint x-1, z-1

                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line from north to center
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete
                    
                    //yellow line center to east
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete


                    //West side side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x-11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete
                    
                    //north east side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+1} concrete 8");

                    //south east side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+11} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete
                    return;
                }
                if (north && west)
                {
                    //build north and west
                    // *
                    //**

                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {centerPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete
                    //2 yellow center line bricks
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete

                    //east side side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x+11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete

                    //north west side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {startingPoint.x + 1} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 1} concrete 8");

                    //south side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 11} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete
                    return;
                }
                if (east && west)
                {
                    // ***
                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete

                    //draws the two sidewalks
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 11} concrete 8"); //grey concrete
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 11} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");
                    return;
                }
                if(south && west)
                {
                    //**
                    // *

                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {centerPoint.x} {centerPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete
                    //draws right angle yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {centerPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {centerPoint.x} {centerPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete

                    // n/s east side side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete

                    // e/w north side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 11} concrete 8");

                    //south west square
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+11} {startingPoint.x+1} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete
                    return;
                }
                if(south && east)
                {
                    //south and east
                    //**
                    //*

                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete
                    //draws right angle yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {centerPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete

                    //west side side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x-11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete
                    //North Sidewalk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z-11} concrete 8"); //grey concrete

                    //southeast side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x+11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+11} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");

                    return;
                }
            }

            if (intersectionCount == 3)
            {
                if(north && east && west)
                {
                    // *
                    //***

                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete
                    //2 yellow center line bricks
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+1} concrete 4"); //grey concrete

                    //south Sidewalk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+11} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete

                    //north west side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {startingPoint.x + 1} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 1} concrete 8");

                    //north east side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 11} concrete 8");
                    return;
                }
                if(north && east && south)
                {
                    //*
                    //**
                    //*
                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete

                    //West side side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x - 11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete

                    //north east side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 11} concrete 8");

                    //2 yellow center line bricks
                    Generate_Commands.Add_Command($"fill {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x-1} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //grey concrete

                    //south east side square
                    Generate_Commands.Add_Command($"fill {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} {endPoint.x - 1} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 1} concrete 8"); //grey concrete
                    return;
                }
                if (east && west && south)
                {
                    //***
                    // *

                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete

                    //North Sidewalk
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z-11} concrete 8"); //grey concrete

                    //south west side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+11} {endPoint.x-11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");

                    //2 yellow center line bricks
                    Generate_Commands.Add_Command($"fill {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x - 1} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //grey concrete

                    //south east side square
                    Generate_Commands.Add_Command($"fill {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} {endPoint.x - 1} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 1} concrete 8"); //grey concrete
                    return;

                }
                if(south && west && north)
                {
                    // *
                    //**
                    // *
                    //fills entire square black
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                    //draws the center yellow line
                    Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete
                    //2 yellow center line bricks
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {startingPoint.x+1} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //grey concrete

                    //East side side walk
                    Generate_Commands.Add_Command($"fill {startingPoint.x+11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8"); //grey concrete

                    //north west side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {startingPoint.x+1} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z+1} concrete 8");

                    //south west side square
                    Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 11} {endPoint.x - 11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");
                    return;
                }
            }


            if (intersectionCount == 4)
            {
                // *
                //***
                // *

                //fills entire square black
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 15"); //black concrete

                //draws the center yellow line - NS
                Generate_Commands.Add_Command($"fill {centerPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {centerPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 4"); //yellow concrete
                //draws the center yellow line - EW
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {centerPoint.z} concrete 4"); //yellow concrete


                //north west side square
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {startingPoint.x + 1} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 1} concrete 8");

                //north east side square
                Generate_Commands.Add_Command($"fill {startingPoint.x + 11} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z} {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 11} concrete 8");

                //south east side square
                Generate_Commands.Add_Command($"fill {endPoint.x} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} {endPoint.x - 1} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z - 1} concrete 8"); //grey concrete

                //south west side square
                Generate_Commands.Add_Command($"fill {startingPoint.x} {startingPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {startingPoint.z + 11} {endPoint.x - 11} {endPoint.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {endPoint.z} concrete 8");


                return;
            }
        }
    }
}

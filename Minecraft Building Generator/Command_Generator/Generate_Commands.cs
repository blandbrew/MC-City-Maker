using Minecraft_Building_Generator.Grid_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Command_Generator
{
    public class Generate_Commands
    {

        //List of commands to be exported to the mcfunction file
        private static List<string> generatedFunctionLines = new List<string>();



        /// <summary>
        /// Adds a command to the list
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static int Add_Command(string command)
        {
            generatedFunctionLines.Add(command);
            return generatedFunctionLines.Count;
        }



        public string ExportCommandstoFiles(Grid_Container[,] map)
        {

            //try
            //{
                List<string> generatedGridContainer_Commands = new List<string>();
                List<string> rangeOfCommands;

                string docPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\development_behavior_packs");
                //% LOCALAPPDATA % C:\Users\Username\AppData\Local
                //AppData\Local\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\development_behavior_packs

                int numberOfFilesToGenerate = DivideCommands();
                int rangeStart = 0;
                Console.WriteLine("number of files to generate: " + numberOfFilesToGenerate);
                //int rangeEnd = Shared_Constants.MAX_NUMBER_OF_COMMANDS;

                //Sets the Grid Container commands to this container
                generatedGridContainer_Commands = GenerateGrid_FillCommands(generatedGridContainer_Commands, map);

                StreamWriter[] outputFile = new StreamWriter[numberOfFilesToGenerate];

                for (int i = 0; i < numberOfFilesToGenerate; i++)
                {
                    string nameOfFile = $"run{i + 1}.mcfunction";

                    //outputFile[i] = new StreamWriter(Path.Combine(docPath, nameOfFile));
                    outputFile[i] = new StreamWriter(Path.Combine(docPath, nameOfFile));


                    if (i == 0)
                    {
                        foreach (string line in generatedGridContainer_Commands)
                            outputFile[i].WriteLine(line);
                    }
                    rangeOfCommands = RangeOfCommandsToPrint(rangeStart, numberOfFilesToGenerate);

                    foreach (string line in rangeOfCommands)
                        outputFile[i].WriteLine(line);


                    Console.WriteLine("Output Closing");
                    Console.WriteLine("VALUE OF I: " + i);
                    outputFile[i].Close();

                    rangeStart += rangeOfCommands.Count;
                }
            //} catch(Exception anError)
            //{
            //    Console.WriteLine(anError.Message);
            //    return anError.Message;
            //}

            generatedFunctionLines.Clear(); //clears the list of functions
            return "Export Completed";

        }

        private List<string> RangeOfCommandsToPrint(int start, int splitValue)
        {
                Console.WriteLine(start);

            int chunk = generatedFunctionLines.Count / splitValue;

            List<string> commands = new List<string>();
            if(generatedFunctionLines.Count < Shared_Constants.MAX_NUMBER_OF_COMMANDS)
            {
                return generatedFunctionLines;
            }

            if(start + chunk > generatedFunctionLines.Count)
            {
                //Console.WriteLine("Chunk: " + chunk);
                //Console.WriteLine("start: " + start);
                //Console.WriteLine("total: " + generatedFunctionLines.Count);
                int remaining = (start + chunk) - generatedFunctionLines.Count;
                //Console.WriteLine("remaining: " + remaining);
                return commands = generatedFunctionLines.GetRange(start, remaining);
            } else
            {
                return commands = generatedFunctionLines.GetRange(start, chunk);
            }

            
        
        }


        private int DivideCommands()
        {
            int number_of_commands = generatedFunctionLines.Count;
            //Console.WriteLine("Number of commands: " + number_of_commands);
            double chunks_of_commands = 1;

            if(number_of_commands > Shared_Constants.MAX_NUMBER_OF_COMMANDS)
            {
                chunks_of_commands = (double) number_of_commands / (double)Shared_Constants.MAX_NUMBER_OF_COMMANDS;
                //Console.WriteLine(chunks_of_commands);
                chunks_of_commands = Math.Ceiling(chunks_of_commands);
                
            } 

            return (int)chunks_of_commands;
        }

        private List<string> GenerateGrid_FillCommands(List<string> generatedGridContainer_Commands, Grid_Container[,] map)
        {
            
            for (int i = 0; i < Math.Sqrt(map.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(map.Length); j++)
                {
                    
                    Grid_Container aContainer = map[i, j];
                    //Console.WriteLine("Acontainer.length: " + map.Length);
                    //Console.WriteLine("aContainer start coord " + aContainer.startCoordinate.x);
                    generatedGridContainer_Commands.Add(
                        "fill " + aContainer.startCoordinate.x +
                        " " + aContainer.startCoordinate.y +
                        " " + aContainer.startCoordinate.z +
                        " " + aContainer.endCoordinate.x +
                        " " + aContainer.endCoordinate.y +
                        " " + aContainer.endCoordinate.z +
                        " stone");
                }
            }

            return generatedGridContainer_Commands;
        }

       





        /**
         * This is a functional test to demonstrate that the squares are all individually generated procedurally and they can such be recalled.
         */
        public void ExportTESTING(Grid_Container[,] map)
        {
            // Set a variable to the Documents path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "cityGenerator.mcfunction")))
            {
                outputFile.WriteLine("teleport @s 143 115 -17");
                for (int i = 0; i < map.Length/2; i++)
                {
                    for (int j = 0; j < map.Length/2; j++)
                    {
                        Grid_Container aContainer = map[i, j];

                        Grid_Square[,] squareMap = aContainer.gridSquareMap;

                        for (int k = 0; k < 13; k++)
                        {
                            for (int m = 0; m < 13; m++)
                            {

                                int _tempX = squareMap[k, m].startCoordinate.x;
                                int _tempY = squareMap[k, m].startCoordinate.y+1;
                                int _tempZ = squareMap[k, m].startCoordinate.z;

                                int _tempXPrime = _tempX + 13;

                                int _tempZPrime = _tempZ + 13;

                                outputFile.WriteLine("fill " + _tempX + " " + _tempY + " " + _tempZ + " " + _tempXPrime + " " + _tempY + " " + _tempZPrime + " stone");


                            }
                        }
                    }
                }
            }
        }
    }
}

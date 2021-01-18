using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Constants
{
    public class Block
    {
        public int id { get; set; }
        public int dataValue { get; set; }
        public string name { get; set; }
        public string type { get; set; }


        public Block(int id)
        {
            this.id = id;
        }


    }



    //deserialize
    //Account account = JsonConvert.DeserializeObject<Block>(json);

    //Console.WriteLine(account.Email);

    // read file into a string and deserialize JSON to a type
    //Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\movie.json"));

    // deserialize JSON directly from a file
    //using (StreamReader file = File.OpenText(@"c:\movie.json"))
    //{
    //    JsonSerializer serializer = new JsonSerializer();
    //    Movie movie2 = (Movie)serializer.Deserialize(file, typeof(Movie));
    //}



}

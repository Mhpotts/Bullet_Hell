using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
//using BulletHell.Source.Patterns;
//using BulletHell.Source;
using System;
//using Newtonsoft.Json;

namespace BulletHell
{
    public class StageReader
    {
        public Root LoadJSON()
        {
            string path = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(path).Parent.Parent.FullName;
            using (StreamReader sr = new StreamReader(projectDirectory + "\\Content\\Scripts\\stage1.json"))
            {

                string json = sr.ReadToEnd();
                Root waves = JsonConvert.DeserializeObject<Root>(json);

                System.Diagnostics.Debug.WriteLine(waves);

                return waves;
            }
        }

        public class Root
        {
            public Waves waves { get; set; }
        }

        public class Waves
        {
            public WaveItem wave1 { get; set; }
            public WaveItem wave2 { get; set; }
            public WaveItem wave3 { get; set; }
            public WaveItem wave4 { get; set; }

        }
        

        public class WaveItem
        {
            public string id { get; set; }
            public Time time { get; set; }

            public List<Patterns> patterns;
            public List<Entity> entities;
            

            public string enemyAmount { get; set; }
            public string bossAmount { get; set; }
        }

        public class Time
        {
            public string startTime { get; set; }
            public string endTime { get; set; }
        }

        public class Patterns
        {
            public string id { get; set; }
            public string type { get; set; }
            public string radius { get; set; }
            public string forwardMomentum { get; set; }
            public string speed { get; set; }
            public string bounds { get; set; }
            public string goRight { get; set; }
            public string wobble { get; set; }
        }

        public class Entity
        {
            public string type { get; set; }
            public string x { get; set; }
            public string y { get; set; }
            public string pattern_id { get; set; }
            public string health { get; set; }
            public string damage { get; set; }
            public string xDimension { get; set; }
            public string yDimension { get; set; }
            public string owner { get; set; }

        }

    }    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Game
{   
    [DataContract]
    public class Settings
    {       
        [DataMember]
        public int EnemyStartingCount { get; set; }
        [DataMember]
        public double EnemySpeed { get; set; }
        [DataMember]
        public double HumanSpeed { get; set; }
        [DataMember]
        public int Ammo { get; set; }
        [DataMember]
        public double EnemyRadius { get; set; }
        [DataMember]
        public double HumanRadius { get; set; }
        [DataMember]
        public double BoardWidth { get; set; }
        [DataMember]
        public double BoardHeight { get; set; }
        [DataMember]
        public int PlacementBuffer { get; set; }
        [DataMember]
        public int RespawnRate { get; set; }
    }
}

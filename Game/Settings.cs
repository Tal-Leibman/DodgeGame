using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Game
{
    [DataContract]
    public class Settings
    {

        [DataMember]
        public const int PLACEMENT_BUFFER = 4;
        [DataMember]
        public int RespawnRate { get; set; }
        [DataMember]
        public int HighScore { get; set; }
        [DataMember]
        public int EnemyStartingCount { get; set; }
        [DataMember]
        public double EnemyMaxSpeed { get; set; }
        [DataMember]
        public double EnemyMinSpeed { get; set; }
        [DataMember]
        public Color HumanColor { get; set; }
        [DataMember]
        public double HumanSpeed { get; set; }
        [DataMember]
        public double EnemyMaxRadius { get; set; }
        [DataMember]
        public double EnemyMinRadius { get; set; }
        [DataMember]
        public double HumanRadius { get; set; }

        public double BoardWidth { get; set; }

        public double BoardHeight { get; set; }
    }
}

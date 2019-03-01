using System.ComponentModel;
using System.Runtime.Serialization;
using Windows.UI;

namespace Game
{
    [DataContract]
    public class Settings
    {
        public const int PLACEMENT_BUFFER = 4;

        public double BoardHeight { get; set; }
        public double BoardWidth { get; set; }

        public int CurrentScore { get; set; }

        [DataMember]
        public double EnemyMaxRadius { get; set; }

        [DataMember]
        public double EnemyMaxSpeed { get; set; }

        [DataMember]
        public double EnemyMinRadius { get; set; }

        [DataMember]
        public double EnemyMinSpeed { get; set; }

        [DataMember]
        public int EnemyStartingCount { get; set; }

        [DataMember]
        public int HighScore { get; set; }

        [DataMember]
        public Color HumanColor { get; set; }

        [DataMember]
        public double HumanRadius { get; set; }

        [DataMember]
        public double HumanSpeed { get; set; }

        [DataMember]
        public int RespawnRate { get; set; }
    }
}

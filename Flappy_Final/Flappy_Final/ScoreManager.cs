using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Flappy_Final
{
   // https://www.youtube.com/watch?v=JzEwVCgALuY
    public class ScoreManager
    {
        private static readonly string _fileName = "FBHS.xml"; // since there isnt a full path this will be saved in the bin folder

        public List<Score> HighScores { get; private set; }

        public List<Score> Scores { get; private set; }


        public ScoreManager () : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;
            UpdateHighScores();
        }

        public void Add(Score score) // add current score to list of scores
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Value).ToList(); // orders the list so that the highest values are first

            UpdateHighScores();
        }

        public void UpdateHighScores()
        {
            HighScores = Scores.Take (5).ToList(); // take the top 5 elements
        }


        // LOAD AND SAVE

        public static ScoreManager Load()
        {
            //  if the file doesn't exist return a new instance of the ScoreManager
            if (!File.Exists(_fileName))
            {
                return new ScoreManager();
            }
            // otherwise

            using (StreamReader reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));

                List<Score> scores = (List<Score>)serializer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public static void Save(ScoreManager scoreManager)
        {
            // If file exists overwrite the contents
            using (StreamWriter writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));
                serializer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}

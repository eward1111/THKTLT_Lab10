using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THKTLT_Lab10
{
    public class abc
    {
        public string[,] myArray = new string[4, 4];
        public void ReturnArray()
        {
            List<string> animalEmoiji = new List<string>()
            {
            "🐸", "🐸",
            "🐼", "🐼",
            "🐻", "🐻",
            "🦀", "🦀",
            "🐓", "🐓",
            "🐟", "🐟",
            "🦔", "🦔",
            "🐐", "🐐",
            };
            List<string> randomAnimalList = new List<string>();
            Random rnd = new Random();
            for (int i = 0; i < 16; i++)
            {
                int idx = rnd.Next(animalEmoiji.Count);
                string aEmoji = animalEmoiji[idx];
                randomAnimalList.Add(aEmoji);
                animalEmoiji.RemoveAt(idx);
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    myArray[i, j] = randomAnimalList[(i * 4) + j];
                }
            }
        }
    }
}

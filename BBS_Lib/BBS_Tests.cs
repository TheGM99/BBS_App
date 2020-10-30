using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Lib
{
    public class BBS_Tests
    {
        /// <summary>
        /// Test pokerowy z testów statystycznych FIPS 140-2
        /// </summary>
        /// <param name="series">ciąg bitów poddawany testowi</param>
        /// <returns> wynik testu oraz wartość X</returns>
        public static (bool result, double value) poker_test(int[] series)
        {
            Dictionary<int, int> temp_dic = new Dictionary<int, int>();
            for (int i = 0; i <= 15; i++) temp_dic.Add(i, 0);
            for(int i = 0; i < series.Length; i+=4)
            {
                string temp = Convert.ToString(series[i]) + Convert.ToString(series[i+1]) + Convert.ToString(series[i+2]) + Convert.ToString(series[i+3]);
                temp_dic[Convert.ToInt32(temp, 2)]++;
            }
            float result;
            float sum = 0;
            foreach(var i in temp_dic)
            {
                sum += i.Value * i.Value;
            }
            result = ((float)16 / 5000) * sum - 5000;
            Console.WriteLine("X = " + result);
            if (result < 46.17 && result > 2.16) return (true, result);
            else return (false, result);

        }

        /// <summary>
        /// Test długiej serii z testów statystycznych FIPS 140-2
        /// </summary>
        /// <param name="series">ciąg bitów poddawany testowi</param>
        /// <returns>wynik testu oraz długość najdłuższej serii</returns>
        public static (bool result, double value) long_series_test(int[] series)
        {
            int count=0;
            int longest_count = 0;
            int temp = 0;
            foreach (int i in series)
            {
                if (i == temp) count++;
                else
                {
                    count = 1;
                    temp = i;
                }
                if (count >= 26) return (false, count);
                if (count > longest_count) longest_count = count;
            }
            return (true, longest_count);
        }

        /// <summary>
        /// Test serii z testów statystycznych FIPS 140-2
        /// </summary>
        /// <param name="series">ciąg bitów poddawany testowi</param>
        /// <returns>wynik testu oraz ilość wystąpień każdej serii</returns>
        public static (bool result, Dictionary<int,int> value) series_test(int[] series)
        {
            Dictionary<int, int> num_count = new Dictionary<int, int>();
            for (int i = 1; i <= 6; i++) num_count.Add(i, 0);
            int count = 0;

            foreach(int i in series)
            {
                if(i == 0)
                {
                    if (count >= 6) num_count[6]++;
                    else if(count > 0) num_count[count]++;
                    count = 0;
                }
                if (i == 1) count++;
            }

            foreach (var i in num_count)
                Console.WriteLine("[ " + i.Key + ": " + i.Value + " ]");
            Console.Write('\n');

            if (num_count[1] < 2315 && num_count[1] > 2685) return (false, num_count);
            else if (num_count[2] < 1114 && num_count[2] > 1386) return (false, num_count);
            else if (num_count[3] < 527 && num_count[3] > 723) return (false, num_count);
            else if (num_count[4] < 240 && num_count[4] > 384) return (false, num_count);
            else if (num_count[5] < 103 && num_count[5] > 209) return (false, num_count);
            else if (num_count[6] < 103 && num_count[6] > 209) return (false, num_count);
            else return (true, num_count);
        }

        /// <summary>
        /// Test pojedynczych bitów z testów statycznych FIPS 140-2
        /// </summary>
        /// <param name="series">ciąg bitów poddawany testowi</param>
        /// <returns>wynik testu oraz ilość bitów o wartości 1</returns>
        public static (bool result, int value) SingleBitTest(int[] series)
        {
            int count = 0;
            foreach (int i in series)
                if (i == 1) count++;
            Console.WriteLine("Ilość jedynek: " + count);
            if (count < 9725 || count > 10275) return (false, count);
            else return (true, count);
        }
    }
}

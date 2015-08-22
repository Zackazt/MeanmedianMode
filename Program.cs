using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MeanMedianMode                 //BE SURE THERE IS NO EXCESS TRAILING OR LEADING WHITESPACE IN YOUR TEXT FILE.
{
    class Program
    {
        private static string inputNumbers;                            //The list of numbers will be a string at first.
        private static bool isEven;       //This is for determining if the number of items in the list are even or odd.
        static List<int> numberList = new List<int>();              //Creates a list. This will be the list of numbers.

        static void Main(string[] args)                                                     //When the program starts.
        {
            inputNumbers = File.ReadAllText("file.txt");            //The numbers are read from a file into the string.
            string[] nums = inputNumbers.Split(' ');                       //The numbers are split into a string array.         
            foreach (string x in nums)                                   //Loops through each item of the string array.
            {
                int toAdd = Int32.Parse(x);                                   //Each item is "converted" to an integer.
                numberList.Add(toAdd);                                                //Each item is added to the list.
            }

            int numberAmount = numberList.Count();           //This tells the program how many numbers are in the list.
            if (numberAmount % 2 == 0) { isEven = true; }         //If the amount is evenly divisible by 2, it is even.
            else if (numberAmount % 2 != 0) { isEven = false; }                              //If it is not, it is odd.
            numberList.Sort();                                                                   //This sorts the list.
            Console.WriteLine("The Mean is: " + findMean(numberAmount).ToString());                //Displays the mean.
            Console.WriteLine("The Median is: " + findMedian(numberAmount).ToString());         // Displays the median.
            if (findMode(numberAmount) == 0)   //The findMode method is set to return 0 if there is more than one mode.
            {
                Console.WriteLine("This list has more than one Mode.");
            }
            if (findMode(numberAmount) == 1)              //The findMode method is set to return 1 if there is no mode.
            {
                Console.WriteLine("This list has no mode.");
            }
            if (findMode(numberAmount) != 0 && findMode(numberAmount) != 1) //If there is a mode, it displays the mode.
            {
                Console.WriteLine("The Mode is: " + findMode(numberAmount).ToString());            //Displays the mode.
            }            
            Console.ReadLine();
        }

        private static double findMean(int amountInList)                                   //Finding the mean(average).
        {
            double mean = 0;                                                     //Instantiates the variable mean as 0.
            double sum = 0;                                                       //Instantiates the variable sum as 0.
            foreach (int i in numberList)                                   //Iterates through each number of the list.
            {
                sum += i;                                                     //Adds each item together creating a sum.
            }
            mean = sum / amountInList;                   //Sets the mean as the sum divided byt the amount in the list.
            return mean;                                                                   //Returns the mean(average).
        }

        private static double findMedian(int amountInList)                                        //Finding the median.
        {
            int x = 0;
            double median = 0;                                         //Using a double in case of non-integer outcome.
            if (isEven == true)                                         //If the amount of numbers in the list is even, 
            {                                                                //finding the median is slightly trickier.
                int count = 1;                                               //Using this to keep track of positioning.
                x = amountInList / 2;      //The first middle number is the amount of numbers in the list divided by 2.
                int y = x + 1;                                  //The second middle number is the next number in order.          
                foreach(int i in numberList)                                       //Begin to iterate through the list.
                {
                    if (count == x)                      //If the number it is currently on is the first middle number.
                    {
                        median = i;                                           //We set the median equal to that number.
                    }
                    if (count == y)                                                      //The next number in the list.
                    {
                        median = (median + i) / 2;          //The median is the sum ofboth middle numbers divided by 2.
                        break;                                                             //We can now break the loop.
                    }
                    count++;                                                   //After each iteration I add 1 to count.  
                }
                
            }

            else if (isEven == false)                                   //If the amount of numbers in the list are odd. 
            {
                x = (amountInList + 1) / 2;          //The middle number is the amount in the list +1 all divided by 2.
                int count = 1;                                                          //To keep track of positioning.
                foreach(int i in numberList)                                              //Iterating through the list.
                {
                    if (count == x)                                              //If we are at the middle of the list.
                    {
                        median = i;                                               //The median is the number we are on.
                        break;                                                        //We can now break from the loop.
                    }
                    count++;                                                   //After each iteration I add 1 to count.  
                }            
            }
            return median;                    //After it's all said and done, I return the median to the function call.
        }

        private static double findMode(int amountInList)                                            //Finding the Mode.
        {
            double mode = 1;                           //Default mode is 1.  If this does not change, there is no mode.
            int count = 0;                    //This will be used to count how many times a number appears in the list.
            int highestCount = 0;                 //This will be used to tell which number has occured the most so far.
            bool multipleModes = false;                //This will be used to determine if there is more than one mode.
            foreach (int i in numberList)                                        //Begin by iterating through the list.
            {
                count = 0;                   //Ensuring that count resets to 0 at the beginning of each main iteration.
                foreach (int x in numberList)           //The number we are on is compared to every number in the list.
                {
                    if (i == x)           //If the number we are on in loop'1' matches the number we are on in loop'2'.
                    {
                        count++;                                                             //Increase the count by 1.
                    }
                }  
                if (count == highestCount && mode != i)    //If the current count is the same as the highest count, and
                {                                 //the current mode is not the same as the number we are currently on.
                    multipleModes = true;                                   //So far it appears we have multiple modes.
                }
                if (count > highestCount && count > 1)      //If the current count is bigger than the highest count and
                {                                                                //the current count is greater than 1,
                    highestCount = count;                              //the current count is the highest count so far.
                    mode = i;                                       //So we set the mode equal to the number we are on.
                    multipleModes = false;                          //And now it appears we do not have multiple modes.
                }
            }
            if (multipleModes == true)                          //If it is all said and done and multipleModes is true,
            {
                mode = 0;                     //we set mode to 0 in order to tell the program there are multiple modes.
            }
            return mode;                                             //We finally return the mode to the function call.
        }
    }
}

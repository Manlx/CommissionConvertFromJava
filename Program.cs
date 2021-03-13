using System;
//Manue A Nunes
namespace CommissionConvertFromJava
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declare Values as is
            decimal[] arrBracks = { 0,5000,10000 }, arrPerc = {8,10,12 };
            //Writes query statment
            Console.Write("Enter commision target: ");
            //Reads input
            decimal input = Decimal.Parse( Console.ReadLine());
            //Prints output
            Console.WriteLine($"Sell {CalCommision(input, arrBracks, arrPerc).ToString("C")} to get a commision of {input.ToString("C")}");
        }
        private static decimal CalCommision(in decimal dInput,decimal [] BracketVals,in decimal[] BracketPrecent)
        {
            //Converts the input array to array of value changes
            ConToArr(ref BracketVals);
            bool bBracketFound = false;
            int i = 0;
            //dAccumed is the closest value below the input of the brackets.
            //dUpcoming is the bracket that encludes and usual overshoots the value and thus must be tested before begin
            // added to dAccumed.
            //dTotal is what is going to be outputted 
            decimal dAccumed = 0,dUpComing = 0,dTotal = 0;
            while (!bBracketFound && i < BracketVals.Length)
            {
                //Addes the upcomming value to the accumulated value after it passes the check.
                dAccumed += dUpComing;
                //Calculates the upcomming value
                dUpComing = (BracketVals[i] * BracketPrecent[i] / 100);
                //Test if the dInput values bracket was found
                bBracketFound =( dInput < dUpComing + dAccumed) ;
                i++;
            }
            //Yes I could swap this around and remove the Not but ye.
            if (!bBracketFound)
                //If the value is outside the bracket it would add the last value and calculated
                //and continue because of the nature of the while loop.
                dAccumed += dUpComing;
            else
                //If Found then just decrement I bc the value was incremeted one time to much.
                i--;
            //I could have removed the else of the if and added it to the i-1 but I feel that this is more effiecient.
            //Now I'm actually thinking that it gets optimized out by the compiler.
            for (int x = 0; x < i; x++)
                dTotal += BracketVals[x];
            //Any how This is the return statement i could rewrite it to optimize it a bit but I feel it is fine
            //return dTotal += (dInput - dAccumed) * 100 / BracketPrecent[i];
            //I did rewrite it and now it should run a teany weany bit better.
            return dTotal + (dInput - dAccumed) * 100 / BracketPrecent[i];
        }
        private static void ConToArr( ref decimal[] RawData)
        {
            //Converts the inputed array to an array of difference element N and N -1;
            decimal[] dOut = new decimal[0];
            decimal dOld = 0;
            for (int i = 0; i < RawData.Length;i++)
            {
                if (RawData[i]-dOld > 0)
                {
                    ResizeArr(ref dOut);
                    dOut[dOut.Length - 1] = RawData[i] - dOld;
                }
                dOld = RawData[i];
            }
            RawData = dOut;
        }
        private static void ResizeArr(ref decimal[] RawData)
        {
            // Resizes the array
            decimal[] dOut = new decimal[RawData.Length + 1];
            MoveData(ref dOut,RawData);
            RawData = dOut;
        }
        private static void MoveData(ref decimal[] RawData, in decimal[] Input)
        {
            //Copies data from one array to the next
            for (int i = 0; i < Input.Length; i++)
                RawData[i] = Input[i];
        }
    }
}

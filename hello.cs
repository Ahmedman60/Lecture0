using System;

struct EntryPOINT
{
    public static void Main()
    {

        Console.Clear();

        //jagged array.
        //int[][] nums = new int[5][];
        //nums[0] = new int[] { 5, 4, 7 };


        //int[] i = { 5, 7, 89, 8 };
        //int[][] jaggedarray = GetArrayFromUser(4, nums[0][1]);
        //Console.WriteLine(jaggedarray.Length);
     var result =  GetArrayFromUser();

        Console.ReadKey();
    }


    //this function return a jagged array.
    public static int[][] GetArrayFromUser(int NofRows, int[] NofCol)
    {
        int[][] Result = new int[NofRows][];
        for (int i = 0; i < NofRows; i++)
        {
            //as i get the arrays lenght from the array user send ;
            Result[i] = new int[NofCol[i]];
        }
        return Result;

    }
    public static int[][] GetArrayFromUser()
    {
        Console.Write("Please Enter Number of Rows:  ");
        int z = 1;
        int Rows = int.TryParse(Console.ReadLine(), z);
        if (z == 1)
        {
            int[] cols = new int[Rows];
            for (int i = 0; i < Rows; i++)
            {
                Console.Write($"Enter the number of Coloums for {i + 1} Row: ");
                cols[i] = int.Parse(Console.ReadLine());
            }
            int[][] jaggedarray = GetArrayFromUser(Rows, cols);

            //for insert the data in the jagged array.
            for (int i = 0; i < length; i++)
            {
                for (int i = 0; i < length; i++)
                {
                    Console.Write($"data[{i + 1}][{j + 1}] = ");
                    jaggedarray[i][j] = int.Parse(Console.ReadLine());
                }

            }
            return jaggedarray;

        }

        else
        {
            Console.WriteLine("Enter Numbers");
            return null;
        }
    }
    public static void PrintJaggedarray(int [][] jagged)
    {
        foreach (var array in jagged)
        {
            foreach (var item in array)
            {
                Console.Write(item );
            }
        }
    }
}
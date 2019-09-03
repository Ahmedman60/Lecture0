using System;

public class MeMainClass
{

        
    static void Main()
    {
        int i = 5;
        string helloworld = "what's sup";
       var e= helloworld.ToCharArray();
        Hello(e, i);
        Console.WriteLine("-----------------");
        Console.WriteLine("From main");
        Console.WriteLine(i);
        string z = new string(e);
        Console.WriteLine(z);
        Console.ReadKey();
    }
    public static void Hello(char[] helloworld,int i)
    {
        Console.WriteLine("-----------------");
        Console.WriteLine("From Hello");
        helloworld[0] = 'b';
        string z = new string(helloworld);
        i = i + 5;
        Console.WriteLine(z);
        Console.WriteLine(i);
    }
    //public static void Hello()
    //{
    //Console.WriteLine("Hello from third module");

    //}
}

namespace Order
{
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            string separator = ",";

            #region Parsing the command line and setting the above variables to user values.
            for (int i = 0; i < args.Length; i++)
            {
                if (i % 2 == 0)
                {
                    try
                    {
                        switch (args[i])
                        {
                            case "SEPARATOR":
                                separator = args[i + 1];
                                break;
                            default:
                                break;
                        }
                    }
                    catch
                    {
                        Console.Out.WriteLine(Program.OnConfigurationExceptionMessage);
                        foreach (string[] p in Program.Parameters)
                        {
                            Console.WriteLine(string.Format("{0} {1}\t{2}", p[0], p[1], p[2]));
                        }
                    }
                }
            }
            #endregion

            string line;
            while ((line = Console.In.ReadLine()) != null)
            {
                var command = line.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                int total = int.Parse(command[1]);
                int depth = Program.Depth(total-1, 1, 10);
                for (int i = 0; i < total; i++)
                {
                    int leadingZeros = depth - Program.Depth(i, 1, 10);
                    Console.Write(command[0]);
                    for (int j = 0; j < leadingZeros; j++)
                    {
                        Console.Write("0");
                    }
                    Console.WriteLine(i);
                }
            }
        }

        private static int Depth(int subject, int divisor, int base_number)
        {
            var quotiant = (subject / divisor);
            var next = divisor * base_number;
            if (quotiant < base_number)
            {
                return 0;
            }
            else
            {
                return Program.Depth(subject, next, base_number) + 1;
            }
        }

        public static readonly string OnConfigurationExceptionMessage = @"***ERROR PROCESSING args 
Usage: Order.exe <key, value> <key value>
<key, value>: args are grouped in sets of two, with each key preceding it's value.
key: the word that names the next parameter to be set.
value: the word to be associated with the current parameter to be set. 
Parameters:";
        public static readonly string[][] Parameters = new[] { new[] { "SEPARATOR", ",", "The single character used to subdivide input lines." } };
        public static readonly string[] ParameterSchema = new[] { "Parameter", "Default", "Description" };
    }
}

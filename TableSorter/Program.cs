using System;
using System.Data;
using System.Linq;

namespace TableSorter
{
    class TableSorter
    {
        public static void Main(string[] args)
        {
            int rowsCount = int.Parse(Console.ReadLine());
            string[] coloumnNames = Console.ReadLine().Replace(" ", "").Split('|').Where(x => !x.Equals("")).ToArray();
            DataTable table = new DataTable();
            foreach (string coloumnName in coloumnNames)
            {
                table.Columns.Add(coloumnName, typeof(ulong));
            }

            Console.ReadLine();

            for (int i = 0; i < rowsCount - 2; i++)
            {
                int[] row = Console.ReadLine().Split('|').Where(x => x != "").Select(x => int.Parse(x)).ToArray();
                table.Rows.Add(row[0], row[1], row[2]);
            }

            int countOfRepeats = int.Parse(Console.ReadLine());
            string[] listOfColoumnNames = Console.ReadLine().Split(' ');

            string sortFilter = "";
            foreach (string coloumnName in listOfColoumnNames)
            {
                if (sortFilter.Contains(coloumnName + " ASC"))
                {
                    sortFilter = sortFilter.Replace(coloumnName + " ASC", coloumnName + " DESC");
                }
                else if (sortFilter != "")
                {
                    sortFilter = $"{coloumnName} ASC, " + sortFilter;
                }
                else
                {
                    sortFilter += $"{coloumnName} ASC";
                }
                table.DefaultView.Sort = sortFilter;
                var sortedTable = table.DefaultView.ToTable();
                PrintTable(sortedTable);
            }
        }

        public static void PrintTable(DataTable table)
        {
            int[] widthOfColoumn = new int[table.Columns.Count];
            int index = 0;
            foreach (DataColumn column in table.Columns)
            {
                widthOfColoumn[index] = column.ColumnName.Length;
                index++;
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < row.ItemArray.Count(); i++)
                {
                    if (row.ItemArray[i].ToString().Length > widthOfColoumn[i])
                    {
                        widthOfColoumn[i] = row.ItemArray[i].ToString().Length;
                    }
                }
            }

            Console.WriteLine(); // отступ между таблицами
            index = 0;
            foreach (DataColumn column in table.Columns)
            {
                Console.Write($"|{column.ColumnName.PadLeft(widthOfColoumn[index])}");
                index++;
            }
            Console.Write('|');
            Console.WriteLine();

            for (int i = 0; i < widthOfColoumn.Length; i++)
            {
                Console.Write($"|{new string('=', widthOfColoumn[i])}");
            }
            Console.Write('|');
            Console.WriteLine();

            // Print rows
            foreach (DataRow row in table.Rows)
            {
                index = 0;
                foreach (var item in row.ItemArray)
                {
                    Console.Write($"|{item.ToString().PadLeft(widthOfColoumn[index])}");
                    index++;
                }
                Console.Write('|');
                Console.WriteLine();
            }
        }
    }
}

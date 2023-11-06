using System;
using System.Collections.Generic;
using System.Text;

namespace TechnologicalView
{
    internal class BaseView
    {
        public void WriteWelcomeMessage(string message)
        {
            Console.Write("+---");
            for (int i = 0; i < message.Length; ++i)
                Console.Write("-");
            Console.WriteLine("---+");
            Console.Write("|");
            for (int i = 0; i < message.Length + 6; ++i)
                Console.Write(" ");
            Console.WriteLine("|");
            Console.Write("|   ");
            Console.Write(message);
            Console.WriteLine("   |");
            Console.Write("|");
            for (int i = 0; i < message.Length + 6; ++i)
                Console.Write(" ");
            Console.WriteLine("|");
            Console.Write("+---");
            for (int i = 0; i < message.Length; ++i)
                Console.Write("-");
            Console.WriteLine("---+");
            Console.WriteLine();
        }

        public void WriteWarningMessage(string message)
        {
            Console.WriteLine("Ошибка: {0}", message);
        }

        public void WriteInfoMessage(string message)
        {
            Console.WriteLine("{0}", message);
        }

        public void WritePromptMessage(string message)
        {
            Console.WriteLine("{0}", message);
        }

        public void WriteTableHeader(params string[] args)
        {
            string overline = "+";
            foreach (var item in args)
            {
                overline += "-";
                for (int i = 0; i < item.Length; ++i)
                    overline += "-";
                overline += "-";
                overline += "+";
            }
            Console.WriteLine(overline);
            string header = "|";
            int counter = 0;
            foreach (var item in args)
            {
                header += $" {{{counter},{item.Length}}} |";
                counter++;
            }
            Console.WriteLine(string.Format(header, args));
            string underline = "|";
            foreach (var item in args)
            {
                underline += "-";
                for (int i = 0; i < item.Length; ++i)
                    underline += "-";
                underline += "-";
                underline += "+";
            }
            Console.WriteLine(underline);
        }

        public void WriteTableRow(params string[] args)
        {
            string row = "|";
            int counter = 0;
            foreach (var item in args)
            {
                int length = 0;
                if (item != null)
                    length = 0;
                row += $" {{{counter},{length}}} |";
                counter++;
            }
            Console.WriteLine(string.Format(row, args));
        }

        public void WriteTableFoot(params string[] args)
        {
            string overline = "+";
            foreach (var item in args)
            {
                overline += "-";
                int length = 0;
                if (item != null)
                    length = 0;
                for (int i = 0; i < item.Length; ++i)
                    overline += "-";
                overline += "-";
                overline += "+";
            }
            Console.WriteLine(overline);
        }

        public void WriteNotImplementedMessage()
        {
            Console.WriteLine("Ждите в будущих обновлениях");
        }
    }
}

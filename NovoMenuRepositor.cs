using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria
{
    class NovoMenuRepositor
    {
        private int SelectedRepositor;
        private string[] Options;
        private string Prompt;

        public NovoMenuRepositor(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedRepositor = 0;
        }

        private void DisplayRepositor()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefixo;
                if (i == SelectedRepositor)
                {
                    prefixo = "-->";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefixo = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"\n{prefixo}  {currentOption} \n");
            }
            ResetColor();
        }

        public int Run3()
        {
            ConsoleKey KeyPressed2;

            do
            {
                Clear();
                DisplayRepositor();

                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed2 = KeyInfo.Key;

                if (KeyPressed2 == ConsoleKey.UpArrow)
                {
                    SelectedRepositor--;
                    if (SelectedRepositor == -1)
                    {
                        SelectedRepositor = Options.Length - 1;
                    }
                }
                else if (KeyPressed2 == ConsoleKey.DownArrow)
                {
                    SelectedRepositor++;
                    if (SelectedRepositor == Options.Length)
                    {
                        SelectedRepositor = 0;
                    }
                }

            } while (KeyPressed2 != ConsoleKey.Enter);

            return SelectedRepositor;
        }
    }
}

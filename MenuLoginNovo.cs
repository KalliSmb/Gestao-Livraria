using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria
{
    class MenuLoginNovo
    {
        private int SelectedLogin;
        private string[] Options;
        private string Prompt;

        public MenuLoginNovo(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedLogin = 0;
        }

        private void DisplayLogin()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefixo;
                if (i == SelectedLogin)
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
                WriteLine($"\n{prefixo}  {currentOption}");
            }
            ResetColor();
        }

        public int Run1()
        {
            ConsoleKey KeyPressed1;

            do
            {
                Clear();
                DisplayLogin();

                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed1 = KeyInfo.Key;

                if (KeyPressed1 == ConsoleKey.UpArrow)
                {
                    SelectedLogin--;
                    if (SelectedLogin == -1)
                    {
                        SelectedLogin = Options.Length - 1;
                    }
                }
                else if (KeyPressed1 == ConsoleKey.DownArrow)
                {
                    SelectedLogin++;
                    if (SelectedLogin == Options.Length)
                    {
                        SelectedLogin = 0;
                    }
                }

            } while (KeyPressed1 != ConsoleKey.Enter);

            return SelectedLogin;
        }
    }
}
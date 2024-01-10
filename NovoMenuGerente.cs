using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria
{
    class NovoMenuGerente
    {
        private int SelectedGerente;
        private string[] Options;
        private string Prompt;

        public NovoMenuGerente(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedGerente = 0;
        }

        private void DisplayGerente()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefixo;
                if (i == SelectedGerente)
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

        public int Run()
        {
            ConsoleKey KeyPressed;

            do
            {
                Clear();
                DisplayGerente();

                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed = KeyInfo.Key;

                if (KeyPressed == ConsoleKey.UpArrow)
                {
                    SelectedGerente--;
                    if (SelectedGerente == -1)
                    {
                        SelectedGerente = Options.Length - 1;
                    }
                }
                else if (KeyPressed == ConsoleKey.DownArrow)
                {
                    SelectedGerente++;
                    if (SelectedGerente == Options.Length)
                    {
                        SelectedGerente = 0;
                    }
                }

            } while (KeyPressed != ConsoleKey.Enter);

            return SelectedGerente;
        }
    }
}
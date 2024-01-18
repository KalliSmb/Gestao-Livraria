using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria
{
    internal class NovoMenuEditarFuncionario
    {
        private int SelectedFuncionario;
        private string[] Options;
        private string Prompt;

        //Construtor
        public NovoMenuEditarFuncionario(string prompt, string[] options)
        {
            //Valor do parámetro prompt
            Prompt = prompt;

            //Valor do parámetro options
            Options = options;

            //Opção selecionada no menu
            SelectedFuncionario = 0;
        }

        //Método
        private void DisplayFuncionario()
        {
            //Exibe mensagem
            WriteLine(Prompt);

            //Loop com as opcões do menu
            for (int i = 0; i < Options.Length; i++)
            {
                //Obtém opção atual no indice i
                string currentOption = Options[i];
                //Variável do simbolo que aparece atrás de cada opção
                string prefixo;

                //Verifica se a opção atual é a selecionada 
                if (i == SelectedFuncionario)
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
                //Exibe a opção selecionada e o prefixo
                WriteLine($"\n{prefixo}  {currentOption}");
            }
            //Restaura as cores após exibir todas as opções
            ResetColor();
        }

        //Método 
        public int Run5()
        {
            //Variável de tecla que vai pressionar
            ConsoleKey KeyPressed5;

            //Loop estará sempre em funcionamento até que a tecla ENTER seja pressionada
            do
            {

                //Limpar a consola
                Clear();
                //Método para Exibir Menu de Gerente
                DisplayFuncionario();

                //Lê a tecla sem exibir no console
                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed5 = KeyInfo.Key;

                //Verifica se a tecla pressionada é a seta para cima 
                if (KeyPressed5 == ConsoleKey.UpArrow)
                {
                    //Indice da opção selecionada
                    SelectedFuncionario--;

                    //Se o indice fôr -1, então ajusta para a ultima opção do menu 
                    if (SelectedFuncionario == -1)
                    {
                        SelectedFuncionario = Options.Length - 1;
                    }
                }
                //Verifica se a tecla pressionada é a seta para baixo
                else if (KeyPressed5 == ConsoleKey.DownArrow)
                {
                    //Indice da opção selecionada
                    SelectedFuncionario++;

                    //Se atingir o comprimento total das opções, volta para a primeira opção do menu 
                    if (SelectedFuncionario == Options.Length)
                    {
                        SelectedFuncionario = 0;
                    }
                }

            } while (KeyPressed5 != ConsoleKey.Enter);

            //Retorna o indice da opção selecionada
            return SelectedFuncionario;
        }
    }
}

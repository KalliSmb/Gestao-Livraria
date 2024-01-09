using System;

namespace LP1_Livraria
{
    public class Gerente
    {
        public static int MenuGerente()
        {
            Console.Clear();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|============================================|");
                Console.WriteLine("|                Menu Gerente                |");
                Console.WriteLine("|============================================|");
                Console.WriteLine("| 0. Voltar para o menu principal            |");
                Console.WriteLine("| 1. Criar novo funcionário                  |");
                Console.WriteLine("| 2. Eliminar funcionário                    |");
                Console.WriteLine("| 3. Biblioteca                              |");
                Console.WriteLine("|--------------------------------------------|\n");

                Console.Write(" -> ");

                try 
                {
                    int escolha = int.Parse(Console.ReadLine());

                    if (escolha == 0) 
                    {
                        // volta para o menu principal
                    }

                    if (escolha == 1)
                    {
                        // vai para o menu de criação de funcionário
                    }

                    if (escolha == 2)
                    {
                        // vai para o menu de eliminação de funcionário
                    }

                    if (escolha == 3)
                    {
                        // vai para o menu de venda (lista de livros)
                    }
                }
                catch 
                {

                }
            }
        }
    }
}

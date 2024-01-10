using System;
using System.IO;

namespace LP1_Livraria
{
    public class Repositor
    {
        public static void MenuRepositor()
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|============================================|");
                Console.WriteLine("|               Menu Repositor               |");
                Console.WriteLine("|============================================|");
                Console.WriteLine("| 0. Voltar para o menu principal            |");
                Console.WriteLine("| 1. Consultar stock                         |");
                Console.WriteLine("| 2. Adicionar stock                         |");
                Console.WriteLine("| 3. Registar novo produto                   |");
                Console.WriteLine("|--------------------------------------------|\n");

                Console.Write(" -> ");

                try
                {
                    int escolha = int.Parse(Console.ReadLine());

                    switch (escolha)
                    {
                        case 0:
                            Console.Clear();
                            return; // sai do método

                        case 1:
                            ConsultarStock();
                            break;

                        case 2:
                            //AdicionarStock();
                            break;

                        case 3:
                            //RegistarLivro();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Erro: Escolha inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: {ex.Message}");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public static void ConsultarStock()
        {
            Console.Clear();

            Console.Write("Introduza o código do livro que deseja consultar stock: ");
            string codigo = Console.ReadLine();

            string caminhoFicheiro = "..\\..\\Livros.txt";

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro);

                bool livroEncontrado = false;

                for (int i = 0; i < linhas.Length; i++)
                {
                    if (linhas[i].Trim() == codigo)
                    {
                        livroEncontrado = true;

                        // Verifica se há linhas suficientes após a linha do código
                        if (i + 7 < linhas.Length) // Adiciona 7 para ir até a linha do estoque
                        {
                            Console.Clear();
                            Console.WriteLine($"Stock do livro {codigo}: {linhas[i + 7].Trim()}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Erro: Informações de stock ausentes para o livro {codigo}.");
                        }
                    }
                }

                if (!livroEncontrado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: Livro com código {codigo} não encontrado.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao consultar o stock: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

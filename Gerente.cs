using System;
using System.IO;

namespace LP1_Livraria
{
    public class Gerente
    {
        public static void MenuGerente()
        {
            while (true)
            {
                Console.Clear();

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

                    switch (escolha)
                    {
                        case 0:
                            Console.Clear();
                            return; // sai do método

                        case 1:
                            CriarFuncionario();
                            break;

                        case 2:
                            // elimina funcionário
                            break;

                        case 3:
                            // vai para o menu de venda (lista dos livros)
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

        public static void CriarFuncionario() 
        {
            Console.Clear();
            Console.WriteLine("Criar novo funcionário:");

            Console.Write("Nome: ");
            string utilizador = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Cargo: ");
            string cargo = Console.ReadLine();

            string novaLinha = $"{utilizador},{password},{cargo}";

            string caminhoFicheiro = "..\\..\\DadosUtilizadores.txt";
            File.AppendAllText(caminhoFicheiro, Environment.NewLine + novaLinha);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNovo funcionário criado com sucesso!");
            Console.ReadLine();
        }
    }
}

using System;
using System.IO;
using System.Linq;

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
                            EliminarFuncionario();
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
            File.AppendAllText(caminhoFicheiro, novaLinha + Environment.NewLine);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNovo funcionário criado com sucesso!");
            Console.ReadLine();
        }

        public static void EliminarFuncionario()
        {
            Console.Clear();
            Console.WriteLine("Eliminar funcionário:");

            Console.Write("Nome do funcionário a ser removido: ");
            string utilizador = Console.ReadLine();

            string caminhoFicheiro = "..\\..\\DadosUtilizadores.txt";
            string[] linhas = File.ReadAllLines(caminhoFicheiro);

            bool funcionarioEncontrado = false;

            for (int i = 0; i < linhas.Length; i++)
            {
                string[] dados = linhas[i].Split(',');

                if (dados.Length == 3 && dados[0] == utilizador)
                {
                    linhas[i] = null;
                    funcionarioEncontrado = true;
                    break;
                }
            }

            if (funcionarioEncontrado)
            {
                File.WriteAllLines(caminhoFicheiro, linhas.Where(line => !string.IsNullOrEmpty(line)));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nFuncionário removido com sucesso!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFuncionário não encontrado.");
            }

            Console.ReadLine();
        }
    }
}
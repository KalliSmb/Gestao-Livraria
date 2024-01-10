using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace LP1_Livraria
{
    public class Gerente
    {
        public static void MenuGerenteNovo()
        {
            Title = "Menu Gerente";
            MenuGerenteMetodo();

        }

        private static void MenuGerenteMetodo()
        {
            while (true)
            {
                string prompt = @"

   _____                     _       
  / ____|                   | |      
 | |  __  ___ _ __ ___ _ __ | |_ ___ 
 | | |_ |/ _ \ '__/ _ \ '_ \| __/ _ \
 | |__| |  __/ | |  __/ | | | ||  __/
  \_____|\___|_|  \___|_| |_|\__\___|
                                     
                                     

Bem Vindo qual das opções deseja selecionar?";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;

                string[] options = { "Criar novo funcionário", "Eliminar funcionário", "Biblioteca", "Voltar para o menu principal" };
                NovoMenuGerente mainMenu = new NovoMenuGerente(prompt, options);
                int SelectedGerente = mainMenu.Run();

                try
                {

                    switch (SelectedGerente)
                    {
                        case 0:
                            CriarFuncionario();
                            break;

                        case 1:
                            EliminarFuncionario();
                            break;

                        case 2:
                            // vai para o menu de venda (lista dos livros)
                            break;

                        case 3:
                            Console.Clear();
                            return; // sai do método 

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
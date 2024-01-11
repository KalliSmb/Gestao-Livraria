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

        // Método para criar o menu do Gerente
        private static void MenuGerenteMetodo()
        {
            // Ciclo para continuar a correr o menu até alguma opção ser selecionada
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

                string[] options = { "Criar novo funcionário", "Eliminar funcionário", "Listar funcionários", "Voltar para o menu principal" }; // Guarda as opções do menu num array
                NovoMenuGerente mainMenu = new NovoMenuGerente(prompt, options);
                int SelectedGerente = mainMenu.Run();

                try
                {
                    // Switch com os métodos de cada opção do menu
                    switch (SelectedGerente)
                    {
                        case 0:
                            CriarFuncionario();
                            break;

                        case 1:
                            EliminarFuncionario();
                            break;

                        case 2:
                            ListarFuncionarios();
                            break;

                        case 3:
                            Console.Clear();
                            return; // Sai do método

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

        // Método para criar funcionário
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

            string novaLinha = $"{utilizador},{password},{cargo}"; // Linha com todas as informações do funcionário

            string caminhoFicheiro = "..\\..\\DadosUtilizadores.txt"; // Caminho do ficheiro
            File.AppendAllText(caminhoFicheiro, novaLinha + Environment.NewLine); // Guarda as informações no ficheiro e salta uma linha

            // Mensagem de criação do funcionário
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNovo funcionário criado com sucesso!");
            Console.ReadLine();
        }

        // Método para eliminar funcionário
        public static void EliminarFuncionario()
        {
            Console.Clear();
            Console.WriteLine("Eliminar funcionário:");

            Console.Write("Nome do funcionário a ser removido: ");
            string utilizador = Console.ReadLine();

            string caminhoFicheiro = "..\\..\\DadosUtilizadores.txt"; // Caminho do ficheiro
            string[] linhas = File.ReadAllLines(caminhoFicheiro); // Array para ler todas as linhas do ficheiro

            bool funcionarioEncontrado = false;

            // Ciclo para percorrer todas as linhas do ficheiro
            for (int i = 0; i < linhas.Length; i++)
            {
                string[] dados = linhas[i].Split(','); // Guarda num array as informações separadas pelas vírgulas

                // If para encontrar o funcionário
                if (dados.Length == 3 && dados[0] == utilizador)
                {
                    linhas[i] = null;
                    funcionarioEncontrado = true;
                    break;
                }
            }

            // Se o funcionário for encontrado, remove-o, se não, mostra uma mensagem de erro
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

        // Método para listar todos os funcionários
        public static void ListarFuncionarios()
        {
            Console.Clear();
            Console.WriteLine("Listar funcionários:\n");

            string caminhoFicheiro = "..\\..\\DadosUtilizadores.txt"; // Caminho do ficheiro com as informações dos utilizadores

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro); // Guarda num array todas as linhas do ficheiro

                // If para verificar se o ficheiro está vazio, se não, mostra uma mensagem de erro
                if (linhas.Length > 0)
                {
                    foreach (string linha in linhas) // Percorre todas as linhas do ficheiro
                    {
                        string[] dados = linha.Split(','); // Guarda num array as informações sem as vírgulas

                        // If para verificar se todos os valores estão preenchidos
                        if (dados.Length == 3)
                        {
                            Console.WriteLine($"Nome: {dados[0]}, Password: {dados[1]}, Cargo: {dados[2]}");
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não há funcionários registados.");
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar funcionários: {ex.Message}");
                Console.ReadLine();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

                string[] options = { "Criar novo funcionário", "Eliminar funcionário", "Listar funcionários", "Vender livro", "Voltar para o menu principal", }; // Guarda as opções do menu num array
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
                            VenderLivro();
                            break;

                        case 4:
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

        public static void VenderLivro()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            // Ler todos os livros do arquivo
            string caminhoFicheiro = "..\\..\\Livros.txt";
            List<string> linhas = new List<string>(File.ReadAllLines(caminhoFicheiro));

            // Verificar se há livros registrados
            if (linhas.Count > 0)
            {
                try
                {
                    Console.WriteLine("Venda de Livros:\n");

                    // Lista os livros disponíveis
                    ListarLivros();

                    // Solicita os códigos dos livros
                    Console.WriteLine("\nIntroduza os códigos dos livros para vender (separados por espaços):");
                    string[] codigosInput = Console.ReadLine().Split(' ');

                    Console.Clear();

                    double totalVenda = 0;
                    double totalIVA = 0;
                    int totalLivrosVendidos = 0; // Adicionando a variável para rastrear o total de livros vendidos

                    // Lista de linhas modificadas
                    List<string> linhasModificadas = new List<string>();

                    // Loop pelos códigos introduzidos
                    foreach (string codigoInput in codigosInput)
                    {
                        // Verifica se o código é um número válido
                        if (int.TryParse(codigoInput, out int codigo))
                        {
                            // Busca o livro pelo código
                            int indexLivro = -1;
                            for (int i = 0; i < linhas.Count; i += 9)
                            {
                                if (int.Parse(linhas[i + 1].Trim()) == codigo)
                                {
                                    indexLivro = i;
                                    break;
                                }
                            }

                            // Se o livro foi encontrado
                            if (indexLivro != -1)
                            {
                                // Exibe detalhes do livro e solicita a quantidade desejada
                                Console.WriteLine($"\nDetalhes do Livro (Código: {codigo}):");
                                Console.WriteLine($"Título: {linhas[indexLivro + 2].Trim()}");
                                Console.WriteLine($"Autor: {linhas[indexLivro + 3].Trim()}");
                                Console.WriteLine($"Preço: {linhas[indexLivro + 6].Trim()}€");
                                Console.WriteLine($"Stock: {linhas[indexLivro + 8].Trim()}");
                                Console.Write("Quantidade desejada: ");

                                // Verifica se a quantidade é um número válido
                                if (int.TryParse(Console.ReadLine(), out int quantidadeDesejada))
                                {
                                    int estoqueAtual = int.Parse(linhas[indexLivro + 8].Trim());

                                    // Verifica se há estoque suficiente
                                    if (quantidadeDesejada <= estoqueAtual)
                                    {
                                        double precoUnitario = double.Parse(linhas[indexLivro + 6].Trim());
                                        double precoVenda = precoUnitario * quantidadeDesejada;
                                        double iva = double.Parse(linhas[indexLivro + 7].Trim()) * quantidadeDesejada;

                                        totalVenda += precoVenda;
                                        totalIVA += iva;
                                        totalLivrosVendidos += quantidadeDesejada; // Incrementa o total de livros vendidos

                                        Console.WriteLine($"IVA (23%): {iva}€");
                                        Console.WriteLine($"Subtotal: {precoVenda}€");

                                        // Atualiza o estoque
                                        linhas[indexLivro + 8] = (estoqueAtual - quantidadeDesejada).ToString();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Erro: Quantidade desejada superior ao estoque disponível.");
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Erro: Quantidade inválida.");
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Erro: Livro com código {codigo} não encontrado.");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Erro: Código inválido.");
                        }
                    }

                    // Adiciona as linhas modificadas à lista
                    linhasModificadas.AddRange(linhas);

                    Console.WriteLine($"\nTotal de Livros Vendidos: {totalLivrosVendidos}");
                    Console.WriteLine($"Total do IVA: {totalIVA}€");
                    Console.WriteLine($"Total da Venda: {totalVenda}€");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Venda realizada com sucesso!");

                    // Salva as alterações no arquivo com quebra de linha adequada
                    File.WriteAllText(caminhoFicheiro, string.Join(Environment.NewLine, linhasModificadas), Encoding.UTF8);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro ao realizar a venda: {ex.Message}");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não há livros registrados para realizar a venda.");
                Console.ReadKey();
            }

            Console.Clear();
        }

        public static void ListarLivros()
        {
            try
            {
                string[] linhas = File.ReadAllLines("..\\..\\Livros.txt");

                Console.WriteLine("Lista de Livros Disponíveis:\n");

                for (int i = 0; i < linhas.Length; i += 9)
                {
                    // Verifica se há informações suficientes para um livro
                    if (i + 8 < linhas.Length && !string.IsNullOrWhiteSpace(linhas[i + 8]))
                    {
                        Console.WriteLine($"ID: {linhas[i + 1].Trim()}");
                        Console.WriteLine($"Título: {linhas[i + 2].Trim()}");
                        Console.WriteLine($"Autor: {linhas[i + 3].Trim()}");
                        Console.WriteLine($"Preço: {linhas[i + 6].Trim()}€");
                        Console.WriteLine($"Stock: {linhas[i + 8].Trim()}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros: {ex.Message}");
            }
        }
    }
}
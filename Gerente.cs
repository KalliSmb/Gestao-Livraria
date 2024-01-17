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

                string[] options = { "Criar novo funcionário", "Eliminar funcionário", "Listar funcionários", "Mudar Credenciais dos Funcionários", "Vender livro", "Voltar para o menu principal", }; // Guarda as opções do menu num array
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
                            ModificarFuncionario();
                            break;

                        case 4:
                            VenderLivro();
                            break;

                        case 5:
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

            string cargo;

            do
            {
                Console.Write("Cargo (Gerente, Repositor, Caixa): ");
                cargo = Console.ReadLine();

                // Verifica se o cargo é válido
                if (!ValidarCargo(cargo))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: Por favor, digite um cargo válido (Gerente, Repositor ou Caixa).");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (!ValidarCargo(cargo));

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

            Console.Write("ID do funcionário a ser removido: ");

            if (!int.TryParse(Console.ReadLine(), out int numeroLinha))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, insira um ID válido.");
                Console.ReadLine();
                return;
            }

            string caminhoFicheiro = "..\\..\\DadosUtilizadores.txt";
            List<string> linhas = File.ReadAllLines(caminhoFicheiro).ToList();

            if (numeroLinha >= 1 && numeroLinha <= linhas.Count)
            {
                // Encontrar a linha real correspondente
                int linhaReal = 0;
                for (int i = 0; i < linhas.Count; i++)
                {
                    if (!string.IsNullOrEmpty(linhas[i]))
                    {
                        linhaReal++;
                        if (linhaReal == numeroLinha)
                        {
                            linhas.RemoveAt(i);
                            break;
                        }
                    }
                }

                File.WriteAllLines(caminhoFicheiro, linhas);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nFuncionário removido com sucesso!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNúmero de linha inválido. Funcionário não removido.");
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
        public static void ModificarFuncionario()
        {
            Console.Clear();
            Console.WriteLine("Modificar funcionário:");

            Console.Write("ID do funcionário a ser modificado: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroLinha))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, insira um ID válido.");
                Console.ReadLine();
                return;
            }

            string caminhoFicheiro = "..\\..\\DadosUtilizadores.txt";
            List<string> linhas = File.ReadAllLines(caminhoFicheiro).ToList();

            if (numeroLinha >= 1 && numeroLinha <= linhas.Count)
            {
                string[] dados = linhas[numeroLinha - 1].Split(',');

                Console.WriteLine($"Funcionário atual: Nome={dados[0]}, Password={dados[1]}, Cargo={dados[2]}");

                Console.WriteLine("\nO que deseja alterar?");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - Password");
                Console.WriteLine("3 - Cargo");

                if (int.TryParse(Console.ReadLine(), out int opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            Console.Write("Novo nome: ");
                            string novoNome = Console.ReadLine();
                            dados[0] = novoNome;
                            break;

                        case 2:
                            Console.Write("Nova password: ");
                            string novaPassword = Console.ReadLine();
                            dados[1] = novaPassword;
                            break;

                        case 3:
                            Console.Write("Novo cargo: ");
                            string novoCargo = Console.ReadLine();
                            dados[2] = novoCargo;
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opção inválida. Funcionário não modificado.");
                            Console.ReadLine();
                            return;
                    }

                    // Construir a nova linha com as modificações
                    string novaLinha = string.Join(",", dados);

                    // Atualizar a linha no array
                    linhas[numeroLinha - 1] = novaLinha;

                    // Reescrever o arquivo com as modificações
                    File.WriteAllLines(caminhoFicheiro, linhas);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nFuncionário modificado com sucesso!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida. Funcionário não modificado.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNúmero de linha inválido. Funcionário não modificado.");
            }

            Console.ReadLine();
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

                    // Aplica o desconto de 10% se a venda ultrapassar 50 euros
                    if (totalVenda > 50)
                    {
                        double desconto = totalVenda * 0.10;
                        totalVenda -= desconto;
                        Console.WriteLine($"\nTotal de Livros Vendidos: {totalLivrosVendidos}");
                        Console.WriteLine($"Total do IVA: {totalIVA}€");
                        Console.WriteLine($"Desconto (10%): -{desconto}€");
                        Console.WriteLine($"Total da Venda: {totalVenda}€");
                    }
                    else
                    {
                        Console.WriteLine($"\nTotal de Livros Vendidos: {totalLivrosVendidos}");
                        Console.WriteLine($"Total do IVA: {totalIVA}€");
                        Console.WriteLine($"Total da Venda: {totalVenda}€");
                    }

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
        private static bool ValidarCargo(string cargo)
        {
            // Lista de cargos válidos
            string[] cargosValidos = { "Gerente", "Repositor", "Caixa" };

            // Verifica se o cargo fornecido está na lista de cargos válidos
            return cargosValidos.Contains(cargo);
        }
    }
}
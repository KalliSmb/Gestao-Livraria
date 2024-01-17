using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Console;

namespace LP1_Livraria
{
    public class Caixa
    {
        public static void MenuCaixa()
        {
            Title = "Menu Caixa";
            MenuCaixaMetodo();

        }

        public static void MenuCaixaMetodo()
        {
            while (true)
            {
                string prompt = @"

   _____      _           
  / ____|    (_)          
 | |     __ _ ___  ____ _ 
 | |    / _` | \ \/ / _` |
 | |___| (_| | |>  < (_| |
  \_____\__,_|_/_/\_\__,_|
                                                    

Bem Vindo qual das opções deseja selecionar ? ";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;

                string[] options = { "Vender Livro", "Listar livros pelo autor", "Listar livros pelo género", "Voltar para o menu principal" };
                NovoMenuCaixa mainMenu = new NovoMenuCaixa(prompt, options);
                int SelectedCaixa = mainMenu.Run4();

                try
                {
                    switch (SelectedCaixa)
                    {
                        case 0:
                            VenderLivro();
                            break;

                        case 1:
                            ListarLivrosAutor();
                            break;

                        case 2:
                            ListarLivrosGenero();
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

        public static void ListarLivrosAutor()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Introduza o nome do autor que deseja listar os livros:");
                string autorInput = Console.ReadLine();

                string[] linhas = File.ReadAllLines("..\\..\\Livros.txt"); // Guarda no array todas as linhas do ficheiro

                Console.Clear();
                Console.WriteLine("Livros do autor " + autorInput + ":\n");

                // Loop pelas linhas para procurar os livros do autor
                for (int i = 0; i < linhas.Length; i += 9)
                {
                    // Verifica se há algum elemento não preenchido
                    if (i + 8 < linhas.Length)
                    {
                        // Verifica se o autor na linha atual corresponde ao autor introduzido
                        if (linhas[i + 3].Equals(autorInput, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"ID: {linhas[i + 1]}");
                            Console.WriteLine($"Título: {linhas[i + 2]}\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros por autor: {ex.Message}");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public static void ListarLivrosGenero()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Introduza o género de livro que deseja listar:");
                string generoInput = Console.ReadLine();

                string[] linhas = File.ReadAllLines("..\\..\\Livros.txt"); // Guarda no array todas as linhas do ficheiro

                Console.Clear();
                Console.WriteLine("Livros do género " + generoInput + ":\n");

                // Loop pelas linhas para procurar os livros do género
                for (int i = 0; i < linhas.Length; i += 9)
                {
                    // Verifica se há algum elemento não preenchido
                    if (i + 8 < linhas.Length)
                    {
                        // Verifica se o género na linha atual corresponde ao género introduzido
                        if (linhas[i + 5].Equals(generoInput, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"ID: {linhas[i + 1]}");
                            Console.WriteLine($"Título: {linhas[i + 2]}");
                            Console.WriteLine($"Autor: {linhas[i + 3]}\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros por género: {ex.Message}");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}

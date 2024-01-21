using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace LP1_Livraria
{
    public class Repositor
    {
        public static void MenuRepositor()
        {
            Title = "Repositor";
            MenuRepositorMetodo();
        }

        public static void MenuRepositorMetodo()
        {
            while (true)
            {
                string prompt = @"

  _____                      _ _             
 |  __ \                    (_) |            
 | |__) |___ _ __   ___  ___ _| |_ ___  _ __ 
 |  _  // _ \ '_ \ / _ \/ __| | __/ _ \| '__|
 | | \ \  __/ |_) | (_) \__ \ | || (_) | |   
 |_|  \_\___| .__/ \___/|___/_|\__\___/|_|   
            | |                              
            |_|                              


Bem Vindo qual das opções deseja selecionar?";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;

                string[] options = { "Consultar Stock", "Adicionar Stock", "Registar Livro", "Voltar para o menu principal" };
                NovoMenuRepositor mainMenu = new NovoMenuRepositor(prompt, options);
                int SelectedRepositor = mainMenu.Run3();

                try
                {

                    switch (SelectedRepositor)
                    {
                        case 0:
                            ConsultarStock();
                            break;

                        case 1:
                            AdicionarStock();
                            break;

                        case 2:
                            RegistarLivro();
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

        public static void RegistarLivro()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Clear();
            Title = "Registar Livro";

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

            try
            {
                string novaLinha;
                bool continuarRegisto = true;

                // Carregar os códigos existentes do arquivo para uma lista
                List<int> codigosExistentes = new List<int>();
                if (File.Exists(caminhoFicheiro))
                {
                    string[] linhas = File.ReadAllLines(caminhoFicheiro);
                    for (int i = 1; i < linhas.Length; i += 9) // Começar de 1 para pegar a segunda linha de cada bloco
                    {
                        if (int.TryParse(linhas[i], out int codigoLivro))
                        {
                            codigosExistentes.Add(codigoLivro);
                        }
                    }
                }

                while (continuarRegisto)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Código do livro (insira o código 0 para terminar): ");
                    if (!int.TryParse(Console.ReadLine(), out int codigo))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: O código do livro deve ser um número inteiro.");
                        Console.ReadKey();
                        return;
                    }

                    // Se o código for 0, termina o registo
                    if (codigo == 0)
                    {
                        continuarRegisto = false;
                        continue;
                    }

                    // Verificar se o código já existe
                    if (codigosExistentes.Contains(codigo))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: Este código já foi utilizado. Escolha um código diferente.\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Aperte ENTER para digitar um código novo!");
                        Console.ReadKey();
                        Console.Clear();
                        continue;

                    }

                    Console.Write("\nNome do livro: ");
                    string nome = Console.ReadLine();

                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();

                    Console.Write("ISBN: ");
                    if (!long.TryParse(Console.ReadLine(), out long isbn))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: O ISBN do livro deve ser um número longo.");
                        Console.ReadKey();
                        return;
                    }

                    Console.Write("Género: ");
                    string genero = Console.ReadLine();

                    Console.Write("Preço final: € ");
                    if (!double.TryParse(Console.ReadLine(), out double precoFinal))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: O preço final do livro deve ser um número decimal.");
                        Console.ReadKey();
                        return;
                    }

                    double precoIVA = precoFinal * 0.23;
                    precoIVA = Math.Round(precoIVA, 2);
                    Console.Write($"Preço do IVA (23%): € {precoIVA}\n");

                    Console.Write("Quantidade em stock: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantidadeStock))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: A quantidade em stock do livro deve ser um número inteiro.");
                        Console.ReadKey();
                        return;
                    }

                    novaLinha = $"{Environment.NewLine}{codigo}\n{nome}\n{autor}\n{isbn}\n{genero}\n{precoFinal}\n{precoIVA}\n{quantidadeStock}\n---------------------------------------------------";

                    File.AppendAllText(caminhoFicheiro, novaLinha);
                    codigosExistentes.Add(codigo); // Adicionar o novo código à lista de códigos existentes
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Novo livro registado com sucesso!\n");
                    Console.WriteLine("Aperte ENTER para digitar um código novo!");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao registar livros: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public static void AdicionarStock()
        {
            Console.Clear();

            Console.Write("Introduza o código do livro para adicionar stock: ");
            string codigo = Console.ReadLine();

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

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
                        if (i + 7 < linhas.Length) // Adiciona 7 para ir até a linha do stock
                        {
                            Console.WriteLine($"\nStock atual do livro '{linhas[i + 1].Trim()}': {linhas[i + 7].Trim()}");
                            Console.Write("Quantidade a adicionar ao stock: ");

                            if (int.TryParse(Console.ReadLine(), out int quantidadeAdicionar))
                            {
                                int stockAtual = int.Parse(linhas[i + 7].Trim());
                                int novoStock = stockAtual + quantidadeAdicionar;

                                // Atualiza a linha do stock no array
                                linhas[i + 7] = novoStock.ToString();

                                // Gera as linhas formatadas para o ficheiro
                                string[] linhasFormatadas = linhas.Select((line, index) =>
                                {
                                    if (index == i + 7)
                                        return novoStock.ToString();
                                    return line;
                                }).ToArray();

                                // Atualiza o ficheiro com as novas linhas
                                File.WriteAllText(caminhoFicheiro, string.Join("\n", linhasFormatadas));

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\nStock atualizado com sucesso! Novo stock: {novoStock}\n");
                                Console.WriteLine("Aperte ENTER para voltar ao menu!");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Erro: A quantidade a adicionar deve ser um número inteiro!");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Erro: Informações de stock ausentes para o livro {codigo}!");
                        }
                    }
                }

                if (!livroEncontrado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: Livro com código {codigo} não encontrado!");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao adicionar stock: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void ConsultarStock()
        {
            Console.Clear();

            Console.Write("Introduza o código do livro que deseja consultar stock: ");
            string codigo = Console.ReadLine();

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

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
                        if (i + 8 < linhas.Length) // Adiciona 8 para ir até a linha após a divisão
                        {
                            // Exibe apenas o nome e o stock do livro
                            Console.WriteLine($"\nLivro: {linhas[i + 1].Trim()}\nStock: {linhas[i + 7].Trim()}\n");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Aperte ENTER para regressar ao menu!");
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

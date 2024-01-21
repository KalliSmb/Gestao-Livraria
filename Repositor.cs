using System;
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
            WriteLine("Registar livro:\n");

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

            try
            {
                string novaLinha;
                bool continuarRegisto = true;

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

                    Console.Write("Nome do livro: ");
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

                    novaLinha = $"{codigo}|{nome}|{autor}|{isbn}|{genero}|{precoFinal}|{precoIVA}|{quantidadeStock}";

                    // Adiciona uma linha em branco apenas se o arquivo não estiver vazio
                    if (new FileInfo(caminhoFicheiro).Length > 0)
                    {
                        novaLinha = Environment.NewLine + novaLinha;
                    }

                    File.AppendAllText(caminhoFicheiro, novaLinha);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Novo livro registado com sucesso!\n");
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
                                Console.WriteLine($"\nStock atualizado com sucesso. Novo stock: {novoStock}");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Erro: A quantidade a adicionar deve ser um número inteiro.");
                            }
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
                Console.WriteLine($"Erro ao adicionar stock: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void ConsultarStock()
        {
            Console.Clear();

            Console.Write("Introduza o código do livro que deseja consultar stock: ");
            if (!int.TryParse(Console.ReadLine(), out int codigoLivro))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, insira um código de livro válido (número inteiro).");
                Console.ReadKey();
                return;
            }

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro);

                bool livroEncontrado = false;

                for (int i = 0; i < linhas.Length; i++)
                {
                    string[] dadosLivro = linhas[i].Split('|');

                    // Verifica se o código do livro corresponde ao código fornecido
                    if (dadosLivro.Length > 0 && int.TryParse(dadosLivro[0], out int codigo) && codigo == codigoLivro)
                    {
                        livroEncontrado = true;

                        // Exibe as informações do livro, incluindo o stock
                        Console.WriteLine($"Livro: {dadosLivro[1].Trim()}\nStock: {dadosLivro[7].Trim()}");
                        break;
                    }
                }

                if (!livroEncontrado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: Livro com código {codigoLivro} não encontrado.");
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

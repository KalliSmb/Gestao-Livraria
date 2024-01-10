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
                Console.WriteLine("| 1. Registar novo livro                     |");
                Console.WriteLine("| 2. Adicionar stock a um livro              |");
                Console.WriteLine("| 3. Consultar stock                         |");
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
                            RegistarLivro();
                            break;

                        case 2:
                            AdicionarStock();
                            break;

                        case 3:
                            ConsultarStock();
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

        public static void RegistarLivro()
        {
            Console.Clear();

            Console.WriteLine("Registar novo livro:");

            Console.Write("Código do livro: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: O código do livro deve ser um número inteiro.");
                Console.ReadKey();
                return;
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

            Console.Write("Preço final: ");
            if (!double.TryParse(Console.ReadLine(), out double precoFinal))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: O preço final do livro deve ser um número decimal.");
                Console.ReadKey();
                return;
            }

            // Calcula o preço do IVA (23% do valor final)
            double precoIVA = precoFinal * 0.23;

            Console.Write($"Preço do IVA (23%): {precoIVA}\n");

            Console.Write("Quantidade em stock: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidadeStock))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: A quantidade em stock do livro deve ser um número inteiro.");
                Console.ReadKey();
                return;
            }

            string novaLinha = $"{codigo}\n{nome}\n{autor}\n{isbn}\n{genero}\n{precoFinal}\n{precoIVA}\n{quantidadeStock}\n---------------------------------------------------";

            string caminhoFicheiro = "..\\..\\Livros.txt";

            try
            {
                // Adiciona uma linha em branco apenas se o arquivo não estiver vazio
                if (new FileInfo(caminhoFicheiro).Length > 0)
                {
                    novaLinha = Environment.NewLine + novaLinha;
                }

                File.AppendAllText(caminhoFicheiro, novaLinha);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nNovo livro registado com sucesso!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao registar o livro: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void AdicionarStock()
        {
            Console.Clear();

            Console.Write("Introduza o código do livro para adicionar stock: ");
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
                            Console.WriteLine($"\nStock atual do livro '{linhas[i + 1].Trim()}': {linhas[i + 7].Trim()}");
                            Console.Write("Quantidade a adicionar ao stock: ");

                            if (int.TryParse(Console.ReadLine(), out int quantidadeAdicionar))
                            {
                                int stockAtual = int.Parse(linhas[i + 7].Trim());
                                int novoStock = stockAtual + quantidadeAdicionar;

                                // Atualiza a linha do stock no array
                                linhas[i + 7] = novoStock.ToString();

                                // Atualiza o arquivo com as novas linhas
                                File.WriteAllLines(caminhoFicheiro, linhas);

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
                        if (i + 8 < linhas.Length) // Adiciona 8 para ir até a linha após a divisão
                        {
                            // Exibe apenas o nome e o stock do livro
                            Console.WriteLine($"\nLivro: {linhas[i + 1].Trim()}\nStock: {linhas[i + 7].Trim()}");
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

using System;
using System.IO;
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
                            //VenderLivro();
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

        public static void ListarLivrosAutor()
        {
            Console.Clear();
            Console.WriteLine("Listar livros pelo autor:\n");

            string caminhoFicheiro = "..\\..\\Livros.txt";

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro);

                if (linhas.Length > 0)
                {
                    Console.Write("Introduza o nome do autor: ");
                    string autorInput = Console.ReadLine();

                    Console.Clear();
                    Console.WriteLine($"Livros do autor '{autorInput}':");

                    for (int i = 0; i < linhas.Length; i += 8)
                    {
                        string autor = linhas[i + 2].Trim();

                        if (autor.Equals(autorInput, StringComparison.OrdinalIgnoreCase))
                        {
                            //Console.WriteLine($"Código: {linhas[i].Trim()}");
                            Console.WriteLine($"Título: {linhas[i + 1].Trim()}");
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não há livros registados.");
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros pelo autor: {ex.Message}");
                Console.ReadLine();
            }
        }

        public static void ListarLivrosGenero()
        {
            Console.Clear();
            Console.WriteLine("Listar livros pelo género:\n");

            string caminhoFicheiro = "..\\..\\Livros.txt";

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro);

                if (linhas.Length > 0)
                {
                    Console.Write("Introduza o género: ");
                    string generoInput = Console.ReadLine();

                    Console.Clear();
                    Console.WriteLine($"Livros do género '{generoInput}':");

                    for (int i = 0; i < linhas.Length; i += 8)
                    {
                        string genero = linhas[i + 4].Trim();

                        if (genero.Equals(generoInput, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Código: {linhas[i].Trim()}");
                            Console.WriteLine($"Título: {linhas[i + 1].Trim()}");
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não há livros registados.");
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros pelo género: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}

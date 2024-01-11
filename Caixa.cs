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

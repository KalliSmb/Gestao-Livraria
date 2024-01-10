using System;

namespace LP1_Livraria
{
    public class Caixa
    {
        public static void MenuCaixa()
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|============================================|");
                Console.WriteLine("|                 Menu Caixa                 |");
                Console.WriteLine("|============================================|");
                Console.WriteLine("| 0. Voltar para o menu principal            |");
                Console.WriteLine("| 1. Vender livro                            |");
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
                            //VenderLivro();
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
    }
}

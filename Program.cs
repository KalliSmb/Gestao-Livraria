using System;

namespace LP1_Livraria
{
    class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|============================================|");
                Console.WriteLine("|                  Livraria                  |");
                Console.WriteLine("|============================================|");
                Console.WriteLine("| 0. Sair                                    |");
                Console.WriteLine("| 1. Login                                   |");
                Console.WriteLine("|                                            |");
                Console.WriteLine("|--------------------------------------------|\n");

                Console.Write(" -> ");

                try
                {
                    int escolha = int.Parse(Console.ReadLine());

                    if (escolha == 0)
                    {
                        break;
                    }

                    if (escolha == 1)
                    {
                        Console.Clear();
                        Console.Write("Utilizador: ");
                        string utilizador = Console.ReadLine();
                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        if (Login.VerificarLogin(utilizador, password))
                        {
                            // vai para o método VerificarLogin da classe Login
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Login falhou. Utilizador ou password inválidos.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        throw new Exception("Erro: Valor inválido! Escolha um número entre 0 e 1.");
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: Valor inválido! Escolha um número inteiro.");
                    Console.ReadKey();
                    Console.Clear();

                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}

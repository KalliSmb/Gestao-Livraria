using System;

namespace LP1_Livraria
{
    class Program
    {
        private static bool sairPrograma = false;

        public static void Main(string[] args)
        {
            while (!sairPrograma)
            {
                MenuPrincipal();
            }
        }

        public static void MenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("|============================================|");
            Console.WriteLine("|                  Livraria                  |");
            Console.WriteLine("|============================================|");
            Console.WriteLine("| 0 --> Sair                                 |");
            Console.WriteLine("| 1 --> Login                                |");
            Console.WriteLine("|                                            |");
            Console.WriteLine("|--------------------------------------------|\n");

            Console.Write(" -> ");

            try
            {
                int escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {
                    case 0:
                        sairPrograma = true;
                        break;

                    case 1:
                        FazerLogin();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: Opção inválida.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
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

        private static void FazerLogin()
        {
            Console.Clear();
            Console.Write("Utilizador: ");
            string utilizador = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (Login.VerificarLogin(utilizador, password))
            {
                Gerente.MenuGerente();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Login falhou. Utilizador ou password inválidos.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void Logout()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Logout bem sucedido!");
            Console.ReadKey();
        }
    }
}

using System;
using static System.Console;

namespace LP1_Livraria
{
    class Program
    {
        private static bool sairPrograma = false;

        public static Login.Cargo cargoAtual = Login.Cargo.Desconhecido;

        public static void Main(string[] args)
        {
            while (!sairPrograma)
            {
                MenuPrincipal();
            }
        }

        public static void MenuPrincipal()
        {
            Title = "Login";
            MetodoMenuLogin();
        }

        public static void MetodoMenuLogin()
        {
            string prompt = @"

  _                 _       
 | |               (_)      
 | |     ___   __ _ _ _ __  
 | |    / _ \ / _` | | '_ \ 
 | |___| (_) | (_| | | | | |
 |______\___/ \__, |_|_| |_|
               __/ |        
              |___/         


Selecione uma das opcões!";
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

            string[] options = { "Login", "Sair" };
            MenuLoginNovo mainMenu = new MenuLoginNovo(prompt, options);
            int SelectedLogin = mainMenu.Run1();

            try
            {

                switch (SelectedLogin)
                {
                    case 0:
                        FazerLogin();
                        break;

                    case 1:
                        sairPrograma = true;
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

            cargoAtual = Login.VerificarLogin(utilizador, password);

            switch (cargoAtual)
            {
                case Login.Cargo.Gerente:
                    Gerente.MenuGerenteNovo();
                    break;

                case Login.Cargo.Caixa:
                    Caixa.MenuCaixa();
                    break;

                case Login.Cargo.Repositor:
                    Repositor.MenuRepositor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login falhou. Utilizador ou password inválidos ou cargo desconhecido.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }

        public static void Logout()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Logout bem sucedido!");
            Console.ReadKey();
            cargoAtual = Login.Cargo.Desconhecido;
        }
    }
}

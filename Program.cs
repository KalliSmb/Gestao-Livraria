using System;
using System.IO;
using System.Text;

namespace LP1_Livraria
{
    class Program
    {
        static void Main(string[] args)
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

                        if (VerificarLogin(utilizador, password))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Login bem sucedido!");
                            Console.ReadKey();
                            Console.Clear();
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

        static bool VerificarLogin(string utilizador, string password)
        {
            string caminhoArquivo = "..\\..\\DadosUtilizadores.txt";

            try
            {
                if (!File.Exists(caminhoArquivo))
                {
                    return false;
                }

                string[] linhas = File.ReadAllLines(caminhoArquivo);

                foreach (string linha in linhas)
                {
                    string[] dados = linha.Split(',');
                    string[] campos = new string[3];

                    if (dados.Length == campos.Length)
                    {
                        Array.Copy(dados, campos, campos.Length);

                        if (campos[0] == utilizador && campos[1] == password) 
                        {
                            string cargo = campos[2];
                            Console.WriteLine($"Bem vindo, {utilizador}! Cargo: {cargo}");
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao verificar dados de login: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }
    }
}

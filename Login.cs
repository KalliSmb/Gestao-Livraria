using System;
using System.IO;

namespace LP1_Livraria
{
    public class Login
    {
        public static bool VerificarLogin(string utilizador, string password)
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

                    if (dados.Length == dados.Length)
                    {
                        Array.Copy(dados, campos, campos.Length);

                        if (campos[0] == utilizador && campos[1] == password)
                        {
                            string cargo = campos[2];

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Login bem sucedido!");
                            Console.ReadKey();
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"Bem vindo, {utilizador}! Cargo: {cargo}.");
                            Console.ReadKey();
                            Console.Clear();

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
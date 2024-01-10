using System;
using System.IO;

namespace LP1_Livraria
{
    public class Login
    {
        public enum Cargo
        {
            Desconhecido,
            Gerente,
            Caixa,
            Repositor
        }

        public static Cargo VerificarLogin(string utilizador, string password)
        {
            string caminhoArquivo = "..\\..\\DadosUtilizadores.txt";

            try
            {
                if (!File.Exists(caminhoArquivo))
                {
                    return Cargo.Desconhecido;
                }

                string[] linhas = File.ReadAllLines(caminhoArquivo);

                foreach (string linha in linhas)
                {
                    string[] dados = linha.Split(',');

                    if (dados.Length == 3 && dados[0] == utilizador && dados[1] == password)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Login bem sucedido! Pressione ENTER para continuar.");
                        Console.ReadKey();

                        if (Enum.TryParse(dados[2], out Cargo cargo))
                        {
                            return cargo;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Erro: Cargo inválido encontrado.");
                            return Cargo.Desconhecido;
                        }
                    }
                }

                return Cargo.Desconhecido;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao verificar dados de login: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
                return Cargo.Desconhecido;
            }
        }
    }
}

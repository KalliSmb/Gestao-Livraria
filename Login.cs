using System;
using System.IO;

namespace LP1_Livraria
{
    public class Login
    {
        //Representa a enumeração dos diferentes cargos 
        public enum Cargo
        {
            Desconhecido,
            Gerente,
            Caixa,
            Repositor
        }

        //Método que verifica o login com base no utilizador e password
        public static Cargo VerificarLogin(string utilizador, string password)
        {
            //Caminho do arquivo que contem os dados dos utilizadores
            string caminhoArquivo = "..\\..\\Funcionarios\\DadosUtilizadores.txt";

            try
            {
                //Verifica se o arquivo existe
                if (!File.Exists(caminhoArquivo))
                {
                    //Se não existir retorna o valor
                    return Cargo.Desconhecido;
                }

                //Lê todas as linhas do arquivo
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                foreach (string linha in linhas)
                {
                    //Divide os dados da linha ao usar ',' como delimitador.
                    string[] dados = linha.Split(',');

                    //Verifica se os dados são válidos
                    if (dados.Length == 3 && dados[0] == utilizador && dados[1] == password)
                    {
                        //Exibir Mensagem
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Login bem sucedido! Pressione ENTER para continuar.");
                        Console.ReadKey();

                        //Tenta fazer o parse do cargo a partir da string e retorna o valor correspondente.
                        if (Enum.TryParse(dados[2], out Cargo cargo))
                        {
                            return cargo;
                        }
                        else
                        {
                            //Caso falhe exibir mensagem
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Erro: Cargo inválido encontrado.");
                            return Cargo.Desconhecido;
                        }
                    }
                }
                //Se não encontrar um cargo correspondente retorna o valor
                return Cargo.Desconhecido;
            }
            catch (Exception ex)
            {
                //Mensagem de erro 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao verificar dados de login: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
                return Cargo.Desconhecido;
            }
        }
    }
}

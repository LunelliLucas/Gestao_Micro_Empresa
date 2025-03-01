using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gestao_Micro_Empresa
{
    public interface ICadastros
    {
        public void Choice(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios);
        public void Create(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios);
        public void Update(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios);
        public void Delete(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios);
        public static void Cabecalho(string? info)
        {
                Console.WriteLine("Sistema de Gerenciamento Financeiro");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"|{info}|\n");
        }
        public static void Serializacao<T>(string caminho, T t)
        {
            using (FileStream stream = new FileStream(caminho,
                                                     FileMode.Create,
                                                     FileAccess.Write))
            {
                JsonSerializer.Serialize(stream, t, new JsonSerializerOptions { WriteIndented = true });
            }
        }

        public static void Deserializacao()
        {
            string? diretorio = @"c:\Gerenciamento Financeiro\Cadastros";
            string? dadosFuncionarios = @"c:\Gerenciamento Financeiro\Cadastros\fornecedores.json";
            string? dadosFornecedores = @"c:\Gerenciamento Financeiro\Cadastros\fornecedores.json";
            string? dadosSocios = @"c:\Gerenciamento Financeiro\Cadastros\socios.json";
            string? dadosDespesasFixas = @"c:\Gerenciamento Financeiro\Cadastros\despesas_fixas.json";
            List<Funcionario>? jsonFuncionarios = new();
            List<Fornecedor>? jsonFornecedores = new();
            List<Socio>? jsonSocios = new();
            List<Despesa>? jsonDespesas = new();

            try
            {
                if (!File.Exists(dadosFornecedores) && !File.Exists(dadosFuncionarios) &&
                    !File.Exists(dadosSocios) && !File.Exists(dadosDespesasFixas))
                {
                    Directory.CreateDirectory(diretorio);
                    File.Create(dadosFornecedores).Dispose();
                    File.Create(dadosFuncionarios).Dispose();
                    File.Create(dadosSocios).Dispose();
                    File.Create(dadosDespesasFixas).Dispose();
                }

                if (new FileInfo(dadosFuncionarios).Length > 0)
                {
                    var jsonFileFunci = File.ReadAllText(dadosFuncionarios);
                    jsonFuncionarios = JsonSerializer.Deserialize<List<Funcionario>>(jsonFileFunci);
                }
                if (new FileInfo(dadosFornecedores).Length > 0)
                {
                    var jsonFileFornec = File.ReadAllText(dadosFornecedores);
                    jsonFornecedores = JsonSerializer.Deserialize<List<Fornecedor>>(jsonFileFornec);
                }
                if (new FileInfo(dadosSocios).Length > 0)
                {
                    var jsonFileSoc = File.ReadAllText(dadosSocios);
                    jsonSocios = JsonSerializer.Deserialize<List<Socio>>(jsonFileSoc);
                }
                if (new FileInfo(dadosDespesasFixas).Length > 0)
                {
                    var jsonFileDesp = File.ReadAllText(dadosDespesasFixas);
                    jsonDespesas = JsonSerializer.Deserialize<List<Despesa>>(jsonFileDesp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }
        
    }
}

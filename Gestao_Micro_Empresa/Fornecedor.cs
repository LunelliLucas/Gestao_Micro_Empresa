using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gestao_Micro_Empresa
{
    public class Fornecedor : ICadastros
    {
        public Fornecedor() { }
        public string? Nome { get; set; }
        private decimal receita;
        public decimal Receita 
        { 
            get { return receita; } 
            set
            {
                if (value >= 0)
                {
                    receita = value;
                }
                else
                {
                    Console.WriteLine("O valor não pode ser negativo!!");
                }
            }
        }

        public void Choice(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Cadastro de Fornecedores");
            Console.WriteLine("[1]Adicionar");
            Console.WriteLine("[2]Editar");
            Console.WriteLine("[3]Excluir");
            Console.WriteLine("[4]Voltar");
            int resp = Convert.ToInt16(Console.ReadLine());
            switch (resp)
            {
                case 1:
                    Console.Clear();
                    Create(fornec, despesas, funcio, socios);
                    break;
                case 2:
                    Console.Clear();
                    Update(fornec, despesas, funcio, socios);
                    break;
                case 3:
                    Console.Clear();
                    Delete(fornec, despesas, funcio, socios);
                    break;
                case 4:
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("\nInforme um número válido!");
                    Console.WriteLine("\nPressione qualquer tecla para continuar..");
                    Console.ReadKey();
                    Choice(fornec, despesas, funcio, socios);
                    break;
            }
            Console.Clear();
        }
        public void Create(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            while (true)
            {
                Fornecedor fornecedor = new();
                ICadastros.Cabecalho("Adicionar novo Fornecedor");
                Console.Write("Informe o nome do Fornecedor: ");
                fornecedor.Nome = Console.ReadLine();
                fornec.Add(fornecedor);
                Console.WriteLine("\nFornecedor adicionado com sucesso!");
                ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\fornecedores.json", fornec);
                Console.Write("\nAdicionar mais Fornecedores? (Tecle \"1\" para SIM ou \"0\" para NÂO): ");
                int resp = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                if (resp != 1)
                    break;
            }
        }
        public void Update(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            Fornecedor fornecedor = new();
            ICadastros.Cabecalho("Editar informações do Fornecedor");
            Console.Write("Informe o número do Fornecedor à ser modificado:\n");
            int i = 0;
            foreach (var forn in fornec)
            {
                Console.WriteLine($"[{i}]{forn.Nome}");
                i++;
            }
            int resp1 = Convert.ToInt16(Console.ReadLine());
            fornec.RemoveAt(resp1);
            Console.WriteLine("Agora informe os novos dados do Fornecedor: ");
            Console.Write("Nome: ");
            fornecedor.Nome = Console.ReadLine();
            fornec.Insert(resp1, fornecedor);
            Console.WriteLine("Dados atualizados com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\fornecedores.json", fornec);
        }
        public void Delete(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Remover Fornecedor");
            Console.Write("Informe o número do Fornecedor à ser removido:\n");
            int i = 0;
            foreach (var forn in fornec)
            {
                Console.WriteLine($"[{i}]{forn.Nome}");
                i++;
            }
            int resp1 = Convert.ToInt16(Console.ReadLine());
            fornec.RemoveAt(resp1);
            Console.WriteLine("Fornecedor removido com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\fornecedores.json", fornec);
        }
    }
}

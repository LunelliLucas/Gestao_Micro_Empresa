using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_Micro_Empresa
{
    public partial class Despesa : ICadastros
    {
        public Despesa() { }
        private decimal valor;
        public decimal Valor
        {
            get { return valor; }
            set
            {
                if (value >= 0)
                {
                    valor = value;
                }
                else
                {
                    Console.WriteLine("O valor não pode ser negativo!!");
                }
            }
        }
        public string? Nome { get; set; }
        public void Choice(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Cadastro de Despesas Fixas");
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
                Despesa despesa = new();
                ICadastros.Cabecalho("Adicionar nova Despesa");
                Console.WriteLine("Informe o nome da Despesa: ");
                despesa.Nome = Console.ReadLine();
                despesas.Add(despesa);
                Console.WriteLine("Despesa adicionada com sucesso!");
                ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\despesas_fixas.json", despesas);
                Console.Write("\nAdicionar mais Despesas? (Tecle \"1\" para SIM ou \"0\" para NÂO): ");
                int resp = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                if (resp != 1)
                    break;
            }
        }
        public void Update(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            Despesa despesa = new();
            ICadastros.Cabecalho("Editar informações da Despesa");
            Console.Write("Informe o número da Despesa à ser modificada:\n");
            int i = 0;
            foreach (var desp in despesas)
            {
                Console.WriteLine($"[{i}]{desp.Nome}");
                i++;
            }
            int resp1 = Convert.ToInt16(Console.ReadLine());
            despesas.RemoveAt(resp1);
            Console.WriteLine("Agora informe os novos dados da Despesa: ");
            Console.Write("Nome: ");
            despesa.Nome = Console.ReadLine();
            despesas.Insert(resp1, despesa);
            Console.WriteLine("Dados atualizados com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\despesas_fixas.json", despesas);
        }
        public void Delete(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Remover Despesa");
            Console.Write("Informe o número da Despesa à ser removida:\n");
            int i = 0;
            foreach (var desp in despesas)
            {
                Console.WriteLine($"[{i}]{desp.Nome}");
                i++;
            }
            int resp1 = Convert.ToInt16(Console.ReadLine());
            despesas.RemoveAt(resp1);
            Console.WriteLine("Despesa removida com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\despesas_fixas.json", despesas);
        }
    }
}

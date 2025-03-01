using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_Micro_Empresa
{
    public class Funcionario : Pessoa, ICadastros
    {
        public Funcionario() { }
        public void Choice(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Cadastro de Funcionários");
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
                    SubMenus.Cadastros(fornec, despesas, funcio, socios);
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
                Funcionario funci = new();
                ICadastros.Cabecalho("Adicionar novo Funcionário");
                Console.WriteLine("Informe o nome do Funcionário: ");
                funci.Nome = Console.ReadLine();
                Console.WriteLine("Informe o cargo do Funcionário: ");
                funci.Cargo = Console.ReadLine();
                Console.WriteLine("Informe o salário do Funcionário: ");
                funci.Salario = Convert.ToDecimal(Console.ReadLine());
                funcio.Add(funci);
                Console.WriteLine("Funcionário(a) adicionado com sucesso!");
                ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\funcionarios.json", funcio);
                Console.Write("\nAdicionar mais Funcionários? (Tecle \"1\" para SIM ou \"0\" para NÂO): ");
                int resp = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                if (resp != 1)
                    break;
            }       
        }
        public void Update(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            Funcionario funci = new();
            ICadastros.Cabecalho("Editar informações do Funcionário");
            Console.Write("Informe o número do Funcionário à ser modificado: \n");
            int i = 0;
            foreach (var func in funcio)
            {
                Console.WriteLine($"[{i}]{func.Nome}\t{func.Cargo}\t{func.Salario}");
                i++;
            }
            int resp1 = Convert.ToInt16(Console.ReadLine());
            funcio.RemoveAt(resp1);
            Console.Write("Agora informe os novos dados do Funcionário:\n");
            Console.Write("Nome: ");
            funci.Nome = Console.ReadLine();
            Console.Write("Cargo: ");
            funci.Cargo = Console.ReadLine();
            Console.Write("Salário: ");
            funci.Salario = Convert.ToDecimal(Console.ReadLine());
            funcio.Insert(resp1, funci);
            Console.WriteLine("Dados atualizados com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\funcionarios.json", funcio);
        }
        public void Delete(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Remover Funcionário");
            Console.WriteLine("Informe o número do Funcionário à ser removido: ");
            int i = 0;
            foreach (var func in funcio)
            {
                Console.WriteLine($"[{i}]{func.Nome}\t{func.Cargo}\t{func.Salario}");
                i++;
            }
            int resp1 = Convert.ToInt16(Console.ReadLine());
            funcio.RemoveAt(resp1);
            Console.WriteLine("Funcionário(a) removido com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\funcionarios.json", funcio);
        }
    }
}

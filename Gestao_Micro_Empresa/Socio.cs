using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gestao_Micro_Empresa
{
    public class Socio : Pessoa, ICadastros
    {
        public Socio() { }
        private decimal horasTrabalhadas;
        public decimal HorasTrabalhadas 
        {
            get { return horasTrabalhadas; }
            set
            {
                if (value >= 0)
                {
                    horasTrabalhadas = value;
                }
                else
                {
                    Console.WriteLine("As horas não podem ser negativas!");
                }
            }
        }
        public void Choice(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Cadastro de Sócios");
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
                Socio socio = new();
                ICadastros.Cabecalho("Adicionar novo Sócio");
                Console.WriteLine("Informe o nome do Sócio: ");
                socio.Nome = Console.ReadLine();
                Console.WriteLine("Informe o cargo so Sócio: ");
                socio.Cargo = Console.ReadLine();
                socios.Add(socio);
                Console.WriteLine("\nSócio adicionado(a) com Sucesso!");
                ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\socios.json", socios);
                Console.Write("\nAdicionar mais Sócios? (Tecle \"1\" para SIM ou \"0\" para NÂO): ");
                int resp = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                if (resp != 1)
                    break;
            }
        }
        public void Update(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            Socio socio = new();
            ICadastros.Cabecalho("Editar informações do Sócio");
            int i = 0;
            Console.Write("Informe o Número do Sócio à ser modificado:\n");
            foreach (var soc in socios)
            {
                Console.WriteLine($"[{i}] {soc.Nome}\t{soc.Cargo}");
                i++;
            }
            int resp1 = Convert.ToInt16(Console.ReadLine());
            Console.Write("Agora informe os novos dados do Sócio: ");
            Console.Write("Nome: ");
            socio.Nome = Console.ReadLine();
            Console.Write("Cargo: ");
            socio.Cargo = Console.ReadLine();
            socios.Insert(resp1, socio);
            Console.WriteLine("Dados atualizados com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\socios.json", socios);
        }
        public void Delete(List<Fornecedor> fornec, List<Despesa> despesas,
                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Remover Sócio");
            Console.Write("Informe o número do Sócio à ser removido: ");
            int i = 0;
            foreach (var soc in socios)
            {
                Console.WriteLine($"[{i}]{soc.Nome}\t{soc.Cargo}");
                i++;
            }
            Console.Write("Informe o Número do Sócio à ser removido: ");
            int resp1 = Convert.ToInt16(Console.ReadLine());
            socios.RemoveAt(resp1);
            Console.WriteLine("Sócio removido(a) com sucesso!");
            Task.Delay(1500).Wait();
            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\socios.json", socios);
        }
    }
}


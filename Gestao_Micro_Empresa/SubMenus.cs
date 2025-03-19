using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_Micro_Empresa
{
    public class SubMenus
    {
        public SubMenus() { }
        public static string? NomeEmpresa { get; set; }
        public static void FechamentoDeMes(List<Fornecedor> fornec, List<Despesa> despesas,
                                           List<Funcionario> funcio, List<Socio> socios)
        {
            FechamentoMes.FecharMes(fornec, despesas, funcio, socios);
        }

        public static void GetDespesas(List<Despesa> despesas)
        {
            ICadastros.Cabecalho("Despesas");
            if (despesas.Count == 0)
            {
                Console.WriteLine("Ainda não há Despesas cadastradas!");
            }
            else
            {
                Console.WriteLine($"NOME");
                Console.WriteLine("----------------");
                foreach (var i in despesas)
                    Console.WriteLine(i.Nome);
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar..");
            Console.ReadKey();
        }

        public static void GetFornecedores(List<Fornecedor> fornecedores)
        {
            ICadastros.Cabecalho("Fornecedores");
            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Ainda não há Fornecedores cadastrados!");
            }
            else
            {
                Console.WriteLine($"NOME");
                Console.WriteLine("----------------");
                foreach (var i in fornecedores)
                    Console.WriteLine(i.Nome);
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar..");
            Console.ReadKey();
        }

        public static void GetFuncionarios(List<Funcionario> funcionarios)
        {
            ICadastros.Cabecalho("Funcionários");
            if (funcionarios.Count == 0)
            {
                Console.WriteLine("Ainda não há Funcionários cadastrados!");
            }
            else
            {
                Console.WriteLine("NOME\t\t\t\tCARGO");
                Console.WriteLine("-----------------------------------" +
                                  "------------");
                foreach (var i in funcionarios)
                {
                    Console.WriteLine($"{i.Nome}\t\t\t{i.Cargo}");
                }
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar..");
            Console.ReadKey();
        }

        public static void GetSocios(List<Socio?> socios)
        {
            ICadastros.Cabecalho("Sócios");
            if (socios.Count == 0)
            {
                Console.WriteLine("Ainda não há Sócios cadastrados!");
            }
            else
            {
                Console.WriteLine("NOME\t\t\t\tCARGO");
                Console.WriteLine("-----------------------------------" +
                                  "----------------");
                foreach (var i in socios)
                { 
                    Console.WriteLine($"{i.Nome}\t\t\t\t{i.Cargo}");
                }
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar..");
            Console.ReadKey();
        }

        public static async Task Cadastros(List<Fornecedor> fornec, List<Despesa> despesas,
                                     List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Cadastros");
            Console.WriteLine("[0]Minha Empresa");
            Console.WriteLine("[1]Fornecedores");
            Console.WriteLine("[2]Funcionários");
            Console.WriteLine("[3]Sócios");
            Console.WriteLine("[4]Despesas Fixas");
            Console.WriteLine("[5]Voltar");
            int resp = Convert.ToInt16(Console.ReadLine());
            switch (resp)
            {
                case 0:
                    Console.Clear();
                    if (string.IsNullOrEmpty(NomeEmpresa))
                    {
                        ICadastros.Cabecalho("Minha Empresa");
                        Console.Write("Informe o nome da Empresa: ");
                        NomeEmpresa = Console.ReadLine();
                        ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\minha_empresa.json", NomeEmpresa);
                        Console.WriteLine("Nome da empresa cadastrado com sucesso!");
                        Task.Delay(1500).Wait();
                    }
                    else
                    {
                        ICadastros.Cabecalho("Minha Empresa");
                        Console.WriteLine($"Você quer alterar o nome da empresa {NomeEmpresa}? (Tecle \"1\" para \"SIM\" ou \"0\" para \"NÃO\")");
                        int resposta = Convert.ToInt16(Console.ReadLine());
                        Console.Clear();

                        if (resposta == 1)
                        {
                            ICadastros.Cabecalho("Minha Empresa");
                            Console.Write("Informe o nome da Empresa: ");
                            NomeEmpresa = Console.ReadLine();
                            ICadastros.Serializacao(@"c:\Gerenciamento Financeiro\Cadastros\minha_empresa.json", NomeEmpresa);
                            Console.WriteLine("Nome da empresa alterado com sucesso!");
                            Task.Delay(1500).Wait();
                        }
                        else if (resposta == 0)
                            break;
                    }
                        break;
                case 1:
                    Console.Clear();
                    var fornecedor = new Fornecedor();
                    fornecedor.Choice(fornec, despesas, funcio, socios);
                    break;
                case 2:
                    Console.Clear();
                    var funcionario = new Funcionario();
                    funcionario.Choice(fornec, despesas, funcio, socios);
                    break;
                case 3:
                    Console.Clear();
                    var socio = new Socio();
                    socio.Choice(fornec, despesas, funcio, socios);
                    break;
                case 4:
                    Console.Clear();
                    var despesa = new Despesa();
                    despesa.Choice(fornec, despesas, funcio, socios);
                    break;
                case 5:
                    Console.Clear();
                    break;
            }
            Console.Clear();
        }
    }
}

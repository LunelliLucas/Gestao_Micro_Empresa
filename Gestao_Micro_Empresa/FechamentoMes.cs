using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Gestao_Micro_Empresa
{
    public class FechamentoMes
    {
        public FechamentoMes() { }
        protected static decimal Juros { get; set; }
        protected static decimal Receitas { get; set; }
        protected static decimal DespFixas { get; set; }
        protected static decimal DespesasAdicionais { get; set; }
        protected static decimal ReservaCaixa { get; set; }
        protected static decimal PagFuncio { get; set; }
        protected static decimal DespesasTotais { get; set; }
        protected static decimal SaldoLiquido { get; set; }
        protected static decimal HorasTotaisSocio { get; set; }
        protected static decimal ValorPorHora { get; set; }

        public static decimal AddReceitas(List<Fornecedor> fornec)
        {
            Console.WriteLine("Adicione o valor das Receitas à seguir: \n");
            Console.Write($"Informe o rendimento total de juros até {DateTime.Now.ToShortDateString()}: ");
            Juros = Convert.ToDecimal(Console.ReadLine());
            decimal totReceitas = 0;    
            if (fornec.Count != 0)
            {
                foreach (var item in fornec)
                {
                    item.Receita = 0;
                    Console.Write($"\nFaturamento {item.Nome}: ");
                    item.Receita = Convert.ToDecimal(Console.ReadLine());
                    totReceitas += item.Receita;
                }
                return totReceitas + Juros;
            }
            else
            {
                Console.WriteLine("\nVocê não possui Fornecedores Cadastrados!\n");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            return 0 + Juros;
        }
        public static decimal AddDespesasFixas(List<Despesa> despesas)
        {
            decimal totDespesas = 0;
            Console.WriteLine("Adicione o valor das Despesas Fixas à seguir: ");
            foreach (var item in despesas)
            {
                Console.Write($"{item.Nome}: ");
                item.Valor = Convert.ToDecimal(Console.ReadLine());
                totDespesas += item.Valor;
            }
            return totDespesas;
        }
        public static decimal AddDespesasAdicionais()
        {
            decimal totDespesas = 0;
            Console.WriteLine("\nAdicione o nome e o valor das Despesas Adicionais à seguir: ");
            while(true)
            {
                Console.Write("Adicionar mais despesas Adicionais? (Tecle \"1\" para SIM ou \"0\" para NÃO): ");
                int resp = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                ICadastros.Cabecalho("Despesas");
                if (resp == 0)
                    break;
                Console.Write("Nome: ");
                string? nome = Console.ReadLine();
                Console.Write("Valor: ");
                decimal valor = Convert.ToDecimal(Console.ReadLine());
                Despesa.despesasAdicionais.Add(new Despesa { Nome = nome, Valor = valor });
                totDespesas += valor;
            }
            return totDespesas;
        }
        public static decimal ValorReservaCaixa()
        {
            Console.Write("\nInforme o valor destinado ao caixa: ");
            decimal valor = Convert.ToDecimal(Console.ReadLine());
            return valor;
        }
        public static decimal PagFuncionarios(List<Funcionario> funcio)
        {
            decimal totPag = 0m;
            Console.WriteLine("\nPagamento funcionários:\n");
            Console.WriteLine("Informe o Salário dos Funcionários à seguir:");
            foreach (var item in funcio)
            {
                Console.Write($"\n{item.Nome}: ");
                item.Salario = Convert.ToDecimal(Console.ReadLine());
                totPag += item.Salario;
            }
            return totPag;
        }
        public static decimal HorasTotaisSocios(List<Socio> socios)
        {
            decimal horasTotais = 0;
            Console.WriteLine("\nInforme o total de horas trabalhadas por cada Sócio: ");
            foreach (var item in socios)
            {
                Console.WriteLine($"{item.Nome}: ");
                item.HorasTrabalhadas = Convert.ToDecimal(Console.ReadLine());
                horasTotais += item.HorasTrabalhadas;
            }
            return horasTotais;
        }
        public static void FecharMes(List<Fornecedor> fornec, List<Despesa> despesas,
                                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Receitas");
            //Entradas
            Receitas = FechamentoMes.AddReceitas(fornec);
            Console.Clear();
            ICadastros.Cabecalho("Despesas");
            //Saídas
            DespFixas = FechamentoMes.AddDespesasFixas(despesas);
            DespesasAdicionais = FechamentoMes.AddDespesasAdicionais();
            ReservaCaixa = FechamentoMes.ValorReservaCaixa();
            PagFuncio = FechamentoMes.PagFuncionarios(funcio);
            DespesasTotais = (DespFixas + DespesasAdicionais +
                                           ReservaCaixa + PagFuncio);
            SaldoLiquido = Receitas - DespesasTotais;
            HorasTotaisSocio = FechamentoMes.HorasTotaisSocios(socios);
            ValorPorHora = 0m;
            try
            {
                ValorPorHora = SaldoLiquido / HorasTotaisSocio;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Não Existe Divisão por Zero!!");
            }
            //Distribuição dos lucros entre os sócios
            foreach (var item in socios)
            {
                item.Salario = 0m;
                item.Salario = item.HorasTrabalhadas * ValorPorHora;
            }
            Console.Clear();
            
        }
        //Exibindo o resultado do Mês
        public static void ExibirResultado(List<Fornecedor> fornec, List<Despesa> despesas,
                                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Resultados do mês");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Receitas:\n");
            Console.WriteLine($"Rendimento de Juros até {DateTime.Now.ToShortDateString()}: {Juros:C4}");
            foreach (var item in fornec)
            {
                Console.WriteLine($"{item.Nome}: {item.Receita:C4}");
            }
            Console.WriteLine($"Total: {Receitas:C4}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Despesas Fixas:\n");
            foreach (var item in despesas)
            {
                Console.WriteLine($"{item.Nome}: {item.Valor:C4}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Despesas Adicionais:\n");
            foreach (var item in Despesa.despesasAdicionais)
            {
                Console.WriteLine($"{item.Nome}: {item.Valor:C4}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Reserva para o Caixa: \n{ReservaCaixa:C4}\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Pagamento Funcionários:\n");
            foreach (var item in funcio)
            {
                Console.WriteLine($"{item.Nome}: {item.Salario:C4}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Total de Despesas: \n{DespesasTotais:C4}\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Horas Totais por Sócio:\n");
            foreach (var item in socios)
            {
                Console.WriteLine($"{item.Nome}: {item.HorasTrabalhadas}h");
            }
            Console.WriteLine($"Total de {HorasTotaisSocio}h  ");
            Console.WriteLine("-----------------------------------");
            Console.Write($"Saldo Líquido: {SaldoLiquido:C4}  \n");
            Console.WriteLine("-----------------------------------");
            Console.Write($"Valor por Hora: {ValorPorHora:C4}\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Salário dos Sócios: \n");
            foreach (var item in socios)
            {
                Console.WriteLine($"{item.Nome}: {item.Salario:C4}");
            }
            Console.WriteLine("!!ATENÇÃO!! Confira todos os dados acima.\n" +
                "Caso houver algo incorreto, tecle \"1\" para corrigir.\n" +
                "Se estiver tudo certo, tecle \"2\" para salvar os dados\n");
            int resposta = Convert.ToInt16(Console.ReadLine());
            if (resposta == 1)
            {
                AlterarDados(fornec, despesas, funcio, socios);
            }
            else
            {
                SalvarDados(fornec, despesas, funcio, socios);
            }
        }
        //Alterando os dados incorretos quando solicitado
        public static void AlterarDados(List<Fornecedor> fornec, List<Despesa> despesas,
                                           List<Funcionario> funcio, List<Socio> socios)
        {
            ICadastros.Cabecalho("Alterar os Dados");
            Console.WriteLine("Informe em qual parte os dados estão incorretos: ");
            Console.WriteLine("[1]Receitas");
            Console.WriteLine("[2]Despesas Fixas");
            Console.WriteLine("[3]Despesas Adicionais");
            Console.WriteLine("[4]Reserva para o Caixa");
            Console.WriteLine("[5]Pagamento dos Funcionários");
            Console.WriteLine("[6]Horas dos Sócios");
            int resp = Convert.ToInt16(Console.ReadLine());

            switch (resp)
            {
                case 1:
                    Receitas = FechamentoMes.AddReceitas(fornec);
                    break;
                case 2:
                    DespFixas = FechamentoMes.AddDespesasFixas(despesas);
                    break;
                case 3:
                    DespesasAdicionais = FechamentoMes.AddDespesasAdicionais();
                    break;
                case 4:
                    ReservaCaixa = FechamentoMes.ValorReservaCaixa();
                    break;
                case 5:
                    PagFuncio = FechamentoMes.PagFuncionarios(funcio);
                    break;
                case 6:
                    HorasTotaisSocio = FechamentoMes.HorasTotaisSocios(socios);
                    break;
                default:
                    Console.WriteLine("Informe uma opção válida!");
                    AlterarDados(fornec, despesas, funcio, socios);
                    break;
            }
            DespesasTotais = (DespFixas + DespesasAdicionais +
                                          ReservaCaixa + PagFuncio);
            SaldoLiquido = Receitas - DespesasTotais;
            ValorPorHora = 0m;
            try
            {
                ValorPorHora = SaldoLiquido / HorasTotaisSocio;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Não Existe Divisão por Zero!!");
            }
            foreach (var item in socios)
            {
                item.Salario = 0m;
                item.Salario = item.HorasTrabalhadas * ValorPorHora;
            }
            ExibirResultado(fornec, despesas, funcio, socios);
        }
        //Salvando os dados obtidos em um arquivo de texto
        public static void SalvarDados(List<Fornecedor> fornec, List<Despesa> despesas,
                                           List<Funcionario> funcio, List<Socio> socios)
        {
            string caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"FATURAMENTO {SubMenus.NomeEmpresa}");
            string dataFormatada = DateTime.Now.ToString("dd-MM-yyyy");
            string arquivo = $@"Fechamento {dataFormatada}.txt";
            string complete = Path.Combine(caminho, arquivo);

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            using (StreamWriter sw = new StreamWriter(complete))
            {
                sw.WriteLine($"FECHAMENTO DO MÊS {DateTime.Now.ToShortDateString()}\n");
                sw.WriteLine("Receitas:\n");
                sw.WriteLine($"Rendimento de Juros até {DateTime.Now.ToShortDateString()}: {Juros:C4}");
                foreach (var item in fornec)
                {
                    sw.WriteLine($"{item.Nome}: {item.Receita:C4}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Despesas Fixas:\n");
                foreach (var item in despesas)
                {
                    sw.WriteLine($"{item.Nome}: {item.Valor:C4}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Despesas Adicionais:\n");
                foreach (var item in Despesa.despesasAdicionais)
                {
                    sw.WriteLine($"{item.Nome}: {item.Valor:C4}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine($"Reserva para o Caixa: \n{ReservaCaixa:C4}\n");
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Pagamento Funcionários:\n");
                foreach (var item in funcio)
                {
                    sw.WriteLine($"{item.Nome}: {item.Salario:C4}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine($"Total de Despesas: \n{DespesasTotais:C4}\n");
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Horas Totais por Sócio:\n");
                foreach (var item in socios)
                {
                    sw.WriteLine($"{item.Nome}: {item.HorasTrabalhadas}h");
                }
                sw.WriteLine($"Total de {HorasTotaisSocio}h  ");
                sw.WriteLine("-----------------------------------");
                sw.Write($"Saldo Líquido: {SaldoLiquido:C4}  \n");
                sw.WriteLine("-----------------------------------");
                sw.Write($"Valor por Hora: {ValorPorHora:C4}\n");
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Salário dos Sócios: \n");
                foreach (var item in socios)
                {
                    sw.WriteLine($"{item.Nome}: {item.Salario:C4}");
                }
            }
        }
    }
}

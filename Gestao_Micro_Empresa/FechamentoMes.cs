using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Gestao_Micro_Empresa
{
    public class FechamentoMes
    {
        public FechamentoMes() { }
        
        public static decimal AddReceitas(List<Fornecedor> fornec)
        {
            Console.WriteLine("Adicione o valor das Receitas à seguir: \n");
            Console.Write($"Informe o rendimento total de juros até {DateTime.Now.ToShortDateString()}: ");
            decimal juros = Convert.ToDecimal(Console.ReadLine());
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
                return totReceitas + juros;
            }
            else
            {
                Console.WriteLine("\nVocê não possui Fornecedores Cadastrados!\n");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            return 0 + juros;
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
            Console.WriteLine("\nPagamento funcionários:");
            foreach (var item in funcio)
            {
                Console.WriteLine($"{item.Nome}: {item.Salario:C2}");
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
            var receitas = FechamentoMes.AddReceitas(fornec);
            Console.Clear();
            ICadastros.Cabecalho("Despesas");
            //Saídas
            var despFixas = FechamentoMes.AddDespesasFixas(despesas);
            var despesasAdicionais = FechamentoMes.AddDespesasAdicionais();
            var reservaCaixa = FechamentoMes.ValorReservaCaixa();
            var pagFuncio = FechamentoMes.PagFuncionarios(funcio);
            var despesasTotais = (despFixas + despesasAdicionais +
                                           reservaCaixa + pagFuncio);
            var saldoLiquido = receitas - despesasTotais;
            var horasTotaisSocios = FechamentoMes.HorasTotaisSocios(socios);
            var valorPorHora = 0m;
            try
            {
                valorPorHora = saldoLiquido / horasTotaisSocios;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Não Existe Divisão por Zero!!");
            }
            //Distribuição dos lucros entre os sócios
            foreach (var item in socios)
            {
                item.Salario = 0m;
                item.Salario = item.HorasTrabalhadas * valorPorHora;
            }
            Console.Clear();
            //Exibindo o resultado do Mês
            ICadastros.Cabecalho("Resultados do mês");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Receitas:\n");
            foreach (var item in fornec)
            {
                Console.WriteLine($"{item.Nome}: {item.Receita:C2}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Despesas Fixas:\n");
            foreach (var item in despesas)
            {
                Console.WriteLine($"{item.Nome}: {item.Valor:C2}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Despesas Adicionais:\n");
            foreach (var item in Despesa.despesasAdicionais)
            {
                Console.WriteLine($"{item.Nome}: {item.Valor:C2}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Reserva para o Caixa: \n{reservaCaixa:C2}\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Pagamento Funcionários:\n");
            foreach (var item in funcio)
            {
                Console.WriteLine($"{item.Nome}: {item.Salario:C2}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Total de Despesas: \n{despesasTotais:C2}\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Horas Totais por Sócio:\n");
            foreach (var item in socios)
            {
                Console.WriteLine($"{item.Nome}: {item.HorasTrabalhadas}h");
            }
            Console.WriteLine($"Total de {horasTotaisSocios}h  ");
            Console.WriteLine("-----------------------------------");
            Console.Write($"Saldo Líquido: {saldoLiquido:C2}  \n");
            Console.WriteLine("-----------------------------------");
            Console.Write($"Valor por Hora: {valorPorHora:C2}\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Salário dos Sócios: \n");
            foreach (var item in socios)
            {
                Console.WriteLine($"{item.Nome}: {item.Salario:C2}");
            }
            Console.ReadKey();
            //
            //Salvando os dados obtidos em um arquivo de texto
            //
            string caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resultados do mês");
            string dataFormatada = DateTime.Now.ToString("dd-MM-yyyy");
            string arquivo = $@"Fechamento {dataFormatada}.txt";
            string complete = Path.Combine(caminho, arquivo);
            
            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);
            
            using (StreamWriter sw = new StreamWriter(complete))
            {
                sw.WriteLine($"FECHAMENTO DO MÊS {DateTime.Now.ToShortDateString()}\n");
                sw.WriteLine("Receitas:\n");
                foreach (var item in fornec)
                {
                    sw.WriteLine($"{item.Nome}: {item.Receita:C2}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Despesas Fixas:\n");
                foreach (var item in despesas)
                {
                    sw.WriteLine($"{item.Nome}: {item.Valor:C2}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Despesas Adicionais:\n");
                foreach (var item in Despesa.despesasAdicionais)
                {
                    sw.WriteLine($"{item.Nome}: {item.Valor:C2}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine($"Reserva para o Caixa: \n{reservaCaixa:C2}\n");
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Pagamento Funcionários:\n");
                foreach (var item in funcio)
                {
                    sw.WriteLine($"{item.Nome}: {item.Salario:C2}");
                }
                sw.WriteLine("-----------------------------------");
                sw.WriteLine($"Total de Despesas: \n{despesasTotais:C2}\n");
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Horas Totais por Sócio:\n");
                foreach (var item in socios)
                {
                    sw.WriteLine($"{item.Nome}: {item.HorasTrabalhadas}h");
                }
                sw.WriteLine($"Total de {horasTotaisSocios}h  ");
                sw.WriteLine("-----------------------------------");
                sw.Write($"Saldo Líquido: {saldoLiquido:C2}  \n");
                sw.WriteLine("-----------------------------------");
                sw.Write($"Valor por Hora: {valorPorHora:C2}\n");
                sw.WriteLine("-----------------------------------");
                sw.WriteLine("Salário dos Sócios: \n");
                foreach (var item in socios)
                {
                    sw.WriteLine($"{item.Nome}: {item.Salario:C2}");
                }
            }

        }
    }
}

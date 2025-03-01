using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_Micro_Empresa
{
    public class Pessoa
    {
        public Pessoa() { }
        public string? Nome { get; set; }
        public string? Cargo { get; set; }
        private decimal salario;
        public decimal Salario 
        { 
            get { return salario; }
            set
            {
                if (value >= 0)
                {
                    salario = value;
                }
                else
                {
                    Console.WriteLine("O salário não pode ser negativo!!");
                }
            }
        }
    }
}

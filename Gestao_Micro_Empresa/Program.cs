using Gestao_Micro_Empresa;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.IO;
using System.Text.Json;
using System.Threading.Channels;
//Deserializando os dados

string diretorio = @"c:\Gerenciamento Financeiro\Cadastros";
string dadosFuncionarios = @"c:\Gerenciamento Financeiro\Cadastros\funcionarios.json";
string dadosFornecedores = @"c:\Gerenciamento Financeiro\Cadastros\fornecedores.json";
string dadosSocios = @"c:\Gerenciamento Financeiro\Cadastros\socios.json";
string dadosDespesasFixas = @"c:\Gerenciamento Financeiro\Cadastros\despesas_fixas.json";
List<Funcionario> jsonFuncionarios = new();
List<Fornecedor> jsonFornecedores = new();
List<Socio> jsonSocios = new();
List<Despesa> jsonDespesas = new();

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

//Início do programa

while (true)
{
    int resp = 0;
    try
    {
        ICadastros.Cabecalho("Menu de Opções");
        Console.WriteLine("[1]Fechamento de mês");
        Console.WriteLine("[2]Fornecedores");
        Console.WriteLine("[3]Funcionários");
        Console.WriteLine("[4]Sócios");
        Console.WriteLine("[5]Despesas");
        Console.WriteLine("[6]Cadastros");
        Console.WriteLine("[7]Sair");
        resp = Convert.ToInt16(Console.ReadLine());
        switch (resp)
        {
            case 1: Console.Clear();
                SubMenus.FechamentoDeMes(jsonFornecedores, jsonDespesas, jsonFuncionarios, jsonSocios);
                break;
            case 2: Console.Clear(); 
                SubMenus.GetFornecedores(jsonFornecedores);
                break;
            case 3: Console.Clear(); 
                SubMenus.GetFuncionarios(jsonFuncionarios);
                break;
            case 4: Console.Clear(); 
                SubMenus.GetSocios(jsonSocios);
                break;
            case 5: Console.Clear();
                SubMenus.GetDespesas(jsonDespesas);
                break;
            case 6: Console.Clear();
                SubMenus.Cadastros(jsonFornecedores, jsonDespesas, jsonFuncionarios, jsonSocios);
                break;
            case 7:
                break;
        }
        if (resp == 7)
        {
            Console.WriteLine("Saindo..");
            await Task.Delay(1000);
            break;
        }
        Console.Clear();

    }
    catch (FormatException)
    {
        Console.WriteLine("Informe um número!");
        await Task.Delay(1000);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro inesperado: {ex.Message}");
        await Task.Delay(1000);
    }
    Console.Clear();
}




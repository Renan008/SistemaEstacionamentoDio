using System;
using System.Collections.Generic;

class Veiculo
{
    public string Placa { get; set; }
    public DateTime Entrada { get; set; }
    public TipoVeiculo Tipo { get; set; }
}

enum TipoVeiculo
{
    Carro,
    Moto,
    Caminhao
}

class Estacionamento
{
    private List<Veiculo> veiculosEstacionados;
    private const double TaxaCarro = 3.5;
    private const double TaxaMoto = 2.0;
    private const double TaxaCaminhao = 5.0;

    public Estacionamento()
    {
        veiculosEstacionados = new List<Veiculo>();
    }

    public void AdicionarVeiculo(string placa, TipoVeiculo tipo)
    {
        Veiculo novoVeiculo = new Veiculo
        {
            Placa = placa,
            Entrada = DateTime.Now,
            Tipo = tipo
        };

        veiculosEstacionados.Add(novoVeiculo);
        ImprimirMensagem($"Veículo {tipo} com placa {placa} adicionado ao estacionamento.");
    }

    public void RemoverVeiculo(string placa)
    {
        Veiculo veiculoRemovido = veiculosEstacionados.Find(v => v.Placa == placa);

        if (veiculoRemovido != null)
        {
            veiculosEstacionados.Remove(veiculoRemovido);

            double valorCobrado = CalcularValorCobrado(veiculoRemovido);

            ImprimirMensagem($"Veículo {veiculoRemovido.Tipo} com placa {placa} removido do estacionamento.");
            ImprimirMensagem($"Valor cobrado: R$ {valorCobrado}");
        }
        else
        {
            ImprimirMensagem($"Veículo com placa {placa} não encontrado no estacionamento.");
        }
    }

    private double CalcularValorCobrado(Veiculo veiculo)
    {
        TimeSpan tempoEstacionado = DateTime.Now - veiculo.Entrada;

        double valorCobrado = 0;
        switch (veiculo.Tipo)
        {
            case TipoVeiculo.Carro:
                valorCobrado = Math.Ceiling(tempoEstacionado.TotalHours) * TaxaCarro;
                break;
            case TipoVeiculo.Moto:
                valorCobrado = Math.Ceiling(tempoEstacionado.TotalHours) * TaxaMoto;
                break;
            case TipoVeiculo.Caminhao:
                valorCobrado = Math.Ceiling(tempoEstacionado.TotalHours) * TaxaCaminhao;
                break;
        }

        return valorCobrado;
    }

    public void ListarVeiculos()
    {
        if (veiculosEstacionados.Count == 0)
        {
            ImprimirMensagem("Nenhum veículo estacionado no momento.");
        }
        else
        {
            ImprimirMensagem("Veículos estacionados:");
            foreach (var veiculo in veiculosEstacionados)
            {
                ImprimirMensagem($"Placa: {veiculo.Placa} | Tipo: {veiculo.Tipo} | Entrada: {veiculo.Entrada.ToString("dd/MM/yyyy HH:mm:ss")}");
            }
        }
    }

    private void ImprimirMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }
}

class Program
{
    static void Main()
    {
        Estacionamento estacionamento = new Estacionamento();

        estacionamento.AdicionarVeiculo("ABC123", TipoVeiculo.Carro);
        estacionamento.AdicionarVeiculo("XYZ789", TipoVeiculo.Moto);
        estacionamento.ListarVeiculos();
        estacionamento.RemoverVeiculo("ABC123");

        Console.ReadLine();
    }
}

using AluraTunes.Dados;
using System.Linq;
using System;

namespace AluraTunes
{
    public static class ItemMaisVendido
    {
        /* capturar o item mais vendido */

        public static void Executar()
        {
            var context = new DatabaseEntities();
            
            var produtosMaisvendidos = from fx in context.Faixa
                        where fx.ItemNotaFiscal.Count > 0
                        let totalVendido = fx.ItemNotaFiscal.Sum(inf => inf.Quantidade * inf.PrecoUnitario)
                        orderby totalVendido descending
                        select new
                        {
                            fx.FaixaId,
                            fx.Nome,
                            Total = totalVendido
                        };

            var maisVendido = produtosMaisvendidos.FirstOrDefault();

            var clientesCompraramFaixaMaisVendida = from inf in context.ItemNotaFiscal
                         where inf.FaixaId == maisVendido.FaixaId
                         select new
                         {
                             Nome = inf.NotaFiscal.Cliente.PrimeiroNome
                         };

            Console.WriteLine($"a faixa mais vendida é: {maisVendido.FaixaId} | {maisVendido.Nome} | {maisVendido.Total}\n\n");
            foreach (var cliente in clientesCompraramFaixaMaisVendida)
            {
                Console.WriteLine($"O cliente {cliente.Nome} comprou a faixa {maisVendido.Nome}");
            }

            Console.ReadKey();
        }
    }
}

using AluraTunes.Dados;
using System.Linq;
using System;

namespace AluraTunes
{
    public static class Subconsulta
    {
        public static void Executar()      
        {
            var context = new DatabaseEntities();

            var queryMedia = context.NotaFiscal.Average(nota => nota.Total); /* esse cara é uma subconsulta - pois é utilizado em uma consulta maior. */
            var query = from nf in context.NotaFiscal
                        where nf.Total > queryMedia
                        orderby nf.Total ascending
                        select new
                        {
                            nf.NotaFiscalId,
                            nf.DataNotaFiscal,
                            Cliente = nf.Cliente.PrimeiroNome,
                            nf.Total
                        };

            foreach (var nf in query)
            {
                Console.WriteLine($"{nf.NotaFiscalId} | {nf.DataNotaFiscal.ToString("dd/MM/yyyy")} | {nf.Cliente} | {nf.Total}");
            }
            
            Console.WriteLine($"A média é: {queryMedia}");
            Console.ReadKey();

        }
    }
}

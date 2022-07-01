using AluraTunes.Dados;
using System;
using System.Linq;

namespace AluraTunes
{
    public static class Paginacao
    {
         static Paginacao() { }

        public static void Executar()
        {
            var context = new DatabaseEntities();
            var tamanhoPagina = 10;
            var qtdLinhas = context.NotaFiscal.Count();
            var qtdPaginas = qtdLinhas / tamanhoPagina;

            for (int pag = 1; pag <= qtdPaginas; pag++)
            {
                var skiper = (pag - 1) * tamanhoPagina;
                Console.WriteLine($"--Página {pag}----------------------------------------------------");
                ImprimirPagina(tamanhoPagina, context, skiper);
            }
            Console.ReadLine();
        }


        private static void ImprimirPagina(int tamanhoPagina, DatabaseEntities context, int skip)
        {
            var query = from nf in context.NotaFiscal
                        orderby nf.NotaFiscalId
                        select
                        new
                        {
                            nf.NotaFiscalId,
                            Cliente = nf.Cliente.PrimeiroNome,
                            nf.Total,
                            nf.Estado
                        };

            query = query.Skip(skip);
            query = query.Take(tamanhoPagina);

            foreach (var nf in query)
            {
                Console.WriteLine($"{nf.NotaFiscalId} | {nf.Cliente} | {nf.Total} | {nf.Estado}");
            }
        }
    }
}


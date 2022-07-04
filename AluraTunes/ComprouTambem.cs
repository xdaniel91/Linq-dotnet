using AluraTunes.Dados;
using System;
using System.Linq;

namespace AluraTunes
{
    public static class ComprouTambem
    {
        public static void Executar()
        {
            /*******************************************************************************************************************************
             *                                                                                                                             *  
             * objetivo: selecionar uma faixa que está sendo vendida, e recomendar outras faixas, a partir de compras feitas anteriormente * 
             *                                                                                                                             *
             * referência: https://cursos.alura.com.br/course/linq-c-sharp-parte-2/task/21522                                              *
             *                                                                                                                             *         
             ******************************************************************************************************************************/



            /* 1: obter uma faixa para comparar */
            /* 2: obter todas as faixas cujo id seja igual ao da faixa obtida. */

            /* 3: item comprado em itemNotasFiscal (orderItem) - 
             * self join em itemNotasFiscal (orderItem) - 
             * on - 
             * id nota fiscal do item comprado - 
             * seja igual id da nota fiscal do item q foi comprado por outra pessoa -
             * onde os ids selecionados tenha o item q está sendo comprado -
             * e o item q foi comprado por outra pessoa seja diferente do item q está sendo levado */


            var context = new DatabaseEntities();

            var faixaComprada = context.Faixa.FirstOrDefault(f => f.FaixaId == 1);

            var faixaIds = context.Faixa.Where(f => f.FaixaId == faixaComprada.FaixaId).Select(f => f.FaixaId);

            var faixasRecomendar = from comprado in context.ItemNotaFiscal
                                   join comprouTbm in context.ItemNotaFiscal
                                   on comprado.NotaFiscalId equals comprouTbm.NotaFiscalId
                                   where faixaIds.Contains(comprado.Faixa.FaixaId) && comprouTbm.Faixa.FaixaId != faixaComprada.FaixaId
                                   select comprouTbm;

            foreach (var item in faixasRecomendar)
            {
                Console.WriteLine($"{item.Faixa.Nome}");
            }

            Console.ReadKey();
        }
    }
}

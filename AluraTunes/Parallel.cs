using AluraTunes.Dados;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ZXing;

namespace AluraTunes
{
    public static class Parallel
    {
        /*********************************************************************/
        /* Objetivo: gerar um QR Code para item na tabela faixas do banco    */
        /* Utilizando AsParallel para fazer a consulta dos itens async       */
        /* Utilizando ForAll() para salvar os QR Codes async                 */
        /*********************************************************************/


        const string pictures = "Pictures";
        public static void Executar()
        {
            var context = new DatabaseEntities();

            var barcode = new BarcodeWriter();
            barcode.Format = BarcodeFormat.QR_CODE;
            barcode.Options = new ZXing.Common.EncodingOptions
            {
                Width = 100,
                Height = 100
            };

            if (!Directory.Exists(pictures))
                Directory.CreateDirectory(pictures);

            Stopwatch stopWatch = Stopwatch.StartNew();

            var query = context.Faixa.ToList();

            var result = query.AsParallel().Select(f =>
            new
            {
                Path = string.Format("{0}\\{1}.jpg", pictures, f.FaixaId),
                Imagem = barcode.Write($"aluratunes.com\\faixa\\{f.FaixaId}")
            });

            var count = result.Count();

            stopWatch.Stop();
            Console.WriteLine($"{count} itens consultados em {stopWatch.ElapsedMilliseconds} ms"); // 1303 ms / 1985 ms / 5389 ms / 4500 ms / 3333 ms -  parallel
                                                                                                   // 9631 ms / 6530 ms - sync

            Stopwatch stopWatch2 = Stopwatch.StartNew();

            result.ForAll(item => item.Imagem.Save(item.Path, ImageFormat.Jpeg));

            stopWatch2.Stop();
            Console.WriteLine($"{count} itens salvos em {stopWatch2.ElapsedMilliseconds} ms"); // 33940 ms / 19800 ms / 24000 ms / 18111 ms / 22508 ms  - parallel                                                                                   // 42307 ms / 54424 ms  - sync
        }
    }
}

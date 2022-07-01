using System;
using System.Linq;

namespace AluraTunes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Paginacao.Executar();
            //Subconsulta.Executar();
            //ItemMaisVendido.Executar();

            var meses = new[] {
                "Janeiro"
                , "Fevereiro"
                , "Março"
                , "Abril"
                , "Maio"
                , "Junho"
                , "Julho"
                , "Agosto"
                , "Setembro"
                , "Outubro"
                , "Novembro"
                , "Dezembro" };

            var segundoSemestre = from m in meses select m;

            segundoSemestre = segundoSemestre.Skip(6);

            foreach (var mes in segundoSemestre)
            {
                Console.WriteLine(mes);
            }

            Console.ReadKey();
        }
    }
}

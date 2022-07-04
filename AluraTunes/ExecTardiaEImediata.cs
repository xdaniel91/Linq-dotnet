using AluraTunes.Dados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AluraTunes
{
    public static class ExecTardiaEImediata
    {
        public static void Executar()
        {
            /********************************************************************************************************************************************************
             * quando o resultado da query é usado após uma modificação no datasource, o resultado será o atualizado                                                *
             * para isso podemos salvar o resultado da query em mémoria com ToList(), assim qualquer modificação no datasource não afeterá os dados salvos na lista.*
             * fonte: https://cursos.alura.com.br/course/linq-c-sharp-parte-2/task/21407                                                                                                                                     *
             *******************************************************************************************************************************************************/

            var mesAniversario = 1;
            var context = new DatabaseEntities();

            while (mesAniversario <= 12)
            {           
                var query = from func in context.Funcionario
                            where func.DataNascimento.Value.Month == mesAniversario
                            select func;
                
                mesAniversario++;

                foreach (var func in query)
                {
                    Console.WriteLine($"mês: {mesAniversario} ---- {func.DataNascimento.Value:dd/MM/yyyy} | {func.PrimeiroNome} {func.Sobrenome} | {func.DataAdmissao.Value:dd/MM/yyyy}");
                }
            }
            Console.ReadKey();
        }
    }
}

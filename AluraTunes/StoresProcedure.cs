using AluraTunes.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraTunes
{
    public static class StoresProcedure
    {
        public static void Executar()
        {
            var context = new DatabaseEntities();
            var clienteId = 17;

            var query = from vnd in context.ps_Vendas_Por_Cliente(clienteId) select vnd;
 
        }
    }
} 

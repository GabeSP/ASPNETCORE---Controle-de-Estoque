using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoSaoBento
{
        public class Paginacao<T> : List<T>
        {
            public int IndicePagina { get; private set; }
            public int TotalPaginas { get; private set; }

            public Paginacao(List<T> itens, int contador,
                int indicePagina, int tamanhoPagina)
            {
                IndicePagina = indicePagina;
                TotalPaginas = (int)Math.Ceiling(contador / (double)tamanhoPagina);

                AddRange(itens);
            }

            public bool TemPaginaAnterior
            {
                get { return (IndicePagina > 1); }
            }

            public bool TemPaginaProxima
            {
                get { return (IndicePagina < TotalPaginas); }
            }
            public static async Task<Paginacao<T>> CreateAsync(IQueryable<T> source, int indicePagina, int tamanhoPagina)
            {
                var contador = await source.CountAsync();
                var itens = await source.Skip((indicePagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
                return new Paginacao<T>(itens, contador, indicePagina, tamanhoPagina);
            }

        }
    }

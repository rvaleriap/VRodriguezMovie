using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class Estadistica
    {
        public static ML.Result Porcentaje()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CineContext context = new DL.CineContext())
                {
                    var query = context.Porcentajes.FromSqlRaw("PorcentajesGetAll").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Estadistica porcentaje = new ML.Estadistica();
                        porcentaje.IdPorcentaje = query.IdPorcentaje;
                        porcentaje.PorcentajeN = query.PorcentajeN.Value;
                        porcentaje.PorcentajeS = query.PorcentajeS.Value;
                        porcentaje.PorcentajeE = query.PorcentajeE.Value;
                        porcentaje.PorcentajeO = query.PorcentajeO.Value;

                        result.Object = porcentaje;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se puede mostrar la informacion";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = " No se puede realizar la operacion";
            }
            return result;
        }
    }
}

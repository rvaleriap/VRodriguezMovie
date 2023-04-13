using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CineContext context = new DL.CineContext())
                {
                    var query = context.Cines.FromSqlRaw("GetAllCine").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();
                            cine.IdCine = obj.IdCine;
                            cine.Latitud = obj.Latitud.ToString();
                            cine.Longitud = obj.Longitud.ToString();
                            cine.Direccion = obj.Direccion;
                            cine.Venta = obj.Venta.Value;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = obj.IdZona.Value;
                            cine.Zona.Nombre = obj.Nombre;
                            result.Objects.Add(cine);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pueden mostrar los datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CineContext context = new DL.CineContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Latitud}','{cine.Longitud}','{cine.Direccion}','{cine.Venta}','{cine.Zona.IdZona}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se logro insertar el registro";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
           
            return result;
        }
        public static ML.Result GetById(int? IdCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CineContext context = new DL.CineContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById '{IdCine}'").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.IdCine = query.IdCine;
                        cine.Latitud = query.Latitud.ToString();
                        cine.Longitud = query.Longitud.ToString();
                        cine.Direccion = query.Direccion;
                        cine.Venta = query.Venta.Value;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona.Value;
                        cine.Zona.Nombre = query.Nombre;

                        result.Object = cine;
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
                result.Message = "No se pueden mostrar los datos";
            }
            return result;
        }

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CineContext context = new DL.CineContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Longitud}','{cine.Latitud}','{cine.Direccion}','{cine.Venta}','{cine.Zona.IdZona}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se actualizo el cine";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "No se logro actualizar el cine";
            }
            return result;
        }
        public static ML.Result Delete(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CineContext context = new DL.CineContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineDelete {cine.IdCine}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se elimino el cine";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "No se elimino el cine";
            }
            return result;
        }

     

    }
}

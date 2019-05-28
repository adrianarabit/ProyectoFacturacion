﻿using facturacion.Model;
using facturacion.Data;
using System;
using System.Linq;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using facturacion.Classes;

namespace facturacion.Business
{
    public class Clientes : IClients
    {
        public Clientes()
        {

        }
                
        public void Buscarclientes(string busqueda)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Devuelve una colección de clientes con todos los clientes que existen en la base de datos.
        /// </summary>
        /// <returns>ICollection con todos los objetos Cliente de la base de datos.</returns>
        public ICollection<Cliente> ListarClientes()
        {
            using (FacturacionContext context = new FacturacionContext())
            {
                var logger = new Logger();
                context.Database.Log = l => logger.Log("Listar Clientes", 0, l.ToString());
                var clientes = context.Clientes.ToList();
                return clientes;
            }
        }

        
        /// <summary>
        /// Crea un nuevo cliente en la base de datos.
        /// </summary>
        /// <param name="cliente"> Objeto de tipo cliente que representa el cliente que va a ser creado. </param>
        public void NuevoCliente(Cliente cliente)
        {
            using (FacturacionContext context = new FacturacionContext())
            {
                var logger = new Logger();
                context.Database.Log = l => logger.Log("Nuevo Cliente", 0, l.ToString());
                try
                {
                    cliente.Creacion = DateTime.Now;
                    cliente.Modificacion = DateTime.Now;
                    
                    context.Clientes.Add(cliente);
                    
                    context.SaveChangesAsync().Wait();                    
                }
                catch (DbEntityValidationException ex)
                {
                    /*EJEMPLO DE GESTION DE ERRORES EF
                     * // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    var log = NLog.LogManager.GetCurrentClassLogger();
                    foreach (var e in ex.EntityValidationErrors)
                    {
                        log.Error(e.ValidationErrors.);
                    }
                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);*/
                                                                          
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    var log = NLog.LogManager.GetCurrentClassLogger();
                    foreach (var e in ex.EntityValidationErrors)
                    {
                        //log.Error(e.ValidationErrors.Select(e => e.ErrorMessage);
                    }
                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                    
                }
            }
        }

        public ICollection<TipoCliente> Tipos()
        {
            using (FacturacionContext context = new FacturacionContext())
            {
                var resultado = context.Tipoclientes.ToList();
                return resultado;
            }
        }
    }
}

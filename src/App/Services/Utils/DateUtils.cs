using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace App.Services.Utils
{
    public static class DateUtils
    {
        /// <summary>
        /// Ver lista de patrones:http://www.geekzilla.co.uk/View00FF7904-B510-468C-A2C8-F859AA20581F.htm
        /// </summary>
        public static string IsoDatePattern
        {
            get
            {

                return "yyyy-MM-dd'T'HH:mm:ssK";
            }
        }
        /// <summary>
        /// Returns the next date that matches the weekday
        /// </summary>
        /// <param name="day"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetNextDay(DayOfWeek day, DateTime date)
        {
            
            DateTime current = date;
            while (current.DayOfWeek != day)
            {
               current =  current.AddDays(1);
            }
            return current;
        }

        /// <summary>
        /// Retorna una fecha con su propiedad kind en utc
        /// no altera el valor de la fecha.
        /// debe usarse al obtener todas las fechas que se guardan como utc
        /// para que al serializarlas se incluya el identificador de utc
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DateTime SetUtcDateKind(DateTime d)
        {
            d = DateTime.SpecifyKind(d, DateTimeKind.Utc);
            return d;
        }
        /// <summary>
        /// Convierte de una fecha de java.
        /// (El valor corresponde a los milisegundos despues de epoch)
        /// </summary>
        /// <param name="javaTimeStamp"></param>
        /// <returns></returns>
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is millisecods past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Math.Round(javaTimeStamp / 1000)).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Crea una fecha en UTC a partir de un timestamp de unix
        /// OJO: No se convierte a local time como esta en la pregunta. Porque en el sistema las fechas estan siempre en utc
        /// http://stackoverflow.com/questions/249760/how-to-convert-unix-timestamp-to-datetime-and-vice-versa
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var date =  epoch.AddSeconds(unixTime);
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }

        /// <summary>
        /// Obtiene el timestamp de unix a partir de una fecha
        /// Se llama ToUniversalTime sobre la fecha, esto quiere decir que
        /// si no se especifica el Kind de la fecha se asume local y puede haber
        /// problemas
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double unix;

            /* Se convierte a universaltime primero, esto tiene implicaciones
             * Si la fecha no tiene Kind, se asume formato local
             * Esto tendría problemas si la fecha está en UTC pero su Kind no ha sido
             * asignado
             */ 
            unix = date.ToUniversalTime().Subtract(epoch).TotalSeconds;
            return unix;
        }
    }
}

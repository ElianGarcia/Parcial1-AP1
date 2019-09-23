using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1_AP1.Entidades
{
    public class Evaluaciones
    {
        [Key]
        public int IDEvaluacion { get; set; }
        public string  Estudiante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public decimal Logrado { get; set; }
        public decimal Perdido { get; set; }
        public string Pronostico { get; set; }

        public Evaluaciones()
        {
        }

        public Evaluaciones(int iDEvaluacion, string estudiante, DateTime fecha, decimal valor, decimal logrado, decimal perdido, string pronostico)
        {
            IDEvaluacion = iDEvaluacion;
            Estudiante = estudiante ?? throw new ArgumentNullException(nameof(estudiante));
            Fecha = fecha;
            Valor = valor;
            Logrado = logrado;
            Perdido = perdido;
            Pronostico = pronostico ?? throw new ArgumentNullException(nameof(pronostico));
        }
    }
}

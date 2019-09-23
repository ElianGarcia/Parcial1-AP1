using Parcial1_AP1.Entidades;
using System.Data.Entity;

namespace Register.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Evaluaciones> Evaluacion { get; set; }
        public Contexto() : base("ConStr")
        {
        }
    }
}

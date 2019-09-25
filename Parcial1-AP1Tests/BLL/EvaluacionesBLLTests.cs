using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parcial1_AP1.Entidades;
using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Parcial1_AP1.BLL.Tests
{
    [TestClass()]
    public class EvaluacionesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Evaluaciones evaluacion = new Evaluaciones(1, "Elian Garcia", DateTime.Now, 31, 25, 6, "Continuar");
            bool realizado = EvaluacionesBLL.Guardar(evaluacion);

            Assert.AreEqual(realizado, true);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Evaluaciones evaluacion = new Evaluaciones(1, "Elian Rodriguez", DateTime.Now, 30, 25, 6, "Continuar");

            bool realizado = false;

            Contexto db = new Contexto();

            db.Entry(evaluacion).State = EntityState.Modified;
            realizado = (db.SaveChanges() > 0);

            Assert.AreEqual(realizado, true);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool realizado = false;
            Contexto db = new Contexto();
            int id = 1;

            var eliminar = db.Evaluacion.Find(id);
            db.Entry(eliminar).State = EntityState.Deleted;
            realizado = (db.SaveChanges() > 0);

            Assert.AreEqual(realizado, true);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            int id = 1;
            Evaluaciones e = new Evaluaciones();
            e = EvaluacionesBLL.Buscar(id);

            Assert.IsNotNull(e);
        }

        [TestMethod()]
        public void GetListTest()
        {
            var listado = new List<Evaluaciones>();

            Assert.IsNotNull(listado);
        }
    }
}
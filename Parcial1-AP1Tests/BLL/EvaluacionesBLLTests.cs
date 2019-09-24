using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parcial1_AP1.BLL;
using Parcial1_AP1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Evaluaciones evaluacion = new Evaluaciones(1, "Elian Rodriguez", DateTime.Now, 31, 25, 6, "Continuar");
            bool realizado = EvaluacionesBLL.Modificar(evaluacion);

            Assert.AreEqual(realizado, true);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool realizado = EvaluacionesBLL.Eliminar(2);

            Assert.AreEqual(realizado, true);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Evaluaciones evaluacion = EvaluacionesBLL.Buscar(1);

            Assert.IsNotNull(evaluacion);
        }
    }
}
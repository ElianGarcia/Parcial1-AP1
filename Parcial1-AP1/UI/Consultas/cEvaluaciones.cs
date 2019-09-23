using Parcial1_AP1.BLL;
using Parcial1_AP1.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Parcial1_AP1.UI.Consultas
{
    public partial class cEvaluaciones : Form
    {
        public cEvaluaciones()
        {
            InitializeComponent();
        }

        private void BtConsulta_Click(object sender, EventArgs e)
        {
            var listado = new List<Evaluaciones>();

            if (tbCriterio.Text.Trim().Length > 0)
            {
                switch (cbFiltrar.SelectedIndex)
                {
                    case 0:
                        listado = EvaluacionesBLL.GetList(evaluacion => true);
                        break;

                    case 1:
                        int id = Convert.ToInt32(tbCriterio.Text);
                        listado = EvaluacionesBLL.GetList(evaluacion => evaluacion.IDEvaluacion == id);
                        break;

                    case 2:
                        listado = EvaluacionesBLL.GetList(evaluacion => evaluacion.Estudiante.Contains(tbCriterio.Text));
                        break;
                }

                listado = listado.Where(c => c.Fecha.Date >= DesdeDateTimePicker.Value.Date && c.Fecha.Date <= HastaDateTimePicker.Value.Date).ToList();
            }
            else
            {
                listado = EvaluacionesBLL.GetList(p => true);
            }

            ConsultaDataGridView.DataSource = null;
            ConsultaDataGridView.DataSource = listado;
        }
    }
}

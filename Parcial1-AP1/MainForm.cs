using Parcial1_AP1.UI.Consultas;
using Parcial1_AP1.UI.Registros;
using System;
using System.Windows.Forms;

namespace Parcial1_AP1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void EvaluacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rEvaluaciones evaluaciones = new rEvaluaciones();
            evaluaciones.MdiParent = this;
            evaluaciones.Show();
        }

        private void EvaluacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cEvaluaciones evaluaciones = new cEvaluaciones();
            evaluaciones.MdiParent = this;
            evaluaciones.Show();
        }
    }
}

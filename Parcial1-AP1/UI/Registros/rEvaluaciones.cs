using Parcial1_AP1.BLL;
using Parcial1_AP1.Entidades;
using System;
using System.Windows.Forms;

namespace Parcial1_AP1.UI.Registros
{
    public partial class rEvaluaciones : Form
    {
        public rEvaluaciones()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            IDEvaluacionesnumericUpDown.Value = 0;
            EstudiantetextBox.Text = "";
            FechadateTimePicker.Value = DateTime.Now;
            ValormaskedTextBox.Text = "";
            PerdidomaskedTextBox.Text = "";
            LogradomaskedTextBox.Text = "";
            PronosticocomboBox.SelectedItem = 0;
        }

        private Evaluaciones LlenaClase()
        {
            Evaluaciones evaluacion = new Evaluaciones();

            evaluacion.IDEvaluacion =  (int) IDEvaluacionesnumericUpDown.Value;
            evaluacion.Estudiante =  EstudiantetextBox.Text;
            evaluacion.Fecha = FechadateTimePicker.Value;
            evaluacion.Valor = Decimal.Parse(ValormaskedTextBox.Text);
            evaluacion.Perdido = Decimal.Parse(PerdidomaskedTextBox.Text);
            evaluacion.Logrado = Decimal.Parse(LogradomaskedTextBox.Text);
            evaluacion.Pronostico = PronosticocomboBox.SelectedItem.ToString();

            return evaluacion;
        }

        private void LlenaCampos(Evaluaciones evaluacion)
        {
            IDEvaluacionesnumericUpDown.Value = evaluacion.IDEvaluacion;
            EstudiantetextBox.Text = evaluacion.Estudiante;
            FechadateTimePicker.Value = evaluacion.Fecha;
            ValormaskedTextBox.Text = evaluacion.Valor.ToString();
            PerdidomaskedTextBox.Text = evaluacion.Perdido.ToString();
            LogradomaskedTextBox.Text = evaluacion.Logrado.ToString();
            PronosticocomboBox.SelectedItem = evaluacion.Pronostico;
        }

        private bool Existe()
        {
            Evaluaciones evaluacion = EvaluacionesBLL.Buscar((int)IDEvaluacionesnumericUpDown.Value);
            return (evaluacion != null);
        }

        private bool Validar()
        {
            bool realizado = true;

            if (string.IsNullOrWhiteSpace(EstudiantetextBox.Text))
            {
                errorProvider.SetError(EstudiantetextBox, "El campo Estudiante no debe estar vacio");
                EstudiantetextBox.Focus();
                realizado = false;
            }
            if (string.IsNullOrWhiteSpace(ValormaskedTextBox.Text))
            {
                errorProvider.SetError(ValormaskedTextBox, "El campo Valor no debe estar vacio");
                ValormaskedTextBox.Focus();
                realizado = false;
            }
            if (string.IsNullOrWhiteSpace(LogradomaskedTextBox.Text))
            {
                errorProvider.SetError(LogradomaskedTextBox, "El campo Logrado no debe estar vacio");
                LogradomaskedTextBox.Focus();
                realizado = false;
            }

            return realizado;
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Evaluaciones evaluacion = new Evaluaciones();
            bool realizado = false;

            if (!Validar())
                return;

            try
            {
                evaluacion = LlenaClase();

                if (!Existe())
                {
                    realizado = EvaluacionesBLL.Guardar(evaluacion);
                }
                else
                {
                    realizado = EvaluacionesBLL.Modificar(evaluacion);
                }

            } catch (Exception)
            {
                throw;
            }

            if (realizado)
            {
                MessageBox.Show("Guardado exitosamente!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show("Error al guardar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int Id;
            int.TryParse(IDEvaluacionesnumericUpDown.Value.ToString(), out Id);

            try
            {
                Evaluaciones evaluacion = EvaluacionesBLL.Buscar(Id);

                if (evaluacion != null)
                {
                    MessageBox.Show("Evaluacion encontrada", "Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenaCampos(evaluacion);
                }
                else
                    MessageBox.Show("Evaluacion no encontrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            int Id;
            bool realizado = false;
            int.TryParse(IDEvaluacionesnumericUpDown.Value.ToString(), out Id);

            try
            {
                if (EvaluacionesBLL.Eliminar(Id))
                {
                    MessageBox.Show("Eliminada Correctamente", "Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void ValormaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal perdido = 0;

            if(LogradomaskedTextBox.Text.Replace(".", "") != null && ValormaskedTextBox.Text.Replace(".", "") != null)
            {
                perdido = Decimal.Parse(ValormaskedTextBox.Text) - Decimal.Parse(LogradomaskedTextBox.Text);
            }

            PerdidomaskedTextBox.Text = perdido.ToString();

        }

        private void LogradomaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal perdido = 0;

            if (LogradomaskedTextBox.Text != null && ValormaskedTextBox.Text != null)
            {
                perdido = Decimal.Parse(ValormaskedTextBox.Text) - Decimal.Parse(LogradomaskedTextBox.Text);
            }

            PerdidomaskedTextBox.Text = perdido.ToString();
        }
    }
}

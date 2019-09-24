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
            ValortextBox.Text = "";
            PerdidotextBox.Text = "";
            LogradotextBox.Text = "";
            PronosticocomboBox.SelectedItem = 0;
        }

        private Evaluaciones LlenaClase()
        {
            Evaluaciones evaluacion = new Evaluaciones();

            evaluacion.IDEvaluacion =  (int) IDEvaluacionesnumericUpDown.Value;
            evaluacion.Estudiante =  EstudiantetextBox.Text;
            evaluacion.Fecha = FechadateTimePicker.Value;
            evaluacion.Valor = Decimal.Parse(ValortextBox.Text);
            evaluacion.Perdido = Decimal.Parse(PerdidotextBox.Text);
            evaluacion.Logrado = Decimal.Parse(LogradotextBox.Text);
            evaluacion.Pronostico = PronosticocomboBox.SelectedItem.ToString();

            return evaluacion;
        }

        private void LlenaCampos(Evaluaciones evaluacion)
        {
            IDEvaluacionesnumericUpDown.Value = evaluacion.IDEvaluacion;
            EstudiantetextBox.Text = evaluacion.Estudiante;
            FechadateTimePicker.Value = evaluacion.Fecha;
            ValortextBox.Text = evaluacion.Valor.ToString();
            PerdidotextBox.Text = evaluacion.Perdido.ToString();
            LogradotextBox.Text = evaluacion.Logrado.ToString();
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
            if (string.IsNullOrWhiteSpace(ValortextBox.Text))
            {
                errorProvider.SetError(ValortextBox, "El campo Valor no debe estar vacio");
                ValortextBox.Focus();
                realizado = false;
            }
            if (string.IsNullOrWhiteSpace(LogradotextBox.Text))
            {
                errorProvider.SetError(LogradotextBox, "El campo Logrado no debe estar vacio");
                LogradotextBox.Focus();
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

                if(IDEvaluacionesnumericUpDown.Value == 0)
                {
                    realizado = EvaluacionesBLL.Guardar(evaluacion);
                }
                else
                {
                    if (!Existe())
                    {
                        MessageBox.Show("No se puede modificar porque no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    realizado = EvaluacionesBLL.Modificar(evaluacion);
                }
                   

            } catch (Exception)
            {
                throw;
            }

            if (realizado)
            {
                LimpiarCampos();
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
            decimal valor = 0;
            decimal logrado = 0;

            if (!string.IsNullOrWhiteSpace(ValortextBox.Text))
            {
                valor = decimal.Parse(ValortextBox.Text);
            }
            if(!string.IsNullOrWhiteSpace(LogradotextBox.Text))
            {
                logrado = decimal.Parse(LogradotextBox.Text);
            }
            
            decimal perdido = valor - logrado;

            PerdidotextBox.Text = perdido.ToString();

            if(perdido >= 25 && perdido <= 30)
            {
                PronosticocomboBox.SelectedItem = 2;
            }
            if(perdido < 25)
            {
                PronosticocomboBox.SelectedItem = 1;
            }
            if(perdido > 30)
            {
                PronosticocomboBox.SelectedItem = 3;
            }
        }

        private void LogradotextBox_TextChanged(object sender, EventArgs e)
        {
            decimal valor = 0;
            decimal logrado = 0;

            if (!string.IsNullOrWhiteSpace(ValortextBox.Text))
            {
                valor = decimal.Parse(ValortextBox.Text);
            }
            if (!string.IsNullOrWhiteSpace(LogradotextBox.Text))
            {
                logrado = decimal.Parse(LogradotextBox.Text);
            }

            decimal perdido = valor - logrado;

            PerdidotextBox.Text = perdido.ToString();

            if (perdido >= 25 && perdido <= 30)
            {
                PronosticocomboBox.SelectedItem = 2;
            }
            if (perdido < 25)
            {
                PronosticocomboBox.SelectedItem = 1;
            }
            if (perdido > 30)
            {
                PronosticocomboBox.SelectedItem = 3;
            }
        }
    }
}

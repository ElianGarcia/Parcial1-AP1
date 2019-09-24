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
            PronosticocomboBox.SelectedIndex = 0;
        }

        private Evaluaciones LlenaClase()
        {
            Evaluaciones evaluacion = new Evaluaciones();

            evaluacion.IDEvaluacion =  (int) IDEvaluacionesnumericUpDown.Value;
            evaluacion.Estudiante =  EstudiantetextBox.Text;
            evaluacion.Fecha = FechadateTimePicker.Value;
            try
            {
                evaluacion.Valor = Convert.ToDecimal(ValortextBox.Text);
                evaluacion.Perdido = Convert.ToDecimal(PerdidotextBox.Text);
                evaluacion.Logrado = Convert.ToDecimal(LogradotextBox.Text);
            }
            catch (Exception)
            {
                errorProvider.SetError(LogradotextBox, "El campo debe ser numerico");
            }
            
            if(PronosticocomboBox.SelectedIndex == 0)
            {
                evaluacion.Pronostico = "Continuar";
            }
            if (PronosticocomboBox.SelectedIndex == 1)
            {
                evaluacion.Pronostico = "Suspenso";
            }
            if (PronosticocomboBox.SelectedIndex == 2)
            {
                evaluacion.Pronostico = "Retirar";
            }


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
            if (string.IsNullOrWhiteSpace(ValortextBox.Text) || Convert.ToDecimal(ValortextBox.Text) < 0)
            {
                errorProvider.SetError(ValortextBox, "El campo Valor no debe estar vacio \n El campo Logrado no debe ser menor que 0");
                ValortextBox.Focus();
                realizado = false;
            }
            if (string.IsNullOrWhiteSpace(LogradotextBox.Text) || Convert.ToDecimal(LogradotextBox.Text) < 0)
            {
                errorProvider.SetError(LogradotextBox, "El campo Logrado no debe estar vacio \n El campo Logrado no debe ser menor que 0");
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
                if(EvaluacionesBLL.Buscar(Id) != null)
                {
                    if (EvaluacionesBLL.Eliminar(Id))
                    {
                        MessageBox.Show("Eliminada Correctamente", "Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar porque no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                PronosticocomboBox.SelectedIndex = 1;
            }
            if(perdido < 25)
            {
                PronosticocomboBox.SelectedIndex = 0;
            }
            if(perdido > 30)
            {
                PronosticocomboBox.SelectedIndex = 2;
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
                PronosticocomboBox.SelectedIndex = 1;
            }
            if (perdido < 25)
            {
                PronosticocomboBox.SelectedIndex = 0;
            }
            if (perdido > 30)
            {
                PronosticocomboBox.SelectedIndex = 2;
            }
        }
    }
}

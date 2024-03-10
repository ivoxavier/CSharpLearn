using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win02.Modelo;

namespace Win02
{
    public partial class CadastroFuncionario : Form
    {
        public CadastroFuncionario()
        {
            InitializeComponent();
        }

        private void SalvarAction(object sender, EventArgs e)
        {
            //Mover os dados para a classe funcionario
            Funcionario funcionario = new Funcionario();
            funcionario.Nome = txtNome.Text;
            funcionario.Email = txtEmail.Text;
            funcionario.Salario = decimal.Parse(txtSalario.Text);
            funcionario.Sexo = (rbMasculino.Checked) ? "M" : "F";
            funcionario.TipoContrato = (rbPartime.Checked) ? "Part" : (rbFullTime.Checked) ? "Full" : "Ind";
            funcionario.DataRegisto = DateTime.Now;

            //Validar os dados
            //para guardar os erros que forem gerados pelo DataAnnotations
            List<ValidationResult> listErros = new List<ValidationResult>();

            ValidationContext contexto = new ValidationContext(funcionario);
            bool validado = Validator.TryValidateObject(funcionario, contexto, listErros, true);
            if (validado)
            {
                //Salvar os dados
                //Fechar e atualizar a tela princial
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach(ValidationResult erro in listErros)
                {
                    sb.Append(erro.ErrorMessage + '\n');
                }
                lblErro.Text = sb.ToString();
            }
        }
    }
}

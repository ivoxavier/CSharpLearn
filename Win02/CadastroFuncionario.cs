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
using Win02.DataBase;

namespace Win02
{
    public partial class CadastroFuncionario : Form
    {   
        //para acedecer aos metodos do Form TelaPrincipal
        private TelaPrincipal telaPrincipal;


        private Funcionario func;
        public CadastroFuncionario(TelaPrincipal tela)
        {
            //referencia a tela principal
            telaPrincipal = tela;
            InitializeComponent();
        }

        public CadastroFuncionario(TelaPrincipal tela, int Id)
        {
            telaPrincipal = tela;
        
            InitializeComponent();

            func = FucionarioDataAccess.PegarFuncionario(Id);
            FuncionarioParaTela(func);
        }

        private void FuncionarioParaTela(Funcionario funcionario)
        {

            txtNome.Text = funcionario.Nome.Trim();
            txtEmail.Text = funcionario.Email.Trim();
            txtSalario.Text = funcionario.Salario.ToString();
            if (funcionario.Sexo == "M") 
            {
                rbMasculino.Checked = true;
            }
            else
            {
                rbFeminino.Checked = true;
            }
            
            if(funcionario.TipoContrato == "Part")
            {
                rbPartime.Checked = true;
            } else if(funcionario.TipoContrato == "Full")
            {
                rbFullTime.Checked = true;
            }
            else
            {
                rbIndependente.Checked = true;
            }

            
         
            
        }
        private void SalvarAction(object sender, EventArgs e)
        {
            Funcionario funcionario = new Funcionario();
            if (func != null)
            {
                //Atualizar
                funcionario = func;
                funcionario.DataAtualizacao = DateTime.Now;
            }
            else
            {
                //novoCadastro
                funcionario = new Funcionario();
                funcionario.DataRegisto = DateTime.Now;
            }
            //Mover os dados para a classe funcionario
            funcionario.Nome = txtNome.Text.Trim();
            funcionario.Email = txtEmail.Text.Trim();
            funcionario.Salario = decimal.Parse(txtSalario.Text);
            funcionario.Sexo = (rbMasculino.Checked) ? "M" : "F";
            funcionario.TipoContrato = (rbPartime.Checked) ? "Part" : (rbFullTime.Checked) ? "Full" : "Ind";
            

            //Validar os dados
            //para guardar os erros que forem gerados pelo DataAnnotations
            List<ValidationResult> listErros = new List<ValidationResult>();

            ValidationContext contexto = new ValidationContext(funcionario);
            bool validado = Validator.TryValidateObject(funcionario, contexto, listErros, true);
            if (validado)
            {
                bool resultado;
                if (func != null)
                {
    
                    resultado = FucionarioDataAccess.AtualizarFuncionario(funcionario);
                }
                else
                {
                    resultado = FucionarioDataAccess.SalvarFuncionario(funcionario);
                }

                //nao preciso instanciar, porque o metodo e static
               if (resultado)
                {
                    //ok e fecha o formulario CadastroFuncionario
                    telaPrincipal.AtualizarTabela();
                    this.Close();
                }
                else
                {
                    lblErro.Text = "Erro na insercao na BD";
                }
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

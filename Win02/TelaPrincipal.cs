using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win02
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            AtualizarTabela();
        }

        public void AtualizarTabela()
        {
            dgvTabelaFuncionario.DataSource = DataBase.FucionarioDataAccess.PegarFuncionario();
        }

        private void NovoAction(object sender, EventArgs e)
        {
            //permite o Form Cadastro Acessar o metodos deste Form Tela
            new CadastroFuncionario(this).Show();
        }

        private void EditarAction(object sender, EventArgs e)
        {
            //obtem o id, da unica linha selecionada e da coluna com index 0 (a primeira)
            int id = (int) dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            new CadastroFuncionario(this,id).Show();
        }

        private void ExcluirAction(object sender, EventArgs e)
        {
            int id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            Win02.DataBase.FucionarioDataAccess.ExcluirFuncionario(id);
            AtualizarTabela();
        }
    }
}

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
            dgvTabelaFuncionario.DataSource = DataBase.FucionarioDataAccess.PegarFuncionario();
        }

        private void NovoAction(object sender, EventArgs e)
        {
            new CadastroFuncionario().Show();
        }
    }
}

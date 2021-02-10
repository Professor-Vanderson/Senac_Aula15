using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Projeto_Aula
{
  
    public partial class frmAula15 : Form
    {

        public frmAula15()
        {
            InitializeComponent();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection objcon = Conexao.Conectar();

                //Gravar as informações digitadas
                SqlCommand sqlComd = new SqlCommand(@"INSERT INTO Contatos(Nome,Endereco,Telefone) "+
                                                    "VALUES(@nome,@endereco,@telefone)", objcon);
                sqlComd.Parameters.AddWithValue("@nome",txtNome.Text);
                sqlComd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                sqlComd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                //Executa o comando
                sqlComd.ExecuteNonQuery();
                MessageBox.Show("Informações Salvas!");
                Limpar();
                Exibir();

                Conexao.fecharConexao();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Exibir()
        {
            try
            {
                SqlConnection objcon = Conexao.Conectar();
                //Objeto excuta somente Leitura 
                SqlCommand cmd = new SqlCommand("Select * from Contatos", objcon);
                SqlDataReader dr = cmd.ExecuteReader();

                //Carrega o ListView
                while (dr.Read())
                {
                    lstDados.Items.Add(dr[0] + " - " + dr[1] + " - " + dr[2] + " - " + dr[3]);
                }
                dr.Close();

                //Carregando o DataGridView
                // cria o dataadapter...
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;

                // preenche o dataset...
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                dtGrid.DataSource = dataSet;
                dtGrid.DataMember = dataSet.Tables[0].TableName;


                Conexao.fecharConexao();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        
        private void Limpar()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            txtNome.Focus();
        }

        private void frmAula15_Load(object sender, EventArgs e)
        {
            Exibir();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show("Deseja realmente sair?",
                "Projeto",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            //O usuário digita o número do id
            //string resposta = Interaction.InputBox("Informe o ID para Excluir");

            // O usuário seleciona dentro do DataGridView
            string resposta = Convert.ToString(this.dtGrid.CurrentRow.Cells["id"].Value);
            if (resposta != string.Empty)
            {
                SqlConnection objCon = Conexao.Conectar();
                SqlCommand Comd = new SqlCommand("delete from contatos where id='" + resposta + "';",objCon);
                Comd.ExecuteNonQuery();
                MessageBox.Show("Registro apagado com sucesso!");
                Conexao.fecharConexao();
                Exibir();
                
            }
        }

        private void dtGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Limpar();
            this.txtNome.Text = Convert.ToString(this.dtGrid.CurrentRow.Cells["nome"].Value);
            this.txtEndereco.Text = Convert.ToString(this.dtGrid.CurrentRow.Cells["endereco"].Value);
            this.txtTelefone.Text = Convert.ToString(this.dtGrid.CurrentRow.Cells["telefone"].Value);
            
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string campoID = Convert.ToString(this.dtGrid.CurrentRow.Cells["id"].Value);
            if (campoID != string.Empty)
            {
                SqlConnection objCon = Conexao.Conectar();
                SqlCommand Comd = new SqlCommand("update contatos set nome='" + txtNome.Text + "',endereco='" + txtEndereco.Text + "',telefone='" + txtTelefone.Text + "' where id=" + campoID + ";",objCon);
                Comd.ExecuteNonQuery();
                MessageBox.Show("Registro atualizado com sucesso!");
                Limpar();
                Exibir();
            }
        }
    }

    class Conexao
    {
        //Atributos do objeto conexão
        private static SqlConnection sqlCon = null;
        private static SqlCommand sqlComd;
        private static SqlDataReader dr;
        private static string conectionString = "Data Source=.\\SQLEXPRESS;" +
                "Initial Catalog=Aula15;" +
                "User ID=sa;" +
                "Password=1425";
        //Ações (métodos)
        public static SqlConnection Conectar()
        {
            sqlCon = new SqlConnection(conectionString);
            try
            {
                sqlCon.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return sqlCon;
        }

        public static void fecharConexao()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
            }
        }
    }
}

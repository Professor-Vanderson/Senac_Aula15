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
            //Abrir o banco de dados 
            Conectar();

            //inserir as informações do form
           
            
            //fechar o banco de dados

            //exibir uma mensagem(ok)
        }
        public void Conectar()
        {
            try
            {
                string conectionString = "Data Source=.\\SQLEXPRESS;" +
                    "Initial Catalog= Aula15;" +
                    "User ID=sa;" +
                    "Password=1425";
                SqlConnection sqlConect = new SqlConnection(conectionString);
                sqlConect.Open();
              
                //inserir informações
                SqlCommand com = new SqlCommand(@"insert into Contatos(Nome,Endereco,Telefone) " +
        "Values(@nome,@endereco,@telefone)", sqlConect);

                //com.CommandText = @"insert into Contatos(Nome,Endereco,Telefone) " +
        //"Values(@nome,@endereco,@telefone)";

                com.Parameters.AddWithValue("@nome", txtNome.Text);
                com.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                com.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                com.ExecuteNonQuery();
                MessageBox.Show("Dados Salvos com sucesso!");


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

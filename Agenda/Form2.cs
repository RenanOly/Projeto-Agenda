using Agenda;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Agenda
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Arquivo JPG (*.JPG)|*.jpg|Arquivo PNG(*.png)|*.png";
            ofd.InitialDirectory = "c:\\";
            ofd.Multiselect = false;
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string telefone = long.Parse(textBox2.Text).ToString(@"(00) 00000-0000");
                if (textBox2.Text.Count() >= 10 && textBox2.Text.Count() <= 11)
                {
                    string operadora = textBox3.Text.ToLower();
                    if (operadora == "claro" || operadora == "oi" || operadora == "tim" || operadora == "vivo")
                    {
                        string caminho = ("C:/Users/Renan/source/repos/Agenda/Agenda/bin/Debug/" + textBox2.Text + ".png");
                        StreamWriter arq = new StreamWriter("dados.txt", true);
                        ((Bitmap)pictureBox1.Image).Save(caminho);
                        arq.WriteLine(textBox1.Text + "$" + telefone + "$" + textBox3.Text + "$" + caminho);
                        arq.Close();

                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Digite uma operadora válida");
                    }
                }
                else
                {
                    MessageBox.Show("Numero de telefone inválido!");
                }
            }
            catch
            {
                MessageBox.Show("Telefone inválido, digite novamente");
            }
            
            

        }

        private void Form2Close(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text.Count() != 0 || textBox2.Text.Count() != 0 || textBox3.Text.Count() != 0)
            {
                if (DialogResult.Yes != MessageBox.Show("Tem certeza que deseja fechar a janela?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    //Cancelar o evento
                    e.Cancel = true;
                }
            }
        }
    }
}

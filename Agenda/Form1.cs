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
    public partial class Form1 : Form
    {
        public List<string> cadastros = new List<string>();
        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("dados.txt"))
            {
                System.IO.StreamWriter txt = new System.IO.StreamWriter("dados.txt");
                txt.Close();
            }
            func();
            


                
        }

        public void func()
        {
            System.IO.StreamReader arq = new System.IO.StreamReader("dados.txt", true);

            string texto;
            cadastros.Clear();
            while ((texto = arq.ReadLine()) != null)
            {
                
                cadastros.Add(texto);
                string[] array = texto.Split('$');
                Image image = Image.FromFile(array[3]);
                dataGridView1.Rows.Add(image, array[0], array[1], array[2]);
                dataGridView1.Sort(dataGridView1.Columns[1],ListSortDirection.Ascending);
            }
            arq.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog().ToString();
            dataGridView1.Rows.Clear();
            func();

        }

        private void button2_Click(object sender, EventArgs e)
        {   
            if (dataGridView1.Rows.Count > 1)
            {
                if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja apagar o contato?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    int Linha = dataGridView1.CurrentRow.Index;
                    dataGridView1.Rows.RemoveAt(Linha);
                    cadastros.RemoveAt(Linha);
                    System.IO.StreamWriter arq = new System.IO.StreamWriter("dados.txt");
                    for (int i = 0; i < cadastros.Count(); i++)
                    {
                        arq.WriteLine(cadastros[i]);
                    }
                    arq.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists("dados.txt"))
            {
                File.Delete("dados.txt");
            }

            System.IO.StreamWriter arq = new System.IO.StreamWriter("dados.txt",true);

            for (int i = 0; i < cadastros.Count(); i++)
            {
                string nome = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string telefone= dataGridView1.Rows[i].Cells[2].Value.ToString();
                string operadora= dataGridView1.Rows[i].Cells[3].Value.ToString();
                string[] dados= cadastros[i].Split('$');
                arq.WriteLine(nome + '$' + telefone + '$' + operadora + '$' + dados[3]);
            }
         
            arq.Close();
            dataGridView1.Rows.Clear();
            func();

        }
    }
}

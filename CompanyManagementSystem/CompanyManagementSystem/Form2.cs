using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManagementSystem
{
    public partial class Form2 : Form
    {
        CompanyDatabaseEntities context = new CompanyDatabaseEntities();
        BindingList<Employee> employees = new BindingList<Employee>();
        public Form2()
        {
            InitializeComponent();
            context.Employees.Load();

            dataGridView1.DataSource = context.Employees.Local;
            foreach (var item in context.Employees.Local)
            {
                employees.Add(item);
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDatabaseDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.companyDatabaseDataSet.Employee);

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv"; 
            sfd.DefaultExt = "csv"; 
            sfd.AddExtension = true;
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var s in employees)
                {
                    sw.Write(s.Id);
                    sw.Write(";");
                    sw.Write(s.FirstName);
                    sw.Write(";");
                    sw.Write(s.LastName);
                    sw.Write(";");
                    sw.Write(s.Gender);
                    sw.Write(";");
                    sw.Write(s.Language);
                    sw.Write(";");
                    sw.Write(s.PhoneNumber);
                    sw.Write(";");
                    sw.Write(s.Email);
                    sw.Write(";");
                    sw.Write(s.LoginName);
                    sw.Write("\n");
                }
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            ofd.DefaultExt = "csv";
            ofd.AddExtension = true;
            employees.Clear();

            if (ofd.ShowDialog() != DialogResult.OK) return;
            using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default))
            {
                
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split(';');

                    Employee emp = new Employee();

                    emp.Id = int.Parse(sor[0]);
                    emp.FirstName = sor[1];
                    emp.LastName = sor[2];
                    emp.Gender = sor[3];
                    emp.Language = sor[4];
                    emp.PhoneNumber = sor[5];
                    emp.Email = sor[6];
                    emp.LoginName = sor[7];

                    employees.Add(emp);
                    dataGridView1.DataSource = employees;
                    dataGridView1.Refresh();
                }
                
            }
            
        }
    }
}

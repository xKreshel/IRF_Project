using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManagementSystem
{
    public partial class Form1 : Form
    {
        CompanyDatabaseEntities context = new CompanyDatabaseEntities();
        public Form1()
        {
            InitializeComponent();
            context.Employees.Load();

            employeeBindingSource.DataSource = context.Employees.Local;

            
            FillDataSource();
            listBox1.DisplayMember = "FirstName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDatabaseDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.companyDatabaseDataSet.Employee);

        }

        private void FillDataSource()
        {
            listBox1.DataSource = (from i in context.Employees
                                   where i.FirstName.Contains(textBox1.Text)
                                   select i).ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FillDataSource();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Employee employee = (Employee)listBox1.SelectedItem;

            var lessons = from l in context.Employees
                          where l.FirstName == employee.FirstName
                          select new
                          {
                             Id = l.Id,
                             LastName = l.LastName,
                             Gender = l.Gender,
                             Language = l.Language,
                             PhoneNumber = l.PhoneNumber,
                             Email = l.Email,
                             LoginName = l.LoginName,
                             FirstName = l.FirstName
                         };
            
            dataGridView1.DataSource = lessons.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}

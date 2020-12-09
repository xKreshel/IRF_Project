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
    public partial class Form2 : Form
    {
        CompanyDatabaseEntities context = new CompanyDatabaseEntities();
        public Form2()
        {
            InitializeComponent();
            context.Employees.Load();

            dataGridView1.DataSource = context.Employees.Local;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDatabaseDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.companyDatabaseDataSet.Employee);

        }
    }
}

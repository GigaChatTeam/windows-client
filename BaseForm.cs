using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GigaChat
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        private void EXTRAEXIT_Click(object sender, EventArgs e)
        {
            // Закрытие всех открытых форм
            foreach (Form form in Application.OpenForms)
            {
                form.Close();
            }

            // Завершение приложения
            Application.Exit();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}

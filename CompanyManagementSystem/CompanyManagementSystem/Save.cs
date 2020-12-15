using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManagementSystem
{
    //Credit: Nishan Chathuranga Wickramarathna for Screen Capturing -> https://medium.com/@nishancw/c-screenshot-utility-to-capture-a-portion-of-the-screen-489ddceeee49
    public partial class Save : Form
    {
        readonly Bitmap bmp;
        public Save(Int32 x, Int32 y, Int32 w, Int32 h, Size s)
        {
            InitializeComponent();
            Rectangle rect = new Rectangle(x, y, w, h);
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);
            bmp.Save(@"E:\screen.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            pbCapture.Image = bmp;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                CheckPathExists = true,
                FileName = "Capture",
                Filter = "PNG Image(*.png)|*.png|JPG Image(*.jpg)|*.jpg|BMP Image(*.bmp)|*.bmp"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pbCapture.Image.Save(sfd.FileName);
            }
        }
    }
}

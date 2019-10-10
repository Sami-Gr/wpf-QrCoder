using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using System.Diagnostics;

namespace QR_Preview
{ 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form = this;
            MaterialSkin.MaterialSkinManager manager = MaterialSkin.MaterialSkinManager.Instance;
            manager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Purple900, MaterialSkin.Primary.Purple900, MaterialSkin.Primary.Purple900, MaterialSkin.Accent.Blue200, MaterialSkin.TextShade.WHITE);
        }

        //MainWindow main = new MainWindow();
        

        public string iconname;
        public static Form1 Form;
        

        public  void RenderQrCode2(string a, string b, Bitmap c, Color c1, Color c2)
        {
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(b == "L" ? 0 : b == "M" ? 1 : b == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(a, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Form.pictureBox1.BackgroundImage = qrCode.GetGraphic(20, c1, c2,c, 15,6,true);
                        Form.pictureBox1.Size = new System.Drawing.Size(Form.pictureBox1.Width, Form.pictureBox1.Height);
                        Form.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                        Form.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPeg Image|*.jpg|Gif Image|*.gif";
            saveFileDialog1.Title = "Save Qr Code";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.

                    ImageFormat imageFormat = null;
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case 2:
                            imageFormat = ImageFormat.Png;
                            break;
                        case 3:
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case 4:
                            imageFormat = ImageFormat.Gif;
                            break;
                        default:
                            throw new NotSupportedException("File extension is not supported");
                    }

                    pictureBox1.BackgroundImage.Save(fs, imageFormat);
                    fs.Close();
                }
            }
        }
    }
}

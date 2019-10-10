using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using QRCoder;
using System.Diagnostics;
using Brushes = System.Windows.Media.Brushes;

namespace Qr_Code__1001Tech_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial   class MainWindow : Window
    {
        QR_Preview.Form1 form = new QR_Preview.Form1();

        public static MainWindow main;
        public string iconname="";
        public string inputt = "Your text/URL here...";
        public string level = "L";
        System.Windows.Media.Color qrcolor = System.Windows.Media.Color.FromRgb(0,0,0);
        System.Windows.Media.Color qrbcolor = System.Windows.Media.Color.FromRgb(255, 255, 255);
        public Bitmap img;

        public MainWindow()
        {
            InitializeComponent();
            main = this;
            foregroundpick.ToolTip = foregroundpick.Background.ToString();
            backgroundpick.ToolTip = backgroundpick.Background.ToString();
            ecclevelbox.ToolTip = ecclevelbox.SelectedValue.ToString();
            lng.ToolTip = lng.SelectedValue.ToString();
        }

        public void foregroundpick_Click(object sender, RoutedEventArgs e)
        {
            ColorPickerWPF.ColorPickerWindow.ShowDialog(out qrcolor);
            foregroundpick.Background = new SolidColorBrush(qrcolor);
            var qcolor = System.Drawing.Color.FromArgb(qrcolor.A, qrcolor.R, qrcolor.G, qrcolor.B);
            foregroundpick.ToolTip = foregroundpick.Background.ToString();
        }

        private void backgroundpick_Click(object sender, RoutedEventArgs e)
        {
            ColorPickerWPF.ColorPickerWindow.ShowDialog(out qrbcolor);
            backgroundpick.Background = new SolidColorBrush(qrbcolor);
            var qbcolor = System.Drawing.Color.FromArgb(qrbcolor.A, qrbcolor.R, qrbcolor.G, qrbcolor.B);
            backgroundpick.ToolTip = backgroundpick.Background.ToString();
        }

        private void addicn_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Title = "Select icon";
            openFileDlg.Multiselect = false;
            openFileDlg.CheckFileExists = true;
            bool? result = openFileDlg.ShowDialog();
            if (result == true)
            {
                iconname = openFileDlg.FileName;
                iconpath.Content = iconname;
                updatepic(iconname);
            }
            else
            {
                iconname = "";
                updatepic("");
            }
            
        }

        
        public  void RenderQrCode()
        {
            try
            {
                inputt = main.input.Text;
                level = ecclevelbox.SelectedValue.ToString();
              
                GetIconBitmap();

                var qcolor = System.Drawing.Color.FromArgb(qrcolor.A, qrcolor.R, qrcolor.G, qrcolor.B);
                var qbcolor = System.Drawing.Color.FromArgb(qrbcolor.A, qrbcolor.R, qrbcolor.G, qrbcolor.B);
                

                form.RenderQrCode2(inputt, level, img, qcolor, qbcolor);
            }
            catch (NullReferenceException) { };
        }

        public Bitmap GetIconBitmap()
        {
            Bitmap img = null;
            if (iconname.Length > 0)
            {
                try
                {
                    img = new Bitmap(iconname);
                    updatepic(iconname);
                }
                catch (Exception)
                {
                    img = null;
                    updatepic("");
                }
            }
            if (iconname.Length == 0) { updatepic(""); img = null; };
            
            return img;            
        }        

        private void updatepic(string path) 
        {
            if (path == "") { img = null; iconpath.Content = ">Icon:\\..."; }
            else { img = new Bitmap(path); }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
           
        }

        
        private void ecclevelbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            level = ecclevelbox.SelectedValue.ToString();
            ecclevelbox.ToolTip = ecclevelbox.SelectedValue.ToString();
            RenderQrCode();
        }

        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            RenderQrCode();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenderQrCode();
            form.ShowDialog();
        }

        private void lng_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lng.SelectedValue.ToString() == "English")
             {
                    main.FlowDirection = FlowDirection.LeftToRight;
                contentlabel.Content = "Content:";
                sttlbl.Content = "Settings";
                ecclabel.Content = "ECC Level";
                foregroundpick.Content = "Qr Code color";
                backgroundpick.Content = "Qr Code Background color";
                addicn.Content = "Add logo/image";
                lnglbl.Content = "Display Language:";
                 generate.Content = "Generate";
                    grp.Content = "1001Tech FB Group";
                    t1.Content = "ECC (Error Correction Capability) level?";
                    t2.Content = "This compensates for dirt, damage or fuzziness of the code. ";
                    t3.Content= "Valid values are 'L' (low ECC), 'M', 'Q', 'H' (highest ECC). ";
                    t4.Content = "A high ECC level adds more redundancy at the cost of using more space. ";
                    t5.Content = "A damaged QR code can be restored based on the ECC level:";
                    t6.Content = "level 'L' - about 7% ";
                    t7.Content= "level 'M' - about 15% ";
                    t8.Content = "level 'Q' - about 25%";
                    t9.Content = "level 'H' - about 30% ";
             }
                if (lng.SelectedValue.ToString() == "Français")
             {
                    main.FlowDirection = FlowDirection.LeftToRight;
                    contentlabel.Content = "Contenu:";
                sttlbl.Content = "Paramètres";
                ecclabel.Content = "Niveau ECC";
                foregroundpick.Content = "Couleur";
                backgroundpick.Content = "Arrière-plan";
                addicn.Content = "Ajouter logo/image";
                lnglbl.Content = "Langue d'affichage:";
                generate.Content = "Générer votre QR";
                    grp.Content = "1001Tech Groupe FB";
                    t1.Content = "Niveau ECC (capacité de correction d'erreur)?";
                    t2.Content = "Cela compense (répare) les dommages ou le flou du code QR. ";
                    t3.Content = "Les valeurs valides sont 'L' (ECC bas), 'M', 'Q', 'H' (ECC le plus élevé). ";
                    t4.Content = "Un niveau élevé d'ECC ajoute davantage de redondance au coût d’utilisation de plus d’espace.";
                    t5.Content = "Un code QR endommagé peut être restauré en fonction du niveau ECC:";
                    t6.Content = "Niveau 'L' - environ 7%";
                    t7.Content = "Niveau 'M' - environ 15% ";
                    t8.Content = "Niveau 'Q' - environ 25%";
                    t9.Content = "Niveau 'H' - environ 30% ";
                }
                if (lng.SelectedValue.ToString() == "عربية")
                {
                    main.FlowDirection = FlowDirection.RightToLeft;
                    contentlabel.Content = "المحتوى";
                    sttlbl.Content = "الاعدادات";
                    ecclabel.Content = "معامل التصحيح";
                    foregroundpick.Content = "اللون";
                    backgroundpick.Content = "الخلفية";
                    addicn.Content = "اضافة صورة/شعار";
                    lnglbl.Content = "لغة التطبيق";
                    generate.Content = "اصنع لي الكود";
                    grp.Content = "1001Tech انتقل الى";
                    t1.Content = " ماهو معامل التصحيح؟";
                    t2.Content = "الهدف منه تصحيح الاخطاء في حالة تضرر الكود ";
                    t3.Content = "يشمل اربع درجات: ل، م، ك،ه  ";
                    t4.Content = "المعامل يكرر المحتوى في مساحات اصغر، و يمكنك ذلك من استرجاع معلوماتك في حال تضرر جزء من الكود";
                    t5.Content = "تختلف المعاملات من حيث قدرة الاسترجاع، حيث ان";
                    t6.Content = "المعامل ل يسترجع 7% من الكود";
                    t7.Content = "المعامل م يسترجع 15% من الكود";
                    t8.Content = "المعامل ك يسترجع 25% من الكود";
                    t9.Content = "المعامل ه يسترجع 30% من الكود ";
                }
                if (lng.SelectedValue.ToString() == "русский")
                {
                    main.FlowDirection = FlowDirection.LeftToRight;
                    contentlabel.Content = "Содержание:";
                    sttlbl.Content = "настройки";
                    ecclabel.Content = "Уровень ECC";
                    foregroundpick.Content = "Цвет кода Qr";
                    backgroundpick.Content = "Qr Code Цвет фона";
                    addicn.Content = "Добавить логотип / изображение";
                    lnglbl.Content = "Язык дисплея:";
                    generate.Content = "генерировать";
                    grp.Content = "1001Tech группа в фейсбуке";
                    t1.Content = "Уровень ECC (исправления ошибок)?";
                    t2.Content = "Это компенсирует грязь, повреждение или нечеткость штрих-кода. ";
                    t3.Content = "Допустимые значения: «L» (низкий ECC), «M», «Q», «H» (самый высокий ECC). ";
                    t4.Content = "Высокий уровень ECC добавляет больше избыточности за счет использования большего пространства.";
                    t5.Content = "Поврежденный QR-код может быть восстановлен на основе уровня ECC:";
                    t6.Content = "уровень «L» - около 7% ";
                    t7.Content = "уровень «М» - около 15% ";
                    t8.Content = "уровень «Q» - около 25%";
                    t9.Content = "уровень «Н» - около 30% ";
                }
                if (lng.SelectedValue.ToString() == "中国")
                {
                    main.FlowDirection = FlowDirection.LeftToRight;
                    contentlabel.Content = "内容：";
                    sttlbl.Content = "设定值";
                    ecclabel.Content = "ECC级别";
                    foregroundpick.Content = "二维码颜色";
                    backgroundpick.Content = "二维码背景颜色";
                    addicn.Content = "添加徽标/图像";
                    lnglbl.Content = "显示语言：";
                    generate.Content = "生成";
                    grp.Content = "1001Tech 组脸书";
                    t1.Content = "ECC（纠错能力）级别？";
                    t2.Content = "这样可以补偿代码的污垢，损坏或模糊不清。 ";
                    t3.Content = "有效值为“ L”（低ECC），“ M”，“ Q”，“ H”（最高ECC）。 ";
                    t4.Content = "高ECC级别以使用更多空间为代价增加了更多的冗余。 ";
                    t5.Content = "可以根据ECC级别恢复损坏的QR码：";
                    t6.Content = "等级“ L”-大约7％ ";
                    t7.Content = "M级-约15％ ";
                    t8.Content = "Q级-约25％";
                    t9.Content = "等级'H'-大约30％ ";
                }
            }
            catch (NullReferenceException) { };
            lng.ToolTip = lng.SelectedValue.ToString();
        }

        private void grp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/groups/dztech1001");
        }
        
        private void love_Click(object sender, RoutedEventArgs e)
        {
            //your account...
        }
    }
}

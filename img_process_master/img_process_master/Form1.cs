using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using img_processing_shared_dll.Filter;

namespace img_process_master
{
    public partial class Form1 : Form
    {
        private string CurrentFileName { get; set; }
        private int CurrentFilterID { get; set; }

        private readonly IProcessing processingService;
        public Form1()
        {
            InitializeComponent();
            processingService = new ProcessingServiceImpl();
            processButton.Enabled = false;
            //заполняем выпадающий список
            filterList.Items.AddRange(ZeroNeighborsSharedFilter.GetAllFilters().ToArray());
            filterList.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //выбрали фильтр
        private void OnFilterSelect(object sender, EventArgs e)
        {
            CurrentFilterID = ((ZeroNeighborsSharedFilter)filterList.SelectedItem).FilterID;
        }

        private void OnImageClick(object sender, EventArgs e)
        {

        }

        //по нажатии на Browse Image
        private void OnImageSelect(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                //фильтры для парвого нижнего угла 
                dialog.Filter = ".jpeg(*.jpeg)|*.jpeg|.jpg(*.jpg)|*.jpg|.bmp(*.bmp)|*.bmp|.png(*.png)|*.png|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    CurrentFileName = dialog.FileName;
                    try
                    {
                        pictureBox.Image = new Bitmap(CurrentFileName);
                        processButton.Enabled = true;
                    }
                    catch(ArgumentException)
                    {
                        MessageBox.Show("Unavailable format", "Error");
                        processButton.Enabled = false;
                    }
                }
            }
        }

        //метод завершения обработки картинки
        private void OnImageHandled(int[] img)
        {
            BeginInvoke((MethodInvoker)(()=>{
                Bitmap b = new Bitmap(pictureBox.Image);
                int H = pictureBox.Image.Size.Height;
                int W = pictureBox.Image.Size.Width;
                for (int i = 0; i < H; i++)
                {
                    for(int j = 0; j < W; j++)
                    {
                        b.SetPixel(j, i, Color.FromArgb(img[i * pictureBox.Image.Width + j]));
                    }
                }
                pictureBox.Image = b;
                //энейбл кнопки
                processButton.Enabled = true;
            }));
        }
        //по нажатии на кнопку блокируем её, вызываем обработчик картинки
        private void OnProcessButtonClick(object sender, EventArgs e)
        {
            processButton.Enabled = false;
            Task.Factory.StartNew(
                () => processingService.ProcessImage(pictureBox.Image as Bitmap, CurrentFilterID, OnImageHandled));
        }
    }
}

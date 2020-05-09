using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ResizeImage
{
    /// <summary>
    /// this resizes an image and creates a thumbnail 
    /// </summary>
    public class ResizeImage
    {
        public Image ImageIn { get; set; }
        public Image ImageOut { get; set; }
        public Image ImageThumb { get; set; }
        public ImageFormat TargetFormat { get; set; } = ImageFormat.Jpeg;
        private Size imageSize { get; set; }
        public Size ImageSize
        {
            get
            {
                return imageSize;
            }

            set
            {
                
                if (value.Width == new Size().Width)
                {
                    value.Width = ImageIn.Width;
                    value.Height = ImageIn.Height;
                }
                if (!(imageSize.Width == value.Width && imageSize.Height == value.Height))
                {
                    imageSize = value;
                }
            }
        }

        public string Dir { get; set; } = "";
        public ResizeImage(Image workingImage, Size newSize = new Size())
        {
            if (workingImage == null)
            {
                throw new Exception("workingImage is required for this constructor");
            }
             
            ImageSize = newSize;

            ImageIn = (Image)workingImage.Clone();
            
        }

        public ResizeImage()
        {
        }

        public ResizeImage(string InFile)
        {
            
             ImportImage(InFile);
        }


        public void ProcessTheFiles()
        {
            ImageThumb = GetThumb();
             Resize(ImageSize);
        }
        public  Image GetThumb()
        {
            if (ImageIn == null)
                throw new Exception("Image to work with has not been defined");

            return ((Image)ImageIn.Clone()).GetThumbnailImage(200, ((200 * ImageIn.Height) / ImageIn.Width), null, IntPtr.Zero);
        }

        public void Resize(Size newSize)
        {
            ImageOut = ((Image)ImageIn.Clone()).GetThumbnailImage(ImageSize.Width, ImageSize.Height, null, IntPtr.Zero);

            // Rectangle compressionRectangle = new Rectangle(0, 0,ImageSize.Width, ImageSize.Height);
           // Graphics g = Graphics.FromImage(ImageOut);
             
           // g.DrawImage(ImageOut, ImageSize.Width, ImageSize.Height);
           // g.Dispose();
             
        }

        public void ImportImage(string InfileName)
        {
            if (!File.Exists(InfileName))
                throw new Exception("Input File " + InfileName + " does not exist.");

            ImageIn =  Image.FromFile(InfileName);
        }

        public void SaveThumb(string outFileName)
        {
            ImageThumb.Save(Dir + "\\" + outFileName, TargetFormat);
        }
        public void SaveImage(string outFileName)
        {
            //ResizeAsBitMap();
            ImageOut.Save(Dir + "\\" + outFileName, TargetFormat);
        }
        public void SaveBoth(string thumbName, string imageName)
        {
            SaveThumb(thumbName);
            SaveImage(imageName);
        }
        public void SaveBoth(string name)
        {
             SaveThumb(name + "_thumb.jpg" );
             SaveImage(name + ".jpg" );
        }

       public void AdjustSizeBasedOnWidth(int width)
        {

            ImageSize = new Size(width,  (width * ImageIn.Height) / ImageIn.Width);
             
        }

        ~ResizeImage()
        {
            ImageIn.Dispose();
            ImageOut.Dispose();
            ImageThumb.Dispose();
             
        }
    }
}

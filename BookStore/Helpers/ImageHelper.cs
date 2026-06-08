using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace BookStore.Helpers
{
    public static class ImageHelper
    {
        public static string BaseDirectory
        {
            get { return Application.StartupPath; }
        }

        public static string AssetsDirectory
        {
            get { return Path.Combine(BaseDirectory, "assets"); }
        }

        public static string ImagesDirectory
        {
            get { return Path.Combine(BaseDirectory, "images"); }
        }

        public static string PlaceholderPath
        {
            get { return Path.Combine(AssetsDirectory, "picture.png"); }
        }

        public static void EnsureDirectories()
        {
            if (!Directory.Exists(AssetsDirectory))
            {
                Directory.CreateDirectory(AssetsDirectory);
            }
            if (!Directory.Exists(ImagesDirectory))
            {
                Directory.CreateDirectory(ImagesDirectory);
            }
        }

        public static void CreatePlaceholderIfMissing()
        {
            EnsureDirectories();
            if (File.Exists(PlaceholderPath))
            {
                return;
            }

            using (Bitmap bitmap = new Bitmap(AppConstants.ImageWidth, AppConstants.ImageHeight))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Font font = new Font("Segoe UI", 12))
            {
                graphics.Clear(Color.FromArgb(238, 238, 238));
                graphics.DrawRectangle(Pens.Gray, 10, 10, AppConstants.ImageWidth - 20, AppConstants.ImageHeight - 20);
                graphics.DrawString("Нет изображения", font, Brushes.DimGray, 80, 90);
                bitmap.Save(PlaceholderPath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public static string GetDisplayImagePath(string imagePath)
        {
            CreatePlaceholderIfMissing();
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string fullPath = Path.IsPathRooted(imagePath)
                    ? imagePath
                    : Path.Combine(BaseDirectory, imagePath);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }
            return PlaceholderPath;
        }

        public static Image LoadDisplayImage(string imagePath, int width, int height)
        {
            string path = GetDisplayImagePath(imagePath);
            using (Image original = Image.FromFile(path))
            {
                return ResizeImage(original, width, height);
            }
        }

        public static string SaveProductImage(string sourcePath, string oldRelativePath)
        {
            EnsureDirectories();
            DeleteOldImage(oldRelativePath);

            string fileName = "product_" + Guid.NewGuid().ToString("N") + ".png";
            string relativePath = Path.Combine("images", fileName);
            string destination = Path.Combine(BaseDirectory, relativePath);

            using (Image original = Image.FromFile(sourcePath))
            using (Image resized = ResizeImage(original, AppConstants.ImageWidth, AppConstants.ImageHeight))
            {
                resized.Save(destination, System.Drawing.Imaging.ImageFormat.Png);
            }

            return relativePath;
        }

        private static void DeleteOldImage(string oldRelativePath)
        {
            if (string.IsNullOrWhiteSpace(oldRelativePath))
            {
                return;
            }

            string fullPath = Path.Combine(BaseDirectory, oldRelativePath);
            if (File.Exists(fullPath) && !fullPath.Equals(PlaceholderPath, StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(fullPath);
            }
        }

        private static Image ResizeImage(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }
    }
}

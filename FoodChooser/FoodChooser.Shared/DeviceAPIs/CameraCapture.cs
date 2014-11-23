using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace FoodChooser
{
    public class CameraCapture
    {
        public async void GetPhoto()
        {

        }

        public async Task<byte[]> CapturePhoto(CaptureElement element)
        {
            var a = element.Source;
            var img = ImageEncodingProperties.CreateJpeg();
            img.Width = 640;
            img.Height = 480;

            var photoStorage = await KnownFolders.PicturesLibrary.CreateFileAsync("photo.jpg", CreationCollisionOption.GenerateUniqueName);
            await a.CapturePhotoToStorageFileAsync(img, photoStorage);
            var result = await this.ImageFileToByteArray(photoStorage);

            element.Visibility = Visibility.Collapsed;

            return result;
        }

        public async Task<Byte[]> ImageFileToByteArray(StorageFile file)
        {
            IRandomAccessStream stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            PixelDataProvider pixelData = await decoder.GetPixelDataAsync();
            return pixelData.DetachPixelData();
        }
        
        public static async Task<BitmapImage> GetPhotoFromByteArray(byte[] byteArray)
        {
            var bitmapImage = new BitmapImage();
            var stream = new InMemoryRandomAccessStream();
            bitmapImage.SetSource(stream);
            await stream.WriteAsync(byteArray.AsBuffer());
            stream.Seek(0);

            //var bitmapImage = new BitmapImage();
            //IRandomAccessStream memoryStream = new InMemoryRandomAccessStream(byteArray);
            //bitmapImage.SetSource(memoryStream);
           // return bitmapImage;

            return bitmapImage;
        }
    }
}

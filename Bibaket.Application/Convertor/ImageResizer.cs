using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Convertor
{
    public class ImageResizer
    {
        public void ImageResize(string inputImagePath,string outputImagePath, int? width, int? height)
        {
            var customWidth = width ?? 120;

            var customHeight = height ?? 210;

            using(var image= Image.Load(inputImagePath))
            {
                image.Mutate(i=>i.Resize(customWidth, customHeight));

                image.Save(outputImagePath,new JpegEncoder
                {
                    Quality=100
                });
            }
        }
    }
}

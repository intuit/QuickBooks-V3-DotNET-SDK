////********************************************************************
// <copyright file="ConvertImage.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This class converts the Image object into OLE IPictureDisp object.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    using System.Windows.Forms;

    /// <summary>
    /// This class converts the Image object into OLE IPictureDisp object.
    /// </summary>
    internal sealed class ConvertImage : AxHost
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="ConvertImage"/> class from being created.
        /// </summary>
        private ConvertImage()
            : base(null)
        {
        }

        /// <summary>
        /// Converts the specified image into OLE IPictureDisp object.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns> OLE picture object implementing stdole.IPictureDisp.</returns>
        internal static stdole.IPictureDisp Convert(System.Drawing.Image image)
        {
            return (stdole.IPictureDisp)AxHost.GetIPictureDispFromPicture(image);
        }
    }
}

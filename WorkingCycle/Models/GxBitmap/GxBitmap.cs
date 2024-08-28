using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using DutyCycle.Forms.DutyCycle;
using GxIAPINET;

namespace DutyCycle.Models.GxBitmap
{
    public class GxBitmap
    {

        IGXDevice m_objIGXDevice = null;                ///<The handle for device
        PictureBox m_pic_ShowImage = null;                      ///<Picture display control
        bool m_bIsColor = false;                                ///<The flag indicates whether the color camera 
        byte[] m_byMonoBuffer = null;                           ///<Black and white camera buffer
        byte[] m_byColorBuffer = null;                          ///<Color camera buffer
        int m_nWidth = 0;                   ///<Image width
        int m_nHeight = 0;                   ///<Image height
        Bitmap m_bitmapForSave = null;                ///<bitmap object
        const uint PIXEL_FORMATE_BIT = 0x00FF0000;              ///<Used to AND the current data format to get the current data bits
        const uint GX_PIXEL_8BIT = 0x00080000;                  ///<8-bit data image format

        const int COLORONCOLOR = 3;
        const uint DIB_RGB_COLORS = 0;
        const uint SRCCOPY = 0x00CC0020;
        CWin32Bitmaps.BITMAPINFO m_objBitmapInfo = new CWin32Bitmaps.BITMAPINFO();
        nint m_pBitmapInfo = nint.Zero;
        Graphics m_objGC = null;
        nint m_pHDC = nint.Zero;


        /// <summary>
        /// The construction function
        /// </summary>
        /// <param name="objIGXDevice">Device object</param>
        /// <param name="objPictureBox">Picture display control</param>
        public GxBitmap(IGXDevice objIGXDevice, PictureBox objPictureBox)
        {
            m_objIGXDevice = objIGXDevice;
            m_pic_ShowImage = objPictureBox;
            if (null != objIGXDevice)
            {
                //Get image data
                m_nWidth = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("Width").GetValue();
                m_nHeight = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("Height").GetValue();

                //Gets whether it is a color camera
                __IsSupportColor(ref m_bIsColor);
            }

            //Apply buffer to cache image data
            m_byMonoBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];
            m_byColorBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];

            __CreateBitmap(out m_bitmapForSave, m_nWidth, m_nHeight, m_bIsColor);

            m_objGC = m_pic_ShowImage.CreateGraphics();
            m_pHDC = m_objGC.GetHdc();
            if (m_bIsColor)
            {
                m_objBitmapInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(CWin32Bitmaps.BITMAPINFOHEADER));
                m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                m_objBitmapInfo.bmiHeader.biHeight = m_nHeight;
                m_objBitmapInfo.bmiHeader.biPlanes = 1;
                m_objBitmapInfo.bmiHeader.biBitCount = 24;
                m_objBitmapInfo.bmiHeader.biCompression = 0;
                m_objBitmapInfo.bmiHeader.biSizeImage = 0;
                m_objBitmapInfo.bmiHeader.biXPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biYPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biClrUsed = 0;
                m_objBitmapInfo.bmiHeader.biClrImportant = 0;
            }
            else
            {
                m_objBitmapInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(CWin32Bitmaps.BITMAPINFOHEADER));
                m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                m_objBitmapInfo.bmiHeader.biHeight = m_nHeight;
                m_objBitmapInfo.bmiHeader.biPlanes = 1;
                m_objBitmapInfo.bmiHeader.biBitCount = 8;
                m_objBitmapInfo.bmiHeader.biCompression = 0;
                m_objBitmapInfo.bmiHeader.biSizeImage = 0;
                m_objBitmapInfo.bmiHeader.biXPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biYPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biClrUsed = 0;
                m_objBitmapInfo.bmiHeader.biClrImportant = 0;

                m_objBitmapInfo.bmiColors = new CWin32Bitmaps.RGBQUAD[256];
                // black and white image need initialize palette
                for (int i = 0; i < 256; i++)
                {
                    m_objBitmapInfo.bmiColors[i].rgbBlue = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbGreen = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbRed = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbReserved = 0;
                }
            }
            m_pBitmapInfo = Marshal.AllocHGlobal(2048);
            Marshal.StructureToPtr(m_objBitmapInfo, m_pBitmapInfo, false);
        }

        /// <summary>
        /// Display image
        /// </summary>
        /// <param name="objIBaseData">Image data object</param>
        public void Show(IBaseData objIBaseData)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;

            //Check whether update bitmap information
            __UpdateBufferSize(objIBaseData);


            if (null != objIBaseData)
            {
                emValidBits = __GetBestValudBit(objIBaseData.GetPixelFormat());
                if (GX_FRAME_STATUS_LIST.GX_FRAME_STATUS_SUCCESS == objIBaseData.GetStatus())
                {
                    if (m_bIsColor)
                    {
                        nint pBufferColor = objIBaseData.ConvertToRGB24(emValidBits, GX_BAYER_CONVERT_TYPE_LIST.GX_RAW2RGB_NEIGHBOUR, true);
                        Marshal.Copy(pBufferColor, m_byColorBuffer, 0, __GetStride(m_nWidth, m_bIsColor) * m_nHeight);
                        __ShowImage(m_byColorBuffer);
                    }
                    else
                    {
                        nint pBufferMono = nint.Zero;
                        if (__IsPixelFormat8(objIBaseData.GetPixelFormat()))
                        {
                            pBufferMono = objIBaseData.GetBuffer();
                        }
                        else
                        {
                            pBufferMono = objIBaseData.ConvertToRaw8(emValidBits);
                        }

                        byte[] byMonoBufferTmp = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];
                        Marshal.Copy(pBufferMono, byMonoBufferTmp, 0, __GetStride(m_nWidth, m_bIsColor) * m_nHeight);

                        // Black and white cameras need to flip the data before displaying
                        for (int i = 0; i < m_nHeight; i++)
                        {
                            Buffer.BlockCopy(byMonoBufferTmp, (m_nHeight - i - 1) * m_nWidth, m_byMonoBuffer, i * m_nWidth, m_nWidth);
                        }

                        __ShowImage(m_byMonoBuffer);
                    }
                }
            }
        }

        /// <summary>
        ///  //Check whether update bitmap information
        /// </summary>
        /// <param name="objIBaseData">Image data object</param>
        private void __UpdateBufferSize(IBaseData objIBaseData)
        {
            if (null != objIBaseData)
            {
                if (__IsCompatible(m_bitmapForSave, m_nWidth, m_nHeight, m_bIsColor))
                {
                    m_nWidth = (int)objIBaseData.GetWidth();
                    m_nHeight = (int)objIBaseData.GetHeight();
                }
                else
                {
                    m_nWidth = (int)objIBaseData.GetWidth();
                    m_nHeight = (int)objIBaseData.GetHeight();

                    m_byMonoBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];
                    m_byColorBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];

                    //Update BitmapInfo
                    m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                    m_objBitmapInfo.bmiHeader.biHeight = m_nHeight;
                    Marshal.StructureToPtr(m_objBitmapInfo, m_pBitmapInfo, false);
                }
            }
        }

        /// <summary>
        /// Display image
        /// </summary>
        /// <param name = "byBuffer" > Image data buffer</param>
        private void __ShowImage(byte[] byBuffer)
        {
            if (null != m_pic_ShowImage)
            {
                //fullscreen picturebox size 1190x896
                //default size 648x486
                if (DutyCycleForm.fullscreenCamera)
                {
                    CWin32Bitmaps.SetStretchBltMode(m_pHDC, COLORONCOLOR);

                    float aspectRatio = (float)m_nWidth / m_nHeight;

                    int destWidth = m_pic_ShowImage.Width;
                    int destHeight = m_pic_ShowImage.Height;
                    int destX;
                    int destY;

                    if (m_pic_ShowImage.Width / aspectRatio <= m_pic_ShowImage.Height)
                    {
                        destHeight = (int)(m_pic_ShowImage.Width / aspectRatio);
                    }
                    else
                    {
                        destWidth = (int)(m_pic_ShowImage.Height * aspectRatio);
                    }

                    destX = m_pic_ShowImage.Width / 2 - destWidth / 2;
                    destY = m_pic_ShowImage.Height / 2 - destHeight / 2;

                    CWin32Bitmaps.StretchDIBits(
                                m_pHDC,
                                destX,
                                destY,
                                destWidth,
                                destHeight,
                                0,
                                0,
                                m_nWidth,
                                m_nHeight,
                                byBuffer,
                                m_pBitmapInfo,
                                DIB_RGB_COLORS,
                                SRCCOPY);
                }
                else
                {
                    // Создайте новый Bitmap с нужными размерами
                    Bitmap bmp = new Bitmap(m_nWidth, m_nHeight, PixelFormat.Format8bppIndexed);

                    // Установите палитру для 8-битного изображения
                    ColorPalette palette = bmp.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        palette.Entries[i] = Color.FromArgb(i, i, i); // Заполните палитру градациями серого
                    }
                    bmp.Palette = palette;

                    // Заблокируйте биты Bitmap для записи
                    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
                    Marshal.Copy(byBuffer, 0, bmpData.Scan0, byBuffer.Length);
                    bmp.UnlockBits(bmpData);

                    // Измените размер изображения для вмещения в PictureBox
                    float aspectRatio = (float)m_nWidth / m_nHeight;
                    int destWidth = m_pic_ShowImage.Width;
                    int destHeight = m_pic_ShowImage.Height;

                    if (m_pic_ShowImage.Width / aspectRatio <= m_pic_ShowImage.Height)
                    {
                        destHeight = (int)(m_pic_ShowImage.Width / aspectRatio);
                    }
                    else
                    {
                        destWidth = (int)(m_pic_ShowImage.Height * aspectRatio);
                    }

                    // Создайте новый Bitmap для конечного отображения с нужным размером
                    Bitmap resizedBmp = new Bitmap(destWidth, destHeight);

                    using (Graphics g = Graphics.FromImage(resizedBmp))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.DrawImage(bmp, 0, 0, destWidth, destHeight);
                    }

                    // Отобразите масштабированное изображение в PictureBox
                    m_pic_ShowImage.Image = resizedBmp;
                }
            }
        }



        /// <summary>
        ///  Check whether the PixelFormat is 8 bit
        /// </summary>
        /// <param name="emPixelFormatEntry">Image data format</param>
        /// <returns>true:  is 8 bit,   false:  is not 8 bit</returns>
        private bool __IsPixelFormat8(GX_PIXEL_FORMAT_ENTRY emPixelFormatEntry)
        {
            bool bIsPixelFormat8 = false;
            uint uiPixelFormatEntry = (uint)emPixelFormatEntry;
            if ((uiPixelFormatEntry & PIXEL_FORMATE_BIT) == GX_PIXEL_8BIT)
            {
                bIsPixelFormat8 = true;
            }
            return bIsPixelFormat8;
        }

        /// <summary>
        /// Get the best Bit by GX_PIXEL_FORMAT_ENTRY
        /// </summary>
        /// <param name="em">Image format</param>
        /// <returns>the best Bit</returns>
        private GX_VALID_BIT_LIST __GetBestValudBit(GX_PIXEL_FORMAT_ENTRY emPixelFormatEntry)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
            switch (emPixelFormatEntry)
            {
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG8:
                    {
                        emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG10:
                    {
                        emValidBits = GX_VALID_BIT_LIST.GX_BIT_2_9;
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG12:
                    {
                        emValidBits = GX_VALID_BIT_LIST.GX_BIT_4_11;
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO14:
                    {
                        //There is no such data format
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG16:
                    {
                        //There is no such data format
                        break;
                    }
                default:
                    break;
            }
            return emValidBits;
        }

        /// <summary>
        /// Get  the display image format
        /// </summary>
        /// <param name="bIsColor">The color camera</param>
        /// <returns>Image data format</returns>
        private PixelFormat __GetFormat(bool bIsColor)
        {
            return bIsColor ? PixelFormat.Format24bppRgb : PixelFormat.Format8bppIndexed;
        }

        /// <summary>
        /// Calculate the number of bytes occupied by width
        /// </summary>
        /// <param name="nWidth">Image width</param>
        /// <param name="bIsColor">Whether is a color camera</param>
        /// <returns>The number of bytes of an image line</returns>
        private int __GetStride(int nWidth, bool bIsColor)
        {
            return bIsColor ? nWidth * 3 : nWidth;
        }

        /// <summary>
        ///  whether compatible
        /// </summary>
        /// <param name="bitmap">Bitmap object</param>
        /// <param name="nWidth">Image width</param>
        /// <param name="nHeight">Image height</param>
        /// <param name="bIsColor">Whether is a color camera</param>
        /// <returns>true: compatible , false: not compatible</returns>
        private bool __IsCompatible(Bitmap bitmap, int nWidth, int nHeight, bool bIsColor)
        {
            if (bitmap == null
                || bitmap.Height != nHeight
                || bitmap.Width != nWidth
                || bitmap.PixelFormat != __GetFormat(bIsColor)
             )
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Create Bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap object</param>
        /// <param name="nWidth">Image width</param>
        /// <param name="nHeight">Image height</param>
        /// <param name="bIsColor">Whether is a color camera</param>
        private void __CreateBitmap(out Bitmap bitmap, int nWidth, int nHeight, bool bIsColor)
        {
            bitmap = new Bitmap(nWidth, nHeight, __GetFormat(bIsColor));
            if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette colorPalette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                }
                bitmap.Palette = colorPalette;
            }
        }

        /// <summary>
        /// Support color
        /// </summary>
        /// <param name="bIsColorFilter">Support color</param>
        private void __IsSupportColor(ref bool bIsColorFilter)
        {
            bool bIsImplemented = false;
            bool bIsMono = false;
            string strPixelFormat = "";

            strPixelFormat = m_objIGXDevice.GetRemoteFeatureControl().GetEnumFeature("PixelFormat").GetValue();
            if (0 == string.Compare(strPixelFormat, 0, "Mono", 0, 4))
            {
                bIsMono = true;
            }
            else
            {
                bIsMono = false;
            }

            bIsImplemented = m_objIGXDevice.GetRemoteFeatureControl().IsImplemented("PixelColorFilter");

            // If it is currently black and white and supports pixelcolorfilter, it is color
            if (!bIsMono && bIsImplemented)
            {
                bIsColorFilter = true;
            }
            else
            {
                bIsColorFilter = false;
            }
        }
    }
}

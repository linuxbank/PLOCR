//[License]
//tesseract-ocr - pache License, Version 2.0,  http://code.google.com/p/tesseract-ocr/
//Tesseract.dll - Apache License, Version 2.0,  https://github.com/charlesw/tesseract
//Sarafftwain.dll - GNU Lesser General Public License (LGPL),  https://sarafftwain.codeplex.com/
//SQLite tool box - Microsoft Public License (Ms-PL),  https://visualstudiogallery.msdn.microsoft.com/0e313dfd-be80-4afb-b5e9-6e74d369f7a1
//AForge.dll - GNU Lesser GPL,  http://www.aforgenet.com/ 

//PLOCR(팜스라이프 스캔) - GPL,  http://cafe.daum.net/pharm--poor
//PLObserver(팜스라이프 스캔 옵저버) - GPL,  http://cafe.daum.net/pharm--poor
//DocumentAnalysis(팜스라이프 스캔 환경설정) - GPL,  http://cafe.daum.net/pharm--poor
//PLDB(팜스라이프 스캔 DB 설정) - GPL,  http://cafe.daum.net/pharm--poor 

//http://cafe.daum.net/pharm--poor
//linuxbank@hanmail.net

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;

namespace PLOCR
{
    class Crop
    {  
        //public static Bitmap[] segCrop(Bitmap shrink, int zeroX, int zeroY, int zeroWidth, int zeroHeight, int jumpY)
        //{
        //    int ct = 0;

        //    Bitmap[] cShrink;
        //    cShrink = new Bitmap[13];  // 약품코드, 투약량, 횟수, 일수의 세로 13칸을 배열로 넣기위해
        //    int x = zeroX;      // 약품코드, 투약량, 횟수, 일수의 x 좌표, 여러 형태의 처방전마다 조정할 수 있다.
        //    int y = zeroY;
        //    int width = zeroWidth;
        //    int height = zeroHeight;

        //    do
        //    {
        //        cShrink[ct] = cloneAforge(shrink, x, y, width, height);  // 루프 돌면서 약품코드, 투약량, 횟수, 일수의 부분 잘라내기
        //        cShrink[ct].Save(@".\cShrink[" + ct + "].png");
        //      //  cDrugName[ct].Dispose();

        //        ++ct;
        //        y = y + jumpY;     // y 축으로 jumpY 씩 아래로 이동하며 잘라내기, 이 부분도 여러 크기대로 조절가능              
        //    }

        //    while (ct < 13);
        //    {
        //        return cShrink;
        //    }
        //}               


        //public static Bitmap cloneAforge(Bitmap bmp, int startX, int startY, int width, int height)
        //{
        //   // FiltersSequence seq = new FiltersSequence();
        //   // seq.Add(Grayscale.CommonAlgorithms.BT709);  //First add  GrayScaling filter            
        //   // seq.Add(new OtsuThreshold()); //Then add binarization(thresholding) filter
        //   // bmp = seq.Apply(bmp); // Apply filters on source image  , 8비트로 다시 변환하기 위해 그레이스케일 필터 사용

        //    Bitmap cropImage = bmp;
        //    BitmapData rawOriginal = bmp.LockBits(new Rectangle(startX, startY, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);  // PixelFormat.Format32bppArgb 이부분 절대 변경하지 말것, 비트맵 상호 변환에 있어 필터에 안 들어가는 경우가 생김, 주의!!!!!
        //    cropImage = AForge.Imaging.Image.Clone(rawOriginal);
        //    bmp.UnlockBits(rawOriginal);            
        //    return cropImage;
        //}
    }
}

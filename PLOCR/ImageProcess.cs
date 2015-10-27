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
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using AForge;
using System.IO;
using System.Windows.Forms;

namespace PLOCR
{
    class ImageProcess    
    {
        public static System.Drawing.Bitmap Scaling(System.Drawing.Bitmap imgPhoto, int Percent)      // Aforge image 클래스와 모호해지므로 System.Drawing.Bitmap을 명시해줘야함,
        {
            //퍼센트 0.8 or 0.5 ..

            float nPercent = ((float)Percent / 100);

            //넓이와 높이
            int OriginalWidth = imgPhoto.Width;
            int OriginalHeight = imgPhoto.Height;

            //소스의 처음 위치
            int OriginalX = 0;
            int OriginalY = 0;

            //움직일 위치
            int adjustX = 0;
            int adjustY = 0;

            //조절될 퍼센트 계산
            int adjustWidth = (int)(OriginalWidth * nPercent);
            int adjustHeight = (int)(OriginalHeight * nPercent);

            MessageBox.Show(adjustWidth.ToString());


            //비어있는 비트맵 객체 생성
            Bitmap bmPhoto = new Bitmap(adjustWidth, adjustHeight, PixelFormat.Format24bppRgb);

            //이미지를 그래픽 객체로 만든다.
            Graphics grPhoto = Graphics.FromImage(bmPhoto);            

            //사각형을 그린다.

            //그릴 이미지객체 크기, 그려질 이미지객체 크기
            grPhoto.DrawImage(imgPhoto,
            new Rectangle(adjustX, adjustY, adjustWidth, adjustHeight),
            new Rectangle(OriginalX, OriginalY, OriginalWidth, OriginalHeight),
            GraphicsUnit.Pixel);
            grPhoto.Dispose();

            bmPhoto.Save(@"C:\Program Files\PLOCR\확대후.png");

            return bmPhoto;
        }


        public static Bitmap RGBfilter(Bitmap source)     // rgb 필터
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////  

            int redMin = int.Parse(ini.GetIniValue("색상필터값", "RedMin"));
            int redMax = int.Parse(ini.GetIniValue("색상필터값", "RedMax"));
            int greenMin = int.Parse(ini.GetIniValue("색상필터값", "GreenMin"));
            int greenMax = int.Parse(ini.GetIniValue("색상필터값", "GreenMax"));
            int blueMin = int.Parse(ini.GetIniValue("색상필터값", "BlueMin"));
            int blueMax = int.Parse(ini.GetIniValue("색상필터값", "BlueMax"));

            // create filter
            ColorFiltering filter = new ColorFiltering();
            // set color ranges to keep
            filter.Red = new IntRange(redMin, redMax);
            filter.Green = new IntRange(greenMin, greenMax);
            filter.Blue = new IntRange(blueMin, blueMax);   
            Bitmap processedImage = filter.Apply(source);           
            
            return processedImage;
        }

        public static Bitmap colorFilter(Bitmap source)     // rgb 필터 적용
        {            
            source = RGBfilter(source);            

            return source;
        }

        public static Bitmap invert(Bitmap source)      // 반전
        {         

            Bitmap tmp = (Bitmap)source;        // 중요! 한번 이미지 처리가 끝난 비트맵 source 는 clone 함수로 보내기 전에 다시 한번 (Bitmap) 처리 해줘야함, 이유는 잘 모르겠음            
            // convert to 24 bits per pixel
            source = ImageProcess.Clone(tmp, PixelFormat.Format24bppRgb);
            // delete old image
            tmp.Dispose();                        

            Invert invertfilter = new Invert();
            invertfilter.ApplyInPlace(source);                       

            return source;
        }

        public static Bitmap dark(Bitmap source)        // 어둡게
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////

            int order = int.Parse(ini.GetIniValue("밝기값", "어둡게횟수"));

            for (int i = 0; i < order; i++)
            {
                // create filter
                BrightnessCorrection filter = new BrightnessCorrection(-10);
                // apply the filter
                filter.ApplyInPlace(source);
            }

            return source;
        }

        public static Bitmap bright(Bitmap source)        // 밝게
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////

            int order = int.Parse(ini.GetIniValue("밝기값", "밝게횟수"));

            for (int i = 0; i < order; i++)
            {
                // create filter
                BrightnessCorrection filter = new BrightnessCorrection(+10);
                // apply the filter
                filter.ApplyInPlace(source);
            }

            return source;
        }

        public static Bitmap thin(Bitmap source)        // 선 가늘게
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////
                        
            int order = int.Parse(ini.GetIniValue("선굵기값", "가늘게횟수"));

            for (int i = 0; i < order; i++)
            {
                Bitmap tmp = (Bitmap)source;        // 중요! 한번 이미지 처리가 끝난 비트맵 source 는 clone 함수로 보내기 전에 다시 한번 (Bitmap) 처리 해줘야함, 이유는 잘 모르겠음
                // convert to 24 bits per pixel
                source = ImageProcess.Clone(tmp, PixelFormat.Format24bppRgb);
                // delete old image
                tmp.Dispose();  

                Dilatation filter = new Dilatation();
                filter.ApplyInPlace(source);
            }

            return source;
        }

        public static Bitmap thick(Bitmap source)        // 선 굵게, 24비트로 넣어줘야함
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////

            int order = int.Parse(ini.GetIniValue("선굵기값", "굵게횟수"));

            for (int i = 0; i < order; i++)
            {
                Bitmap tmp = (Bitmap)source;        // 중요! 한번 이미지 처리가 끝난 비트맵 source 는 clone 함수로 보내기 전에 다시 한번 (Bitmap) 처리 해줘야함, 이유는 잘 모르겠음
                // convert to 24 bits per pixel
                source = ImageProcess.Clone(tmp, PixelFormat.Format24bppRgb);
                // delete old image
                tmp.Dispose();  

                Erosion filter = new Erosion();
                filter.ApplyInPlace(source); ;
            }           

            return source;
        }

        public static Bitmap skew(Bitmap source)        // 기울어짐 바로잡기
        {
            // create grayscale filter (BT709)
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);       // 8비트 grayscale 로 바꾸고
            // apply the filter
            Bitmap grayImage = filter.Apply(source);                       

            // create instance of skew checker
            DocumentSkewChecker skewChecker = new DocumentSkewChecker();        // 8비트 grayscale 로 넣어줘야 함
            //    // get documents skew angle
            double angle = skewChecker.GetSkewAngle(grayImage);  // 기울어진 각도를 얻고

            Bitmap tmp = source;
            // convert to 24 bits per pixel
            source = ImageProcess.Clone(tmp, PixelFormat.Format24bppRgb);       // 로테이션 전에 24비트로 바꿔주고
            // delete old image
            tmp.Dispose();

            // create rotation filter
            RotateBilinear rotationFilter = new RotateBilinear(-angle);
            rotationFilter.FillColor = Color.White;
            // rotate image applying the filter
            Bitmap rotatedImage = rotationFilter.Apply(source);  // 원래 이미지를 가져다가 각도만큼 돌리고(원래 이미지는 24비트로 넣어줘야함)           

            return rotatedImage;            
        }


       //     string path = @".\prescription.png";
       //     Bitmap source = (Bitmap)Bitmap.FromFile(path);

        //public static Bitmap ImgProcess(Bitmap source)
        //{
        //    Bitmap tmp = source;
        //    // convert to 24 bits per pixel
        //    source = Clone(tmp, PixelFormat.Format24bppRgb);
        //    // delete old image
        //    tmp.Dispose();

        //    FiltersSequence seq = new FiltersSequence();
        //    seq.Add(Grayscale.CommonAlgorithms.BT709);  //First add  GrayScaling filter
        //    seq.Add(new OtsuThreshold()); //Then add binarization(thresholding) filter
        //    Bitmap temp = seq.Apply(source); // Apply filters on source image       
        //    //new ContrastStretch().ApplyInPlace(temp);
        //    //new Threshold(10).ApplyInPlace(temp);  // 숫자값을 조정하면 선의 굵기가 달라짐

        //    temp.Save(@".\1.png");

        //    // create instance of skew checker
        //    DocumentSkewChecker skewChecker = new DocumentSkewChecker();
        //    // get documents skew angle
        //    double angle = skewChecker.GetSkewAngle(temp);  // 비뚤어진 각도를 얻고

        ////    Console.WriteLine(angle); // 각도값 확인

            

            //    create filter
          //  Invert invertfilter = new Invert();
            // apply the filter
         //   invertfilter.ApplyInPlace(source);

      //      source.Save(@".\invert.png");

            

       //     Bitmap cutBottomFirst = cutBottom(source, 100);  // 초기 스캔한 처방전 아래부분을 일정크기 잘라내고
       //     cutBottomFirst.Save(@".\cutBottomFirst.png");

            // create filter
      //      ColorFiltering filter = new ColorFiltering();  // 인쇄된 부분만 남기고 나머지 색상 필터로 걸러냄
            // set color ranges to keep
     //       filter.Red = new IntRange(100, 180);     // 이 부분의 숫자값 0~255 범위가 남겨질 색상이므로, 인쇄 색상만 남기고 나머지 기본틀을 없앨 수 있다.
    //        filter.Green = new IntRange(100, 180);
    //        filter.Blue = new IntRange(100, 165);
            // apply the filter
   //         filter.ApplyInPlace(source);

     //       source.Save(@".\colorfilter.bmp");                      
         
            // apply the filter
     //       invertfilter.ApplyInPlace(source);

      //      source.Save(@".\invert.png");
           

      //      FiltersSequence filterImage = new FiltersSequence();
      //      filterImage.Add(Grayscale.CommonAlgorithms.BT709);  //First add  GrayScaling filter
      //      filterImage.Add(new OtsuThreshold()); //Then add binarization(thresholding) filter            
      //      Bitmap grayImage = filterImage.Apply(source); // Apply filters on source image

      //      grayImage.Save(@".\2.png");

      //      //new ContrastStretch().ApplyInPlace(grayImage);
      //      //new Threshold(200).ApplyInPlace(grayImage);  // 숫자값을 조정하면 선의 굵기가 달라짐

      //      //grayImage.Save(@".\tuningLine2.png");
            
      //      //FiltersSequence commonSeq = new FiltersSequence();
      //      //commonSeq.Add(Grayscale.CommonAlgorithms.BT709);
      //      //commonSeq.Add(new BradleyLocalThresholding());
      //      //commonSeq.Add(new DifferenceEdgeDetector());
      //      //Bitmap grayImage = commonSeq.Apply(source);

      // //     grayImage.Save(@".\grayImage.bmp");
                       
      ////      grayImage.Save(@".\grayImage.png");    

      //      //// create instance of skew checker
      //      //DocumentSkewChecker skewChecker = new DocumentSkewChecker();
      //      //// get documents skew angle
      //      //double angle = skewChecker.GetSkewAngle(grayImage);  // 비뚤어진 각도를 얻고

      //      // create rotation filter
      //      RotateBilinear rotationFilter = new RotateBilinear(-angle);
      //      rotationFilter.FillColor = Color.White;
      //      // rotate image applying the filter
      //      Bitmap rotatedImage = rotationFilter.Apply(temp);  // 각도만큼 돌리고

      //      rotatedImage.Save(@".\3.png");

      //      // create rotation filter
      //  //    RotateBilinear rotationFilter = new RotateBilinear(-angle);
      //      rotationFilter.FillColor = Color.White;
      //      // rotate image applying the filter
      //      Bitmap originalrotatedImage = rotationFilter.Apply(source);  // 각도만큼 돌리고

      //      originalrotatedImage.Save(@".\4.png");


            //Shrink shrinkfilter = new Shrink(Color.White);
            //Bitmap shrinkfinal = shrinkfilter.Apply(rotatedImage);

            //shrinkfinal.Save(@".\shrinkfinal.bmp");


            //// create structuring element
            //short[,] se = new short[,] {
            //    { -1,  1, -1 },
            //    { -1,  1, -1 },
            //    { -1, -1, -1 }
            //};
            //// create filter
            //Dilatation filter = new Dilatation(se);
            //// apply the filter
            //filter.Apply(rotatedImage);

            //rotatedImage.Save(@".\line1.bmp");


            //new ContrastStretch().ApplyInPlace(rotatedImage);
            //new Threshold(70).ApplyInPlace(rotatedImage);  // 숫자값을 조정하면 선의 굵기가 달라짐

            //rotatedImage.Save(@".\linethining.bmp");

          
            //Invert invertfilter = new Invert();            
            //invertfilter.ApplyInPlace(rotatedImage);

            //rotatedImage.Save(@".\beforeline2.bmp");

            //// define kernel to remove pixels on the right side of objects
            //// (pixel is removed, if there is white pixel on the left and
            //// black pixel on the right)
            //short[,] se = new short[,] {
            //    { -1,  1, -1 },
            //    {  1, -1,  0 },
            //    { -1,  0, -1 }
            //};
            //// create filter
            //HitAndMiss filter = new HitAndMiss(se, HitAndMiss.Modes.Thinning);
            //// apply the filter
            //filter.ApplyInPlace(rotatedImage);

            //rotatedImage.Save(@".\line.bmp");

            //invertfilter.ApplyInPlace(rotatedImage);

            //rotatedImage.Save(@".\line2.bmp");

           // rotatedImage.Save(@".\rotatedImage.bmp");


            //FiltersSequence commonSeq2 = new FiltersSequence();
            //commonSeq2.Add(new BradleyLocalThresholding());    // 번진 얼룩 제거하고, 글자부분만 강조하는 필터
            //commonSeq2.Add(new DifferenceEdgeDetector());  // 테두리만 잡아내는 필터
            //Bitmap edge = commonSeq2.Apply(source);
            
      

            // create filter
        //    Dilatation filter = new Dilatation();
            // apply the filter
       //     filter.Apply(edge);

            // define kernel to remove pixels on the right side of objects
            // (pixel is removed, if there is white pixel on the left and
            // black pixel on the right)
      //      short[,] se = new short[,] {
      //          { -1,  1, -1 },
      //          {  1, -1,  1 },
       //         { -1,  1, -1 }
       //     };
            // create filter
     //       HitAndMiss filter = new HitAndMiss(se, HitAndMiss.Modes.Thickening);
            // apply the filter
       //     filter.ApplyInPlace(edge);


        //    edge.Save(@".\edge2.bmp");

            // create filter
      //      Closing filter3 = new Closing();
            // apply the filter
      //      filter.Apply(edge);



      //      ExtractBiggestBlob extractor = new ExtractBiggestBlob();    // 제일 큰 부분만 잡아내는 필터
      //      Bitmap temp = extractor.Apply(edge); //Extract biggest blob

      //      temp.Save(@".\temp.bmp");

      //      Invert invertfilter = new Invert();
      //      invertfilter.ApplyInPlace(temp);

      //      temp.Save(@".\temp2.bmp");



      //      rotatedImage.Save(@".\rotatedImage.png");

      //      Bitmap cutMarginFirst = cutMargin(rotatedImage, 100);  // 바로 잡힌 초기 스캔한 처방전 종이 테두리를 일정크기 잘라내고 해당 숫자 픽셀 정도
      //      cutMarginFirst.Save(@".\cutMarginFirst.png");

     //       cutMarginFirst = seq.Apply(cutMarginFirst);  // 메모리 에러를 방지하기 위해 한번 더 처리, 다른 해결책을 찾아야함

      //      Bitmap FinalImage = CutShrink(cutMarginFirst, 5);   // 테두리를 줄이고 숫자만큼 픽셀을 자르고를 반복한다.

    //       FinalImage.Save(@".\FinalImage.png");

     //       Shrink shrinkfilter = new Shrink(Color.White);
            // apply the filter
    //        Bitmap FinalCutImage = shrinkfilter.Apply(FinalImage);  // 마지막 shrink 한번 더 해보고

    //        BitmapData rawOriginal = rotatedImage.LockBits(new Rectangle(0, 0, 1500, 2000), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);  // 마지막 특정 크기로 잘라서 좌표적용 가능하도록
    //        Bitmap processedImage = AForge.Imaging.Image.Clone(rawOriginal);
    //        rotatedImage.UnlockBits(rawOriginal);

        //    return originalrotatedImage;
        //}

                




    //    public static Bitmap CutShrink(Bitmap bmp, int margin)
    //    {          
    //        Bitmap compare;
    //        Bitmap compareBefore;
    //        Bitmap bmpImage = bmp;

    //        bmpImage.Save(@".\bmpImage.bmp");

    //        int ct = 0;
                       
    //        do
    //        {
    //            int width = bmpImage.Width - margin;
    //            int height = bmpImage.Height - margin;

    //            BitmapData rawOriginal = bmpImage.LockBits(new Rectangle(margin, margin, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
    //            Bitmap CutShrinkImage = AForge.Imaging.Image.Clone(rawOriginal);
    //            bmpImage.UnlockBits(rawOriginal);

    //            FiltersSequence seq = new FiltersSequence();
    //            seq.Add(Grayscale.CommonAlgorithms.BT709);  //First add  GrayScaling filter            
    //            seq.Add(new OtsuThreshold()); //Then add binarization(thresholding) filter
    //            CutShrinkImage = seq.Apply(CutShrinkImage); // Apply filters on source image  , 8비트로 다시 변환하기 위해 그레이스케일 필터 사용

    //            CutShrinkImage.Save(@".\CutShrinkImage.png");

    //            compareBefore = CutShrinkImage;

    //            compareBefore.Save(@".\compareBefore.png");

    //            Shrink shrinkfilter = new Shrink(Color.White);
    //            bmpImage = shrinkfilter.Apply(CutShrinkImage);

    //            bmpImage.Save(@".\bmpImage.png");

    //            compare = bmpImage;

    //            compare.Save(@".\compare" + ct + ".png");

    //            ++ct;
          
    //        }
            
    //        while (compareBefore.Width != compare.Width || compareBefore.Height != compare.Height);  // shrink 전 과 후의 이미지 폭과 높이가 같으면 이미지 반환
    //        {
    //            return compare;
    //        }            
            
    //    }


    //    public static Bitmap cutMargin(Bitmap bmp, int margin)
    //    {
    //        int width = bmp.Width - margin*2;
    //        int height = bmp.Height - margin*2;

    //        BitmapData rawOriginal = bmp.LockBits(new Rectangle(margin, margin, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
    //        Bitmap cutMarginImage = AForge.Imaging.Image.Clone(rawOriginal);
    //        bmp.UnlockBits(rawOriginal);

    //        FiltersSequence seq = new FiltersSequence();
    //        seq.Add(Grayscale.CommonAlgorithms.BT709);  //First add  GrayScaling filter            
    //        seq.Add(new OtsuThreshold()); //Then add binarization(thresholding) filter
    //        cutMarginImage = seq.Apply(cutMarginImage); // Apply filters on source image  , 8비트로 다시 변환하기 위해 그레이스케일 필터 사용

    //        return cutMarginImage;
    //    }

    //    public static Bitmap cutBottom(Bitmap bmp, int margin)
    //    {
    //        int width = bmp.Width;
    //        int height = bmp.Height - margin;

    //        BitmapData rawOriginal = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
    //        Bitmap cutBottomImage = AForge.Imaging.Image.Clone(rawOriginal);
    //        bmp.UnlockBits(rawOriginal);

    //        FiltersSequence seq = new FiltersSequence();
    //        seq.Add(Grayscale.CommonAlgorithms.BT709);  //First add  GrayScaling filter            
    //        seq.Add(new OtsuThreshold()); //Then add binarization(thresholding) filter
    //        cutBottomImage = seq.Apply(cutBottomImage); // Apply filters on source image  , 8비트로 다시 변환하기 위해 그레이스케일 필터 사용

    //        return cutBottomImage;
    //    }



        //public static Bitmap ScaleByPercent(Bitmap imgPhoto, double Percent)
        //{
        //    double nPercent = (Percent / 100);

        //    int sourceWidth = imgPhoto.Width;
        //    int sourceHeight = imgPhoto.Height;
        //    double destWidthF = (sourceWidth * nPercent);            
        //    double destHeightF = (sourceHeight * nPercent);

        //    var destWidth = (Convert.ToInt32(destWidthF))/100000;  // 부동소수점 오차를 줄이기 위해 1000 배로 올려서 연산후, 마지막에 다시 나눠줌, 1~2 픽셀 단위까지 정확해짐            
        //    var destHeight = (Convert.ToInt32(destHeightF))/100000;

        //    var bmPhoto = new Bitmap(destWidth, destHeight,
        //                             PixelFormat.Format24bppRgb);
        //    bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
        //                          imgPhoto.VerticalResolution);

        //    Graphics grPhoto = Graphics.FromImage(bmPhoto);
        //    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //    grPhoto.DrawImage(imgPhoto,
        //                      new Rectangle(0, 0, destWidth, destHeight),
        //                      new Rectangle(0, 0, sourceWidth, sourceHeight),
        //                      GraphicsUnit.Pixel);

        //    grPhoto.Dispose();
        //    return bmPhoto;
        //}

        //public static Bitmap Clone(Bitmap source, PixelFormat format)
        //{
        //    // copy image if pixel format is the same
        //    if (source.PixelFormat == format)
        //    {
        //        return source;
        //    }

        //    int width = source.Width;
        //    int height = source.Height;

        //    // create new image with desired pixel format
        //    Bitmap bitmap = new Bitmap(width, height, format);

        //    // draw source image on the new one using Graphics
        //    Graphics g = Graphics.FromImage(bitmap);
        //    g.DrawImage(source, 0, 0, width, height);
        //    g.Dispose();

        //    return bitmap;
        //}

        public static Bitmap Clone(Bitmap source, PixelFormat format)   // 이미지 픽셀 포맷 조정하기 위한 클론 함수
        {        
            //// copy image if pixel format is the same  //
            //if (source.PixelFormat == format)          // 이 부분을 주석해제하면
            //{                                          // 포맷이 같으면 처리하지 않으므로
            //    return source;                         // 반복 이미지 처리가 안된다.
            //}                                         // 버그 잡느라 한참 헤멨으니 주석 풀지 말것 :-)

            int width = source.Width;
            int height = source.Height;

            // create new image with desired pixel format
            Bitmap bitmap = new Bitmap(width, height, format);

            // draw source image on the new one using Graphics
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(source, 0, 0, width, height);
            g.Dispose();
            
            return bitmap;
        }      


    }
}

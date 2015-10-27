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
using Tesseract;
using System.Drawing;
using AForge.Imaging.Filters;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace PLOCR
{
    class OcrEngine
    {
        public static string ocrDrugCodeDrugNameArea(Bitmap source, int x, int y, int width, int height)  // 특정 좌표 지역을 받아서 그 부분만 판독하는 함수    
        {
            string htext;

            // var PrescriptionImage = CropedPrescription;                                 
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
                //    using (var img = Pix.LoadFromFile(PrescriptionImage)
                //    {
                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고

                using (var page = engine.Process(source, roi, PageSegMode.SingleLine))  // PageSegMode 에서 여러 인식형태를 조정한다.
                {
                    htext = page.GetText();
                    //  System.IO.File.WriteAllText(@"C:\Program Files\PLOCR\textrecognition.html", htext);  // 인식한 글자를 html 형식으로 저장한다.
                    //  Console.WriteLine(htext);
                    //   Console.Read();
                //   htext = TextProcess.RemoveWhiteSpace(htext);
                    //      htext = TextProcess.DotReplace(htext);
                }
                //    }
                return htext;
            }
        }

        public static string ocrSingleChar(Bitmap source, int x, int y, int width, int height)  // 한 문자씩 읽어내고, 투약횟수, 총투약일수 등에 쓰기 좋다.
        {
            string text;

            // var PrescriptionImage = CropedPrescription;
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "0123456789-."); // 숫자와 . - 만 인식하도록 설정                 

                //     using (var img = Pix.LoadFromFile(PrescriptionImage))
                //      {
                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고
                using (var page = engine.Process(source, roi, PageSegMode.SingleChar))// psm 옵션 설정
                {
                    text = page.GetText();

                    text = TextProcess.RemoveWhiteSpace(text);
                    text = TextProcess.DotReplace(text);
                    text = TextProcess.PointInsert(text);

                    //      Console.WriteLine("인식한 문자: \n{0}\n", text);
                    //   Console.Read();
                }
                //         }
            }
            return text;
        }

        public static string ocrDigitLine(Bitmap source, int x, int y, int width, int height)  // 한줄씩 읽어내고 "-" --> "." 으로 치환하고, 공백 제거등, 투약량에 쓰기 좋다.
        {
            string text;

            // var PrescriptionImage = CropedPrescription;
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "0123456789-."); // 숫자와 . - 만 인식하도록 설정                 
                
                //     using (var img = Pix.LoadFromFile(PrescriptionImage))
                //      {
                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고
                using (var page = engine.Process(source, roi, PageSegMode.SingleLine)) // psm 옵션 설정
                {
                    text = page.GetText();

                    text = TextProcess.RemoveWhiteSpace(text);
              //      text = TextProcess.DotReplace(text);
               //     text = TextProcess.PointInsert(text);

                    //      Console.WriteLine("인식한 문자: \n{0}\n", text);
                    //   Console.Read();
                }
                //         }
            }
            return text;
        }

        public static string ocrDoseLine(Bitmap source, int x, int y, int width, int height)  // 한줄씩 읽어내고 "-" --> "." 으로 치환하고, 공백 제거등, 투약량에 쓰기 좋다.
        {
            string text;

            // var PrescriptionImage = CropedPrescription;
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "0123456789-."); // 숫자와 . - 만 인식하도록 설정                 

                //     using (var img = Pix.LoadFromFile(PrescriptionImage))
                //      {
                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고
                using (var page = engine.Process(source, roi, PageSegMode.SingleLine)) // psm 옵션 설정
                {
                    text = page.GetText();

                    text = TextProcess.RemoveWhiteSpace(text);
                    text = TextProcess.DotReplace(text);
              //      text = TextProcess.decimalInsert(text);
                    text = TextProcess.PointInsert(text);
                    text = TextProcess.ZeroInsert(text);

                    //      Console.WriteLine("인식한 문자: \n{0}\n", text);
                    //   Console.Read();
                }
                //         }
            }
            return text;
        }

        public static string hocrRect(Bitmap source, int x, int y, int width, int height)  // 특정 좌표 지역을 받아서 그 부분만 판독하는 함수    
        {
            string htext;

            // var PrescriptionImage = CropedPrescription;                                 
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
                //    using (var img = Pix.LoadFromFile(PrescriptionImage)
                //    {
                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고

                using (var page = engine.Process(source, roi, PageSegMode.Auto))  // PageSegMode 에서 여러 인식형태를 조정한다.
                {
                    htext = page.GetHOCRText(3);
                    System.IO.File.WriteAllText(@"C:\Program Files\PLOCR\textrecognition.html", htext);  // 인식한 글자를 html 형식으로 저장한다.
                    //  Console.WriteLine(htext);
                    //   Console.Read();
                }
                //    }
                return htext;
            }
        }

        public static string ocrTextLine(Bitmap source, int x, int y, int width, int height)  // 특정 좌표 지역을 받아서 그 부분만 판독하는 함수    
        {
            string htext;

            // var PrescriptionImage = CropedPrescription;                                 
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
                //    using (var img = Pix.LoadFromFile(PrescriptionImage)
                //    {
                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고

                using (var page = engine.Process(source, roi, PageSegMode.SingleLine))  // PageSegMode 에서 여러 인식형태를 조정한다.
                {
                    htext = page.GetText();
                  //  System.IO.File.WriteAllText(@"C:\Program Files\PLOCR\textrecognition.html", htext);  // 인식한 글자를 html 형식으로 저장한다.
                    //  Console.WriteLine(htext);
                    //   Console.Read();
                   htext = TextProcess.RemoveWhiteSpace(htext);
              //      htext = TextProcess.DotReplace(htext);
                }
                //    }
                return htext;
            }
        }


      //  var PrescriptionImage = ".\prescription.png";
        public static string ocrDigit(Bitmap CropedPrescription, int x, int y, int width, int height)
        {
            string text;          

            // var PrescriptionImage = CropedPrescription;
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
           //     engine.SetVariable("tessedit_char_whitelist", "0123456789-."); // 숫자와 . - 만 인식하도록 설정             

                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고
           //     using (var img = Pix.LoadFromFile(PrescriptionImage))
          //      {
                using (var page = engine.Process(CropedPrescription, roi, PageSegMode.Auto))
                    {
                         text = page.GetText();

                         text = TextProcess.RemoveWhiteSpace(text);
                         

                  //      Console.WriteLine("인식한 문자: \n{0}\n", text);
                     //   Console.Read();
                    }
       //         }
            }

            return text;
        }


        //public static string hocr(Bitmap CropedPrescription)     // 초기 한번 돌려서 좌표영점을 찾는다.
        //{
        //    string htext;

        //    // var PrescriptionImage = CropedPrescription;                                 
        //    using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
        //    {
        //        //    using (var img = Pix.LoadFromFile(PrescriptionImage)
        //        //    {
        //        using (var page = engine.Process(CropedPrescription, PageSegMode.Auto))
        //        {
        //            htext = page.GetHOCRText(3);
        //                System.IO.File.WriteAllText(@".\textrecognition.html", htext);  // 인식한 글자를 html 형식으로 저장한다.
        //            //  Console.WriteLine(htext);
        //            //   Console.Read();
        //        }
        //        //    }
        //    }
         
        //    return htext;
        //}

        public static void hocrQuad(Bitmap source, int x, int y, int width, int height)  // 특정 좌표 지역을 받아서 그 부분만 판독하는 함수    
        {
            string htext;

            // var PrescriptionImage = CropedPrescription;                                 
            using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
            {
                //    using (var img = Pix.LoadFromFile(PrescriptionImage)
                //    {


                var roi = new Rect(x, y, width, height); // region of interest 좌표를 생성하고

                using (var page = engine.Process(source, roi, PageSegMode.Auto))
                {
                    htext = page.GetHOCRText(3);
                    System.IO.File.WriteAllText(@".\textrecognition.html", htext);  // 인식한 글자를 html 형식으로 저장한다.
                    //  Console.WriteLine(htext);
                    //   Console.Read();
                }
                //    }
            }
        }



        //public static string ocrPsm(Bitmap CropedPrescription)
        //{
        //    string text;

        //    // var PrescriptionImage = CropedPrescription;
        //    using (var engine = new TesseractEngine(@"C:\Program Files\Tesseract-OCR\tessdata\", "kor", EngineMode.Default))
        //    {
        //        engine.SetVariable("tessedit_char_whitelist", "0123456789-."); // 숫자와 . - 만 인식하도록 설정                 
                
            
        //        //     using (var img = Pix.LoadFromFile(PrescriptionImage))
        //        //      {
        //        using (var page = engine.Process(CropedPrescription, PageSegMode.SingleLine)) // psm 옵션 설정
        //        {
        //            text = page.GetText();

        //            text = TextProcess.RemoveWhiteSpace(text);
        //            text = TextProcess.DotReplace(text);
        //            text = TextProcess.PointInsert(text);                                     

                    

        //            //      Console.WriteLine("인식한 문자: \n{0}\n", text);
        //            //   Console.Read();
        //        }
        //        //         }
        //    }

        //    return text;
        //}


        


        //public static string[] segOcr(Bitmap[] ImageArray, out int Endct)
        //{
        //    int ct = 0;            

        //    String[] tText;            
        //    tText = new String[13];  // 약품코드, 투약량, 횟수, 일수의 세로 13칸을 텍스트 배열로 넣기위해
                
        //    do
        //    {
        //        tText[ct] = ocrPsm(ImageArray[ct]);  // 루프 돌면서 약품코드, 투약량, 횟수, 일수 텍스트 인식

        //    //    tText[ct] = Regex.Replace(tText[ct], @"\s+", string.Empty, RegexOptions.Multiline).TrimEnd(); // 라인의 공백제거                                

        //  //      Console.WriteLine("약품코드, 투약량, 투여횟수, 투여일수 {0:x} : {1:x}",ct, tText[ct]);

        //        if (string.IsNullOrEmpty(tText[ct]) == true)
        //        {                   
        //            break;
        //        }

        //        ++ct;
        //    }

        //    while ( ct < 13 );
        //    {
        //        Endct = ct - 1;
        //        return tText;   // tText 배열로 텍스트 반환
        //    }
        //}

       

    }
}

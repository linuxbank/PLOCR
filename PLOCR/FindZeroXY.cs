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
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace PLOCR
{   
    class FindZeroXY
    {
        public static void startXYRect(string HtmlText, string standardText, out int standardX, out int standardY, out int standardWidth, out int standardHeight)   // 좌표1,2번의 문자를 주고, 해당 문자의 x,y 좌표를 반환한다.
        {
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            int ZeroIndex = HtmlText.IndexOf(standardText);  // 주어진 문자열로 기준이 되는 문자열 인덱스를 찾고,      
            if (ZeroIndex == -1)
            {
                MessageBox.Show("해당 좌표를 찾지 못했습니다.\n 다른 좌표값으로 찾아보세요.");
                System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
            }

            if (ZeroIndex != -1)
            {
                string ZeroRange = HtmlText.Substring(ZeroIndex - 30, 30); // 기준 문자열부터 앞쪽으로 30 인덱스 부분부터 기준까지의 문자열을 선택하고,   


                char[] delimiterChars = { ' ' };
                string[] ZeroDiv = ZeroRange.Split(delimiterChars);  // 공백을 기준으로 문자열을 분리하고                                   



                for (int ct = 0; ct < ZeroDiv.Length; ct++)
                {
                    if (Regex.IsMatch(ZeroDiv[ct], @"^-?\d+$"))  // 숫자인지 아닌지 판단
                    {
                        x1 = int.Parse(Regex.Replace(ZeroDiv[ct], "[^0-9.-]", ""));      // 숫자만 추출하여, x좌표 문자형 숫자를 정수형으로 형변환
                        y1 = int.Parse(Regex.Replace(ZeroDiv[ct + 1], "[^0-9.-]", ""));    // 숫자만 추출하여, y좌표 문자형 숫자를 정수형으로 형변환
                        x2 = int.Parse(Regex.Replace(ZeroDiv[ct + 2], "[^0-9.-]", ""));
                        y2 = int.Parse(Regex.Replace(ZeroDiv[ct + 3], "[^0-9.-]", ""));

                        //      Console.WriteLine(x1);
                        //      Console.WriteLine(y1);
                        //      Console.WriteLine(x2);
                        //      Console.WriteLine(y2);

                        ct = ZeroDiv.Length;  // 숫자요소를 가진 배열을 찾으면 루프 탈출                        
                    }
                }
               // MessageBox.Show("좌표를 찾았습니다!");
            }

            standardWidth = x2 - x1;
            standardHeight = y2 - y1;
            standardX = x1;
            standardY = y1;

            //    Console.WriteLine(XYzeroWidth);
        }

        public static void divSearch1(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @".\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////

            string coordAreaNo1 = ini.GetIniValue("지정한 사분면", "좌표1번사분면");
            string standardNo1 = ini.GetIniValue("좌표텍스트", "좌표1번");

            if (standardNo1 != "")
            {
                if (coordAreaNo1 == "1사분면")
                {
                    int x = (int)calculator.quadrantX(1);
                    int y = 0;

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNo1, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo1X = standardX;
                    Global.coordNo1Y = standardY;
                }
                else if (coordAreaNo1 == "2사분면")
                {
                    int x = 0;
                    int y = 0;

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNo1, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo1X = standardX;
                    Global.coordNo1Y = standardY;
                }
                else if (coordAreaNo1 == "3사분면")
                {
                    int x = 0;
                    int y = (int)calculator.quadrantY(3);

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNo1, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo1X = standardX;
                    Global.coordNo1Y = standardY;
                }
                else if (coordAreaNo1 == "4사분면")
                {
                    int x = (int)calculator.quadrantX(4);
                    int y = (int)calculator.quadrantX(4);

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNo1, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo1X = standardX;
                    Global.coordNo1Y = standardY;
                }
                else if (coordAreaNo1 == "특정영역1")
                {
                    int x = int.Parse(ini.GetIniValue("특정좌표영역1", "X"));
                    int y = int.Parse(ini.GetIniValue("특정좌표영역1", "Y"));
                    int width = int.Parse(ini.GetIniValue("특정좌표영역1", "가로"));
                    int height = int.Parse(ini.GetIniValue("특정좌표영역1", "세로"));

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, width, height);

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNo1, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo1X = standardX;
                    Global.coordNo1Y = standardY;
                }
                else if (coordAreaNo1 == "특정영역2")
                {
                    int x = int.Parse(ini.GetIniValue("특정좌표영역2", "X"));
                    int y = int.Parse(ini.GetIniValue("특정좌표영역2", "Y"));
                    int width = int.Parse(ini.GetIniValue("특정좌표영역2", "가로"));
                    int height = int.Parse(ini.GetIniValue("특정좌표영역2", "세로"));

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, width, height);

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNo1, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo1X = standardX;
                    Global.coordNo1Y = standardY;
                }
                else if (coordAreaNo1 == "전체영역")
                {
                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, 0, 0, source.Width, source.Height);

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNo1, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo1X = standardX;
                    Global.coordNo1Y = standardY;
                }
            }
            else
            {
                MessageBox.Show("환경설정에서 좌표 1번을 등록해주세요.");    // 기준좌표 1번이 등록되지 않은 경우의 처리
                System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
             
            }
        }


        public static void divSearch2(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @".\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////
           
            string coordAreaNo2 = ini.GetIniValue("지정한 사분면", "좌표2번사분면");
            string standardNO2 = ini.GetIniValue("좌표텍스트", "좌표2번");

            if (standardNO2 != "")      // 기준좌표 2번이 등록되어 있는 경우에만 처리하도록
            {
                if (coordAreaNo2 == "1사분면")
                {
                    int x = (int)calculator.quadrantX(1);
                    int y = 0;

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNO2, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo2X = standardX;
                    Global.coordNo2Y = standardY;
                }
                else if (coordAreaNo2 == "2사분면")
                {
                    int x = 0;
                    int y = 0;

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNO2, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo2X = standardX;
                    Global.coordNo2Y = standardY;
                }
                else if (coordAreaNo2 == "3사분면")
                {
                    int x = 0;
                    int y = (int)calculator.quadrantY(3);

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNO2, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo2X = standardX;
                    Global.coordNo2Y = standardY;
                }
                else if (coordAreaNo2 == "4사분면")
                {
                    int x = (int)calculator.quadrantX(4);
                    int y = (int)calculator.quadrantX(4);

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, (int)(source.Width / 2), (int)(source.Height / 2));

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNO2, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo2X = standardX;
                    Global.coordNo2Y = standardY;
                }
                else if (coordAreaNo2 == "특정영역1")
                {
                    int x = int.Parse(ini.GetIniValue("특정좌표영역1", "X"));
                    int y = int.Parse(ini.GetIniValue("특정좌표영역1", "Y"));
                    int width = int.Parse(ini.GetIniValue("특정좌표영역1", "가로"));
                    int height = int.Parse(ini.GetIniValue("특정좌표영역1", "세로"));

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, width, height);

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNO2, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo2X = standardX;
                    Global.coordNo2Y = standardY;
                }
                else if (coordAreaNo2 == "특정영역2")
                {
                    int x = int.Parse(ini.GetIniValue("특정좌표영역2", "X"));
                    int y = int.Parse(ini.GetIniValue("특정좌표영역2", "Y"));
                    int width = int.Parse(ini.GetIniValue("특정좌표영역2", "가로"));
                    int height = int.Parse(ini.GetIniValue("특정좌표영역2", "세로"));

                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, x, y, width, height);

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNO2, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo2X = standardX;
                    Global.coordNo2Y = standardY;
                }
                else if (coordAreaNo2 == "전체영역")
                {
                    //string path = @"C:\Program Files\PLOCR\prescription.png";
                    //Bitmap source = (Bitmap)Bitmap.FromFile(path);
                    string htext = OcrEngine.hocrRect(source, 0, 0, source.Width, source.Height);

                    int standardX, standardY, standardWidth, standardHeight;
                    startXYRect(htext, standardNO2, out standardX, out standardY, out standardWidth, out standardHeight);

                    Global.coordNo2X = standardX;
                    Global.coordNo2Y = standardY;
                }
            }
            else
            {
                MessageBox.Show("환경설정에서 좌표 2번을 등록해주세요.");    // 기준좌표 2번이 등록되지 않은 경우 처리해야함
                System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료       
            }
        }
        
        
     //   public static void zeroSearch(string HtmlText,
     //       out int zeroWidth, 
     //       out int zeroHeight, 
     //       out int ScaleX, 
     //       out int ScaleY,
     //       out int distancePatientX, out int distancePatientY, out int sizePatientHor, out int sizePatientVer,
     //       out int distanceDoctorX, out int distanceDoctorY, out int sizeDoctorHor, out int sizeDoctorVer,
     //       out int distancePreNumX, out int distancePreNumY, out int sizePreNumHor, out int sizePreNumVer,
     //       out int distanceDrugCodeX, out int distanceDrugCodeY, out int sizeDrugCodeHor, out int sizeDrugCodeVer,
     //       out int distanceDoseX, out int distanceDoseY, out int sizeDoseHor, out int sizeDoseVer,
     //       out int distanceTimeX, out int distanceTimeY, out int sizeTimeHor, out int sizeTimeVer,
     //       out int distanceDayX, out int distanceDayY, out int sizeDayHor, out int sizeDayVer,
     //       out int jumpY,
     //       out int factor)
     //   {
     //       ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
     //       //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
     //       FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
     //       string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
     //       string fileName = @".\PLOCRconfig.ini";  // 환경설정 파일명
     //       string filePath = pathini + fileName;   //ini 파일 경로
     //       PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
     //       //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////
            
     //       String CatchText1 = ini.GetIniValue("좌표영점", "1번좌표");
     //       int factor1 = int.Parse(ini.GetIniValue("좌표영점", "1번좌표factor"));
     //       MessageBox.Show(CatchText1.ToString(), factor1.ToString());
     //       String CatchText2 = ini.GetIniValue("좌표영점", "2번좌표");
     //       int factor2 = int.Parse(ini.GetIniValue("좌표영점", "2번좌표factor"));
     //       MessageBox.Show(CatchText2.ToString(), factor2.ToString());
     //       String CatchText3 = ini.GetIniValue("좌표영점", "3번좌표");
     //       int factor3 = int.Parse(ini.GetIniValue("좌표영점", "3번좌표factor"));
     //       MessageBox.Show(CatchText3.ToString(), factor3.ToString());

     //       int XYzeroWidth = 0, XYzeroHeight = 0, XYScaleX = 0, XYScaleY = 0;

     //       int PdistancePatientX = 0, PdistancePatientY = 0, PsizePatientHor = 0, PsizePatientVer = 0;
     //       int PdistanceDoctorX=0, PdistanceDoctorY=0, PsizeDoctorHor=0, PsizeDoctorVer=0;
     //       int PdistancePreNumX=0, PdistancePreNumY=0, PsizePreNumHor=0, PsizePreNumVer=0;
     //       int PdistanceDrugCodeX = 0, PdistanceDrugCodeY = 0, PsizeDrugCodeHor = 0, PsizeDrugCodeVer = 0;
     //       int PdistanceDoseX=0, PdistanceDoseY=0, PsizeDoseHor=0, PsizeDoseVer=0;
     //       int PdistanceTimeX=0, PdistanceTimeY=0, PsizeTimeHor=0, PsizeTimeVer=0;
     //       int PdistanceDayX=0, PdistanceDayY=0, PsizeDayHor=0, PsizeDayVer=0;
     //       int PjumpY=0;
     //       int Pfactor=0;

     //       if (HtmlText.Contains(CatchText1))
     //       {
     //           ZeroXY(HtmlText, CatchText1, factor1, out XYzeroWidth, out XYzeroHeight, out XYScaleX, out XYScaleY);

     //           PdistancePatientX = int.Parse(ini.GetIniValue(CatchText1, "distancePatientX"));
     //           PdistancePatientY = int.Parse(ini.GetIniValue(CatchText1, "distancePatientY"));
     //           PsizePatientHor = int.Parse(ini.GetIniValue(CatchText1, "sizePatientHor"));
     //           PsizePatientVer = int.Parse(ini.GetIniValue(CatchText1, "sizePatientVer"));
     //           PdistanceDoctorX = int.Parse(ini.GetIniValue(CatchText1, "distanceDoctorX"));
     //           PdistanceDoctorY = int.Parse(ini.GetIniValue(CatchText1, "distanceDoctorY"));
     //           PsizeDoctorHor = int.Parse(ini.GetIniValue(CatchText1, "sizeDoctorHor"));
     //           PsizeDoctorVer = int.Parse(ini.GetIniValue(CatchText1, "sizeDoctorVer"));
     //           PdistancePreNumX = int.Parse(ini.GetIniValue(CatchText1, "distancePreNumX"));
     //           PdistancePreNumY = int.Parse(ini.GetIniValue(CatchText1, "distancePreNumY"));
     //           PsizePreNumHor = int.Parse(ini.GetIniValue(CatchText1, "sizePreNumHor"));
     //           PsizePreNumVer = int.Parse(ini.GetIniValue(CatchText1, "sizePreNumVer"));
     //           PdistanceDrugCodeX = int.Parse(ini.GetIniValue(CatchText1, "distanceDrugCodeX"));
     //           PdistanceDrugCodeY = int.Parse(ini.GetIniValue(CatchText1, "distanceDrugCodeY"));
     //           PsizeDrugCodeHor = int.Parse(ini.GetIniValue(CatchText1, "sizeDrugCodeHor"));
     //           PsizeDrugCodeVer = int.Parse(ini.GetIniValue(CatchText1, "sizeDrugCodeVer"));
     //           PdistanceDoseX = int.Parse(ini.GetIniValue(CatchText1, "distanceDoseX"));
     //           PdistanceDoseY = int.Parse(ini.GetIniValue(CatchText1, "distanceDoseY"));
     //           PsizeDoseHor = int.Parse(ini.GetIniValue(CatchText1, "sizeDoseHor"));
     //           PsizeDoseVer = int.Parse(ini.GetIniValue(CatchText1, "sizeDoseVer"));
     //           PdistanceTimeX = int.Parse(ini.GetIniValue(CatchText1, "distanceTimeX"));
     //           PdistanceTimeY = int.Parse(ini.GetIniValue(CatchText1, "distanceTimeY"));
     //           PsizeTimeHor = int.Parse(ini.GetIniValue(CatchText1, "sizeTimeHor"));
     //           PsizeTimeVer = int.Parse(ini.GetIniValue(CatchText1, "sizeTimeVer"));
     //           PdistanceDayX = int.Parse(ini.GetIniValue(CatchText1, "distanceDayX"));
     //           PdistanceDayY = int.Parse(ini.GetIniValue(CatchText1, "distanceDayY"));
     //           PsizeDayHor = int.Parse(ini.GetIniValue(CatchText1, "sizeDayHor"));
     //           PsizeDayVer = int.Parse(ini.GetIniValue(CatchText1, "sizeDayVer"));
     //           PjumpY = int.Parse(ini.GetIniValue(CatchText1, "jumpY"));
     //           Pfactor = int.Parse(ini.GetIniValue(CatchText1, "factor"));

     //    //       Console.WriteLine("1번좌표로 영점을 잡았습니다. \n");
     //       }
     //       else if (HtmlText.Contains(CatchText2))
     //       {
     //           ZeroXY(HtmlText, CatchText2, factor2, out XYzeroWidth, out XYzeroHeight, out XYScaleX, out XYScaleY);

     //           PdistancePatientX = int.Parse(ini.GetIniValue(CatchText2, "distancePatientX"));
     //           PdistancePatientY = int.Parse(ini.GetIniValue(CatchText2, "distancePatientY"));
     //           PsizePatientHor = int.Parse(ini.GetIniValue(CatchText2, "sizePatientHor"));
     //           PsizePatientVer = int.Parse(ini.GetIniValue(CatchText2, "sizePatientVer"));
     //           PdistanceDoctorX = int.Parse(ini.GetIniValue(CatchText2, "distanceDoctorX"));
     //           PdistanceDoctorY = int.Parse(ini.GetIniValue(CatchText2, "distanceDoctorY"));
     //           PsizeDoctorHor = int.Parse(ini.GetIniValue(CatchText2, "sizeDoctorHor"));
     //           PsizeDoctorVer = int.Parse(ini.GetIniValue(CatchText2, "sizeDoctorVer"));
     //           PdistancePreNumX = int.Parse(ini.GetIniValue(CatchText2, "distancePreNumX"));
     //           PdistancePreNumY = int.Parse(ini.GetIniValue(CatchText2, "distancePreNumY"));
     //           PsizePreNumHor = int.Parse(ini.GetIniValue(CatchText2, "sizePreNumHor"));
     //           PsizePreNumVer = int.Parse(ini.GetIniValue(CatchText2, "sizePreNumVer"));
     //           PdistanceDrugCodeX = int.Parse(ini.GetIniValue(CatchText2, "distanceDrugCodeX"));
     //           PdistanceDrugCodeY = int.Parse(ini.GetIniValue(CatchText2, "distanceDrugCodeY"));
     //           PsizeDrugCodeHor = int.Parse(ini.GetIniValue(CatchText2, "sizeDrugCodeHor"));
     //           PsizeDrugCodeVer = int.Parse(ini.GetIniValue(CatchText2, "sizeDrugCodeVer"));
     //           PdistanceDoseX = int.Parse(ini.GetIniValue(CatchText2, "distanceDoseX"));
     //           PdistanceDoseY = int.Parse(ini.GetIniValue(CatchText2, "distanceDoseY"));
     //           PsizeDoseHor = int.Parse(ini.GetIniValue(CatchText2, "sizeDoseHor"));
     //           PsizeDoseVer = int.Parse(ini.GetIniValue(CatchText2, "sizeDoseVer"));
     //           PdistanceTimeX = int.Parse(ini.GetIniValue(CatchText2, "distanceTimeX"));
     //           PdistanceTimeY = int.Parse(ini.GetIniValue(CatchText2, "distanceTimeY"));
     //           PsizeTimeHor = int.Parse(ini.GetIniValue(CatchText2, "sizeTimeHor"));
     //           PsizeTimeVer = int.Parse(ini.GetIniValue(CatchText2, "sizeTimeVer"));
     //           PdistanceDayX = int.Parse(ini.GetIniValue(CatchText2, "distanceDayX"));
     //           PdistanceDayY = int.Parse(ini.GetIniValue(CatchText2, "distanceDayY"));
     //           PsizeDayHor = int.Parse(ini.GetIniValue(CatchText2, "sizeDayHor"));
     //           PsizeDayVer = int.Parse(ini.GetIniValue(CatchText2, "sizeDayVer"));
     //           PjumpY = int.Parse(ini.GetIniValue(CatchText2, "jumpY"));
     //           Pfactor = int.Parse(ini.GetIniValue(CatchText2, "factor"));

     //    //       Console.WriteLine("2번좌표로 영점을 잡았습니다. \n");
     //       }
     //       else if(HtmlText.Contains(CatchText3))
     //       {
     //           ZeroXY(HtmlText, CatchText3, factor3, out XYzeroWidth, out XYzeroHeight, out XYScaleX, out XYScaleY);

     //           PdistancePatientX = int.Parse(ini.GetIniValue(CatchText3, "distancePatientX"));
     //           PdistancePatientY = int.Parse(ini.GetIniValue(CatchText3, "distancePatientY"));
     //           PsizePatientHor = int.Parse(ini.GetIniValue(CatchText3, "sizePatientHor"));
     //           PsizePatientVer = int.Parse(ini.GetIniValue(CatchText3, "sizePatientVer"));
     //           PdistanceDoctorX = int.Parse(ini.GetIniValue(CatchText3, "distanceDoctorX"));
     //           PdistanceDoctorY = int.Parse(ini.GetIniValue(CatchText3, "distanceDoctorY"));
     //           PsizeDoctorHor = int.Parse(ini.GetIniValue(CatchText3, "sizeDoctorHor"));
     //           PsizeDoctorVer = int.Parse(ini.GetIniValue(CatchText3, "sizeDoctorVer"));
     //           PdistancePreNumX = int.Parse(ini.GetIniValue(CatchText3, "distancePreNumX"));
     //           PdistancePreNumY = int.Parse(ini.GetIniValue(CatchText3, "distancePreNumY"));
     //           PsizePreNumHor = int.Parse(ini.GetIniValue(CatchText3, "sizePreNumHor"));
     //           PsizePreNumVer = int.Parse(ini.GetIniValue(CatchText3, "sizePreNumVer"));
     //           PdistanceDrugCodeX = int.Parse(ini.GetIniValue(CatchText3, "distanceDrugCodeX"));
     //           PdistanceDrugCodeY = int.Parse(ini.GetIniValue(CatchText3, "distanceDrugCodeY"));
     //           PsizeDrugCodeHor = int.Parse(ini.GetIniValue(CatchText3, "sizeDrugCodeHor"));
     //           PsizeDrugCodeVer = int.Parse(ini.GetIniValue(CatchText3, "sizeDrugCodeVer"));
     //           PdistanceDoseX = int.Parse(ini.GetIniValue(CatchText3, "distanceDoseX"));
     //           PdistanceDoseY = int.Parse(ini.GetIniValue(CatchText3, "distanceDoseY"));
     //           PsizeDoseHor = int.Parse(ini.GetIniValue(CatchText3, "sizeDoseHor"));
     //           PsizeDoseVer = int.Parse(ini.GetIniValue(CatchText3, "sizeDoseVer"));
     //           PdistanceTimeX = int.Parse(ini.GetIniValue(CatchText3, "distanceTimeX"));
     //           PdistanceTimeY = int.Parse(ini.GetIniValue(CatchText3, "distanceTimeY"));
     //           PsizeTimeHor = int.Parse(ini.GetIniValue(CatchText3, "sizeTimeHor"));
     //           PsizeTimeVer = int.Parse(ini.GetIniValue(CatchText3, "sizeTimeVer"));
     //           PdistanceDayX = int.Parse(ini.GetIniValue(CatchText3, "distanceDayX"));
     //           PdistanceDayY = int.Parse(ini.GetIniValue(CatchText3, "distanceDayY"));
     //           PsizeDayHor = int.Parse(ini.GetIniValue(CatchText3, "sizeDayHor"));
     //           PsizeDayVer = int.Parse(ini.GetIniValue(CatchText3, "sizeDayVer"));
     //           PjumpY = int.Parse(ini.GetIniValue(CatchText3, "jumpY"));
     //           Pfactor = int.Parse(ini.GetIniValue(CatchText3, "factor"));

     //    //       Console.WriteLine("3번좌표로 영점을 잡았습니다. \n");
     //       }
     //       else
     //       {
     //           MessageBox.Show("좌표영점 인식에 실패했습니다.\n 좌표영점을 더 추가해서\n 인식률을 개선시켜 놓을께요.");
     ////           Console.WriteLine("ocr 좌표영점 인식에 실패했습니다.");
     //       }

     //       zeroWidth = XYzeroWidth; zeroHeight = XYzeroHeight; ScaleX = XYScaleX; ScaleY = XYScaleY;

     //       distancePatientX=PdistancePatientX; distancePatientY=PdistancePatientY; sizePatientHor=PsizePatientHor; sizePatientVer=PsizePatientVer;
     //       distanceDoctorX=PdistanceDoctorX; distanceDoctorY=PdistanceDoctorY; sizeDoctorHor=PsizeDoctorHor; sizeDoctorVer=PsizeDoctorVer;
     //       distancePreNumX = PdistancePreNumX; distancePreNumY = PdistancePreNumY; sizePreNumHor = PsizePreNumHor; sizePreNumVer = PsizePreNumVer;
     //       distanceDrugCodeX = PdistanceDrugCodeX; distanceDrugCodeY = PdistanceDrugCodeY; sizeDrugCodeHor = PsizeDrugCodeHor; sizeDrugCodeVer = PsizeDrugCodeVer;
     //       distanceDoseX = PdistanceDoseX; distanceDoseY = PdistanceDoseY; sizeDoseHor = PsizeDoseHor; sizeDoseVer = PsizeDoseVer;
     //       distanceTimeX=PdistanceTimeX; distanceTimeY=PdistanceTimeY; sizeTimeHor=PsizeTimeHor; sizeTimeVer=PsizeTimeVer;
     //       distanceDayX=PdistanceDayX; distanceDayY=PdistanceDayY; sizeDayHor=PsizeDayHor; sizeDayVer=PsizeDayVer;
     //       jumpY=PjumpY;
     //       factor=Pfactor;
     //   }



        //public static void ZeroXY(
        //    string HtmlText,  
        //    string CatchText,
        //    int factor,
        //    out int XYzeroWidth, 
        //    out int XYzeroHeight, 
        //    out int XYScaleX, 
        //    out int XYScaleY)   // 찾고자 하는 좌표영점의 문자를 주고, Html 텍스트 중에서 해당문자의 x,y 좌표를 반환한다.
        //{            
        //    int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
           
        //    int ZeroIndex = HtmlText.IndexOf(CatchText);  // 주어진 문자열로 기준이 되는 문자열 인덱스를 찾고,
        //    string ZeroRange = HtmlText.Substring(ZeroIndex - 30, 30); // 기준 문자열부터 앞쪽으로 30 인덱스 부분부터 기준까지의 문자열을 선택하고,

        //  //     Console.WriteLine(ZeroRange);

        //    char[] delimiterChars = { ' ' };
        //    string[] ZeroDiv = ZeroRange.Split(delimiterChars);  // 공백을 기준으로 문자열을 분리하고                                   

        //    for (int ct = 0; ct < ZeroDiv.Length; ct++)
        //    {
        //        if (Regex.IsMatch(ZeroDiv[ct], @"^-?\d+$"))  // 숫자인지 아닌지 판단
        //        {                      
        //            x1 = int.Parse(Regex.Replace(ZeroDiv[ct], "[^0-9.-]", ""));      // 숫자만 추출하여, x좌표 문자형 숫자를 정수형으로 형변환
        //            y1 = int.Parse(Regex.Replace(ZeroDiv[ct + 1], "[^0-9.-]", ""));    // 숫자만 추출하여, y좌표 문자형 숫자를 정수형으로 형변환
        //            x2 = int.Parse(Regex.Replace(ZeroDiv[ct+2], "[^0-9.-]", ""));
        //            y2 = int.Parse(Regex.Replace(ZeroDiv[ct+3], "[^0-9.-]", ""));

        //      //      Console.WriteLine(x1);
        //      //      Console.WriteLine(y1);
        //      //      Console.WriteLine(x2);
        //      //      Console.WriteLine(y2);
                
        //            ct = ZeroDiv.Length;  // 숫자요소를 가진 배열을 찾으면 루프 탈출                        
        //        }
        //    }          
           
        //    XYzeroWidth = x2 - x1;
        //    XYzeroHeight = y2 - y1;
        //    XYScaleX = x1 * (factor / 10000000) / XYzeroWidth;
        //    XYScaleY = y1 * (factor / 10000000) / XYzeroWidth;

        ////    Console.WriteLine(XYzeroWidth);
        //}


        public static void standardSearch(string HtmlText, int quad, out int x, out int y)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @".\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////                        
                                    
            string standardY1 = ini.GetIniValue("좌표1번", "Y");
            string standardX2 = ini.GetIniValue("좌표2번", "X");
            string standardY2 = ini.GetIniValue("좌표2번", "Y");
            string standardText1 = ini.GetIniValue("좌표텍스트", "좌표1번");
            string standardText2 = ini.GetIniValue("좌표텍스트", "좌표1번");

            int standardX, standardY, standardWidth, standardHeight;

            startXY(HtmlText, standardText1, out standardX, out standardY, out standardWidth, out standardHeight);

            x = standardX; y = standardY;           
        }


        public static void startXY(string HtmlText, string standardText, out int standardX, out int standardY, out int standardWidth, out int standardHeight)   // 좌표1,2번의 문자를 주고, 해당 문자의 x,y 좌표를 반환한다.
        {
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            int ZeroIndex = HtmlText.IndexOf(standardText);  // 주어진 문자열로 기준이 되는 문자열 인덱스를 찾고,
            string ZeroRange = HtmlText.Substring(ZeroIndex - 30, 30); // 기준 문자열부터 앞쪽으로 30 인덱스 부분부터 기준까지의 문자열을 선택하고,

            //     Console.WriteLine(ZeroRange);

            char[] delimiterChars = { ' ' };
            string[] ZeroDiv = ZeroRange.Split(delimiterChars);  // 공백을 기준으로 문자열을 분리하고                                   

            for (int ct = 0; ct < ZeroDiv.Length; ct++)
            {
                if (Regex.IsMatch(ZeroDiv[ct], @"^-?\d+$"))  // 숫자인지 아닌지 판단
                {
                    x1 = int.Parse(Regex.Replace(ZeroDiv[ct], "[^0-9.-]", ""));      // 숫자만 추출하여, x좌표 문자형 숫자를 정수형으로 형변환
                    y1 = int.Parse(Regex.Replace(ZeroDiv[ct + 1], "[^0-9.-]", ""));    // 숫자만 추출하여, y좌표 문자형 숫자를 정수형으로 형변환
                    x2 = int.Parse(Regex.Replace(ZeroDiv[ct + 2], "[^0-9.-]", ""));
                    y2 = int.Parse(Regex.Replace(ZeroDiv[ct + 3], "[^0-9.-]", ""));

                    //      Console.WriteLine(x1);
                    //      Console.WriteLine(y1);
                    //      Console.WriteLine(x2);
                    //      Console.WriteLine(y2);

                    ct = ZeroDiv.Length;  // 숫자요소를 가진 배열을 찾으면 루프 탈출                        
                }
            }

            standardWidth = x2 - x1;
            standardHeight = y2 - y1;
            standardX = x1;
            standardY = y1;

            //    Console.WriteLine(XYzeroWidth);
        }
    }
}

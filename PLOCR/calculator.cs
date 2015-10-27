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
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace PLOCR
{
    class calculator
    {
        public static string getDrugCodeDrugNameArea(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 ///////////////////////////////////////////////////////// 

            int x = int.Parse(ini.GetIniValue("약품영역지정", "X"));
            int y = int.Parse(ini.GetIniValue("약품영역지정", "Y"));
            int width = int.Parse(ini.GetIniValue("약품영역지정", "가로"));
            int height = int.Parse(ini.GetIniValue("약품영역지정", "세로"));

            string tDrugCodeDrugName = OcrEngine.hocrRect(source, x, y, width, height);

            return tDrugCodeDrugName;
        }

        public static string[] getDrugDay(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////   

            int ct = 0;
            String[] tText = new String[13];  // 약품코드, 투약량, 횟수, 일수의 세로 13칸을 텍스트 배열로 넣기위해

            decimal[] x3old = new decimal[13];
            decimal[] y3old = new decimal[13];
            int[] x3new = new int[13];
            int[] y3new = new int[13];
            int[] width = new int[13];
            int[] height = new int[13];

            do
            {
                string checkDayX = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "총투약일수", "X");
                if (checkDayX != "")
                {
                    x3old[ct] = decimal.Parse(checkDayX);
                }
                string checkDayY = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "총투약일수", "Y");
                if (checkDayY != "")
                {
                    y3old[ct] = decimal.Parse(checkDayY);
                }
                decimal x1new = (decimal)Global.coordNo1X;
                decimal x2new = (decimal)Global.coordNo2X;
                x3new[ct] = NewPointX(x3old[ct], x1new, x2new);
                decimal y1new = (decimal)Global.coordNo1Y;
                decimal y2new = (decimal)Global.coordNo2Y;
                y3new[ct] = NewPointY(y3old[ct], y1new, y2new);
                string checkDayWidth = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "총투약일수", "가로");
                if (checkDayWidth != "")
                {
                    width[ct] = int.Parse(checkDayWidth);
                }
                string checkDayHeith = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "총투약일수", "세로");
                if (checkDayHeith != "")
                {
                    height[ct] = int.Parse(checkDayHeith);
                }

            //    MessageBox.Show("총투약일수 좌표는 : " + x3new[ct] + " " + y3new[ct]);

                tText[ct] = OcrEngine.ocrDigitLine(source, x3new[ct], y3new[ct], width[ct], height[ct]);

          //      MessageBox.Show("총투약일수는 = " + tText[ct]);

                if (string.IsNullOrEmpty(tText[ct]) == true)
                {
                    break;
                }

                ++ct;
            }
            while (ct < 13);
            {
                Global.Endct[4] = ct - 1;
       //         MessageBox.Show("Endcd[4]=" + Global.Endct[4]);
                return tText;   // tText 배열로 텍스트 반환
            }

        }

        public static string[] getDrugTime(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////   

            int ct = 0;
            String[] tText = new String[13];  // 약품코드, 투약량, 횟수, 일수의 세로 13칸을 텍스트 배열로 넣기위해

            decimal[] x3old = new decimal[13];
            decimal[] y3old = new decimal[13];
            int[] x3new = new int[13];
            int[] y3new = new int[13];
            int[] width = new int[13];
            int[] height = new int[13];

            do
            {
                string checkTimeX = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약횟수", "X");
                if (checkTimeX != "")        // 환경설정에서 좌표가 설정되어 있지 않은 구역을 읽으려 할 경우 오류를 막기 위해서
                {
                   x3old[ct] = decimal.Parse(checkTimeX);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약횟수 X 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }               
                string checkTimeY = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약횟수", "Y");
                if (checkTimeY != "")
                {
                    y3old[ct] = decimal.Parse(checkTimeY);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약횟수 Y 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                decimal x1new = (decimal)Global.coordNo1X;
                decimal x2new = (decimal)Global.coordNo2X;
                x3new[ct] = NewPointX(x3old[ct], x1new, x2new);
                decimal y1new = (decimal)Global.coordNo1Y;
                decimal y2new = (decimal)Global.coordNo2Y;
                y3new[ct] = NewPointY(y3old[ct], y1new, y2new);
                string checkTimeWidth = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약횟수", "가로");
                if (checkTimeWidth != "")
                {
                   width[ct] = int.Parse(checkTimeWidth);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약횟수 가로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                string checkTimeHeight = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약횟수", "세로");
                if (checkTimeHeight != "")
                {
                   height[ct] = int.Parse(checkTimeHeight);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약횟수 세로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }

          //      MessageBox.Show("투약횟수 좌표는 : " + x3new[ct] + " " + y3new[ct]);

                tText[ct] = OcrEngine.ocrSingleChar(source, x3new[ct], y3new[ct], width[ct], height[ct]);

          //      MessageBox.Show("투약횟수는 = " + tText[ct]);

                if (string.IsNullOrEmpty(tText[ct]) == true)
                {
                    break;
                }

                ++ct;
            }
            while (ct < 13);
            {
                Global.Endct[3] = ct - 1;
         //       MessageBox.Show("Endcd[3]=" + Global.Endct[3]);
                return tText;   // tText 배열로 텍스트 반환
            }
        }

        public static string[] getDrugDose(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////   

            int ct = 0;
            String[] tText = new String[13];  // 약품코드, 투약량, 횟수, 일수의 세로 13칸을 텍스트 배열로 넣기위해

            decimal[] x3old = new decimal[13];
            decimal[] y3old = new decimal[13];
            int[] x3new = new int[13];
            int[] y3new = new int[13];
            int[] width = new int[13];
            int[] height = new int[13];

            do
            {
                string checkDoseX = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약량", "X");
                if (checkDoseX != "")    // 환경설정에서 좌표가 설정되어 있지 않은 구역을 읽으려 할 경우 오류를 막기 위해서
                {
                    x3old[ct] = decimal.Parse(checkDoseX);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약량 X 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                string checkDoseY = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약량", "Y");
                if (checkDoseY != "")
                {
                    y3old[ct] = decimal.Parse(checkDoseY);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약량 Y 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                decimal x1new = (decimal)Global.coordNo1X;
                decimal x2new = (decimal)Global.coordNo2X;
                x3new[ct] = NewPointX(x3old[ct], x1new, x2new);
                decimal y1new = (decimal)Global.coordNo1Y;
                decimal y2new = (decimal)Global.coordNo2Y;
                y3new[ct] = NewPointY(y3old[ct], y1new, y2new);
                string checkDoseWidth = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약량", "가로");
                if (checkDoseWidth != "")
                {
                    width[ct] = int.Parse(checkDoseWidth);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약량 가로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                string checkDoseHeight = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "투약량", "세로");
                if (checkDoseHeight != "")
                {
                    height[ct] = int.Parse(checkDoseHeight);
                }
                else
                {
                    MessageBox.Show("환경설정에서 투약량 세로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }

           //     MessageBox.Show("투약량 좌표는 : " + x3new[ct] + " " + y3new[ct]);

                tText[ct] = OcrEngine.ocrDoseLine(source, x3new[ct], y3new[ct], width[ct], height[ct]);

          //      MessageBox.Show("투약량은 = " + tText[ct]);

                if (string.IsNullOrEmpty(tText[ct]) == true)
                {
                    break;
                }

                ++ct;
            }
            while (ct < 13);
            {
                Global.Endct[2] = ct - 1;
           //     MessageBox.Show("Endcd[2]=" + Global.Endct[2]);
                return tText;   // tText 배열로 텍스트 반환
            }
        }

        public static string[] getDrugName(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////   

            int ct = 0;
            String[] tText = new String[13];  // 약품코드, 투약량, 횟수, 일수의 세로 13칸을 텍스트 배열로 넣기위해

            decimal[] x3old = new decimal[13];
            decimal[] y3old = new decimal[13];
            int[] x3new = new int[13];
            int[] y3new = new int[13];
            int[] width = new int[13];
            int[] height = new int[13];

            do
            {
                string checkDrugNameX = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품명", "X");
                if (checkDrugNameX != "")
                {
                x3old[ct] = decimal.Parse(checkDrugNameX);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품명 X 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                string checkDrugNameY = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품명", "Y");
                if (checkDrugNameY != "")
                {
                    y3old[ct] = decimal.Parse(checkDrugNameY);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품명 Y 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                decimal x1new = (decimal)Global.coordNo1X;
                decimal x2new = (decimal)Global.coordNo2X;
                x3new[ct] = NewPointX(x3old[ct], x1new, x2new);
                decimal y1new = (decimal)Global.coordNo1Y;
                decimal y2new = (decimal)Global.coordNo2Y;
                y3new[ct] = NewPointY(y3old[ct], y1new, y2new);
                string checkDrugNameWidth = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품명", "가로");
                if (checkDrugNameWidth != "")
                {
                    width[ct] = int.Parse(checkDrugNameWidth);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품명 가로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                string checkDrugNameHeight = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품명", "세로");
                if (checkDrugNameHeight != "")
                {
                    height[ct] = int.Parse(checkDrugNameHeight);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품명 세로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }

         //       MessageBox.Show("약품명 좌표는 : " + x3new[ct] + " " + y3new[ct]);

                tText[ct] = OcrEngine.ocrTextLine(source, x3new[ct], y3new[ct], width[ct], height[ct]);

        //        MessageBox.Show("약품명은 = " + tText[ct]);

                if (string.IsNullOrEmpty(tText[ct]) == true)
                {
                    break;
                }

                ++ct;
            }
            while (ct < 13);
            {
                Global.Endct[1] = ct - 1;
                return tText;   // tText 배열로 텍스트 반환
            }
        }

        public static string[] getDrugCode(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////   

            int ct = 0;                        
            String[] tText = new String[13];  // 약품코드, 투약량, 횟수, 일수의 세로 13칸을 텍스트 배열로 넣기위해

            decimal[] x3old = new decimal[13];
            decimal[] y3old = new decimal[13];
            int[] x3new = new int[13];
            int[] y3new = new int[13];
            int[] width = new int[13];
            int[] height = new int[13];

            do
            {
                string checkDrugCodeX = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품코드", "X");
                if (checkDrugCodeX != "")
                {
                    x3old[ct] = decimal.Parse(checkDrugCodeX);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품코드 X 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                string checkDrugCodeY = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품코드", "Y");
                if (checkDrugCodeY != "")
                {
                    y3old[ct] = decimal.Parse(checkDrugCodeY);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품코드 Y 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                decimal x1new = (decimal)Global.coordNo1X;
                decimal x2new = (decimal)Global.coordNo2X;
                x3new[ct] = NewPointX(x3old[ct], x1new, x2new);
                decimal y1new = (decimal)Global.coordNo1Y;
                decimal y2new = (decimal)Global.coordNo2Y;
                y3new[ct] = NewPointY(y3old[ct], y1new, y2new);
                string checkDrugCodeWidth = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품코드", "가로");
                if (checkDrugCodeWidth != "")
                {
                    width[ct] = int.Parse(checkDrugCodeWidth);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품코드 가로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }
                string checkDrugCodeHeight = ini.GetIniValue((ct + 1).ToString() + "번" + " " + "약품코드", "세로");
                if (checkDrugCodeHeight != "")
                {
                    height[ct] = int.Parse(checkDrugCodeHeight);
                }
                else
                {
                    MessageBox.Show("환경설정에서 약품코드 세로 좌표를 설정해주세요.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
                }

         //       MessageBox.Show("약품코드 좌표는 : " + x3new[ct] + " " + y3new[ct]);

                tText[ct] = OcrEngine.ocrDigitLine(source, x3new[ct], y3new[ct], width[ct], height[ct]);

        //        MessageBox.Show("약품코드는 = " + tText[ct]);

                if (string.IsNullOrEmpty(tText[ct]) == true)
                {                   
                    break;
                }

                ++ct;
            }
            while ( ct < 13 );
            {
                Global.Endct[0] = ct - 1;
                return tText;   // tText 배열로 텍스트 반환
            }           
        }
        

        public static string getTargetText(Bitmap source, string targetText)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////   

            decimal x3old = decimal.Parse(ini.GetIniValue(targetText, "X"));
            decimal y3old = decimal.Parse(ini.GetIniValue(targetText, "Y"));
            decimal x1new = (decimal)Global.coordNo1X;
            decimal x2new = (decimal)Global.coordNo2X;
            int x3new = NewPointX(x3old, x1new, x2new);
            decimal y1new = (decimal)Global.coordNo1Y;
            decimal y2new = (decimal)Global.coordNo2Y;
            int y3new = NewPointY(y3old, y1new, y2new);
            int width = int.Parse(ini.GetIniValue(targetText, "가로"));
            int height = int.Parse(ini.GetIniValue(targetText, "세로"));          

            string returnText = OcrEngine.ocrDigit(source, x3new, y3new, width, height);

            return returnText;
        }

        public static int NewPointX(decimal x3old, decimal x1new, decimal x2new)  // old 는 기준처방전 new 는 실제처방전, 1 과 2는 기준좌표 두점을 말하고, 모르는 제3의 new 를 찾는 함수
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////          

            decimal x1old = decimal.Parse(ini.GetIniValue("스캔으로 찾은 좌표1번", "X"));
            decimal x2old = decimal.Parse(ini.GetIniValue("스캔으로 찾은 좌표2번", "X"));

            //MessageBox.Show("x1old = " + x1old);
            //MessageBox.Show("x2old = " + x2old);
            //MessageBox.Show("x3old = " + x3old);

            //MessageBox.Show("x1new = " + x1new);
            //MessageBox.Show("x2new = " + x2new);
            
            decimal x3new = 0;            
        //    decimal factor = ((Math.Abs(x2new - x1new)) / (Math.Abs(x2old - x1old)));      // factor 과 NewPoint 함수에 들어가는 인자들은 소수점까지 계산해야 하므로 반드시 "decimal" 로 해야 한다.
            decimal factor = 1;         // 위 방법으로는 오차가 너무 많이 생겨서 처리 불가능, 나중에 해결책을 찾아보기로 하고 모든 처방전이 동일한 크기라 보고 단순 거리 비교만 하기로 함

            //MessageBox.Show("x2new-x1new = " + (x2new - x1new).ToString());
            //MessageBox.Show("x2old-x1old = " + (x2old - x1old).ToString());
            //MessageBox.Show("Math.Abs(x2new - x1new) = " + (Math.Abs(x2new - x1new)).ToString());
            //MessageBox.Show("Math.Abs(x2old - x1old) = " + (Math.Abs(x2old - x1old)).ToString());
            //MessageBox.Show("factor x = " + factor.ToString());

            if ((x3old - x1old) < 0)
            {
                x3new = x1new - (Math.Abs(x3old - x1old) * factor);  // x1old 는 기준처방의 1번좌표, x2old 는 기준처방의 2번좌표, x3old 는 사용자가 지정한 기준처방의 좌표
            }                                                                                                      // x1new 는 실제처방의 1번좌표, x2new 는 실제처방의 2번좌표, x3new 는 찾으려고 하는 좌표  
            else if ((x3old - x1old) > 0)
            {
                x3new = (Math.Abs(x3old - x1old) * factor) + x1new;
            }
            else if (x3old == x1old)
            {
                MessageBox.Show("환경설정에서 좌표1번 과 좌표2번을 다시 설정해주세요.");
                System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
            }

         //  MessageBox.Show("x3new = " + x3new);
            
            return (int)x3new;  // 실제처방전의 원하는 좌표 x 값
        }

        public static int NewPointY(decimal y3old, decimal y1new, decimal y2new)  // old 는 기준처방전 new 는 실제처방전, 1 과 2는 기준좌표 두점을 말하고, 모르는 제3의 new 를 찾는 함수
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////          

            decimal y1old = decimal.Parse(ini.GetIniValue("스캔으로 찾은 좌표1번", "Y"));
            decimal y2old = decimal.Parse(ini.GetIniValue("스캔으로 찾은 좌표2번", "Y"));

            //MessageBox.Show("y1old = " + y1old);
            //MessageBox.Show("y2old = " + y2old);
            //MessageBox.Show("y3old = " + y3old);

            //MessageBox.Show("y1new = " + y1new);
            //MessageBox.Show("y2new = " + y2new);

            decimal y3new = 0;
           // decimal factor = ((Math.Abs(y2new - y1new)) / (Math.Abs(y2old - y1old)));  // factor 과 NewPoint 함수에 들어가는 인자들은 소수점까지 계산해야 하므로 반드시 "decimal" 로 해야 한다.
            decimal factor = 1;         // 위 방법으로는 오차가 너무 많이 생겨서 처리 불가능, 나중에 해결책을 찾아보기로 하고 모든 처방전이 동일한 크기라 보고 단순 거리 비교만 하기로 함

      //      MessageBox.Show("factor y = " + factor.ToString());

            if ((y3old - y1old) < 0)
            {
                y3new = y1new - (Math.Abs(y3old - y1old) * factor);  // y1old 는 기준처방의 1번좌표, y2old 는 기준처방의 2번좌표, y3old 는 사용자가 지정한 기준처방의 좌표
            }                                                                                                      // y1new 는 실제처방의 1번좌표, y2new 는 실제처방의 2번좌표, y3new 는 찾으려고 하는 좌표  
            else if ((y3old - y1old) > 0)
            {
                y3new = (Math.Abs(y3old - y1old) * factor) + y1new;
            }
            else if (y3old == y1old)
            {
                MessageBox.Show("환경설정에서 좌표1번 과 좌표2번을 다시 설정해주세요.");
                System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
            }
         //   MessageBox.Show("y3new = " + y3new);
            return (int)y3new;
        }

        public static float quadrantX(int quad) //  사분면 선택을 받아 각 사분면에 해당하는 X의 기준좌표값을 조정하기 위한 조정값을 반환한다.
        {
            int order;
            float adjustment = 0;

            order = quad - 1;

            Bitmap img = Global.source;
         //   Bitmap img = new Bitmap(@"C:\Program Files\PLOCR\prescription.png");
            var width = img.Width;

            switch (order)
            {
                case 0:
                    adjustment = width / 2;

                    break;
                case 1:
                    adjustment = 0;

                    break;
                case 2:
                    adjustment = 0;

                    break;
                case 3:
                    adjustment = width / 2;

                    break;
            }

            return adjustment;
        }

        public static float quadrantY(int quad) //  사분면 선택을 받아 각 사분면에 해당하는 Y의 기준좌표값을 조정하기 위한 조정값을 반환한다.
        {
            int order;
            float adjustment = 0;

            order = quad - 1;

            Bitmap img = Global.source;
           // Bitmap img = new Bitmap(@"C:\Program Files\PLOCR\prescription.png");
            var height = img.Height;

            switch (order)
            {
                case 0:
                    adjustment = 0;

                    break;
                case 1:
                    adjustment = 0;

                    break;
                case 2:
                    adjustment = height / 2;

                    break;
                case 3:
                    adjustment = height / 2;

                    break;
            }

            return adjustment;
        }

        public static Bitmap imgProcess(Bitmap source, string callFunction)  // 지정된 이미지 처리를 한다.
        {
            if(callFunction == "색상 필터")
            {
                Global.source = ImageProcess.colorFilter(source);
            }
            else if(callFunction == "색상 반전")
            {              
                Global.source = ImageProcess.invert(source);                
            }
            else if(callFunction == "선 굵게")
            {                
                Global.source = ImageProcess.thick(source);                
            }
            else if(callFunction == "선 가늘게")
            {
                Global.source = ImageProcess.thin(source);
            }
            else if(callFunction == "밝게")
            {
                Global.source = ImageProcess.bright(source);
            }
            else if(callFunction == "어둡게")
            {
                Global.source = ImageProcess.dark(source);
            }
            else if(callFunction == "기울어짐 바로잡기")
            {
                Global.source = ImageProcess.skew(source);
            }
            else if (callFunction == "원본")
            {
                string path = @"C:\Program Files\PLOCR\prescription.png";
                Global.source = (Bitmap)Bitmap.FromFile(path);   
            }
            else
            {
            }

            return Global.source;
        }
                        
        public static Bitmap sequence(Bitmap source)        // 이미지 처리 몇단계까지 해야하나 확인하고 처리
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 ///////////////////////////////////////////////////////// 

            string exeNo1 = ini.GetIniValue("실행 순서", "1단계");
            string exeNo2 = ini.GetIniValue("실행 순서", "2단계");
            string exeNo3 = ini.GetIniValue("실행 순서", "3단계");
            string exeNo4 = ini.GetIniValue("실행 순서", "4단계");
            string exeNo5 = ini.GetIniValue("실행 순서", "5단계");
            string exeNo6 = ini.GetIniValue("실행 순서", "6단계");
            string exeNo7 = ini.GetIniValue("실행 순서", "7단계");          

            if(exeNo7 == "끝")
            {
                source = imgProcess(source, exeNo1);
                source = imgProcess(source, exeNo2);
                source = imgProcess(source, exeNo3);
                source = imgProcess(source, exeNo4);
                source = imgProcess(source, exeNo5);
                source = imgProcess(source, exeNo6);                
            }
            else if(exeNo6 == "끝")
            {
                source = imgProcess(source, exeNo1);
                source = imgProcess(source, exeNo2);
                source = imgProcess(source, exeNo3);
                source = imgProcess(source, exeNo4);
                source = imgProcess(source, exeNo5);                
            }
            else if(exeNo5 == "끝")
            {                
                source = imgProcess(source, exeNo1);              
                source = imgProcess(source, exeNo2);
                source = imgProcess(source, exeNo3);
                source = imgProcess(source, exeNo4);                
            }
            else if (exeNo4 == "끝")
            {
                source = imgProcess(source, exeNo1);
                source = imgProcess(source, exeNo2);
                source = imgProcess(source, exeNo3);
            }
            else if (exeNo3 == "끝")
            {
                source = imgProcess(source, exeNo1);
                source = imgProcess(source, exeNo2);
            }
            else if (exeNo2 == "끝")
            {
                source = imgProcess(source, exeNo1);
            }
            else if (exeNo1 == "끝")
            {               
            }

            return source;
        }

        public static Bitmap originalcheck(Bitmap source)
        {
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 ///////////////////////////////////////////////////////// 

            string exeNo1 = ini.GetIniValue("실행 순서", "1단계");
            string exeNo2 = ini.GetIniValue("실행 순서", "2단계");
            string exeNo3 = ini.GetIniValue("실행 순서", "3단계");
            string exeNo4 = ini.GetIniValue("실행 순서", "4단계");
            string exeNo5 = ini.GetIniValue("실행 순서", "5단계");
            string exeNo6 = ini.GetIniValue("실행 순서", "6단계");
            string exeNo7 = ini.GetIniValue("실행 순서", "7단계");     

            if(exeNo1 == "원본" ||
               exeNo2 == "원본" || 
               exeNo3 == "원본" || 
               exeNo4 == "원본" || 
               exeNo5 == "원본" || 
               exeNo6 == "원본" || 
               exeNo7 == "원본")
            {
                string path = @"C:\Program Files\PLOCR\prescription.png";
                source = (Bitmap)Bitmap.FromFile(path);  
            }
            return source;
        }

        public static void copyDateFolder()  // 파일명 중복체크해서 중복이면 날짜+번호로 저장한다.
        {
            string DirPath = @"C:\Program Files\PLOCR\Image\" + DateTime.Now.ToShortDateString();   // 날짜 디렉토리를 생성하고
            DirectoryInfo di = new DirectoryInfo(DirPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            string srcFileNM = @"C:\Program Files\PLOCR\prescription.png";
            string tgtFileNM = @"C:\Program Files\PLOCR\Image\\" + DateTime.Now.ToShortDateString() + "\\" + DateTime.Now.ToString("yyyyMMdd") + "-" + ".png";
            string FileNameOnly = Path.GetFileNameWithoutExtension(tgtFileNM);
            string Extension = Path.GetExtension(tgtFileNM);
            string path = Path.GetDirectoryName(tgtFileNM);           
            string newFullPath = tgtFileNM;                        
                       
            int Count = 1;          // 날짜+번호 파일명으로 번호 하나씩 증가시키며 저장한다.
            do
            {
                string tmpFileName = string.Format("{0}{1}", FileNameOnly, Count++);
                newFullPath = Path.Combine(DirPath, tmpFileName + Extension);              
            }
            while (File.Exists(newFullPath));
            {                
            }

            try
            {
                File.Copy(srcFileNM, newFullPath);
                tgtFileNM = newFullPath;
            }
            catch (Exception ex)
            {
            }
                                   
        }

    }
}

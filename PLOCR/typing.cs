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
using System.Runtime.InteropServices;
using System.IO;

namespace PLOCR
{
    class typing
    {
        [DllImport("user32.dll")]
        public static extern uint PostMessage(IntPtr hwnd, uint wMsg, uint wParam, uint lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);
        const int WM_SETTEXT = 0X000C;

        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //private static extern int GetFocus();


        public static void typePatientName(IntPtr hwnd, string text)        // 환자명 입력, 실제는 주민번호 넣고 엔터로 진행한다.
        {
            /////////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            ////현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            //FileInfo exefileinfo = new FileInfo(@".\PLOCR.exe");
            //string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            //string fileName = @".\PLOCRconfig.ini";  // 환경설정 파일명
            //string filePath = pathini + fileName;   //ini 파일 경로
            //PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            ////////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////

            //if (string.IsNullOrEmpty(text) == true)
            //{
            //    text = ini.GetIniValue("환경설정값", "기본환자주민번호");
            //}
                SendMessage(hwnd, WM_SETTEXT, 0, text);
                PostMessage(hwnd, 0x0100, 0xD, 0x1C001);
                PostMessage(hwnd, 0x0102, 0xD, 0xC01C001);                        
        }

        public static void typePrescriptionNumber(IntPtr hwnd, string text)
        {
            /////////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            ////현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            //FileInfo exefileinfo = new FileInfo(@".\PLOCR.exe");
            //string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            //string fileName = @".\PLOCRconfig.ini";  // 환경설정 파일명
            //string filePath = pathini + fileName;   //ini 파일 경로
            //PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            ////////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////

     //       if (string.IsNullOrEmpty(text) == true)
      //      {
     //           text = ini.GetIniValue("환경설정값", "기본교부번호");
    //        }
                SendMessage(hwnd, WM_SETTEXT, 0, text);
                PostMessage(hwnd, 0x0100, 0xD, 0x1C001);
                PostMessage(hwnd, 0x0102, 0xD, 0xC01C001);          
        }

        public static void typeDoctorLicenseNumber(IntPtr hwnd, string text)
        {
            /////////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            ////현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            //FileInfo exefileinfo = new FileInfo(@".\PLOCR.exe");
            //string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            //string fileName = @".\PLOCRconfig.ini";  // 환경설정 파일명
            //string filePath = pathini + fileName;   //ini 파일 경로
            //PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            ////////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////

       //     if (string.IsNullOrEmpty(text) == true)
       //     {
       //         text = ini.GetIniValue("환경설정값", "기본의사면허번호");
       //     }

                SendMessage(hwnd, WM_SETTEXT, 0, text);
                PostMessage(hwnd, 0x0100, 0xD, 0x1C001);
                PostMessage(hwnd, 0x0102, 0xD, 0xC01C001);            
        }

        public static void segType(IntPtr hDrugCode, IntPtr hDose, IntPtr hTime, IntPtr hDay, IntPtr hTotalDose, IntPtr hInsureCombo, IntPtr hTakeCombo, IntPtr hAlterCombo, string[] tDrugCode, string[] tDose, string[] tTime, string[] tDay, int EndctMax)
        {            
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////




            int TimeSleep = int.Parse(ini.GetIniValue("입력설정", "입력대기시간"));

            int ct = 0;
                       
            do
            {
                //if (string.IsNullOrEmpty(tDrugCode[ct]) == true)
                //{
                //    tDrugCode[ct] = "641100270";
                //}
                //if (string.IsNullOrEmpty(tDose[ct]) == true)
                //{
                //    tDose[ct] = "1";
                //}
                //if (string.IsNullOrEmpty(tTime[ct]) == true)
                //{
                //    tTime[ct] = "1";
                //}
                //if (string.IsNullOrEmpty(tDay[ct]) == true)
                //{
                //    tDay[ct] = "1";
                //}

            //    if (string.IsNullOrEmpty(Global.tDrugCode[ct]) == false)
            //    {
                    System.Threading.Thread.Sleep(TimeSleep);

                    if (tDrugCode[ct] != "")
                    {
                        SendMessage(hDrugCode, WM_SETTEXT, 0, tDrugCode[ct]);
                        PostMessage(hDrugCode, 0x0100, 0xD, 0x1C001);
                        PostMessage(hDrugCode, 0x0102, 0xD, 0xC01C001);
                    }

            //        if (string.IsNullOrEmpty(tDose[ct]) == false && string.IsNullOrEmpty(tTime[ct]) == false && string.IsNullOrEmpty(tDay[ct]) == false)
            //        {                      
                        System.Threading.Thread.Sleep(TimeSleep);

                        if (tDose[ct] != "")
                        {
                            SendMessage(hDose, WM_SETTEXT, 0, tDose[ct]);
                            PostMessage(hDose, 0x0100, 0xD, 0x1C001);
                            PostMessage(hDose, 0x0102, 0xD, 0xC01C001);
                        }

                        System.Threading.Thread.Sleep(TimeSleep);

                        if (tTime[ct] != "")
                        {
                            SendMessage(hTime, WM_SETTEXT, 0, tTime[ct]);
                            PostMessage(hTime, 0x0100, 0xD, 0x1C001);
                            PostMessage(hTime, 0x0102, 0xD, 0xC01C001);
                        }

                        System.Threading.Thread.Sleep(TimeSleep);

                        if (tDay[ct] != "")
                        {
                            SendMessage(hDay, WM_SETTEXT, 0, tDay[ct]);
                            PostMessage(hDay, 0x0100, 0xD, 0x1C001);
                            PostMessage(hDay, 0x0102, 0xD, 0xC01C001);
                        }

             //           System.Threading.Thread.Sleep(TimeSleep);

              //          float Dose = Convert.ToSingle(tDose[ct]);   // null 값을 무시하기 위해 parse 대신 convert 사용, 소수점이 있는 경우가 있으므로 float 변수로
               //         float Time = Convert.ToSingle(tTime[ct]);
               //         float Day = Convert.ToSingle(tDay[ct]);
             //           string TotalDose = (Dose * Time * Day).ToString();    // 총투약량을 계산하기 위해서 바로 위에 float 로 했지만, 실제 입력 필요없으므로 일단 주석처리

              //          SendMessage(hTotalDose, WM_SETTEXT, 0, TotalDose);
             //           PostMessage(hTotalDose, 0x0100, 0xD, 0x1C001);
             //           PostMessage(hTotalDose, 0x0102, 0xD, 0xC01C001);

                        System.Threading.Thread.Sleep(TimeSleep);

                        PostMessage(hInsureCombo, 0x0100, 0xD, 0x1C001);
                        PostMessage(hInsureCombo, 0x0102, 0xD, 0xC01C001);
                        System.Threading.Thread.Sleep(TimeSleep);

                        PostMessage(hTakeCombo, 0x0100, 0xD, 0x1C001);
                        PostMessage(hTakeCombo, 0x0102, 0xD, 0xC01C001);

                        System.Threading.Thread.Sleep(TimeSleep);

                        PostMessage(hAlterCombo, 0x0100, 0xD, 0x1C001);

                        PostMessage(hAlterCombo, 0x0102, 0xD, 0xC01C001);

                        System.Threading.Thread.Sleep(TimeSleep);
              //      }                   
        //        }                

            //    Console.WriteLine(ct);
           //     Console.WriteLine(Global.EndctMax);
                ++ct;             
            }

            while (ct <= EndctMax);
            {               
            }
            
        }

    }
}

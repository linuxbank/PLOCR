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
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace PLOCR
{
    class Program
    {
        [DllImport("User32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [STAThread]
        static void Main(string[] args)
        {
            ////////////////// 데이터 수정 폼 시작 ///////////////////////      
            //bool createdNew;                                    // 프로세스를 잡아서 윈도우 최상위로 올리기 시작
            //int iP;
            //Process currentProcess = Process.GetCurrentProcess();
            //Mutex m = new Mutex(true, "PLOCR", out createdNew);
            //if (!createdNew)
            //{
            //    // app is already running...
            //    Process[] proc = Process.GetProcessesByName("PLOCR");

            //    // switch to other process
            //    for (iP = 0; iP < proc.Length; iP++)
            //    {
            //        if (proc[iP].Id != currentProcess.Id)
            //            SwitchToThisWindow(proc[0].MainWindowHandle, true);
            //    }

            //    return;
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DataEdit());                          
            ////////////////// 데이터 수정 폼 끝 ///////////////////////       
            // MainProcess.insideProcess();   
        }    
    }
}

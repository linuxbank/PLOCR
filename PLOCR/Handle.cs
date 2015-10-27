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
using System.Windows.Forms;

namespace PLOCR
{
    public class Handle
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowRect(IntPtr hwnd, out RECT rc);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Width;
            public int Height;
        }

        public static IntPtr Findchildchildwindow(IntPtr hParent, int index, string ChildClassName, string ChildChildClassName) // hParent 아래에 ChildClassName 을 가진 핸들을 index 수 만큼 루프를 돌리며 구하고, 구해진 핸들 또 아래에 ChildChildClassName 을 가진 핸들이 있으면, ChildChildClassName 의 부모 윈도우 핸들을 리턴한다.
        {
            if (index == 0)
                return hParent;
            else
            {
                int ct = 0;
                IntPtr hChild = IntPtr.Zero;
                IntPtr hChildChild = IntPtr.Zero;

                do
                {
                    hChild = FindWindowEx(hParent, hChild, ChildClassName, null);  // 주어진 hParent 아래에 주어진 ChildClassName 을 가진 윈도우를 루프를 돌리며 찾는다.
                    //    Console.WriteLine("주어진 핸들의 바로 아래 순환 핸들값:{0:X}", hChild.ToInt32());

                    hChildChild = FindWindowEx(hChild, hChildChild, ChildChildClassName, null);  // 찾은 ChildClassName 을 가진 윈도우 하위에 ChildChildClassName 을 가지고 있는 윈도우를 찾는다.
                    //      Console.WriteLine("아래 아래 순환 핸들값:{0:X}", hChildChild.ToInt32());

                    if (hChildChild != IntPtr.Zero) // ChildChildClassName 을 가지고 있는 원하는 TPanel 을 찾으면 루프를 탈출한다.
                    {
                        break;
                    }

                    if (hChild != IntPtr.Zero)
                    {
                        ++ct;
                    }
                }
                while (ct < index && hChild != IntPtr.Zero);
                {
                    return hChild;       // 찾아낸 원하는 TPanel 핸들을 리턴한다.               
                }
            }
        }

        public static IntPtr FindSizeWindow(IntPtr hParent, int index, string ChildClassName, int Width, int Height) // 상위 핸들과 클래스명을 받아서, 그 중에 가로 세로 크기가 같은 텍스트박스를 찾아서 핸들값을 반환하는 함수이다.
        {
            if (index == 0)
                return hParent;
            else
            {
                int ct = 0;
                IntPtr hChild = IntPtr.Zero;

                do
                {
                    hChild = FindWindowEx(hParent, hChild, ChildClassName, null);
                    //        Console.WriteLine("주어진 핸들의 바로 아래 순환 핸들값:{0:X}", hChild.ToInt32());

                    RECT pRect = new RECT();

                    GetWindowRect(hChild, out pRect);           // TwEdit 을 가진 핸들을 넣어 입력창의 좌표 구조체를 얻고, 입력창의 가로 세로 길이를 구한다.

                    pRect.Width = pRect.Right - pRect.Left;   // 입력창의 가로길이
                    pRect.Height = pRect.Bottom - pRect.Top;   // 입력창의 세로길이

                    //       Console.WriteLine("TwEdit 의 가로 길이:{0}", pRect.Width);
                    //       Console.WriteLine("입력된 Width 의 가로 길이:{0}", Width);
                    //       Console.WriteLine("TwEdit 의 세로 길이:{0}", pRect.Height);
                    //       Console.WriteLine("입력된 Height 의 세로 길이:{0}", Height);

                    if (pRect.Width == Width && pRect.Height == Height)   // 입력창의 가로 세로 길이가 같으면 루프를 탈출한다.
                    {
                        break;
                    }

                    if (hChild != IntPtr.Zero)
                    {
                        ++ct;
                    }
                }
                while (ct < index && hChild != IntPtr.Zero);
                {
                    return hChild;
                }
            }
        }

 //       public static IntPtr FindChildWindow(IntPtr hParent, int index, string ChildClassName) // hParent 아래에 ChildClassName 을 가진 핸들을 index 수 만큼 루프를 돌리며 찾는다.
 //       {
 //           if (index == 0)
 //               return hParent;
//            else
//            {
//                int ct = 0;
//                IntPtr hChild = IntPtr.Zero;
//
//                do
//                {
//                    hChild = FindWindowEx(hParent, hChild, ChildClassName, null);  // 주어진 hParent 아래에 주어진 ChildClassName 을 가진 윈도우를 루프를 돌리며 찾는다.
//                    //    Console.WriteLine("주어진 핸들의 바로 아래 순환 핸들값:{0:X}", hChild.ToInt32());
//
//                    if (hChild != IntPtr.Zero)
//                    {
//                        ++ct;
//                    }
//                }
//                while (ct < index && hChild != IntPtr.Zero);
//                {
//                    return hChild;       // 찾아낸 원하는 TPanel 핸들을 리턴한다.               
//                }
//            }
//        }

        public static IntPtr FindSortWindow(IntPtr hParent, int index, string ChildClassName, int OrderX, int OrderY) // 상위 핸들을 받아서 하위 윈도우를 찾는 루프를 돌리고, 윈도우의 상대 위치에 따라 정렬하고 원하는 순위의 핸들을 리턴하는 함수, 핸들과 좌표를 배열로 저장
        {
            if (index == 0)
                return hParent;
            else
            {
                int ct = 0;
                IntPtr hChild = IntPtr.Zero;
                IntPtr[] hArray;
                int[] RecCompareX;
                int[] RecCompareY;
                hArray = new IntPtr[index];
                RecCompareX = new int[index];
                RecCompareY = new int[index];

                do
                {
                    hChild = FindWindowEx(hParent, hChild, ChildClassName, null);
                    //        Console.WriteLine("주어진 핸들의 바로 아래 순환 핸들값:{0:X}", hChild.ToInt32());
                    hArray[ct] = hChild;

                    RECT pRect = new RECT();

                    GetWindowRect(hChild, out pRect);   // TwEdit 을 가진 핸들을 넣어 입력창의 좌표 구조체를 얻고, 입력창의 가로 세로 길이를 구한다.
                    
                    RecCompareX[ct] = pRect.Left;  // 루프를 돌리면서 윈도우의 왼쪽 좌표를 배열에 저장한다.
                    RecCompareY[ct] = pRect.Top;   // 루프를 돌리면서 윈도우의 위쪽 좌표를 배열에 저장한다.
                    //      Console.WriteLine("RecCompareY[ct] : {0:X}",RecCompareY[ct]);

                    if (hChild == IntPtr.Zero)   // 핸들이 없으면 루프를 탈출한다.
                    {
                        break;
                    }

                    if (hChild != IntPtr.Zero)
                    {
                        ++ct;
                    }
                }
                while (ct < index && hChild != IntPtr.Zero);
                {
                    int IndexNumber = 0;
                                       
                    try
                    {
                        if (OrderY == -1)
                        {
                            int SortedElementX = (from number in RecCompareX orderby number descending select number).Distinct().Skip(OrderX).First(); // X 좌표 배열중에서 element 의 크기순으로 정렬하고 Skip(OrderX) - (0부터 시작) 번째 큰 값을 찾는다.
                            IndexNumber = Array.IndexOf(RecCompareX, SortedElementX);  // 윗줄에서 찾아낸 SortedElement 의 index 를 찾는다.
                        }
                        else if (OrderX == -1)
                        {
                            int SortedElementY = (from number in RecCompareY orderby number descending select number).Distinct().Skip(OrderY).First(); // Y 좌표 배열중에서 element 의 크기순으로 정렬하고 Skip(OrderY) - (0부터 시작) 번째 큰 값을 찾는다.
                            IndexNumber = Array.IndexOf(RecCompareY, SortedElementY);  // 윗줄에서 찾아낸 SortedElement 의 index 를 찾는다.
                        }
                    }
                    catch (System.InvalidOperationException e) 
                    {
                        MessageBox.Show("처방조제 화면을 먼저 열어주세요.");
                 //       throw new System.InvalidOperationException("처방조제 화면을 먼저 열어주세요.", e);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료, 일단 종료시키고 다른 나은 방법을 찾아봐야함.
                        // 이 부분에 처방조제 화면이 열리지 않은 경우의 처리를 해줘야함, ex) 프로그램 전체를 재실행 하는 방법등으로, 가능한 빠른 방법을 찾아야함
                    }


                    //         Console.WriteLine("SortedElement : {0:X}", SortedElement); // order 순위에 따른 element 값을 출력한다.
                    //         Console.WriteLine("IndexNumber : {0:X}", IndexNumber);
                    //         Console.WriteLine("ct 값 : {0:X}", ct);
                    //         Console.WriteLine("핸들 값 : {0:X}", hChild.ToInt32());
                    //         Console.WriteLine("hArray[ct] : {0:X}", hArray[ct].ToInt32());
                    //         Console.WriteLine("hArray[IndexNumber] : {0:X}", hArray[IndexNumber].ToInt32());

                    return hArray[IndexNumber];  // OrderX, OrderY 순위에 해당하는 윈도우의 핸들을 반환한다.
                }



            }
        }
    }
}

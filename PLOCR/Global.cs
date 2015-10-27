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
//http://cafe.daum.net/pharm-poor
//linuxbank@hanmail.net

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PLOCR
{
    public static class Global    // 전역변수처럼 쓰기 위한 클래스
    {                       
        public static IntPtr hTFrmPrescriptionEdit;
        public static IntPtr hTPanelTop;
        public static IntPtr hTPanelDrugName;
        public static IntPtr hDrugCode;
        public static IntPtr hTPanelDoseTimeDay;
        public static IntPtr hDose;
        public static IntPtr hTime;
        public static IntPtr hDay;
        public static IntPtr hTotalDose;
        //     public static IntPtr hCopayDay;
        public static IntPtr hPatientName;
        public static IntPtr hPrescriptionNumber;
        public static IntPtr hDoctorLicenseNumber;
        //    public static IntPtr hHospitalCode;
        //     public static IntPtr hPrescriptionDay;
        //    public static IntPtr hFillPrescriptionDay;
        //   public static IntPtr hTimeOutCombo;
        //    public static IntPtr hAdditionCombo;
        //    public static IntPtr hTimeOutEdit;
        public static IntPtr hAlterCombo;
        public static IntPtr hInsureCombo;
        public static IntPtr hTakeCombo;
        public static IntPtr hDrugSelectCode;
        public static IntPtr hDrugName;

        public static IntPtr hPLOCREdit;

        public static string tPatientNumber;
        public static string tPatientName;
        public static string tDoctorLicenseNumber;
        public static string tPrescriptionNumber;
        public static String[] tDrugCode;
        public static String[] tDrugName;
        public static String[] tDose;
        public static String[] tTime;
        public static String[] tDay;              

        public static int[] Endct = new int[5];     
        public static int EndctMax;

      //  public static Bitmap ScanedImage;

        public static int coordNo1X;
        public static int coordNo1Y;
        public static int coordNo2X;
        public static int coordNo2Y;

        public static Bitmap source;     
    }
}

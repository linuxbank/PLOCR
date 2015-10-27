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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data.SQLite;
using System.IO;

namespace PLOCR
{
    public partial class DataEdit : Form
    {
        [DllImport("User32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        public DataEdit()
        {
            InitializeComponent();
                        
            // ...
            // 
            // timer1
            // 
            this.timer1.Enabled = true;    
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(scan.timer1_Tick);
        }
       
        private void button1_Click(object sender, EventArgs e) // 입력 버튼이 눌러지면, 모든 텍스트 박스의 값을 변수에 저장하고, 입력코드로 진행
        {            
            Global.tPatientNumber = textBox1.Text;
            Global.tPatientName = textBox69.Text;
            Global.tPrescriptionNumber = textBox2.Text;
            Global.tDoctorLicenseNumber = textBox3.Text;

            if (0 <= Global.EndctMax)
            {
                if (textBox4.Text != "")
                {
                    Global.tDrugCode[0] = textBox4.Text;
                }
                if (textBox56.Text != "")
                {
                    Global.tDrugName[0] = textBox56.Text;
                }
                if (textBox5.Text != "")
                { 
                Global.tDose[0] = textBox5.Text;
                }
                if (textBox6.Text != "")
                {
                    Global.tTime[0] = textBox6.Text;
                }
                if (textBox7.Text != "")
                {
                    Global.tDay[0] = textBox7.Text;
                }                      
            }
            else
            {

            }

            if (1 <= Global.EndctMax)
            {            
                if (textBox8.Text != "")
                { 
                Global.tDrugCode[1] = textBox8.Text;
                }
                if (textBox57.Text != "")
                {
                Global.tDrugName[1] = textBox57.Text;
                }
                if (textBox9.Text != "")
                {
                Global.tDose[1] = textBox9.Text;
                }
                if (textBox10.Text != "")
                {
                Global.tTime[1] = textBox10.Text;
                }
                if (textBox11.Text != "")
                {
                    Global.tDay[1] = textBox11.Text;
                }
            }
            else
            {

            }

            if (2 <= Global.EndctMax)
            {
                if (textBox12.Text != "")
                { 
                Global.tDrugCode[2] = textBox12.Text;
                }
                if (textBox58.Text != "")
                {
                Global.tDrugName[2] = textBox58.Text;
                }
                if (textBox13.Text != "")
                {
                Global.tDose[2] = textBox13.Text;
                }
                if (textBox14.Text != "")
                {
                Global.tTime[2] = textBox14.Text;                
                }
                if (textBox15.Text != "")
                {
                    Global.tDay[2] = textBox15.Text;
                }
            }
            else
            {

            }

            if (3 <= Global.EndctMax)
            {
                if (textBox16.Text != "")
                { 
                Global.tDrugCode[3] = textBox16.Text;                
                }
                if (textBox59.Text != "")
                {
                Global.tDrugName[3] = textBox59.Text;
                }
                if (textBox17.Text != "")
                {
                Global.tDose[3] = textBox17.Text;
                }
                if (textBox18.Text != "")
                {
                Global.tTime[3] = textBox18.Text;
                }
                if (textBox19.Text != "")
                {
                    Global.tDay[3] = textBox19.Text;
                }
            }
            else
            {

            }

            if (4 <= Global.EndctMax)
            {
                if (textBox20.Text != "")
                { 
                Global.tDrugCode[4] = textBox20.Text;
                }
                if (textBox60.Text != "")
                {
                Global.tDrugName[4] = textBox60.Text;
                }
                if (textBox21.Text != "")
                {
                Global.tDose[4] = textBox21.Text;
                }
                if (textBox22.Text != "")
                {
                Global.tTime[4] = textBox22.Text;
                }
                if (textBox23.Text != "")
                {
                    Global.tDay[4] = textBox23.Text;
                }
            }
            else
            {

            }

            if (5 <= Global.EndctMax)
            {
                if (textBox24.Text != "")
                { 
                Global.tDrugCode[5] = textBox24.Text;
                }
                if (textBox61.Text != "")
                {
                Global.tDrugName[5] = textBox61.Text;
                }
                if (textBox25.Text != "")
                {
                Global.tDose[5] = textBox25.Text;
                }
                if (textBox26.Text != "")
                {
                Global.tTime[5] = textBox26.Text;
                }
                if (textBox27.Text != "")
                {
                    Global.tDay[5] = textBox27.Text;
                }
            }
            else
            {

            }

            if (6 <= Global.EndctMax)
            {
                if (textBox28.Text != "")
                { 
                Global.tDrugCode[6] = textBox28.Text;
                }
                if (textBox62.Text != "")
                {
                Global.tDrugName[6] = textBox62.Text;
                }
                if (textBox29.Text != "")
                {
                Global.tDose[6] = textBox29.Text;
                }
                if (textBox30.Text != "")
                {
                Global.tTime[6] = textBox30.Text;
                }
                if (textBox31.Text != "")
                {
                    Global.tDay[6] = textBox31.Text;
                }
            }
            else
            {

            }

            if (7 <= Global.EndctMax)
            {
                if (textBox32.Text != "")
                { 
                Global.tDrugCode[7] = textBox32.Text;
                }
                if (textBox63.Text != "")
                {
                Global.tDrugName[7] = textBox63.Text;
                }
                if (textBox33.Text != "")
                {
                Global.tDose[7] = textBox33.Text;
                }
                if (textBox34.Text != "")
                {
                Global.tTime[7] = textBox34.Text;
                }
                if (textBox35.Text != "")
                {
                    Global.tDay[7] = textBox35.Text;
                }
            }
            else
            {

            }

            if (8 <= Global.EndctMax)
            {
                if (textBox36.Text != "")
                { 
                Global.tDrugCode[8] = textBox36.Text;
                }
                if (textBox64.Text != "")
                {
                Global.tDrugName[8] = textBox64.Text;
                }
                if (textBox37.Text != "")
                {
                Global.tDose[8] = textBox37.Text;
                }
                if (textBox38.Text != "")
                {
                Global.tTime[8] = textBox38.Text;
                }
                if (textBox39.Text != "")
                {
                    Global.tDay[8] = textBox39.Text;
                }
            }
            else
            {

            }

            if (9 <= Global.EndctMax)
            {
                if (textBox40.Text != "")
                { 
                Global.tDrugCode[9] = textBox40.Text;
                }
                if (textBox65.Text != "")
                {
                Global.tDrugName[9] = textBox65.Text;
                }
                if (textBox41.Text != "")
                {
                Global.tDose[9] = textBox41.Text;
                }
                if (textBox42.Text != "")
                {
                Global.tTime[9] = textBox42.Text;
                }
                if (textBox43.Text != "")
                {
                    Global.tDay[9] = textBox43.Text;
                }
            }
            else
            {

            }

            if (10 <= Global.EndctMax)
            {
                if (textBox44.Text != "")
                { 
                Global.tDrugCode[10] = textBox44.Text;
                }
                if (textBox66.Text != "")
                {
                Global.tDrugName[10] = textBox66.Text;
                }
                if (textBox45.Text != "")
                {
                Global.tDose[10] = textBox45.Text;
                }
                if (textBox46.Text != "")
                {
                Global.tTime[10] = textBox46.Text;
                }
                if (textBox47.Text != "")
                {
                    Global.tDay[10] = textBox47.Text;
                }
            }
            else
            {

            }

            if (11 <= Global.EndctMax)
            {
                if (textBox48.Text != "")
                { 
                Global.tDrugCode[11] = textBox48.Text;
                }
                if (textBox67.Text != "")
                {
                Global.tDrugName[11] = textBox67.Text;
                }
                if (textBox49.Text != "")
                {
                Global.tDose[11] = textBox49.Text;
                }
                if (textBox50.Text != "")
                {
                Global.tTime[11] = textBox50.Text;
                }
                if (textBox51.Text != "")
                {
                    Global.tDay[11] = textBox51.Text;
                }
            }
            else
            {

            }

            if (12 <= Global.EndctMax)
            {
                if (textBox52.Text != "")
                { 
                Global.tDrugCode[12] = textBox52.Text;
                }
                if (textBox68.Text != "")
                {
                Global.tDrugName[12] = textBox68.Text;
                }
                if (textBox53.Text != "")
                {
                Global.tDose[12] = textBox53.Text;
                }
                if (textBox54.Text != "")
                {
                Global.tTime[12] = textBox54.Text;
                }
                if (textBox55.Text != "")
                {
                    Global.tDay[12] = textBox55.Text;
                }
            }
            else
            {

            }

            //////////////////DB 에 정확한 약품코드 약품명 저장 시작///////////////
            int ct = 0;
            do
            {
                if (Global.tDrugCode[ct] != "" && Global.tDrugName[ct] != "")       // 배열이 인덱스를 벗어난 오류를 처리해야 함.
                {
                    SQLite.insertDrugCodeDrugName(Global.tDrugCode[ct], Global.tDrugName[ct]);
                }

                ++ct;
            }
            while(ct <= Global.EndctMax);
            {
            }
            //////////////////DB 에 정확한 약품코드 약품명 저장 끝///////////////
                                    
            //////////////// 핸들로 전달 시작 //////////////////////
            typing.typePatientName(Global.hPatientName, Global.tPatientNumber);        // 환자 주민번호 입력 후 엔터

       //     IntPtr hSujin = Handle.FindWindow("TfrmSuJinJsS", "수진자 조회");    // 환자명 입력후 수진자조회창이 뜨는지(급여, 차상위) 핸들을 탐색한다.

            typing.typePrescriptionNumber(Global.hPrescriptionNumber, Global.tPrescriptionNumber);  // 교부번호 입력 후 엔터
            typing.typeDoctorLicenseNumber(Global.hDoctorLicenseNumber, Global.tDoctorLicenseNumber); // 의사면허 입력 후 엔터

            typing.segType(Global.hDrugCode, Global.hDose, Global.hTime, Global.hDay, Global.hTotalDose, Global.hInsureCombo, Global.hTakeCombo, Global.hAlterCombo, Global.tDrugCode, Global.tDose, Global.tTime, Global.tDay, Global.EndctMax);   // 루프를 돌면서 약품코드, 투약량, 횟수, 투약일수를 입력한다.
            /////////////////  핸들로 전달 끝 ///////////////////////            

            SQLite.insertPatientNumberPatientName(textBox1.Text, textBox69.Text);   //DB 에 정확한 주민번호 환자명 저장
            SQLite.deleteNullDrugCodeDrugName();            // null 값의 약품코드 약품명 삭제
            SQLite.deleteNullPatientNumberPatientName();    // null 값의 주민번호 환자명 삭제

            //////////////////글로벌 변수값을 "" 로 초기화 시작///////////////            
            ct = 0;
            do
            {
                Global.tDrugCode[ct] = "";
                Global.tDrugName[ct] = "";
                Global.tDose[ct] = "";
                Global.tTime[ct] = "";
                Global.tDay[ct] = "";

                ++ct;
            }
            while (ct <= Global.EndctMax);
            {
            }
            //////////////////글로벌 변수값을 "" 로 초기화 끝///////////////

            foreach (Control ctl in this.Controls)          // 이 폼 안에 있는 모든 텍스트박스 초기화
            {
                if (typeof(System.Windows.Forms.TextBox) == ctl.GetType())
                {
                    ctl.Text = null;
                }
            }

            Global.EndctMax = 0;    // 루프 끝을 지정한 배열 인덱스 한계값의 초기화(그래야 재스캔해도 다음 처방의 줄 수 때문에 생기는 index out of range 가 발생하지 않는다.                
            //Global.tDrugCode = new String[0];
            //Global.tDrugName = new String[0];
            //Global.tDose = new String[0];
            //Global.tTime = new String[0];
            //Global.tDay = new String[0];                     

        //    scan.scanImage();   // 다시 스캔 대기
            
           // this.timer1.Enabled = true;    // 반복 스캔 하는 타이머 시작
                       
       //     File.Delete(@"C:\Program Files\PLOCR\prescription.png");
        //    Global.source.Save(@"C:\Program Files\PLOCR\prescription.png"); 
            calculator.copyDateFolder();    // 처방전 이미지를 날짜폴더에 순차적으로 이름붙여가며 저장

            /////////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            ////현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            //FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            //string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            //string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            //string filePath = pathini + fileName;   //ini 파일 경로
            //PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            ////////////// ini 객체 생성 끝 ///////////////////////////////////////////////////////// 

            //ini.SetIniValue("스캔후자동실행", "자동실행여부", "0");  // 스캔후 자동실행 여부를 미실행으로 다시 지정해둠

        //    this.WindowState = FormWindowState.Minimized;   // 입력 마치고 창 최소화
                        
         //   Application.OpenForms["DataEdit"].Close();  // 윈도우 폼 닫기

            //System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            //start.FileName = @"C:\Program Files\PLOCR\PLOCRscan.exe";         // 스캔 프로그램 시작
            //start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;   // 윈도우 속성을  windows hidden  으로 지정
            //start.CreateNoWindow = true;  // hidden 을 시키기 위해서 이 속성도 true 로 체크해야 함                                    

            //this.TopMost = false;   // 창 최상위 해제
            //if (this.WindowState != FormWindowState.Minimized)      // 창 상태가 최소화가 아니면
            //{
            //    this.WindowState = FormWindowState.Minimized;   // 창 최소화
            //}

            this.TopMost = true;        // 창 최상위로
            while (this.WindowState != FormWindowState.Normal)      // 창 상태가 노멀이 아니면
            {
                this.WindowState = FormWindowState.Normal;   // 창 노멀화              
                this.Activate();
                textBox1.Focus();
            }

            button3.PerformClick(); // 입력 끝내고 새로운 처방전 스캔 시작           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.OpenForms["DataEdit"].Close();  // 윈도우 폼 닫기
        }

        private void button3_Click(object sender, EventArgs e)  // 스캔 실행
        {
           // this.Opacity = 0;       // 화면 숨기고
            if (this.WindowState != FormWindowState.Minimized)      // 창 상태가 최소화가 아니면
            {
                this.WindowState = FormWindowState.Minimized;   // 창 최소화
            }

            MainProcess.insideProcess();  // 메인 처리를 실행하고

            // MainProcess.insideProcessAuto();  // 스캔 모듈에서 ocr 본체를 호출한 경우의 메인처리

            textBox1.Text = Global.tPatientNumber;
            string tPatientName = SQLite.PatientNameFromPatientNumber(Global.tPatientNumber);
            if (string.IsNullOrEmpty(tPatientName) == false)                          
            {
                textBox69.Text = tPatientName;    
            }
            else
            {
                textBox69.Text = Global.tPatientName;
            }
            textBox2.Text = Global.tPrescriptionNumber;
            textBox3.Text = Global.tDoctorLicenseNumber;
                       
            if (0 <= Global.EndctMax && 0 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[0]) == false)     // 배열 인덱스 에러를 방지하기 위해 변수값이 null 인 경우는 건너뛴다. 이하 모든 텍스트 박스칸에 적용해야함.
                {
                    textBox4.Text = Global.tDrugCode[0];
                }
                string tDrugName0 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[0]);   // DB 로부터 정확한 약품명 가져오고
                if (string.IsNullOrEmpty(tDrugName0) == false)                           // DB 에 정확한 약품명이 있으면 그것을 넣고
                {
                    textBox56.Text = tDrugName0;
                }
                else
                {
                    textBox56.Text = Global.tDrugName[0];                               // DB 에 정확한 약품명이 없으면 인식한 그대로 넣고
                }
                if (string.IsNullOrEmpty(Global.tDose[0]) == false)
                {
                    textBox5.Text = Global.tDose[0];
                }
                if (string.IsNullOrEmpty(Global.tTime[0]) == false)
                {
                    textBox6.Text = Global.tTime[0];
                }
                if (string.IsNullOrEmpty(Global.tDay[0]) == false)
                {
                    textBox7.Text = Global.tDay[0];
                }
            }
            else
            {
                textBox4.Text = "";
                textBox56.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }

            if (1 <= Global.EndctMax && 1 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[1]) == false)
                {
                    textBox8.Text = Global.tDrugCode[1];
                }
                string tDrugName1 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[1]);
                if (string.IsNullOrEmpty(tDrugName1) == false)
                {
                    textBox57.Text = tDrugName1;
                }
                else
                {
                    textBox57.Text = Global.tDrugName[1];
                }
                if (string.IsNullOrEmpty(Global.tDose[1]) == false)
                {
                    textBox9.Text = Global.tDose[1];
                }
                if (string.IsNullOrEmpty(Global.tTime[1]) == false)
                {
                    textBox10.Text = Global.tTime[1];
                }
                if (string.IsNullOrEmpty(Global.tDay[1]) == false)
                {
                    textBox11.Text = Global.tDay[1];
                }
            }
            else
            {
                textBox8.Text = "";
                textBox57.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
            }

            if (2 <= Global.EndctMax && 2 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[2]) == false)
                {
                    textBox12.Text = Global.tDrugCode[2];
                }
                string tDrugName2 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[2]);
                if (string.IsNullOrEmpty(tDrugName2) == false)
                {
                    textBox58.Text = tDrugName2;
                }
                else
                {
                    textBox58.Text = Global.tDrugName[2];
                }
                if (string.IsNullOrEmpty(Global.tDose[2]) == false)
                {
                    textBox13.Text = Global.tDose[2];
                }
                if (string.IsNullOrEmpty(Global.tTime[2]) == false)
                {
                    textBox14.Text = Global.tTime[2];
                }
                if (string.IsNullOrEmpty(Global.tDay[2]) == false)
                {
                    textBox15.Text = Global.tDay[2];
                }
            }
            else
            {
                textBox12.Text = "";
                textBox58.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
            }


            if (3 <= Global.EndctMax && 3 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[3]) == false)
                {
                    textBox16.Text = Global.tDrugCode[3];
                }
                string tDrugName3 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[3]);
                if (string.IsNullOrEmpty(tDrugName3) == false)
                {
                    textBox59.Text = tDrugName3;
                }
                else
                {
                    textBox59.Text = Global.tDrugName[3];
                }
                if (string.IsNullOrEmpty(Global.tDose[3]) == false)
                {
                    textBox17.Text = Global.tDose[3];
                }
                if (string.IsNullOrEmpty(Global.tTime[3]) == false)
                {
                    textBox18.Text = Global.tTime[3];
                }
                if (string.IsNullOrEmpty(Global.tDay[3]) == false)
                {
                    textBox19.Text = Global.tDay[3];
                }
            }
            else
            {
                textBox16.Text = "";
                textBox59.Text = "";
                textBox17.Text = "";
                textBox18.Text = "";
                textBox19.Text = "";
            }

            if (4 <= Global.EndctMax && 4 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[4]) == false)
                {
                    textBox20.Text = Global.tDrugCode[4];
                }
                string tDrugName4 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[4]);
                if (string.IsNullOrEmpty(tDrugName4) == false)
                {
                    textBox60.Text = tDrugName4;
                }
                else
                {
                    textBox60.Text = Global.tDrugName[4];
                }
                if (string.IsNullOrEmpty(Global.tDose[4]) == false)
                {
                    textBox21.Text = Global.tDose[4];
                }
                if (string.IsNullOrEmpty(Global.tTime[4]) == false)
                {
                    textBox22.Text = Global.tTime[4];
                }
                if (string.IsNullOrEmpty(Global.tDay[4]) == false)
                {
                    textBox23.Text = Global.tDay[4];
                }
            }
            else
            {
                textBox20.Text = "";
                textBox60.Text = "";
                textBox21.Text = "";
                textBox22.Text = "";
                textBox23.Text = "";
            }


            if (5 <= Global.EndctMax && 5 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[5]) == false)
                {
                    textBox24.Text = Global.tDrugCode[5];
                }
                string tDrugName5 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[5]);
                if (string.IsNullOrEmpty(tDrugName5) == false)
                {
                    textBox61.Text = tDrugName5;
                }
                else
                {
                    textBox61.Text = Global.tDrugName[5];
                }
                if (string.IsNullOrEmpty(Global.tDose[5]) == false)
                {
                    textBox25.Text = Global.tDose[5];
                }
                if (string.IsNullOrEmpty(Global.tTime[5]) == false)
                {
                    textBox26.Text = Global.tTime[5];
                }
                if (string.IsNullOrEmpty(Global.tDay[5]) == false)
                {
                    textBox27.Text = Global.tDay[5];
                }
            }
            else
            {
                textBox24.Text = "";
                textBox61.Text = "";
                textBox25.Text = "";
                textBox26.Text = "";
                textBox27.Text = "";
            }

            if (6 <= Global.EndctMax && 6 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[6]) == false)
                {
                    textBox28.Text = Global.tDrugCode[6];
                }
                string tDrugName6 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[6]);
                if (string.IsNullOrEmpty(tDrugName6) == false)
                {
                    textBox62.Text = tDrugName6;
                }
                else
                {
                    textBox62.Text = Global.tDrugName[6];
                }
                if (string.IsNullOrEmpty(Global.tDose[6]) == false)
                {
                    textBox29.Text = Global.tDose[6];
                }
                if (string.IsNullOrEmpty(Global.tTime[6]) == false)
                {
                    textBox30.Text = Global.tTime[6];
                }
                if (string.IsNullOrEmpty(Global.tDay[6]) == false)
                {
                    textBox31.Text = Global.tDay[6];
                }
            }
            else
            {
                textBox28.Text = "";
                textBox62.Text = "";
                textBox29.Text = "";
                textBox30.Text = "";
                textBox31.Text = "";
            }

            if (7 <= Global.EndctMax && 7 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[7]) == false)
                {
                    textBox32.Text = Global.tDrugCode[7];
                }
                string tDrugName7 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[7]);
                if (string.IsNullOrEmpty(tDrugName7) == false)
                {
                    textBox63.Text = tDrugName7;
                }
                else
                {
                    textBox63.Text = Global.tDrugName[7];
                }
                if (string.IsNullOrEmpty(Global.tDose[7]) == false)
                {
                    textBox33.Text = Global.tDose[7];
                }
                if (string.IsNullOrEmpty(Global.tTime[7]) == false)
                {
                    textBox34.Text = Global.tTime[7];
                }
                if (string.IsNullOrEmpty(Global.tDay[7]) == false)
                {
                    textBox35.Text = Global.tDay[7];
                }
            }
            else
            {
                textBox32.Text = "";
                textBox63.Text = "";
                textBox33.Text = "";
                textBox34.Text = "";
                textBox35.Text = "";
            }

            if (8 <= Global.EndctMax && 8 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[8]) == false)
                {
                    textBox36.Text = Global.tDrugCode[8];
                }
                string tDrugName8 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[8]);
                if (string.IsNullOrEmpty(tDrugName8) == false)
                {
                    textBox64.Text = tDrugName8;
                }
                else
                {
                    textBox64.Text = Global.tDrugName[8];
                }
                if (string.IsNullOrEmpty(Global.tDose[8]) == false)
                {
                    textBox37.Text = Global.tDose[8];
                }
                if (string.IsNullOrEmpty(Global.tTime[8]) == false)
                {
                    textBox38.Text = Global.tTime[8];
                }
                if (string.IsNullOrEmpty(Global.tDay[8]) == false)
                {
                    textBox39.Text = Global.tDay[8];
                }
            }
            else
            {
                textBox36.Text = "";
                textBox64.Text = "";
                textBox37.Text = "";
                textBox38.Text = "";
                textBox39.Text = "";
            }

            if (9 <= Global.EndctMax && 9 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[9]) == false)
                {
                    textBox40.Text = Global.tDrugCode[9];
                }
                string tDrugName9 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[9]);
                if (string.IsNullOrEmpty(tDrugName9) == false)
                {
                    textBox65.Text = tDrugName9;
                }
                else
                {
                    textBox65.Text = Global.tDrugName[9];
                }
                if (string.IsNullOrEmpty(Global.tDose[9]) == false)
                {
                    textBox41.Text = Global.tDose[9];
                }
                if (string.IsNullOrEmpty(Global.tTime[9]) == false)
                {
                    textBox42.Text = Global.tTime[9];
                }
                if (string.IsNullOrEmpty(Global.tDay[9]) == false)
                {
                    textBox43.Text = Global.tDay[9];
                }
            }
            else
            {
                textBox40.Text = "";
                textBox65.Text = "";
                textBox41.Text = "";
                textBox42.Text = "";
                textBox43.Text = "";
            }

            if (10 <= Global.EndctMax && 10 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[10]) == false)
                {
                    textBox44.Text = Global.tDrugCode[10];
                }
                string tDrugName10 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[10]);
                if (string.IsNullOrEmpty(tDrugName10) == false)
                {
                    textBox66.Text = tDrugName10;
                }
                else
                {
                    textBox66.Text = Global.tDrugName[10];
                }
                if (string.IsNullOrEmpty(Global.tDose[10]) == false)
                {
                    textBox45.Text = Global.tDose[10];
                }
                if (string.IsNullOrEmpty(Global.tTime[10]) == false)
                {
                    textBox46.Text = Global.tTime[10];
                }
                if (string.IsNullOrEmpty(Global.tDay[10]) == false)
                {
                    textBox47.Text = Global.tDay[10];
                }
            }
            else
            {
                textBox44.Text = "";
                textBox66.Text = "";
                textBox45.Text = "";
                textBox46.Text = "";
                textBox47.Text = "";
            }

            if (11 <= Global.EndctMax && 11 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[11]) == false)
                {
                    textBox48.Text = Global.tDrugCode[11];
                }
                string tDrugName11 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[11]);
                if (string.IsNullOrEmpty(tDrugName11) == false)
                {
                    textBox67.Text = tDrugName11;
                }
                else
                {
                    textBox67.Text = Global.tDrugName[11];
                }
                if (string.IsNullOrEmpty(Global.tDose[11]) == false)
                {
                    textBox49.Text = Global.tDose[11];
                }
                if (string.IsNullOrEmpty(Global.tTime[11]) == false)
                {
                    textBox50.Text = Global.tTime[11];
                }
                if (string.IsNullOrEmpty(Global.tDay[11]) == false)
                {
                    textBox51.Text = Global.tDay[11];
                }
            }
            else
            {
                textBox48.Text = "";
                textBox67.Text = "";
                textBox49.Text = "";
                textBox50.Text = "";
                textBox51.Text = "";
            }

            if (12 <= Global.EndctMax && 12 < Global.tDrugCode.Length)
            {
                if (string.IsNullOrEmpty(Global.tDrugCode[12]) == false)
                {
                    textBox52.Text = Global.tDrugCode[12];
                }
                string tDrugName12 = SQLite.DrugNameFromDrugCode(Global.tDrugCode[12]);
                if (string.IsNullOrEmpty(tDrugName12) == false)
                {
                    textBox68.Text = tDrugName12;
                }
                else
                {
                    textBox68.Text = Global.tDrugName[12];
                }
                if (string.IsNullOrEmpty(Global.tDose[12]) == false)
                {
                    textBox53.Text = Global.tDose[12];
                }
                if (string.IsNullOrEmpty(Global.tTime[12]) == false)
                {
                    textBox53.Text = Global.tTime[12];
                }
                if (string.IsNullOrEmpty(Global.tDay[12]) == false)
                {
                    textBox55.Text = Global.tDay[12];
                }
            }
            else
            {
                textBox52.Text = "";
                textBox68.Text = "";
                textBox53.Text = "";
                textBox53.Text = "";
                textBox55.Text = "";
            }

         //   this.Opacity = 1;       // 화면 다시 보여주고

            this.TopMost = true;        // 창 최상위로
            if (this.WindowState != FormWindowState.Normal)      // 창 상태가 노멀이 아니면
            {
                this.WindowState = FormWindowState.Normal;   // 창 노멀화              
                this.Activate();
                textBox1.Focus();
            }

            /////////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            ////현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            //FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            //string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            //string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            //string filePath = pathini + fileName;   //ini 파일 경로
            //PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            ////////////// ini 객체 생성 끝 ///////////////////////////////////////////////////////// 

            //ini.SetIniValue("스캔후자동실행", "자동실행여부", "0");  // 스캔후 자동실행 여부를 미실행으로 다시 지정해둠
        }             
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)  // 입력(F12) 단축키 설정
        {
            if (!base.ProcessCmdKey(ref msg, keyData)) // 위에서 처리 안했으면
            {
                
                if (keyData.Equals(Keys.F12))
                {
                    button1.PerformClick();                    
                    return true;
                }
                if (keyData.Equals(Keys.Escape))
                {
                    button2.PerformClick();
                    return true;
                }
                if (keyData.Equals(Keys.F1))
                {
                    button3.PerformClick();
                    return true;
                }
                if (keyData.Equals(Keys.F9))
                {
                    button4.PerformClick();
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            else
            {
                return true;
            }
        }

        protected void ReallyCenterToScreen()   // 윈도우를 화면 가운데에 위치 시키기
        {
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };

        }

        private void DataEdit_Load(object sender, EventArgs e)
        {
            ReallyCenterToScreen();

            var sourceDrugName = new AutoCompleteStringCollection();        // 약품명 자동완성 
            var sourceDrugCode = new AutoCompleteStringCollection();        // 약품코드 자동완성   
            var sourcePatientName = new AutoCompleteStringCollection();     // 환자명 자동완성 
            var sourcePatientNumber = new AutoCompleteStringCollection();   // 주민번호 자동완성 

            sourceDrugName = SQLite.autocompleteDrugName();      // DB로부터 가져온 약품명 Autocompletecustomsource
            sourceDrugCode = SQLite.autocompleteDrugCode();      // DB로부터 가져온 약품코드 Autocompletecustomsource
            sourcePatientName = SQLite.autocompletePatientName();       // DB로부터 가져온 환자명 Autocompletecustomsource
            sourcePatientNumber = SQLite.autocompletePatientNumber();       // DB로부터 가져온 주민번호 Autocompletecustomsource

            textBox56.AutoCompleteCustomSource = sourceDrugName;        // 이하 약품명 자동완성 적용
            textBox57.AutoCompleteCustomSource = sourceDrugName;
            textBox58.AutoCompleteCustomSource = sourceDrugName;
            textBox59.AutoCompleteCustomSource = sourceDrugName;
            textBox60.AutoCompleteCustomSource = sourceDrugName;
            textBox61.AutoCompleteCustomSource = sourceDrugName;
            textBox62.AutoCompleteCustomSource = sourceDrugName;
            textBox63.AutoCompleteCustomSource = sourceDrugName;
            textBox64.AutoCompleteCustomSource = sourceDrugName;
            textBox65.AutoCompleteCustomSource = sourceDrugName;
            textBox66.AutoCompleteCustomSource = sourceDrugName;
            textBox67.AutoCompleteCustomSource = sourceDrugName;
            textBox68.AutoCompleteCustomSource = sourceDrugName;

            textBox4.AutoCompleteCustomSource = sourceDrugCode;         // 이하 약품코드 자동완성 적용
            textBox8.AutoCompleteCustomSource = sourceDrugCode;
            textBox12.AutoCompleteCustomSource = sourceDrugCode;
            textBox16.AutoCompleteCustomSource = sourceDrugCode;
            textBox20.AutoCompleteCustomSource = sourceDrugCode;
            textBox24.AutoCompleteCustomSource = sourceDrugCode;
            textBox28.AutoCompleteCustomSource = sourceDrugCode;
            textBox32.AutoCompleteCustomSource = sourceDrugCode;
            textBox36.AutoCompleteCustomSource = sourceDrugCode;
            textBox40.AutoCompleteCustomSource = sourceDrugCode;
            textBox44.AutoCompleteCustomSource = sourceDrugCode;
            textBox48.AutoCompleteCustomSource = sourceDrugCode;
            textBox52.AutoCompleteCustomSource = sourceDrugCode;

            textBox69.AutoCompleteCustomSource = sourcePatientName;         // 환자명 자동완성 적용

            textBox1.AutoCompleteCustomSource = sourcePatientNumber;         // 주민번호 자동완성 적용    

       //     button3.PerformClick();     // 폼 로딩후 스캔 바로 시작
 
                                              

            /////////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            ////현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            //FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            //string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            //string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            //string filePath = pathini + fileName;   //ini 파일 경로
            //PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            ////////////// ini 객체 생성 끝 ///////////////////////////////////////////////////////// 

            //string autovalue = ini.GetIniValue("스캔후자동실행", "자동실행여부");
            //if (autovalue == "1")   // 스캔후 자동실행할지 여부를 판단한다. "1" 이면 자동으로 스캔버튼 누른 것처럼 진행, "0" 이면 실행 안함
            //{
          //      scanAutoProcess();     // 스캔 모듈이 끝나고 DataEdit 폼이 로드되면 스캔버튼을 누른 것처럼 진행한다.  
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {           
            foreach (Control ctl in this.Controls)          // 이 폼 안에 있는 모든 텍스트박스 초기화
            {
                if (typeof(System.Windows.Forms.TextBox) == ctl.GetType())
                {
                    ctl.Text = "";
                }
            }

            button3.PerformClick();     // 새로운 처방전 스캔 시작
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            start.FileName = @"C:\Program Files\PLOCR\DocumentAnalysis.exe";         // 환경설정 프로그램 시작
         //   start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;   // 윈도우 속성을 지정
         //   start.CreateNoWindow = true;  // hidden 을 시키기 위해서 이 속성도 true 로 체크해야 함

            Process process = Process.Start(start);
         //   process.WaitForExit(); // 외부 프로세스가 끝날 때까지 프로그램의 진행을 멈춘다. 
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)  // 텍스트 박스간 탭 오더 이동을 엔터로 이동하도록 한다.
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}"); // 또는 "{TAB}" 대신에 "\t" 
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)     // 텍스트 박스간 탭 오더 이동을 화살표로 한다.
        {
            if (e.KeyCode == Keys.Left)
            {
                SendKeys.Send("+{TAB}");
            }
            if (e.KeyCode == Keys.Right)
            {
                SendKeys.Send("{Tab}");
            }
            if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{TAB}+{TAB}+{TAB}+{TAB}+{TAB}");
            }
            if (e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{TAB}{TAB}{TAB}{TAB}{TAB}");
            }  
        }

        public static void TextBox_NextFocus(Control ParentControl, Control CurrentControl)     // 탭 이동을 엔터키로 대신
        {
            CurrentControl.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                // if (string.IsNullOrEmpty(CurrentControl.Text) == false)
                {
                    SendKeys.Send("{TAB}");
                }
            };
        }                

        private void textBox56_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox56.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox4.Text = SQLite.DrugCodeFromDrugName(textBox56.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox57_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox57.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox8.Text = SQLite.DrugCodeFromDrugName(textBox57.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox58_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox58.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox12.Text = SQLite.DrugCodeFromDrugName(textBox58.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox59_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox59.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox16.Text = SQLite.DrugCodeFromDrugName(textBox59.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox60_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox60.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox20.Text = SQLite.DrugCodeFromDrugName(textBox60.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox61_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox61.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox24.Text = SQLite.DrugCodeFromDrugName(textBox61.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox62_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox62.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox28.Text = SQLite.DrugCodeFromDrugName(textBox62.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox63_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox63.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox32.Text = SQLite.DrugCodeFromDrugName(textBox63.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox64_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox64.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox36.Text = SQLite.DrugCodeFromDrugName(textBox64.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox65_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox65.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox40.Text = SQLite.DrugCodeFromDrugName(textBox65.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox66_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox66.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox44.Text = SQLite.DrugCodeFromDrugName(textBox66.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox67_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox67.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox48.Text = SQLite.DrugCodeFromDrugName(textBox67.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void textBox68_TextChanged(object sender, EventArgs e)
        {
            string DrugCode = SQLite.DrugCodeFromDrugName(textBox68.Text);
            if (string.IsNullOrEmpty(DrugCode) == false)
            {
                textBox52.Text = SQLite.DrugCodeFromDrugName(textBox68.Text);        // 사용자가 약품명을 수정하면 DB 에서 정확한 약품코드를 가져와서 약품코드 텍스트박스에 보여준다.
            }
        }

        private void TextBox_Click(object sender, EventArgs e)      // 텍스트박스 클릭시 전체블럭 선택하도록
        {
            ((TextBox)sender).SelectAll();
        }
                
    }
}

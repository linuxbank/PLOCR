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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace PLOCR
{
    class MainProcess
    {        
        public static void insideProcess()
        {            
            ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
            //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
            FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
            string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
            string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
            string filePath = pathini + fileName;   //ini 파일 경로
            PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
            //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////           

            //String inihDose = ini.GetIniValue("PLOCR_handle", "hDose"); 
            //Global.hDose = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", -1, 3);

            //if (inihDose != Global.hDose.ToString())  // 찾아온 핸들값과 .ini 에서 읽어온 핸들값이 다르면 새로 핸들값 가져온다.
            //{
            //   Console.WriteLine(Global.hPatientName);
            //   Console.WriteLine(inihDose);

            //    Console.WriteLine("핸들값이 없어서 새로 읽어왔어요.");

            /////////////////////// 핸들 찾기 시작 ////////////////////////////     
         //   if (Global.hTFrmPrescriptionEdit == null)       // 찾아놓은 핸들이 없을 때만 탐색하도록 하기 위해
        //    {
                Global.hTFrmPrescriptionEdit = Handle.FindWindow("TFrmPrescriptionEdit", "처방조제"); // 처방조제 최상위 윈도우 핸들찾기          
                Global.hTPanelTop = Handle.Findchildchildwindow(Global.hTFrmPrescriptionEdit, 10, "TPanel", "TwMaskEdit"); // 처방조제 최상위 바로 아래 TPanel 이 8개 있으므로 10번 정도 돌려보고 찾은 TPanel 중에 하위에 TwMaskEdit 이 있는 TPanel 의 핸들값을 찾는다.      
                Global.hTPanelDrugName = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TPanel", 460, 86); // 처방조제 최상위 아래의 TPanel 중 가로 세로 460 86인 TPanel 을 찾는다. 이 TPanel 아래에 TwEdit (명칭) 이 들어있다.        
                Global.hDrugCode = Handle.FindSizeWindow(Global.hTPanelDrugName, 10, "TwEdit", 263, 20); // 명칭 상위의 TPanel 을 넣어, 하위에 "명칭" 가로 세로 263 20 을 찾는다.
                Global.hTPanelDoseTimeDay = Handle.FindSizeWindow(Global.hTPanelTop, 10, "TPanel", 461, 174); // 투약량,횟수,일수 의 상위 TPanel 을 가로 세로 461 74로 찾는다.
                Global.hDose = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", -1, 3);  // 투약량,횟수,일수 를 찾기 위해 OrderY, OrderX(배열이므로 0부터 시작) 순위를 넣어주어 윈도우 핸들을 찾는다. Y축으로 찾을 때는 OrderX = -1, X축으로 찾을 때는 OrderY = -1 을 넣는다.
                Global.hTime = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", -1, 2);  // 투약량,횟수,일수 를 찾기 위해 OrderY, OrderX(배열이므로 0부터 시작) 순위를 넣어주어 윈도우 핸들을 찾는다. Y축으로 찾을 때는 OrderX = -1, X축으로 찾을 때는 OrderY = -1 을 넣는다.
                Global.hDay = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", 2, -1);  // 투약량,횟수,일수 를 찾기 위해 OrderY, OrderX(배열이므로 0부터 시작) 순위를 넣어주어 윈도우 핸들을 찾는다. Y축으로 찾을 때는 OrderX = -1, X축으로 찾을 때는 OrderY = -1 을 넣는다.
                Global.hTotalDose = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", 77, 20); // 총투약, 가로 세로 77 20   , 입력하지 않아도 엔터치며 이동해야 하기 때문에 핸들 탐색해둬야함
                //     Global.hCopayDay = Handle.FindSizeWindow(hTPanelDoseTimeDay, 15, "TwEdit", 55, 20); // 본인부담일수, 가로 세로 55 20
                Global.hPatientName = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TwEdit", 78, 20); // hTPanelTop 를 부모 윈도우로 하여, 아래에 TwEdit 이 있는 텍스트박스를 찾고 박스의 Width 와 Height 를 비교하여 "환자이름" 의TwEdit 을 찾는다. 환자이름 입력창의 가로 = 78, 세로 = 20 이다.
                Global.hPrescriptionNumber = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TwEdit", 138, 20); // 교부번호의 가로 세로는 138 20 이다.
                Global.hDoctorLicenseNumber = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TwEdit", 64, 20); // 의사면허의 가로 세로는 64 20 이다.
                //    Global.hHospitalCode = Handle.FindSizeWindow(hTPanelTop, 50, "TwEdit", 110, 20); // 발행기관의 가로 세로는 110 20 이다.
                //     Global.hPrescriptionDay = Handle.FindSortWindow(hTPanelTop, 50, "TDateTimePicker", -1, 1);
                //    Global.hFillPrescriptionDay = Handle.FindSortWindow(hTPanelTop, 50, "TDateTimePicker", -1, 0);
                //   Global.hTimeOutCombo = Handle.FindSizeWindow(hTPanelTop, 50, "TComboBox", 117, 22); // 시간외 콤보박스 가로 세로 117 22
                //    Global.hAdditionCombo = Handle.FindSizeWindow(hTPanelTop, 50, "TComboBox", 76, 22); // 비가산/소아가산/65세 콤보박스 가로 세로 76 22
                //    Global.hTimeOutEdit = Handle.FindSizeWindow(hTPanelTop, 50, "TwMaskEdit", 77, 20);  // FindWindowEx 또는 만든 함수 FindChildWindow 를 사용하는 것이 좋은데 작동을 안해서 FindSizeWindow 사용해서 찾았음
                Global.hAlterCombo = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TComboBox", 107, 22); // 형태(대체 등..) 가로 세로 107 22     , 입력하지 않아도 엔터치며 이동해야 하기 때문에 핸들 탐색해둬야함
                Global.hInsureCombo = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TComboBox", 84, 22); // 급여(100/100 등..) 가로 세로 107 22  , 입력하지 않아도 엔터치며 이동해야 하기 때문에 핸들 탐색해둬야함
                Global.hTakeCombo = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TComboBox", 84, 20);  // 용법 가로 세로 84 20   , 입력하지 않아도 엔터치며 이동해야 하기 때문에 핸들 탐색해둬야함
          //  }

            //   IntPtr hDrugSelectCode = Handle.FindWindow("TFrmSelectCode", "코드조회"); // 약품입력 팝업창

            //if (Global.hTFrmPrescriptionEdit == IntPtr.Zero)
            //{
            //    Console.WriteLine("처방조제 입력화면을 먼저 열어주세요.");
            //}
            /////////////////////// 핸들 찾기 끝////////////////////////////

           // Console.WriteLine("핸들을 찾았습니다.\n");

            //////////////////////// ini 값 입력 시작 /////////////////////////////
            //ini.SetIniValue("PLOCR_handle", "hTFrmPrescriptionEdit", Global.hTFrmPrescriptionEdit.ToString());  // ini 파일에 핸들값을 string으로 형변환 하여 텍스트로 저장한다. string 값이지만 int32 로 다시 변환하면 핸들값이 된다.
            //ini.SetIniValue("PLOCR_handle", "hTPanelTop", Global.hTPanelTop.ToString());
            //ini.SetIniValue("PLOCR_handle", "hTPanelDrugName", Global.hTPanelDrugName.ToString());
            //ini.SetIniValue("PLOCR_handle", "hDrugCode", Global.hDrugCode.ToString());
            //ini.SetIniValue("PLOCR_handle", "hTPanelDoseTimeDay", Global.hTPanelDoseTimeDay.ToString());
            //ini.SetIniValue("PLOCR_handle", "hDose", Global.hDose.ToString());
            //ini.SetIniValue("PLOCR_handle", "hTime", Global.hTime.ToString());
            //ini.SetIniValue("PLOCR_handle", "hDay", Global.hDay.ToString());
            //ini.SetIniValue("PLOCR_handle", "hTotalDose", Global.hTotalDose.ToString());
            //ini.SetIniValue("PLOCR_handle", "hPatientName", Global.hPatientName.ToString());
            //ini.SetIniValue("PLOCR_handle", "hPrescriptionNumber", Global.hPrescriptionNumber.ToString());
            //ini.SetIniValue("PLOCR_handle", "hDoctorLicenseNumber", Global.hDoctorLicenseNumber.ToString());
            //ini.SetIniValue("PLOCR_handle", "hAlterCombo", Global.hAlterCombo.ToString());
            //ini.SetIniValue("PLOCR_handle", "hInsureCombo", Global.hInsureCombo.ToString());
            //ini.SetIniValue("PLOCR_handle", "hTakeCombo", Global.hTakeCombo.ToString());
            ////   ini.SetIniValue("PLOCR_handle", "hDrugSelectCode", hDrugSelectCode.ToString());
            //////////////////////// ini 값 입력 끝 ///////////////////////////////
            //}
            //else  // 핸들값이 .ini 에 있으면 값을 가져온다.
            //{
            //ini.GetIniValue("PLOCR_handle", "hTFrmPrescriptionEdit");  // ini 파일에서 핸들값을 가져옴, 형변환 해야함
            //ini.GetIniValue("PLOCR_handle", "hTPanelTop");
            //ini.GetIniValue("PLOCR_handle", "hTPanelDrugCode");
            //ini.GetIniValue("PLOCR_handle", "hDrugCode");
            //ini.GetIniValue("PLOCR_handle", "hTPanelDoseTimeDay");
            //ini.GetIniValue("PLOCR_handle", "hDose");
            //ini.GetIniValue("PLOCR_handle", "hTime");
            //ini.GetIniValue("PLOCR_handle", "hDay");
            //ini.GetIniValue("PLOCR_handle", "hTotalDose");
            //ini.GetIniValue("PLOCR_handle", "hPatientName");
            //ini.GetIniValue("PLOCR_handle", "hPrescriptionNumber");
            //ini.GetIniValue("PLOCR_handle", "hDoctorLicenseNumber");
            //ini.GetIniValue("PLOCR_handle", "hAlterCombo");
            //ini.GetIniValue("PLOCR_handle", "hInsureCombo");
            //ini.GetIniValue("PLOCR_handle", "hTakeCombo");
            //}

            ///////////////// 처방전 스캔 시작 ///////////////////////////
          //  splashPres fmPres = new splashPres();
        //    fmPres.Owner = DataEdit.ActiveForm;  // child form 을 알리고
         //   fmPres.Show();       // 스캔중 화면을 띄우고

            Bitmap source = scan.scanImage();
            if (source != null)
            {
                Global.source = source;
            }
            else
            {
                MessageBox.Show("스캐너 연결상태 또는 처방전 급지상태를 확인하고 재실행 해주세요.");
                System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
            }

       //     fmPres.Close(); // 스캔중 화면 닫고
            /////////////////// 처방전 스캔 끝 ///////////////////////////

            /////////////// 처방전 스캔 프로그램 시작 ///////////////////////////
            //splashPres fmPres = new splashPres();
            //fmPres.Owner = DataEdit.ActiveForm;  // child form 을 알리고
            //fmPres.Show();       // 스캔중 화면을 띄우고

            //System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            //start.FileName = @"C:\Program Files\PLOCR\PLOCRscan.exe";         // 스캔 프로그램 시작
            //start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;   // 윈도우 속성을  windows hidden  으로 지정
            //start.CreateNoWindow = true;  // hidden 을 시키기 위해서 이 속성도 true 로 체크해야 함

            //Process process = Process.Start(start);
            //process.WaitForExit(); // 외부 프로세스가 끝날 때까지 프로그램의 진행을 멈춘다.  

            //fmPres.Close(); // 스캔중 화면 닫고
            ///////////////// 처방전 스캔 프로그램 끝 ///////////////////////////
            
            splashImageProcess fmImageProcess = new splashImageProcess();
            fmImageProcess.Owner = DataEdit.ActiveForm;  // child form 을 알리고
            fmImageProcess.Show();       // 이미지 처리중 화면을 띄우고

        //    string path = @"C:\Program Files\PLOCR\prescription.png";
        //    Bitmap source = (Bitmap)Bitmap.FromFile(path);            
            if (source != null)
            {
                source = calculator.sequence(source); // 이미지 전처리
            }
            else
            {
                MessageBox.Show("스캐너 연결상태를 확인하고 재실행 해주세요.");
                System.Diagnostics.Process.GetCurrentProcess().Kill();      // 프로그램 강제 종료
            }

       //     source.Save(@"C:\Program Files\PLOCR\processedprescription.png");

          //  source = ImageProcess.Scaling(source, 500);                 // % 로 확대한다. 
            if (source != null)
            {
                FindZeroXY.divSearch1(source);  // 좌표1번 찾고
                FindZeroXY.divSearch2(source);  // 좌표2번 찾고
            }
            else
            {
             //   MessageBox.Show("다시 스캔해주세요.");
            }

            fmImageProcess.Close();


            //MessageBox.Show("찾아낸 좌표1번은 : " + Global.coordNo1X + " " + Global.coordNo1Y);            
            //MessageBox.Show("찾아낸 좌표2번은 : " + Global.coordNo2X + " " + Global.coordNo2Y);                        

            //MessageBox.Show("기준좌표1번 x 는" + Global.coordNo1X.ToString());
            //MessageBox.Show("기준좌표1번 y 는" + Global.coordNo1Y.ToString());
            //MessageBox.Show("기준좌표2번 x 는" + Global.coordNo2X.ToString());
            //MessageBox.Show("기준좌표2번 y 는" + Global.coordNo2Y.ToString());      
           

            //string path = @"C:\Program Files\PLOCR\prescription.png";
            //Bitmap source = (Bitmap)Bitmap.FromFile(path);

            splashOcr fmOcr = new splashOcr();  // ocr 인식 알림
            fmOcr.Owner = DataEdit.ActiveForm;  // child form 을 알리고
            fmOcr.Show();       // OCR 인식중 화면을 띄우고

            if (source != null)
            {
                source = calculator.originalcheck(source);      // 처리순서 끝에 원본이 필요한가 확인한다. 처방전이 흐린 경우 이미지처리후 좌표잡고 실제 읽을 때는 원본으로 하는 경우가 된다.

                Global.tPatientNumber = calculator.getTargetText(source, "주민번호");   // 환자 주민번호 txt 저장

                //    MessageBox.Show("읽어낸 주민번호는 = " + Global.tPatientNumber);

                Global.tPatientName = calculator.getTargetText(source, "환자이름");

                //      MessageBox.Show("읽어낸 환자이름은 = " + Global.tPatientName);

                Global.tDoctorLicenseNumber = calculator.getTargetText(source, "의사면허");  // 의사 면허번호 txt 저장

                //      MessageBox.Show("읽어낸 면허번호는 = " + Global.tDoctorLicenseNumber);

                Global.tPrescriptionNumber = calculator.getTargetText(source, "교부번호");

                //      MessageBox.Show("읽어낸 교부번호는 = " + Global.tPrescriptionNumber);

                fmOcr.Close();  // 인식창 닫고 


                splashData fmData = new splashData();  // 데이터 처리 알림
                fmData.Owner = DataEdit.ActiveForm;  // child form 을 알리고
                fmData.Show();       // 데이터 처리중 화면을 띄우고

                string DrugAreaCheck = ini.GetIniValue("약품영역사용", "사용여부");   // 약품영역으로 할 것인지, 약품코드와 약품명을 각각 읽어낼 것인지 결정한다. 1이면 약품영역좌표 전체로, 0 이면 각각 코드와 약품명 좌표로 읽어낸다.

                if (DrugAreaCheck == "1")
                {
                    Global.tDose = calculator.getDrugDose(source);           // 투약량 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
                    Global.tTime = calculator.getDrugTime(source);           // 투여횟수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
                    Global.tDay = calculator.getDrugDay(source);             // 총투약일수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.

                    Global.EndctMax = Global.Endct.Max();                                        
                    //           MessageBox.Show("세가지로만 잡은 EndctMax 는" + Global.EndctMax.ToString());

                    string tDrugCodeDrugNameArea = calculator.getDrugCodeDrugNameArea(source);     // 약품코드 약품명 영역을 읽어낸다.
                    Global.tDrugCode = TextProcess.DrugCodeSplit(tDrugCodeDrugNameArea);   // 읽어낸 약품코드를 문자열 후처리를 통해 글로벌 변수에 저장한다.
                    Global.tDrugName = TextProcess.DrugNameSplit(tDrugCodeDrugNameArea);   // 읽어낸 약품명을 문자열 후처리를 통해 글로벌 변수에 저장한다.                                             
                }
                else if (DrugAreaCheck == "0")
                {
                    Global.tDrugCode = calculator.getDrugCode(source);    // 약품코드 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
                    Global.tDrugName = calculator.getDrugName(source);
                    Global.tDose = calculator.getDrugDose(source);           // 투약량 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
                    Global.tTime = calculator.getDrugTime(source);           // 투여횟수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
                    Global.tDay = calculator.getDrugDay(source);             // 총투약일수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.

                    Global.EndctMax = Global.Endct.Max();   // 투약량,횟수,일수 중에서 가장 많이 인식한 숫자만큼 입력을 하기 위해 카운팅 값을 구한다.
                    // 입력 모듈 반복수를 구하기 위해 스캔하면서 정해둔다. 이 값이 4라면 0부터 시작하므로 5행까지 입력하게 된다.
                }

                Array.Resize<String>(ref Global.tDrugCode, Global.EndctMax + 1);     // 배열 인덱스 오류를 방지하기 위해 전체 배열의 크기를 EndcdMax 로 재조정한다.
                Array.Resize<String>(ref Global.tDrugName, Global.EndctMax + 1);    // 프로그램 완료후 다시 스캔할 때, 앞선 처방전의 배열크기가 크면 다음
                Array.Resize<String>(ref Global.tDose, Global.EndctMax + 1);        // 다음 처방전에서 줄수가 적으면 배열 인덱스 범위를 벗어나기 때문에 오류가 생긴다.
                Array.Resize<String>(ref Global.tTime, Global.EndctMax + 1);        // 그래서 여기서 배열의 크기를 바꿔준다.
                Array.Resize<String>(ref Global.tDay, Global.EndctMax + 1);         // 중요한 오류임, 주의할 것.

                fmData.Close();     // 데이터 처리 알림창 닫고
            }

            

                                  

      //      ini.SetIniValue("스캔후자동실행", "자동실행여부", "0");  // 스캔후 자동실행 여부를 미실행으로 다시 지정해둠

           // MessageBox.Show("EndctMax = " + Global.EndctMax);   
            
       //     ////////////////////// 이미지 전처리 시작 //////////////////////
       //     string path = @"C:\Program Files\PLOCR\prescription.png";    // twain 이용하여 스캐너로부터 가져와야 하는 이미지, twain 연결 부분
       //     Bitmap source = (Bitmap)Bitmap.FromFile(path);
       //     Bitmap ProcessedImage = ImageProcess.ImgProcess(source);
       //                ProcessedImage.Save(@".\ProcessedImage.png");
       //     ////////////////////  이미지 전처리 끝 /////////////////////////

       //  //   Console.WriteLine("이미지 전처리를 끝냈습니다.\n");
       //       MessageBox.Show("이미지 전처리를 끝냈습니다.\n");
                                   

       //     ////////////////////  좌표영점 찾아 좌표설정 시작 //////////////////////
       //     string ZeroHtml = OcrEngine.hocr(ProcessedImage);  // 좌표 영점을 찾기 위해 hocr 실행       
           
       //     int zeroWidth, zeroHeight, ScaleX, ScaleY;

       //     int distancePatientX, distancePatientY, sizePatientHor, sizePatientVer;
       //     int distanceDoctorX, distanceDoctorY, sizeDoctorHor, sizeDoctorVer;
       //     int distancePreNumX, distancePreNumY, sizePreNumHor, sizePreNumVer;
       //     int distanceDrugCodeX, distanceDrugCodeY, sizeDrugCodeHor, sizeDrugCodeVer;
       //     int distanceDoseX, distanceDoseY, sizeDoseHor, sizeDoseVer;
       //     int distanceTimeX, distanceTimeY, sizeTimeHor, sizeTimeVer;
       //     int distanceDayX, distanceDayY, sizeDayHor, sizeDayVer;
       //     int jumpY;
       //     int factor;

       //     FindZeroXY.zeroSearch(ZeroHtml,
       //         out zeroWidth, out zeroHeight, out ScaleX, out ScaleY,
       //         out distancePatientX, out distancePatientY, out sizePatientHor, out sizePatientVer,
       //         out distanceDoctorX, out distanceDoctorY, out sizeDoctorHor, out sizeDoctorVer,
       //         out distancePreNumX, out distancePreNumY, out sizePreNumHor, out sizePreNumVer,
       //         out distanceDrugCodeX, out distanceDrugCodeY, out sizeDrugCodeHor, out sizeDrugCodeVer,
       //         out distanceDoseX, out distanceDoseY, out sizeDoseHor, out sizeDoseVer,
       //         out distanceTimeX, out distanceTimeY, out sizeTimeHor, out sizeTimeVer,
       //         out distanceDayX, out distanceDayY, out sizeDayHor, out sizeDayVer,
       //         out jumpY,
       //         out factor);  // 좌표영점의 가로세로와 표준화된 좌표영점을 반환받는다.       

       //   //    Console.WriteLine(zeroWidth);
       //   //     Console.WriteLine(ScaleX);

       //         MessageBox.Show(zeroWidth.ToString());
       //         MessageBox.Show(ScaleX.ToString());

       //     ////////////////////  좌표영점 찾아 좌표설정 끝 ///////////////////////

       // //          Console.WriteLine("좌표영점을 찾았습니다.\n");
       //        MessageBox.Show("좌표영점을 찾았습니다.\n");

       //     ///////////////////   이미지 사이즈 표준화 시작 ///////////////////    
       //     if (zeroWidth == 0) 
       //     {
       //         MessageBox.Show("좌표영점 인식에 실패했습니다.\n 다른 처방전으로 시험해보세요.\n 인식률을 더 개선시켜 놓을께요.");
       //     }

       //     Bitmap ScaledImage = ImageProcess.ScaleByPercent(ProcessedImage, factor / zeroWidth);
       //               ScaledImage.Save(@".\ScaledImage.png");            
       //     //////////////////    이미지 사이즈 표준화 끝 ///////////////////

       ////     Console.WriteLine("이미지 사이즈 표준화를 끝냈습니다.\n");

       //     ///////////////////  이미지 잘라내기 시작 /////////////////////
       //     Bitmap cPatientName = Crop.cloneAforge(ScaledImage, ScaleX - distancePatientX, ScaleY + distancePatientY, sizePatientHor, sizePatientVer);   // 환자 주민번호 위치 잘라내기
       //                cPatientName.Save(@".\cPatientName.png");
       //     Bitmap cDoctorLicenseNumber = Crop.cloneAforge(ScaledImage, ScaleX - distanceDoctorX, ScaleY + distanceDoctorY, sizeDoctorHor, sizeDoctorVer);   // 의사면허 위치 잘라내기
       //                cDoctorLicenseNumber.Save(@".\cDoctorLicenseNumber.png");
       //     Bitmap cPrescriptionNumber = Crop.cloneAforge(ScaledImage, ScaleX - distancePreNumX, ScaleY + distancePreNumY, sizePreNumHor, sizePreNumVer);  // 교부번호 위치 잘라내기
       //                cPrescriptionNumber.Save(@".\cPrescriptionNumber.png");
       //     Bitmap[] cDrugCode = Crop.segCrop(ScaledImage, ScaleX - distanceDrugCodeX, ScaleY + distanceDrugCodeY, sizeDrugCodeHor, sizeDrugCodeVer, jumpY);  // 약품코드 위치를 잘라내어 배열에 저장
       //     Bitmap[] cDose = Crop.segCrop(ScaledImage, ScaleX - distanceDoseX, ScaleY + distanceDoseY, sizeDoseHor, sizeDoseVer, jumpY);     // 투약량 위치를 잘라내어 배열에 저장
       //     Bitmap[] cTime = Crop.segCrop(ScaledImage, ScaleX - distanceTimeX, ScaleY + distanceTimeY, sizeTimeHor, sizeTimeVer, jumpY);     // 투여횟수 위치를 잘라내어 배열에 저장
       //     Bitmap[] cDay = Crop.segCrop(ScaledImage, ScaleX - distanceDayX, ScaleY + distanceDayY, sizeDayHor, sizeDayVer, jumpY);      // 투약일수 위치를 잘라내어 배열에 저장
       //     ///////////////////  이미지 잘라내기 끝 //////////////////////

       //     /////////////////////  이미지 잘라내기 시작 /////////////////////
       //     //Bitmap cPatientName = Crop.cloneAforge(ScaledImage, ScaleX - distancePatientX, ScaleY + distancePatientY, sizePatientHor, sizePatientVer);   // 환자 주민번호 위치 잘라내기
       //     //cPatientName.Save(@".\cPatientName.png");
       //     //Bitmap cDoctorLicenseNumber = Crop.cloneAforge(ScaledImage, ScaleX - distanceDoctorX, ScaleY + distanceDoctorY, sizeDoctorHor, sizeDoctorVer);   // 의사면허 위치 잘라내기
       //     //cDoctorLicenseNumber.Save(@".\cDoctorLicenseNumber.png");
       //     //Bitmap cPrescriptionNumber = Crop.cloneAforge(ScaledImage, ScaleX - distancePreNumX, ScaleY + distancePreNumY, sizePreNumHor, sizePreNumVer);  // 교부번호 위치 잘라내기
       //     //cPrescriptionNumber.Save(@".\cPrescriptionNumber.png");
       //     //Bitmap[] cDrugCode = Crop.segCrop(ScaledImage, ScaleX - distanceDrugCodeX, ScaleY + distanceDrugCodeY, sizeDrugCodeHor, sizeDrugCodeVer, jumpY);  // 약품코드 위치를 잘라내어 배열에 저장
       //     //Bitmap[] cDose = Crop.segCrop(ScaledImage, ScaleX - distanceDoseX, ScaleY + distanceDoseY, sizeDoseHor, sizeDoseVer, jumpY);     // 투약량 위치를 잘라내어 배열에 저장
       //     //Bitmap[] cTime = Crop.segCrop(ScaledImage, ScaleX - distanceTimeX, ScaleY + distanceTimeY, sizeTimeHor, sizeTimeVer, jumpY);     // 투여횟수 위치를 잘라내어 배열에 저장
       //     //Bitmap[] cDay = Crop.segCrop(ScaledImage, ScaleX - distanceDayX, ScaleY + distanceDayY, sizeDayHor, sizeDayVer, jumpY);      // 투약일수 위치를 잘라내어 배열에 저장
       //     /////////////////////  이미지 잘라내기 끝 //////////////////////

       // //    Console.WriteLine("이미지를 좌표대로 분할했습니다.\n");
       //     MessageBox.Show("이미지를 좌표대로 분할했습니다.\n");

            ///////////////////// ocr 인식 시작 ////////////////////////           
            //Global.tPatientName = OcrEngine.ocr(cPatientName);   // 환자 주민번호 txt 저장
            //Console.WriteLine("환자 주민번호:{0:x}", Global.tPatientName);
            //Global.tDoctorLicenseNumber = OcrEngine.ocr(cDoctorLicenseNumber);   // 의사면허 txt 저장
            //Console.WriteLine("의사 면허번호:{0:x}", Global.tDoctorLicenseNumber);
            //Global.tPrescriptionNumber = OcrEngine.ocr(cPrescriptionNumber);    // 교부번호 txt 저장   
            //Console.WriteLine("교부번호:{0:x}", Global.tPrescriptionNumber);

            //Global.Endct = new int[4];
            //Global.tDrugCode = OcrEngine.segOcr(cDrugCode, out Global.Endct[0]);    // 약품코드 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.tDose = OcrEngine.segOcr(cDose, out Global.Endct[1]);           // 투약량 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.tTime = OcrEngine.segOcr(cTime, out Global.Endct[2]);           // 투여횟수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.tDay = OcrEngine.segOcr(cDay, out Global.Endct[3]);             // 투약일수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.EndctMax = Global.Endct.Max();   // 투약량,횟수,일수 중에서 가장 많이 인식한 숫자만큼 입력을 하기 위해 카운팅 값을 구한다.

            //     Console.WriteLine("EndctMax : {0:x}", Global.EndctMax);

            //   Console.WriteLine("End CT:{0:x}", Endct);

            // Console.Read();  // 핸들에 데이터 입력까지 볼 때는 이 부분을 주석처리 해야 진행됨, 콘솔창에서 테스트 할때 대기하려고.
            /////////////////// ocr 인식 끝 //////////////////////////

        //    fmOcr.Close();  // OCR 인식중 창 닫고

            //     Application.OpenForms["ocrRecogTime"].Close();  // ocr 인식 알림창 닫기

        //    Console.WriteLine("ocr 인식을 끝냈습니다.\n");
            //MessageBox.Show("ocr 인식을 끝냈습니다.\n");


            ////////////////// 핸들로 전달 시작 ////////////////////// --> 윈도우폼에서 처리하기로 함
            //typing.typePatientName(Global.hPatientName, Global.tPatientName);        // 환자 주민번호 입력 후 엔터
            //typing.typePrescriptionNumber(Global.hPrescriptionNumber, Global.tPrescriptionNumber);  // 교부번호 입력 후 엔터
            //typing.typeDoctorLicenseNumber(Global.hDoctorLicenseNumber, Global.tDoctorLicenseNumber); // 의사면허 입력 후 엔터

            //typing.segType(Global.hDrugCode, Global.hDose, Global.hTime, Global.hDay, Global.hTotalDose, Global.hInsureCombo, Global.hTakeCombo, Global.hAlterCombo, Global.tDrugCode, Global.tDose, Global.tTime, Global.tDay, Global.EndctMax);   // 루프를 돌면서 약품코드, 투약량, 횟수, 투약일수를 입력한다.
            ///////////////////  핸들로 전달 끝 ///////////////////////

       //     Console.WriteLine("핸들로 문자를 전달했습니다.\n");

            //   Console.Read();


        //    //반복 실행시 변수에 쓰레기값이 들어가서 종료시 초기화 해본다.
        //    Global.tPatientNumber = null;
        //    Global.tPatientName = null;
        //    Global.tDoctorLicenseNumber = null;
        //    Global.tPrescriptionNumber = null;
        //    Global.tDrugCode = null;
        //    Global.tDrugName = null;
        //    Global.tDose = null;
        //    Global.tTime = null;
        //    Global.tDay = null;

        //    Global.Endct = null;
        //  //  Global.EndctMax = null;

        // //   Global.coordNo1X = null;
        // //   Global.coordNo1Y = null;
        ////    Global.coordNo2X = null;
        ////    Global.coordNo2Y = null;

        //    Global.source = null;



        }

        //public static void insideProcessAuto()
        //{
        //    ///////////// ini 객체 생성 시작 /////////////////////////////////////////////////////
        //    //현재 프로그램이 실행되고 있는정보 가져오기: 디버깅 모드라면 bin/debug/프로그램명.exe
        //    FileInfo exefileinfo = new FileInfo(@"C:\Program Files\PLOCR\PLOCR.exe");
        //    string pathini = exefileinfo.Directory.FullName.ToString();  //프로그램 실행되고 있는데 path 가져오기
        //    string fileName = @"\PLOCRconfig.ini";  // 환경설정 파일명
        //    string filePath = pathini + fileName;   //ini 파일 경로
        //    PLOCR.IniUtil ini = new PLOCR.IniUtil(filePath);   // 만들어 놓았던 iniUtil 객체 생성(생성자 인자로 파일경로 정보 넘겨줌)
        //    //////////// ini 객체 생성 끝 /////////////////////////////////////////////////////////           

        //    //String inihDose = ini.GetIniValue("PLOCR_handle", "hDose"); 
        //    //Global.hDose = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", -1, 3);

        //    //if (inihDose != Global.hDose.ToString())  // 찾아온 핸들값과 .ini 에서 읽어온 핸들값이 다르면 새로 핸들값 가져온다.
        //    //{
        //    //   Console.WriteLine(Global.hPatientName);
        //    //   Console.WriteLine(inihDose);

        //    //    Console.WriteLine("핸들값이 없어서 새로 읽어왔어요.");

        //    /////////////////////// 핸들 찾기 시작 ////////////////////////////            
        //    Global.hTFrmPrescriptionEdit = Handle.FindWindow("TFrmPrescriptionEdit", "처방조제"); // 처방조제 최상위 윈도우 핸들찾기          
        //    Global.hTPanelTop = Handle.Findchildchildwindow(Global.hTFrmPrescriptionEdit, 10, "TPanel", "TwMaskEdit"); // 처방조제 최상위 바로 아래 TPanel 이 8개 있으므로 10번 정도 돌려보고 찾은 TPanel 중에 하위에 TwMaskEdit 이 있는 TPanel 의 핸들값을 찾는다.      
        //    Global.hTPanelDrugName = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TPanel", 460, 86); // 처방조제 최상위 아래의 TPanel 중 가로 세로 460 86인 TPanel 을 찾는다. 이 TPanel 아래에 TwEdit (명칭) 이 들어있다.        
        //    Global.hDrugCode = Handle.FindSizeWindow(Global.hTPanelDrugName, 10, "TwEdit", 263, 20); // 명칭 상위의 TPanel 을 넣어, 하위에 "명칭" 가로 세로 263 20 을 찾는다.
        //    Global.hTPanelDoseTimeDay = Handle.FindSizeWindow(Global.hTPanelTop, 10, "TPanel", 461, 174); // 투약량,횟수,일수 의 상위 TPanel 을 가로 세로 461 74로 찾는다.
        //    Global.hDose = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", -1, 3);  // 투약량,횟수,일수 를 찾기 위해 OrderY, OrderX(배열이므로 0부터 시작) 순위를 넣어주어 윈도우 핸들을 찾는다. Y축으로 찾을 때는 OrderX = -1, X축으로 찾을 때는 OrderY = -1 을 넣는다.
        //    Global.hTime = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", -1, 2);  // 투약량,횟수,일수 를 찾기 위해 OrderY, OrderX(배열이므로 0부터 시작) 순위를 넣어주어 윈도우 핸들을 찾는다. Y축으로 찾을 때는 OrderX = -1, X축으로 찾을 때는 OrderY = -1 을 넣는다.
        //    Global.hDay = Handle.FindSortWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", 2, -1);  // 투약량,횟수,일수 를 찾기 위해 OrderY, OrderX(배열이므로 0부터 시작) 순위를 넣어주어 윈도우 핸들을 찾는다. Y축으로 찾을 때는 OrderX = -1, X축으로 찾을 때는 OrderY = -1 을 넣는다.
        //    Global.hTotalDose = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TwEdit", 77, 20); // 총투약, 가로 세로 77 20
        //    //     Global.hCopayDay = Handle.FindSizeWindow(hTPanelDoseTimeDay, 15, "TwEdit", 55, 20); // 본인부담일수, 가로 세로 55 20
        //    Global.hPatientName = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TwEdit", 78, 20); // hTPanelTop 를 부모 윈도우로 하여, 아래에 TwEdit 이 있는 텍스트박스를 찾고 박스의 Width 와 Height 를 비교하여 "환자이름" 의TwEdit 을 찾는다. 환자이름 입력창의 가로 = 78, 세로 = 20 이다.
        //    Global.hPrescriptionNumber = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TwEdit", 138, 20); // 교부번호의 가로 세로는 138 20 이다.
        //    Global.hDoctorLicenseNumber = Handle.FindSizeWindow(Global.hTPanelTop, 50, "TwEdit", 64, 20); // 의사면허의 가로 세로는 64 20 이다.
        //    //    Global.hHospitalCode = Handle.FindSizeWindow(hTPanelTop, 50, "TwEdit", 110, 20); // 발행기관의 가로 세로는 110 20 이다.
        //    //     Global.hPrescriptionDay = Handle.FindSortWindow(hTPanelTop, 50, "TDateTimePicker", -1, 1);
        //    //    Global.hFillPrescriptionDay = Handle.FindSortWindow(hTPanelTop, 50, "TDateTimePicker", -1, 0);
        //    //   Global.hTimeOutCombo = Handle.FindSizeWindow(hTPanelTop, 50, "TComboBox", 117, 22); // 시간외 콤보박스 가로 세로 117 22
        //    //    Global.hAdditionCombo = Handle.FindSizeWindow(hTPanelTop, 50, "TComboBox", 76, 22); // 비가산/소아가산/65세 콤보박스 가로 세로 76 22
        //    //    Global.hTimeOutEdit = Handle.FindSizeWindow(hTPanelTop, 50, "TwMaskEdit", 77, 20);  // FindWindowEx 또는 만든 함수 FindChildWindow 를 사용하는 것이 좋은데 작동을 안해서 FindSizeWindow 사용해서 찾았음
        //    Global.hAlterCombo = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TComboBox", 107, 22); // 형태(대체 등..) 가로 세로 107 22
        //    Global.hInsureCombo = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TComboBox", 84, 22); // 급여(100/100 등..) 가로 세로 107 22
        //    Global.hTakeCombo = Handle.FindSizeWindow(Global.hTPanelDoseTimeDay, 15, "TComboBox", 84, 20);  // 용법 가로 세로 84 20

        //    //   IntPtr hDrugSelectCode = Handle.FindWindow("TFrmSelectCode", "코드조회"); // 약품입력 팝업창

        //    //if (Global.hTFrmPrescriptionEdit == IntPtr.Zero)
        //    //{
        //    //    Console.WriteLine("처방조제 입력화면을 먼저 열어주세요.");
        //    //}
        //    /////////////////////// 핸들 찾기 끝////////////////////////////

        //    // Console.WriteLine("핸들을 찾았습니다.\n");

        //    //////////////////////// ini 값 입력 시작 /////////////////////////////
        //    //ini.SetIniValue("PLOCR_handle", "hTFrmPrescriptionEdit", Global.hTFrmPrescriptionEdit.ToString());  // ini 파일에 핸들값을 string으로 형변환 하여 텍스트로 저장한다. string 값이지만 int32 로 다시 변환하면 핸들값이 된다.
        //    //ini.SetIniValue("PLOCR_handle", "hTPanelTop", Global.hTPanelTop.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hTPanelDrugName", Global.hTPanelDrugName.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hDrugCode", Global.hDrugCode.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hTPanelDoseTimeDay", Global.hTPanelDoseTimeDay.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hDose", Global.hDose.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hTime", Global.hTime.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hDay", Global.hDay.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hTotalDose", Global.hTotalDose.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hPatientName", Global.hPatientName.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hPrescriptionNumber", Global.hPrescriptionNumber.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hDoctorLicenseNumber", Global.hDoctorLicenseNumber.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hAlterCombo", Global.hAlterCombo.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hInsureCombo", Global.hInsureCombo.ToString());
        //    //ini.SetIniValue("PLOCR_handle", "hTakeCombo", Global.hTakeCombo.ToString());
        //    ////   ini.SetIniValue("PLOCR_handle", "hDrugSelectCode", hDrugSelectCode.ToString());
        //    //////////////////////// ini 값 입력 끝 ///////////////////////////////
        //    //}
        //    //else  // 핸들값이 .ini 에 있으면 값을 가져온다.
        //    //{
        //    //ini.GetIniValue("PLOCR_handle", "hTFrmPrescriptionEdit");  // ini 파일에서 핸들값을 가져옴, 형변환 해야함
        //    //ini.GetIniValue("PLOCR_handle", "hTPanelTop");
        //    //ini.GetIniValue("PLOCR_handle", "hTPanelDrugCode");
        //    //ini.GetIniValue("PLOCR_handle", "hDrugCode");
        //    //ini.GetIniValue("PLOCR_handle", "hTPanelDoseTimeDay");
        //    //ini.GetIniValue("PLOCR_handle", "hDose");
        //    //ini.GetIniValue("PLOCR_handle", "hTime");
        //    //ini.GetIniValue("PLOCR_handle", "hDay");
        //    //ini.GetIniValue("PLOCR_handle", "hTotalDose");
        //    //ini.GetIniValue("PLOCR_handle", "hPatientName");
        //    //ini.GetIniValue("PLOCR_handle", "hPrescriptionNumber");
        //    //ini.GetIniValue("PLOCR_handle", "hDoctorLicenseNumber");
        //    //ini.GetIniValue("PLOCR_handle", "hAlterCombo");
        //    //ini.GetIniValue("PLOCR_handle", "hInsureCombo");
        //    //ini.GetIniValue("PLOCR_handle", "hTakeCombo");
        //    //}

        //    /////////////////// 처방전 스캔 시작 ///////////////////////////
        //    //splashPres fmPres = new splashPres();
        //    //fmPres.Owner = DataEdit.ActiveForm;  // child form 을 알리고
        //    //fmPres.Show();       // 스캔중 화면을 띄우고

        //    //Bitmap source = scan.scanImage();

        //    //fmPres.Close(); // 스캔중 화면 닫고
        //    ///////////////////// 처방전 스캔 끝 ///////////////////////////

        //    /////////////// 처방전 스캔 프로그램 시작 ///////////////////////////
        //    //splashPres fmPres = new splashPres();
        //    //fmPres.Owner = DataEdit.ActiveForm;  // child form 을 알리고
        //    //fmPres.Show();       // 스캔중 화면을 띄우고

        //    //System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
        //    //start.FileName = @"C:\Program Files\PLOCR\PLOCRscan.exe";         // 스캔 프로그램 시작
        //    //start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;   // 윈도우 속성을  windows hidden  으로 지정
        //    //start.CreateNoWindow = true;  // hidden 을 시키기 위해서 이 속성도 true 로 체크해야 함

        //    //Process process = Process.Start(start);
        //    //process.WaitForExit(); // 외부 프로세스가 끝날 때까지 프로그램의 진행을 멈춘다.  

        //    //fmPres.Close(); // 스캔중 화면 닫고
        //    ///////////////// 처방전 스캔 프로그램 끝 ///////////////////////////


        //    splashImageProcess fmImageProcess = new splashImageProcess();
        //    fmImageProcess.Owner = DataEdit.ActiveForm;  // child form 을 알리고
        //    fmImageProcess.Show();       // 이미지 처리중 화면을 띄우고

        //    string path = @"C:\Program Files\PLOCR\prescription.png";
        //    Bitmap source = (Bitmap)Bitmap.FromFile(path);

        //    source = calculator.sequence(source); // 이미지 전처리

        //    //     source.Save(@"C:\Program Files\PLOCR\processedprescription.png");

        //    //  source = ImageProcess.Scaling(source, 500);                 // % 로 확대한다. 

        //    FindZeroXY.divSearch1(source);  // 좌표1번 찾고
        //    FindZeroXY.divSearch2(source);  // 좌표2번 찾고

        //    fmImageProcess.Close();


        //    //MessageBox.Show("찾아낸 좌표1번은 : " + Global.coordNo1X + " " + Global.coordNo1Y);            
        //    //MessageBox.Show("찾아낸 좌표2번은 : " + Global.coordNo2X + " " + Global.coordNo2Y);                        

        //    //MessageBox.Show("기준좌표1번 x 는" + Global.coordNo1X.ToString());
        //    //MessageBox.Show("기준좌표1번 y 는" + Global.coordNo1Y.ToString());
        //    //MessageBox.Show("기준좌표2번 x 는" + Global.coordNo2X.ToString());
        //    //MessageBox.Show("기준좌표2번 y 는" + Global.coordNo2Y.ToString());      


        //    //string path = @"C:\Program Files\PLOCR\prescription.png";
        //    //Bitmap source = (Bitmap)Bitmap.FromFile(path);

        //    splashOcr fmOcr = new splashOcr();  // ocr 인식 알림
        //    fmOcr.Owner = DataEdit.ActiveForm;  // child form 을 알리고
        //    fmOcr.Show();       // OCR 인식중 화면을 띄우고


        //    source = calculator.originalcheck(source);      // 처리순서 끝에 원본이 필요한가 확인한다. 처방전이 흐린 경우 이미지처리후 좌표잡고 실제 읽을 때는 원본으로 하는 경우가 된다.

        //    Global.tPatientNumber = calculator.getTargetText(source, "주민번호");   // 환자 주민번호 txt 저장

        //    //    MessageBox.Show("읽어낸 주민번호는 = " + Global.tPatientNumber);

        //    Global.tPatientName = calculator.getTargetText(source, "환자이름");

        //    //      MessageBox.Show("읽어낸 환자이름은 = " + Global.tPatientName);

        //    Global.tDoctorLicenseNumber = calculator.getTargetText(source, "의사면허");  // 의사 면허번호 txt 저장

        //    //      MessageBox.Show("읽어낸 면허번호는 = " + Global.tDoctorLicenseNumber);

        //    Global.tPrescriptionNumber = calculator.getTargetText(source, "교부번호");

        //    //      MessageBox.Show("읽어낸 교부번호는 = " + Global.tPrescriptionNumber);

        //    string DrugAreaCheck = ini.GetIniValue("약품영역사용", "사용여부");   // 약품영역으로 할 것인지, 약품코드와 약품명을 각각 읽어낼 것인지 결정한다. 1이면 약품영역좌표 전체로, 0 이면 각각 코드와 약품명 좌표로 읽어낸다.

        //    if (DrugAreaCheck == "1")
        //    {
        //        Global.tDose = calculator.getDrugDose(source);           // 투약량 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
        //        Global.tTime = calculator.getDrugTime(source);           // 투여횟수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
        //        Global.tDay = calculator.getDrugDay(source);             // 총투약일수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.

        //        Global.EndctMax = Global.Endct.Max();
        //        //           MessageBox.Show("세가지로만 잡은 EndctMax 는" + Global.EndctMax.ToString());

        //        string tDrugCodeDrugNameArea = calculator.getDrugCodeDrugNameArea(source);     // 약품코드 약품명 영역을 읽어낸다.
        //        Global.tDrugCode = TextProcess.DrugCodeSplit(tDrugCodeDrugNameArea);   // 읽어낸 약품코드를 문자열 후처리를 통해 글로벌 변수에 저장한다.
        //        Global.tDrugName = TextProcess.DrugNameSplit(tDrugCodeDrugNameArea);   // 읽어낸 약품명을 문자열 후처리를 통해 글로벌 변수에 저장한다.                                             
        //    }
        //    else if (DrugAreaCheck == "0")
        //    {
        //        Global.tDrugCode = calculator.getDrugCode(source);    // 약품코드 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
        //        Global.tDrugName = calculator.getDrugName(source);
        //        Global.tDose = calculator.getDrugDose(source);           // 투약량 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
        //        Global.tTime = calculator.getDrugTime(source);           // 투여횟수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
        //        Global.tDay = calculator.getDrugDay(source);             // 총투약일수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.

        //        Global.EndctMax = Global.Endct.Max();   // 투약량,횟수,일수 중에서 가장 많이 인식한 숫자만큼 입력을 하기 위해 카운팅 값을 구한다.
        //        // 입력 모듈 반복수를 구하기 위해 스캔하면서 정해둔다. 이 값이 4라면 0부터 시작하므로 5행까지 입력하게 된다.
        //    }

        //    fmOcr.Close();  // 인식창 닫고

      //      ini.SetIniValue("스캔후자동실행", "자동실행여부", "0");  // 스캔후 자동실행 여부를 미실행으로 다시 지정해둠

            // MessageBox.Show("EndctMax = " + Global.EndctMax);   

            //     ////////////////////// 이미지 전처리 시작 //////////////////////
            //     string path = @"C:\Program Files\PLOCR\prescription.png";    // twain 이용하여 스캐너로부터 가져와야 하는 이미지, twain 연결 부분
            //     Bitmap source = (Bitmap)Bitmap.FromFile(path);
            //     Bitmap ProcessedImage = ImageProcess.ImgProcess(source);
            //                ProcessedImage.Save(@".\ProcessedImage.png");
            //     ////////////////////  이미지 전처리 끝 /////////////////////////

            //  //   Console.WriteLine("이미지 전처리를 끝냈습니다.\n");
            //       MessageBox.Show("이미지 전처리를 끝냈습니다.\n");


            //     ////////////////////  좌표영점 찾아 좌표설정 시작 //////////////////////
            //     string ZeroHtml = OcrEngine.hocr(ProcessedImage);  // 좌표 영점을 찾기 위해 hocr 실행       

            //     int zeroWidth, zeroHeight, ScaleX, ScaleY;

            //     int distancePatientX, distancePatientY, sizePatientHor, sizePatientVer;
            //     int distanceDoctorX, distanceDoctorY, sizeDoctorHor, sizeDoctorVer;
            //     int distancePreNumX, distancePreNumY, sizePreNumHor, sizePreNumVer;
            //     int distanceDrugCodeX, distanceDrugCodeY, sizeDrugCodeHor, sizeDrugCodeVer;
            //     int distanceDoseX, distanceDoseY, sizeDoseHor, sizeDoseVer;
            //     int distanceTimeX, distanceTimeY, sizeTimeHor, sizeTimeVer;
            //     int distanceDayX, distanceDayY, sizeDayHor, sizeDayVer;
            //     int jumpY;
            //     int factor;

            //     FindZeroXY.zeroSearch(ZeroHtml,
            //         out zeroWidth, out zeroHeight, out ScaleX, out ScaleY,
            //         out distancePatientX, out distancePatientY, out sizePatientHor, out sizePatientVer,
            //         out distanceDoctorX, out distanceDoctorY, out sizeDoctorHor, out sizeDoctorVer,
            //         out distancePreNumX, out distancePreNumY, out sizePreNumHor, out sizePreNumVer,
            //         out distanceDrugCodeX, out distanceDrugCodeY, out sizeDrugCodeHor, out sizeDrugCodeVer,
            //         out distanceDoseX, out distanceDoseY, out sizeDoseHor, out sizeDoseVer,
            //         out distanceTimeX, out distanceTimeY, out sizeTimeHor, out sizeTimeVer,
            //         out distanceDayX, out distanceDayY, out sizeDayHor, out sizeDayVer,
            //         out jumpY,
            //         out factor);  // 좌표영점의 가로세로와 표준화된 좌표영점을 반환받는다.       

            //   //    Console.WriteLine(zeroWidth);
            //   //     Console.WriteLine(ScaleX);

            //         MessageBox.Show(zeroWidth.ToString());
            //         MessageBox.Show(ScaleX.ToString());

            //     ////////////////////  좌표영점 찾아 좌표설정 끝 ///////////////////////

            // //          Console.WriteLine("좌표영점을 찾았습니다.\n");
            //        MessageBox.Show("좌표영점을 찾았습니다.\n");

            //     ///////////////////   이미지 사이즈 표준화 시작 ///////////////////    
            //     if (zeroWidth == 0) 
            //     {
            //         MessageBox.Show("좌표영점 인식에 실패했습니다.\n 다른 처방전으로 시험해보세요.\n 인식률을 더 개선시켜 놓을께요.");
            //     }

            //     Bitmap ScaledImage = ImageProcess.ScaleByPercent(ProcessedImage, factor / zeroWidth);
            //               ScaledImage.Save(@".\ScaledImage.png");            
            //     //////////////////    이미지 사이즈 표준화 끝 ///////////////////

            ////     Console.WriteLine("이미지 사이즈 표준화를 끝냈습니다.\n");

            //     ///////////////////  이미지 잘라내기 시작 /////////////////////
            //     Bitmap cPatientName = Crop.cloneAforge(ScaledImage, ScaleX - distancePatientX, ScaleY + distancePatientY, sizePatientHor, sizePatientVer);   // 환자 주민번호 위치 잘라내기
            //                cPatientName.Save(@".\cPatientName.png");
            //     Bitmap cDoctorLicenseNumber = Crop.cloneAforge(ScaledImage, ScaleX - distanceDoctorX, ScaleY + distanceDoctorY, sizeDoctorHor, sizeDoctorVer);   // 의사면허 위치 잘라내기
            //                cDoctorLicenseNumber.Save(@".\cDoctorLicenseNumber.png");
            //     Bitmap cPrescriptionNumber = Crop.cloneAforge(ScaledImage, ScaleX - distancePreNumX, ScaleY + distancePreNumY, sizePreNumHor, sizePreNumVer);  // 교부번호 위치 잘라내기
            //                cPrescriptionNumber.Save(@".\cPrescriptionNumber.png");
            //     Bitmap[] cDrugCode = Crop.segCrop(ScaledImage, ScaleX - distanceDrugCodeX, ScaleY + distanceDrugCodeY, sizeDrugCodeHor, sizeDrugCodeVer, jumpY);  // 약품코드 위치를 잘라내어 배열에 저장
            //     Bitmap[] cDose = Crop.segCrop(ScaledImage, ScaleX - distanceDoseX, ScaleY + distanceDoseY, sizeDoseHor, sizeDoseVer, jumpY);     // 투약량 위치를 잘라내어 배열에 저장
            //     Bitmap[] cTime = Crop.segCrop(ScaledImage, ScaleX - distanceTimeX, ScaleY + distanceTimeY, sizeTimeHor, sizeTimeVer, jumpY);     // 투여횟수 위치를 잘라내어 배열에 저장
            //     Bitmap[] cDay = Crop.segCrop(ScaledImage, ScaleX - distanceDayX, ScaleY + distanceDayY, sizeDayHor, sizeDayVer, jumpY);      // 투약일수 위치를 잘라내어 배열에 저장
            //     ///////////////////  이미지 잘라내기 끝 //////////////////////

            //     /////////////////////  이미지 잘라내기 시작 /////////////////////
            //     //Bitmap cPatientName = Crop.cloneAforge(ScaledImage, ScaleX - distancePatientX, ScaleY + distancePatientY, sizePatientHor, sizePatientVer);   // 환자 주민번호 위치 잘라내기
            //     //cPatientName.Save(@".\cPatientName.png");
            //     //Bitmap cDoctorLicenseNumber = Crop.cloneAforge(ScaledImage, ScaleX - distanceDoctorX, ScaleY + distanceDoctorY, sizeDoctorHor, sizeDoctorVer);   // 의사면허 위치 잘라내기
            //     //cDoctorLicenseNumber.Save(@".\cDoctorLicenseNumber.png");
            //     //Bitmap cPrescriptionNumber = Crop.cloneAforge(ScaledImage, ScaleX - distancePreNumX, ScaleY + distancePreNumY, sizePreNumHor, sizePreNumVer);  // 교부번호 위치 잘라내기
            //     //cPrescriptionNumber.Save(@".\cPrescriptionNumber.png");
            //     //Bitmap[] cDrugCode = Crop.segCrop(ScaledImage, ScaleX - distanceDrugCodeX, ScaleY + distanceDrugCodeY, sizeDrugCodeHor, sizeDrugCodeVer, jumpY);  // 약품코드 위치를 잘라내어 배열에 저장
            //     //Bitmap[] cDose = Crop.segCrop(ScaledImage, ScaleX - distanceDoseX, ScaleY + distanceDoseY, sizeDoseHor, sizeDoseVer, jumpY);     // 투약량 위치를 잘라내어 배열에 저장
            //     //Bitmap[] cTime = Crop.segCrop(ScaledImage, ScaleX - distanceTimeX, ScaleY + distanceTimeY, sizeTimeHor, sizeTimeVer, jumpY);     // 투여횟수 위치를 잘라내어 배열에 저장
            //     //Bitmap[] cDay = Crop.segCrop(ScaledImage, ScaleX - distanceDayX, ScaleY + distanceDayY, sizeDayHor, sizeDayVer, jumpY);      // 투약일수 위치를 잘라내어 배열에 저장
            //     /////////////////////  이미지 잘라내기 끝 //////////////////////

            // //    Console.WriteLine("이미지를 좌표대로 분할했습니다.\n");
            //     MessageBox.Show("이미지를 좌표대로 분할했습니다.\n");

            ///////////////////// ocr 인식 시작 ////////////////////////           
            //Global.tPatientName = OcrEngine.ocr(cPatientName);   // 환자 주민번호 txt 저장
            //Console.WriteLine("환자 주민번호:{0:x}", Global.tPatientName);
            //Global.tDoctorLicenseNumber = OcrEngine.ocr(cDoctorLicenseNumber);   // 의사면허 txt 저장
            //Console.WriteLine("의사 면허번호:{0:x}", Global.tDoctorLicenseNumber);
            //Global.tPrescriptionNumber = OcrEngine.ocr(cPrescriptionNumber);    // 교부번호 txt 저장   
            //Console.WriteLine("교부번호:{0:x}", Global.tPrescriptionNumber);

            //Global.Endct = new int[4];
            //Global.tDrugCode = OcrEngine.segOcr(cDrugCode, out Global.Endct[0]);    // 약품코드 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.tDose = OcrEngine.segOcr(cDose, out Global.Endct[1]);           // 투약량 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.tTime = OcrEngine.segOcr(cTime, out Global.Endct[2]);           // 투여횟수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.tDay = OcrEngine.segOcr(cDay, out Global.Endct[3]);             // 투약일수 이미지 배열을 인자로 전달하여 ocr 실행후 텍스트 배열을 받는다.
            //Global.EndctMax = Global.Endct.Max();   // 투약량,횟수,일수 중에서 가장 많이 인식한 숫자만큼 입력을 하기 위해 카운팅 값을 구한다.

            //     Console.WriteLine("EndctMax : {0:x}", Global.EndctMax);

            //   Console.WriteLine("End CT:{0:x}", Endct);

            // Console.Read();  // 핸들에 데이터 입력까지 볼 때는 이 부분을 주석처리 해야 진행됨, 콘솔창에서 테스트 할때 대기하려고.
            /////////////////// ocr 인식 끝 //////////////////////////

            //    fmOcr.Close();  // OCR 인식중 창 닫고

            //     Application.OpenForms["ocrRecogTime"].Close();  // ocr 인식 알림창 닫기

            //    Console.WriteLine("ocr 인식을 끝냈습니다.\n");
            //MessageBox.Show("ocr 인식을 끝냈습니다.\n");


            ////////////////// 핸들로 전달 시작 ////////////////////// --> 윈도우폼에서 처리하기로 함
            //typing.typePatientName(Global.hPatientName, Global.tPatientName);        // 환자 주민번호 입력 후 엔터
            //typing.typePrescriptionNumber(Global.hPrescriptionNumber, Global.tPrescriptionNumber);  // 교부번호 입력 후 엔터
            //typing.typeDoctorLicenseNumber(Global.hDoctorLicenseNumber, Global.tDoctorLicenseNumber); // 의사면허 입력 후 엔터

            //typing.segType(Global.hDrugCode, Global.hDose, Global.hTime, Global.hDay, Global.hTotalDose, Global.hInsureCombo, Global.hTakeCombo, Global.hAlterCombo, Global.tDrugCode, Global.tDose, Global.tTime, Global.tDay, Global.EndctMax);   // 루프를 돌면서 약품코드, 투약량, 횟수, 투약일수를 입력한다.
            ///////////////////  핸들로 전달 끝 ///////////////////////

            //     Console.WriteLine("핸들로 문자를 전달했습니다.\n");

            //   Console.Read();


            //    //반복 실행시 변수에 쓰레기값이 들어가서 종료시 초기화 해본다.
            //    Global.tPatientNumber = null;
            //    Global.tPatientName = null;
            //    Global.tDoctorLicenseNumber = null;
            //    Global.tPrescriptionNumber = null;
            //    Global.tDrugCode = null;
            //    Global.tDrugName = null;
            //    Global.tDose = null;
            //    Global.tTime = null;
            //    Global.tDay = null;

            //    Global.Endct = null;
            //  //  Global.EndctMax = null;

            // //   Global.coordNo1X = null;
            // //   Global.coordNo1Y = null;
            ////    Global.coordNo2X = null;
            ////    Global.coordNo2Y = null;

            //    Global.source = null;



       // }

        
    }
}

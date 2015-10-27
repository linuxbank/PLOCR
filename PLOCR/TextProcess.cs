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

namespace PLOCR
{
    class TextProcess
    {
        public static string RemoveWhiteSpace(string OriginText)
        {
            string ProcessedText = Regex.Replace(OriginText, @"\s+", string.Empty, RegexOptions.Multiline).TrimEnd(); // 라인의 내외부 공백제거
        
            return ProcessedText;
        }
        
        public static string DotReplace(string OriginText)
        {
            string ProcessedText = OriginText.Replace("-", "."); // . 을 - 로 오인식 하는 경우를 대비해 . 으로 바꾼다.

            return ProcessedText;
        }

        public static string PointInsert(string OriginText)  // 투약량에서 소수점 인식을 못한 경우, 소수점을 추가하는 함수
        {
            string pattern = "^0[.]?";  // 문자열의 첫글자가 "0" 또는 "0." 이면
            string replacement = "0.";  // 위 패턴을 가진 문자열 부분을 모두 "0." 으로 바꾼다.
            string ProcessedText = Regex.Replace(OriginText, pattern, replacement);

            return ProcessedText;
        }

        public static string ZeroInsert(string OriginText)  // 투약량에서 소수점이 있는 경우 소수점 바로 앞자리는 "0"으로 바꾸는 함수
        {
            string pattern = "^d.?";  // 문자열에 소수점이 있으면
            string replacement = "0.";  // 소수점 바로 앞자리를 "0" 으로 바꾼다.
            string ProcessedText = Regex.Replace(OriginText, pattern, replacement);

            return ProcessedText;
        }

        public static string decimalInsert(string OriginText)   // 3.3333 이나 6.6666 을 가진 수의 경우 처음 나오는 숫자를 "0" 으로 바꾸는 함수
        {
            string pattern = "^[3,6,9]?.";
            string replacement = "0.";
            string ProcessedText = Regex.Replace(OriginText, pattern, replacement);

            return ProcessedText;
        }

        public static string[] DrugCodeSplit(string OriginText)   // 약품코드 약품명 영역에서 인식한 문자열을 받아서 약품코드와 약품명으로 구분한 문자열 생성해서 약품코드/약품명 변수에 저장
        {
            String[] tDrugName = new String[13];
            String[] tDrugCode = new String[13];
      //      MessageBox.Show("초기 영역 인식한 결과는 =" + OriginText);

            string ProcessedText = Regex.Replace(OriginText, @"<(.|\n)*?>", String.Empty);  // html 태그 제거
      //      MessageBox.Show("html 태그 제거후 =" + ProcessedText);
            
            ProcessedText = Regex.Replace(ProcessedText, "[^\uAC00-\uD7A3xfe0-9a-zA-Z\\s]", "");   // 특수문자를 없애고
      //      MessageBox.Show("특수문자 제거후 =" + ProcessedText);

            ProcessedText = Regex.Replace(ProcessedText, @"\s+", string.Empty, RegexOptions.Multiline).TrimEnd(); // 라인의 내외부 공백제거
      //      MessageBox.Show("공백제거후 =" + ProcessedText);

            ProcessedText = Regex.Replace(ProcessedText, "내복", "");         // "내복" 글자 제거
      //      MessageBox.Show("내복 글자제거후 =" + ProcessedText);             

            ProcessedText = Regex.Replace(ProcessedText, @"[0-9]{6,9}", "$0#");     // 약품코드 뒤로 구분자 # 붙이기
     //       MessageBox.Show("구분자 붙인후 = " + ProcessedText);             
                        
            string ProcessedDrugName = Regex.Replace(ProcessedText, @"[0-9]{6,9}", ""); // 약품코드 부분을 삭제하고 
   //         MessageBox.Show("약품코드를 제거하고 남은 약품명 =" + ProcessedDrugName);

            ProcessedDrugName = Regex.Replace(ProcessedDrugName, @"^#", "");  // 제일 처음 #을 삭제하고
     //       MessageBox.Show("처음 # 을 제거하고 약품명=" + ProcessedDrugName);               
        
            tDrugName = ProcessedDrugName.Split('#');    // 구분자 # 을 기준으로 문자열 분리해서 약품명 배열에 저장
            Global.Endct[1] = tDrugName.Length - 1;   // 약품코드 배열의 갯수를 저장하고
     //       MessageBox.Show("코드 찾는 중에 Global.Endct[1]은 " + Global.Endct[1]);

            int ct = 0;
            string ProcessedDrugCode = ProcessedText;
            do
            {                
                ProcessedDrugCode = Regex.Replace(ProcessedDrugCode, tDrugName[ct], "");
                ++ct;
                
       //         MessageBox.Show("현재 카운트 수는 =" + ct.ToString());
       //         MessageBox.Show("약품명을 한개씩 제거하고 남은 코드는=" + ProcessedDrugCode);                
            }
            while (ct < tDrugName.Length);  // 약품명이 있는 수 만큼만 루프를 돌려야 out of range 배열에러가 안 남 !! 한참 헤맸음 ㅋㅋㅋ
            {
                ProcessedDrugCode = Regex.Replace(ProcessedDrugCode, @"#$", "");
       //         MessageBox.Show("끝의 # 을 제거하면 =" + ProcessedDrugCode);
            }

            tDrugCode = ProcessedDrugCode.Split('#');    // 약품코드를 구분자 #을 기준으로 잘라서 배열에 저장
            Global.Endct[0] = tDrugCode.Length - 1;  // 약품코드가 몇번째줄까지 있나 저장해둠             

      //      MessageBox.Show(tDrugCode[0]);
      //      MessageBox.Show(tDrugCode[1]);
      //      MessageBox.Show(tDrugCode[2]);
      //      MessageBox.Show(tDrugCode[3]);
      //      MessageBox.Show(tDrugCode[4]);
      //      MessageBox.Show("Global.Endct[1]은 " + Global.Endct[1]);
                  

        //    int ct = 0;
      //      do
       //     {
       //         Global.tDrugCode[ct] = splitedDrugCodeDrugName[(ct * 2)+1];         // 이 부분 에러남, 배열에서 배열로 넘겨줄 때 참조값으로 되어서 null 예외가 나오는 것 같음

        //        MessageBox.Show("약품코드" + (ct+1) + "번은" + Global.tDrugCode[ct]);

       //         Global.tDrugName[ct] = splitedDrugCodeDrugName[(ct * 2)+2];

       //         MessageBox.Show("약품명" + (ct+1) + "번은" + Global.tDrugName[ct]);

      //          ++ct;
      //      }
      //      while (splitedDrugCodeDrugName[ct] != null);
       //     {
      //      }      
            return tDrugCode;
        }

        public static string[] DrugNameSplit(string OriginText)   // 약품코드 약품명 영역에서 인식한 문자열을 받아서 약품코드와 약품명으로 구분한 문자열 생성해서 약품코드/약품명 변수에 저장
        {
            String[] tDrugName = new String[13];           
            //      MessageBox.Show("초기 영역 인식한 결과는 =" + OriginText);

            string ProcessedText = Regex.Replace(OriginText, @"<(.|\n)*?>", String.Empty);  // html 태그 제거
            //      MessageBox.Show("html 태그 제거후 =" + ProcessedText);

            ProcessedText = Regex.Replace(ProcessedText, "[^\uAC00-\uD7A3xfe0-9a-zA-Z\\s]", "");   // 특수문자를 없애고
            //      MessageBox.Show("특수문자 제거후 =" + ProcessedText);

            ProcessedText = Regex.Replace(ProcessedText, @"\s+", string.Empty, RegexOptions.Multiline).TrimEnd(); // 라인의 내외부 공백제거
            //      MessageBox.Show("공백제거후 =" + ProcessedText);

            ProcessedText = Regex.Replace(ProcessedText, "내복", "");         // "내복" 글자 제거
            //      MessageBox.Show("내복 글자제거후 =" + ProcessedText);             

            ProcessedText = Regex.Replace(ProcessedText, @"[0-9]{6,9}", "$0#");     // 약품코드 뒤로 구분자 # 붙이기
            //       MessageBox.Show("구분자 붙인후 = " + ProcessedText);             

            string ProcessedDrugName = Regex.Replace(ProcessedText, @"[0-9]{6,9}", ""); // 약품코드 부분을 삭제하고 
            //         MessageBox.Show("약품코드를 제거하고 남은 약품명 =" + ProcessedDrugName);

            ProcessedDrugName = Regex.Replace(ProcessedDrugName, @"^#", "");  // 제일 처음 #을 삭제하고
            //       MessageBox.Show("처음 # 을 제거하고 약품명=" + ProcessedDrugName);               

            tDrugName = ProcessedDrugName.Split('#');    // 구분자 # 을 기준으로 문자열 분리해서 약품명 배열에 저장            
            Global.Endct[1] = tDrugName.Length - 1;   // 약품코드 배열의 갯수를 저장하고
        //    MessageBox.Show("약품명만 돌리는 곳에서 Global.Endct[1]은 " + Global.Endct[1]);          

      //      int ct = 0;
      //      string ProcessedDrugCode = ProcessedText;
      //      do
      //      {
      //          ProcessedDrugCode = Regex.Replace(ProcessedDrugCode, Global.tDrugName[ct], "");
      //          ++ct;
                //        MessageBox.Show("약품명을 한개씩 제거하고 남은 코드는=" + ProcessedDrugCode);
      //      }
      //      while (ct < Global.Endct[0]);
      //      {
      //          ProcessedDrugCode = Regex.Replace(ProcessedDrugCode, @"#$", "");
                //      MessageBox.Show("끝의 # 을 제거하면 =" + ProcessedDrugCode);
      //      }

      //      tDrugCode = ProcessedDrugCode.Split('#');    // 약품코드를 구분자 #을 기준으로 잘라서 배열에 저장
      //      Global.Endct[1] = tDrugCode.Length - 1;  // 약품코드가 몇번째줄까지 있나 저장해둠   
            //      MessageBox.Show("Global.Endct[1]은 " + Global.Endct[1]);


            //    int ct = 0;
            //      do
            //     {
            //         Global.tDrugCode[ct] = splitedDrugCodeDrugName[(ct * 2)+1];         // 이 부분 에러남, 배열에서 배열로 넘겨줄 때 참조값으로 되어서 null 예외가 나오는 것 같음

            //        MessageBox.Show("약품코드" + (ct+1) + "번은" + Global.tDrugCode[ct]);

            //         Global.tDrugName[ct] = splitedDrugCodeDrugName[(ct * 2)+2];

            //         MessageBox.Show("약품명" + (ct+1) + "번은" + Global.tDrugName[ct]);

            //          ++ct;
            //      }
            //      while (splitedDrugCodeDrugName[ct] != null);
            //     {
            //      }   
            return tDrugName;
        }
    }
}

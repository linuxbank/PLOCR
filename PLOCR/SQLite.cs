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
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PLOCR
{
    class SQLite
    {
        public static string DrugCodeFromDrugName(string DrugName)      // 사용자가 수정한 약품명으로 DB 검색하여 정확한 약품코드를 가져온다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            string DrugCode = "";

            using (var conn = new SQLiteConnection(strConn))
            {
                if (string.IsNullOrEmpty(DrugName) == false)    // 사용자가 수정한 약품명이 빈칸일 때는 건너뛰기 위해
                {
                    conn.Open();
                    string sql = String.Format("SELECT [DrugCode] FROM [Drug] WHERE [DrugName]='{0}'", DrugName);

                    //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        DrugCode = (string)rdr["DrugCode"];
                    }
                    rdr.Close();
                    conn.Close();
                }
            }

            return DrugCode;
        }

        public static string PatientNumberFromPatientName(string PatientName)      // 사용자가 수정한 환자명으로 DB 검색하여 정확한 주민번호를 가져온다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            string PatientNumber = "";

            using (var conn = new SQLiteConnection(strConn))
            {
                if (string.IsNullOrEmpty(PatientName) == false)
                {
                    conn.Open();
                    string sql = String.Format("SELECT [PatientNumber] FROM [Patient] WHERE [PatientName]='{0}'", PatientName);

                    //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        PatientName = (string)rdr["PatientNumber"];
                    }
                    rdr.Close();
                    conn.Close();
                }
            }

            return PatientNumber;
        }

        public static string DrugNameFromDrugCode(string DrugCode)      // 인식한 약품코드로 DB 검색하여 정확한 약품명을 가져온다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            string DrugName = "";

            using (var conn = new SQLiteConnection(strConn))
            {
                if (string.IsNullOrEmpty(DrugCode) == false)
                {
                    conn.Open();
                    string sql = String.Format("SELECT [DrugName] FROM [Drug] WHERE [DrugCode]='{0}'", DrugCode);

                    //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        DrugName = (string)rdr["DrugName"];
                    }
                    rdr.Close();
                    conn.Close();
                }
            }

            return DrugName;
        }

        public static void insertDrugCodeDrugName(string DrugCode, string DrugName) // 사용자가 수정한 정확한 약품코드와 약품명을 DB 에 저장한다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            string DrugCodeInDB ="";
            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                if (string.IsNullOrEmpty(DrugCode) == false && string.IsNullOrEmpty(DrugName) == false)
                {
                    conn.Open();

                    string sqlcheckCode = String.Format("SELECT [DrugCode] FROM [Drug] WHERE [DrugCode]='{0}'", DrugCode);
                    //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                    SQLiteCommand cmdCheckCode = new SQLiteCommand(sqlcheckCode, conn);
                    SQLiteDataReader rdr = cmdCheckCode.ExecuteReader();
                    while (rdr.Read())
                    {
                        DrugCodeInDB = (string)rdr["DrugCode"];
                    }
                    rdr.Close();

                    if (DrugCodeInDB == "")
                    {
                        string sql = String.Format("INSERT INTO [Drug] ([DrugCode],[DrugName],[UseTF],[InsuranceTF],[DoseTF]) VALUES('{0}','{1}',1,1,0)", DrugCode, DrugName);
                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }

        public static void deleteNullDrugCodeDrugName()     // null 인 약품코드와 약품명 row 를 삭제한다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";            
            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                conn.Open();                             
                
                    string sql = "DELETE FROM [Drug] WHERE [DrugCode]=\"\" OR [DrugName]=\"\"";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                
                conn.Close();
            }
        }

        public static void deleteNullPatientNumberPatientName()     // null 인 주민번호와 환자명 row 를 삭제한다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                conn.Open();

                string sql = "DELETE FROM [Patient] WHERE [PatientNumber]=\"\" OR [PatientName]=\"\"";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        public static string PatientNameFromPatientNumber(string PatientNumber)      // 인식한 주민번호로 DB 검색하여 정확한 환자명을 가져온다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            string PatientName = Global.tPatientName;

            using (var conn = new SQLiteConnection(strConn))
            {
                if (string.IsNullOrEmpty(PatientNumber) == false)
                {
                    conn.Open();
                    string sql = String.Format("SELECT [PatientName] FROM [Patient] WHERE [PatientNumber]='{0}'", PatientNumber);

                    //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        PatientName = (string)rdr["PatientName"];
                    }
                    rdr.Close();
                    conn.Close();
                }
            }

            return PatientName;
        }

        public static void insertPatientNumberPatientName(string PatientNumber, string PatientName) // 사용자가 수정한 정확한 주민번호와 환자명을 DB 에 저장한다.
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            string DrugCodeInDB = "";
            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                if (string.IsNullOrEmpty(PatientNumber) == false && string.IsNullOrEmpty(PatientName) == false)
                {
                    conn.Open();

                    string sqlcheckCode = String.Format("SELECT [PatientNumber] FROM [Patient] WHERE [PatientNumber]='{0}'", PatientNumber);
                    //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                    SQLiteCommand cmdCheckCode = new SQLiteCommand(sqlcheckCode, conn);
                    SQLiteDataReader rdr = cmdCheckCode.ExecuteReader();
                    while (rdr.Read())
                    {
                        DrugCodeInDB = (string)rdr["PatientNumber"];
                    }
                    rdr.Close();

                    if (DrugCodeInDB == "")
                    {
                        string sql = String.Format("INSERT INTO [Patient] ([PatientNumber],[PatientName],[UseTF],[InquiryTF]) VALUES('{0}','{1}',1,0)", PatientNumber, PatientName);
                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }

        public static AutoCompleteStringCollection autocompleteDrugName()    // 약품명을 DB로부터 가져와서 텍스트박스에 보여주기 위한 스트링컬렉션 반환함수
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            using (var conn = new SQLiteConnection(strConn))
            {
                conn.Open();
                string sql = String.Format("SELECT [DrugName] FROM [Drug]"); 

                //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    col.Add(rdr["DrugName"].ToString());
                }
                rdr.Close();
                conn.Close();
            }            
            return col;
        }

        public static AutoCompleteStringCollection autocompleteDrugCode()    // 약품코드를 DB로부터 가져와서 텍스트박스에 보여주기 위한 스트링컬렉션 반환함수
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            using (var conn = new SQLiteConnection(strConn))
            {
                conn.Open();
                string sql = String.Format("SELECT [DrugCode] FROM [Drug]");

                //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    col.Add(rdr["DrugCode"].ToString());
                }
                rdr.Close();
                conn.Close();
            }
            return col;
        }

        public static AutoCompleteStringCollection autocompletePatientNumber()    // 주민번호를 DB로부터 가져와서 텍스트박스에 보여주기 위한 스트링컬렉션 반환함수
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            using (var conn = new SQLiteConnection(strConn))
            {
                conn.Open();
                string sql = String.Format("SELECT [PatientNumber] FROM [Patient]");

                //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    col.Add(rdr["PatientNumber"].ToString());
                }
                rdr.Close();
                conn.Close();
            }
            return col;
        }

        public static AutoCompleteStringCollection autocompletePatientName()    // 환자명을 DB로부터 가져와서 텍스트박스에 보여주기 위한 스트링컬렉션 반환함수
        {
            string strConn = @"Data Source=C:\Program Files\PLOCR\Data\PLOCR.db";
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            using (var conn = new SQLiteConnection(strConn))
            {
                conn.Open();
                string sql = String.Format("SELECT [PatientName] FROM [Patient]");

                //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    col.Add(rdr["PatientName"].ToString());
                }
                rdr.Close();
                conn.Close();
            }
            return col;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using Meiday;
using System.Data;
using log4net;

namespace Meiday
{
    public class Log
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); 

        public static void Debug()
        {
            String patient = "";

            //MessageBox.Show("현재 레벨 : DEBUG");
            String level = "DEBUG";

            //ip주소 알림
            string ipaddress = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                ipaddress = endPoint.Address.ToString();
            }
            //MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            //MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            //MessageBox.Show("현재 Class명 : " + prevClassName);

            // 환자 번호
            if (LoginViewModel.patient_id != null)
            {
                patient = LoginViewModel.patient_id;
            }

            //DB 로그데이터 삽입 
            OracleDBManager oracleDBManager = new OracleDBManager();
            oracleDBManager.GetConnection();

            string query = @"INSERT INTO LOG(LOG_LEVEL, CLASS, METHOD, IPADDRESS, PATIENT_ID) 
            VALUES('" + level + "' , '" + prevClassName + "', '" + prevFuncName + "','" + ipaddress + "','" + patient + "'" + ")";
            string query1 = @"commit";
            OracleDBManager.Instance.ExecuteNonQuery(query);
            OracleDBManager.Instance.ExecuteNonQuery(query1);

            oracleDBManager.Close();

        }
        public static void Debug(String message)
        {
            String patient = "";

            //MessageBox.Show("현재 레벨 : DEBUG");
            String level = "DEBUG";

            //ip주소 알림
            string ipaddress = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                ipaddress = endPoint.Address.ToString();
            }
            //MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            //MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            //MessageBox.Show("현재 Class명 : " + prevClassName);

            // 환자 번호
            if (LoginViewModel.patient_id != null)
            {
                patient = LoginViewModel.patient_id;
            }

            //DB 로그데이터 삽입 
            OracleDBManager oracleDBManager = new OracleDBManager();
            oracleDBManager.GetConnection();

            string query = @"INSERT INTO LOG(LOG_LEVEL, CLASS, METHOD, IPADDRESS, PATIENT_ID, ETC) 
            VALUES('" + level + "' , '" + prevClassName + "', '" + prevFuncName + "','" + ipaddress + "','" + patient + "','" + message + "')";
            string query1 = @"commit";
            OracleDBManager.Instance.ExecuteNonQuery(query);
            OracleDBManager.Instance.ExecuteNonQuery(query1);

            oracleDBManager.Close();

        }
        public static void Error(Exception ex)
        {
            String patient = "";
            String error = "";

            //MessageBox.Show("현재 레벨 : DEBUG");
            String level = "ERROR";

            //ip주소 알림
            string ipaddress = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                ipaddress = endPoint.Address.ToString();
            }
            //MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            //MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            //MessageBox.Show("현재 Class명 : " + prevClassName);

            // 환자 번호
            if (LoginViewModel.patient_id != null)
            {
                patient = LoginViewModel.patient_id;
            }

            // 오류 메시지
            if (ex != null)
            {
                error = ex.ToString();
                //MessageBox.Show("오류명 : " + ex.ToString());
            }

            //DB 로그데이터 삽입 
            OracleDBManager oracleDBManager = new OracleDBManager();
            oracleDBManager.GetConnection();

            string query = @"INSERT INTO LOG(LOG_LEVEL, CLASS, METHOD, IPADDRESS, PATIENT_ID, ERROR_MESSAGE) 
            VALUES('" + level + "' , '" + prevClassName + "', '" + prevFuncName + "','" + ipaddress + "','" + patient + "','" + error + "')";
            string query1 = @"commit";
            OracleDBManager.Instance.ExecuteNonQuery(query);
            OracleDBManager.Instance.ExecuteNonQuery(query1);

            oracleDBManager.Close();
        }
        public static void Error(Exception ex, string message)
        {
            String patient = "";
            String error = "";

            //MessageBox.Show("현재 레벨 : DEBUG");
            String level = "ERROR";

            //ip주소 알림
            string ipaddress = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                ipaddress = endPoint.Address.ToString();
            }
            //MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            //MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            //MessageBox.Show("현재 Class명 : " + prevClassName);

            // 환자 번호
            if (LoginViewModel.patient_id != null)
            {
                patient = LoginViewModel.patient_id;
            }

            // 오류 메시지
            if (ex != null)
            {
                error = ex.ToString();
                //MessageBox.Show("오류명 : " + ex.ToString());
            }

            //DB 로그데이터 삽입 
            OracleDBManager oracleDBManager = new OracleDBManager();
            oracleDBManager.GetConnection();

            string query = @"INSERT INTO LOG(LOG_LEVEL, CLASS, METHOD, IPADDRESS, PATIENT_ID, ERROR_MESSAGE) 
            VALUES('" + level + "' , '" + prevClassName + "', '" + prevFuncName + "','" + ipaddress + "','" + patient + "','" + message + "')";
            string query1 = @"commit";
            OracleDBManager.Instance.ExecuteNonQuery(query);
            OracleDBManager.Instance.ExecuteNonQuery(query1);

            oracleDBManager.Close();

            log.Error(message);//txt 저장 부분

        }
        public static void Fatal(Exception ex)
        {
            String patient = "";
            String error = "";

            //MessageBox.Show("현재 레벨 : DEBUG");
            String level = "FATAL";

            //ip주소 알림
            string ipaddress = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                ipaddress = endPoint.Address.ToString();
            }
            //MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            //MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            //MessageBox.Show("현재 Class명 : " + prevClassName);

            // 환자 번호
            if (LoginViewModel.patient_id != null)
            {
                patient = LoginViewModel.patient_id;
            }

            // 오류 메시지
            if (ex != null)
            {
                error = ex.ToString();
                //MessageBox.Show("오류명 : " + ex.ToString());
            }

            //DB 로그데이터 삽입 
            OracleDBManager oracleDBManager = new OracleDBManager();
            oracleDBManager.GetConnection();

            string query = @"INSERT INTO LOG(LOG_LEVEL, CLASS, METHOD, IPADDRESS, PATIENT_ID, ERROR_MESSAGE) 
            VALUES('" + level + "' , '" + prevClassName + "', '" + prevFuncName + "','" + ipaddress + "','" + patient + "','" + error + "')";
            string query1 = @"commit";
            OracleDBManager.Instance.ExecuteNonQuery(query);
            OracleDBManager.Instance.ExecuteNonQuery(query1);

            oracleDBManager.Close();

            log.Fatal("Fatal"); //txt 저장 부분 
        }
        public static void Fatal(Exception ex, String message)
        {
            String patient = "";
            String error = "";

            //MessageBox.Show("현재 레벨 : DEBUG");
            String level = "FATAL";

            //ip주소 알림
            string ipaddress = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                ipaddress = endPoint.Address.ToString();
            }
            //MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            //MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            //MessageBox.Show("현재 Class명 : " + prevClassName);

            // 환자 번호
            if (LoginViewModel.patient_id != null)
            {
                patient = LoginViewModel.patient_id;
            }

            // 오류 메시지
            if (ex != null)
            {
                error = ex.ToString();
                //MessageBox.Show("오류명 : " + ex.ToString());
            }

            //DB 로그데이터 삽입 
            OracleDBManager oracleDBManager = new OracleDBManager();
            oracleDBManager.GetConnection();

            string query = @"INSERT INTO LOG(LOG_LEVEL, CLASS, METHOD, IPADDRESS, PATIENT_ID, ERROR_MESSAGE) 
            VALUES('" + level + "' , '" + prevClassName + "', '" + prevFuncName + "','" + ipaddress + "','" + patient + "','" + message + "')";
            string query1 = @"commit";
            OracleDBManager.Instance.ExecuteNonQuery(query);
            OracleDBManager.Instance.ExecuteNonQuery(query1);

            oracleDBManager.Close();

            log.Fatal(message);//txt 저장 부분

        }
    }
}

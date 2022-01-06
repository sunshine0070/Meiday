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

namespace LogPractice
{
    public class LogRecord
    {
        public static void Debug()
        {
            MessageBox.Show("현재 레벨 : DEBUG");

            //ip주소 알림
            string localIP = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            MessageBox.Show("현재 Class명 : " + prevClassName);
        }

        public static void Error(Exception ex)
        {
            MessageBox.Show("현재 레벨 : DEBUG");

            //ip주소 알림
            string localIP = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            // 이전 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            MessageBox.Show("실행 함수명 : " + prevFuncName);

            // 이전 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            MessageBox.Show("현재 Class명 : " + prevClassName);

            if(ex != null)
            {
                MessageBox.Show("오류명 : " + ex.ToString());
            }

        }

        public static void Fatal(Exception ex)
        {
            MessageBox.Show("현재 레벨 : DEBUG");

            //ip주소 알림
            string localIP = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            MessageBox.Show("현재 IP : " + localIP); //endPoint의 IP주소

            //현재 함수명 
            string prevFuncName = new StackFrame(1, true).GetMethod().Name;
            MessageBox.Show("실행 함수명 : " + prevFuncName);

            //현재 Class명
            string prevClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            MessageBox.Show("현재 Class명 : " + prevClassName);

            //오류 메시지
            if(ex != null)
            {
                MessageBox.Show("오류명 : " + ex.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Shortcut
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // GUID를 뮤텍스명으로 사용  
            string mtxName = "{033fee45-4b60-43e6-a071-f56a5f3a9d07}";
           
            Mutex mtx = new Mutex(true, mtxName);

            // 1초 동안 뮤텍스를 획득하려 대기  
            TimeSpan tsWait = new TimeSpan(0, 0, 1);
            bool success = mtx.WaitOne(tsWait);

            // 실패하면 프로그램 종료  
            if (!success)
            {
                //MessageBox.Show("이미실행중입니다.");
                return;

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}

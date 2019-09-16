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
            Mutex mtx = new Mutex(true, mtxName, out bool isNew);
            if (isNew == false)
                return;
            mtx.ReleaseMutex();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new FrmSplash());
            Application.Run(new FrmMain());
        }
    }
}

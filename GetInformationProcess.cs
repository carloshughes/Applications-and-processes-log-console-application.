using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO; 

namespace Bitacora
{
    class GetInformationProcess
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowTextLength(IntPtr hWnd);

        //  int GetWindowText(
        //      __in   HWND hWnd,
        //      __out  LPTSTR lpString,
        //      __in   int nMaxCount
        //  );
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //  DWORD GetWindowThreadProcessId(
        //      __in   HWND hWnd,
        //      __out  LPDWORD lpdwProcessId
        //  );
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //HANDLE WINAPI OpenProcess(
        //  __in  DWORD dwDesiredAccess,
        //  __in  BOOL bInheritHandle,
        //  __in  DWORD dwProcessId
        //);
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        //  DWORD WINAPI GetModuleBaseName(
        //      __in      HANDLE hProcess,
        //      __in_opt  HMODULE hModule,
        //      __out     LPTSTR lpBaseName,
        //      __in      DWORD nSize
        //  );
        [DllImport("psapi.dll")]
        private static extern uint GetModuleBaseName(IntPtr hWnd, IntPtr hModule, StringBuilder lpFileName, int nSize);

        //  DWORD WINAPI GetModuleFileNameEx(
        //      __in      HANDLE hProcess,
        //      __in_opt  HMODULE hModule,
        //      __out     LPTSTR lpFilename,
        //      __in      DWORD nSize
        //  );
        [DllImport("psapi.dll")]
        private static extern uint GetModuleFileNameEx(IntPtr hWnd, IntPtr hModule, StringBuilder lpFileName, int nSize);

        //-------------------------------------------------------------------------------------//

        //This syntax is hide the windows application 

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

       


        public static string HideWindows ()
        {
            const int SW_HIDE = 0;
            const int SW_SHOW = 5;

            var handle = GetConsoleWindow();


            StringBuilder hide = new StringBuilder();
            // Hide
            ShowWindow(handle, SW_HIDE);
            return hide.ToString();
        }
  

        //-------------------------------------------------------------------------------------//

        public static string GetTopWindowText()
        {
            IntPtr hWnd = GetForegroundWindow();
            int length = GetWindowTextLength(hWnd);
            StringBuilder text = new StringBuilder(length + 1);
            GetWindowText(hWnd, text, text.Capacity);
            return text.ToString();
        }

        public static string GetTopWindowName()
        {
            IntPtr hWnd = GetForegroundWindow();
            uint lpdwProcessId;
            GetWindowThreadProcessId(hWnd, out lpdwProcessId);
            IntPtr hProcess = OpenProcess(0x0410, false, lpdwProcessId);

            StringBuilder text = new StringBuilder(1000);
            GetModuleBaseName(hProcess, IntPtr.Zero, text, text.Capacity);
            GetModuleFileNameEx(hProcess, IntPtr.Zero, text, text.Capacity);

            CloseHandle(hProcess);


            return text.ToString();
        }


        public static string ProductName()
        {
            //get
            //{
            AssemblyProductAttribute myProduct = (AssemblyProductAttribute)AssemblyProductAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
            typeof(AssemblyProductAttribute));
            return myProduct.Product;
            //}
        }

        //This syntax is a function for getting a current date
        public static string CurrentDate()
        {

            string FechaFinal;
            DateTime Fecha = DateTime.Now;
            String FechaActual = Fecha.ToString("MM/dd/yyyy H:mm:ss");
            FechaFinal = Convert.ToString(FechaActual);
            return FechaActual;
        }

       
        public static string GetIDAllProcess()
        {
            //System.Diagnostics.Process procces = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcesses();
            System.Diagnostics.ProcessThreadCollection threadCollection = process[2].Threads;

           
            string threads = string.Empty;

            foreach (System.Diagnostics.ProcessThread processThread in threadCollection)
            {

               
               threads += string.Format(CurrentDate() + " " + "Thread Id: {0}, ThreadState: {1} \r\n", processThread.Id, processThread.ThreadState); //, myProduct.Product);
               
               
            }

            return threads.ToString();
        }


        public static string GetIDWindows()
        { 

             System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcesses();
             IntPtr hWnd;


            

            string IdWidows = string.Empty;

            foreach (System.Diagnostics.Process proc in procs)
             {
                //string application = "iTunes";

                if ((hWnd = proc.MainWindowHandle) != IntPtr.Zero)
                    {

                    IdWidows += string.Format(CurrentDate() + " " + "Windows ID: {0} ,  Name Application : {1} is runing \r\n", hWnd, proc.ProcessName);

                    //if(proc.ProcessName.Contains(application))
                    //{
                    //    IdWidows += string.Format(CurrentDate() + " " + "Windows ID: {0} ,  Name Application : {1} is runing \r\n", hWnd, proc.ProcessName);
                    //}

                    //if (!proc.ProcessName.Contains(application))
                    //{
                    //    IdWidows += string.Format(CurrentDate() + " " + "Name Application : {0} is NOT runing \r\n", application);
                    //}
                }
            }
            return IdWidows.ToString();
        }         



    }
}

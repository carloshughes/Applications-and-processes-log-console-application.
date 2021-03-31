using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Runtime.InteropServices;


namespace Bitacora
{
    class Program
    {

        static void Main(string[] args)
        {

            //This function is for hide the windows console when the application start 
            GetInformationProcess.HideWindows();
                        

            //This syntax will be excute a timmer in the application 
            try
            {
                //This a timer of 10 seconds. 
                // *When you are programming the seconds will be millisecounds. For example 10 second will be 10000 millisecond*
                Timer timer = new Timer();
                timer.Enabled = true;
                timer.Interval = 3000; //This the interval of time 
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed); //Here you have to put the name of function that will be called when the time is elapsed 
                timer.Start(); //This function is when the time is elapsed and will start
                Console.ReadKey();
                
            }
            catch
            {
       
            }
        }

        //This the function that contains all about the instrucctions will execute when the time is elapsed
            public static void timer_Elapsed (object sender, ElapsedEventArgs e)
            {

                try
                {
                //If the Bitacora.txt file exists only will type it 

                if (File.Exists(@"C:\temp\Bitacora.txt"))
                        {
                            StreamWriter WriteReportFile = File.AppendText(@"C:\temp\Bitacora.txt");
                            WriteReportFile.WriteLine(GetInformationProcess.CurrentDate() + " Name application who type in the log : " + GetInformationProcess.ProductName());  //This syntax is if you want to get the path of the application 
                            WriteReportFile.WriteLine(GetInformationProcess.CurrentDate() + " Name top application: " + GetInformationProcess.GetTopWindowText());    //This syntax is if you want to get name the top application 
                            WriteReportFile.WriteLine(GetInformationProcess.GetIDWindows());
                            WriteReportFile.Close();
                        }
                        else
                        {
                        //If the file  Bitacora.txt file doesn't exists create a file and type 
                            StreamWriter writer = new StreamWriter(@"C:\temp\Bitacora.txt");
                            writer.WriteLine(GetInformationProcess.CurrentDate() + " Name application who type in the log : " + GetInformationProcess.ProductName());  //This syntax is if you want to get the path of the application 
                            writer.WriteLine(GetInformationProcess.CurrentDate() + " Name top application: " + GetInformationProcess.GetTopWindowText());    //This syntax is if you want to get name the top application 
                            writer.WriteLine(GetInformationProcess.GetIDWindows()); //This syntaxis is for get the name of application and the Windows ID 
                            writer.Close();
                            writer.Dispose();
                        }
                }
                catch { }
               
                        
        }
    }
}

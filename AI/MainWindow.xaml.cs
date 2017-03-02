using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        volatile int entered = 0;
        volatile int entering = 2;
        private volatile string display = string.Empty;
        private object _syncroot = new object();
        private bool _Init = true;
        private Process _cmd = new Process();
        public MainWindow()
        {
            InitializeComponent();
            Initializing();
            Solve();
        }

        private void Solve()
        {
            _Init = false;
            lock(_syncroot)
            while (entered != 0) ;
            //Thread.Sleep(500);
            lock (display)
            {
                display = "";
            }
            entering = 2;
            _cmd.StandardInput.WriteLine("solve.");
            _cmd.StandardInput.Flush();
            //Thread.Sleep(500);
            while (entering!=0) ;
            Console.WriteLine(display);
        }

        private void Initializing()
        {
            _cmd.StartInfo.FileName = @"cmd.exe";
            _cmd.StartInfo.RedirectStandardInput = true;
            _cmd.StartInfo.RedirectStandardOutput = true;
            _cmd.StartInfo.RedirectStandardError = true;
            _cmd.StartInfo.UseShellExecute = false;
            _cmd.StartInfo.CreateNoWindow = false;
            _cmd.Start();
            _cmd.OutputDataReceived += ConsoleDataReceived;
            _cmd.ErrorDataReceived += ErrorReceived;
            _cmd.BeginOutputReadLine();
            _cmd.BeginErrorReadLine();
            entered = 10;
            _cmd.StandardInput.WriteLine(@"cd C:\");
                _cmd.StandardInput.Flush();

                    _cmd.StandardInput.WriteLine(@"set PATH=%PATH%;C:\Program Files\swipl\bin");
                    _cmd.StandardInput.Flush();
               
                    _cmd.StandardInput.WriteLine(@"cd C:\Users\Lion\Desktop");
                    _cmd.StandardInput.Flush();
               
                    _cmd.StandardInput.WriteLine("swipl -s disease");
                    _cmd.StandardInput.Flush();





        }


        public void ConsoleDataReceived(object sender, DataReceivedEventArgs e)
        {
           
                //Monitor.Enter(_syncroot);

                /*if (_Init)
                {
                    if (e.Data != null)
                    {
                        
                        Console.WriteLine(e.Data);
                        //display = e.Data;

                    }
                    else
                    {
                        Console.WriteLine("===================");
                        Console.WriteLine("It's Null");
                        Console.WriteLine("===================");
                    }
                }*/
                //else
                //{
                //Console.WriteLine(_Init);
                if (e.Data != null)
                {
                    //Thread.Sleep(500);
                    //Console.WriteLine(e.Data);
                    lock (display)
                    {
                        display += e.Data;
                    }
                }
                entered--;
            entering--;
        }
            //    else
            //    {
            //        Console.WriteLine("===================");
            //        Console.WriteLine("It's Null");
            //        Console.WriteLine("===================");
            //    }
            //}/






        void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            /*if (e.Data != null)
                Thread.Sleep(20000);
           Console.WriteLine(e.Data);*/
        }
    }
}

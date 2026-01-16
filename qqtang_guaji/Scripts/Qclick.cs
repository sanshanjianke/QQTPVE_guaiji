/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2025/12/23
 * 时间: 4:09
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */


using System;
using System.Threading;

namespace Scripts
{
    public class Qclick : BaseScript
    {
        public Qclick(CancellationToken ct) : base(ct)
        {
        }

        public override string Name 
        { 
            get { return "鼠标连点器"; } 
        }
        
        public override string Description 
        { 
            get { return "用于快速合成道具。"; } 
        }
        
        public override void Run()
        {
            
            int clickCount = GetValidInput("请输入要鼠标连点的次数[默认50]:");
            int clickTime = GetValidInput("请输入每次点击鼠标间隔（速度）[默认50ms]:");
            Console.WriteLine("您输入的连点次数是：" + clickCount.ToString() );
            
            Log("3秒后脚本开始运行...");
            Wait(3000);
            
            input.ClickMouse(clickCount, clickTime);
            
        }
        
	    static int GetValidInput(string message)
	    {
	        int result;
	        bool isValid = false;
	        
	        do
	        {
	            Console.Write(message);
	            string input = Console.ReadLine();
	            
	            // 检查输入是否为空
	            if (string.IsNullOrWhiteSpace(input))
	            {
	            	return 50;
	            }
	            
	            // 尝试将输入转换为整数
	            if (!int.TryParse(input, out result))
	            {
	                Console.WriteLine("错误：请输入有效的数字（正整数且小于1000000）。");
	                continue;
	            }
	            
	            // 检查是否为负数或零
	            if (result <= 0)
	            {
	                Console.WriteLine("错误：连点次数必须是正整数。");
	                continue;
	            }

	            
	            isValid = true;
	            
	        } while (!isValid);
	        
	        return result;
	    }
        
    }
}
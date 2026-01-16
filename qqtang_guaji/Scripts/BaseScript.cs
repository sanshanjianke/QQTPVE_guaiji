/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2025/12/22
 * 时间: 17:25
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */

using System;
using System.Threading;
using InputSimulator;

namespace Scripts
{
    // 脚本接口
    public interface IGameScript
    {
        string Name { get; }
        string Description { get; }
        void Run();
    }
    
    // 脚本基类
    public abstract class BaseScript : IGameScript
    {
        protected DirectInputSimulator input;
        
        public BaseScript()
        {
            input = DirectInputSimulator.Instance;
        }
        
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract void Run();
        
        // 公共辅助方法
        protected void Log(string message)
        {
            Console.WriteLine(string.Format("[{0:HH:mm:ss}] {1}", DateTime.Now, message));
        }
        
        protected void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}

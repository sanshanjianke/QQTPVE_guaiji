using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace qqtang_guaji
{
    public partial class MainForm : Form
    {
        private Timer usageTimer; // 使用时间计时器
        private int usageSeconds; // 已使用秒数
        private const int MAX_USAGE_SECONDS = 1800; // 30分钟 = 1800秒
        
        public MainForm()
        {
            InitializeComponent();
            
            // 初始化计时器
            InitializeUsageTimer();
            
            // 检查时间限制
            CheckTimeRestriction();
        }
        
        private void InitializeUsageTimer()
        {
            // 创建计时器，每秒触发一次
            usageTimer = new Timer();
            usageTimer.Interval = 1000; // 1秒
            usageTimer.Tick += UsageTimer_Tick;
        }
        
        private void CheckTimeRestriction()
        {
            // 获取中国时间 (UTC+8)
            DateTime chinaTime = GetChinaTime();
            
            // 获取当前小时
            int currentHour = chinaTime.Hour;
            
            // 判断是否在限制时间段内 (8:00-23:59)
            if (currentHour >= 8 && currentHour < 24)
            {
                // 在限制时间段内，开始计时
                StartUsageTimer();
                
                // 可以显示剩余时间（可选）
                ShowTimeRemaining();
            }
            else
            {
                // 不在限制时间段内，不限制使用
                MessageBox.Show("当前是非限制时间段，可以正常使用软件。", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private DateTime GetChinaTime()
        {
            // 获取UTC时间
            DateTime utcNow = DateTime.UtcNow;
            
            // 转换为中国时间 (UTC+8)
            TimeZoneInfo chinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime chinaTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chinaTimeZone);
            
            return chinaTime;
        }
        
        private void StartUsageTimer()
        {
            usageSeconds = 0;
            usageTimer.Start();
            
            // 显示提示信息
            MessageBox.Show("当前在限制时间段内（8:00-24:00），您有30分钟使用时间。", "时间限制", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        private void UsageTimer_Tick(object sender, EventArgs e)
        {
            usageSeconds++;
            
            // 每60秒更新一次显示（可选）
            if (usageSeconds % 60 == 0)
            {
                ShowTimeRemaining();
            }
            
            // 检查是否达到30分钟
            if (usageSeconds >= MAX_USAGE_SECONDS)
            {
                usageTimer.Stop();
                
                // 关闭应用程序
                this.Close();
            }
        }
        
        private void ShowTimeRemaining()
        {
            int remainingSeconds = MAX_USAGE_SECONDS - usageSeconds;
            int remainingMinutes = remainingSeconds / 60;
            
            // 可以在状态栏或标签显示剩余时间（如果有的话）
            // 例如：this.Text = $"主窗口 - 剩余时间: {remainingMinutes}分钟";
            
            
        }
        
        void Button1Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 woniu = new Form2();
            woniu.ShowDialog();
            this.Close();
        }

        void Button2Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 xiaoguo = new Form1();
            xiaoguo.ShowDialog();
            this.Close();
        }
        
        void Button3Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 siji = new Form3();
            siji.ShowDialog();
            this.Close();
        }
        
        // 窗体关闭时停止计时器
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            if (usageTimer != null)
            {
                usageTimer.Stop();
                usageTimer.Dispose();
            }
        }
    }
}

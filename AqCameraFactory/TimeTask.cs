using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AqCameraFactory
{
	//未使用，仅作为保留示例
	//多线程定时器
	public class TimeTask
	{
		private static readonly TimeTask _task = null;
		private System.Timers.Timer _timer = null;

		//定义时间（默认设置时间为5s）
		private int _interval = 1000 * 5;
		public int Interval
		{
			set
			{
				_interval = value;
			}
			get
			{
				return _interval;
			}
		}

		public event System.Timers.ElapsedEventHandler ExecuteTask;

		static TimeTask()
		{
			_task = new TimeTask();
		}

		public static TimeTask Instance()
		{
			return _task;
		}

		//开始
		public void Start()
		{
			if (_timer == null)
			{
				_timer = new System.Timers.Timer(_interval);
				_timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);
				_timer.Enabled = true;
				_timer.Start();
			}
		}

		protected void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (null != ExecuteTask)
			{
				ExecuteTask(sender, e);
			}
		}

		//停止
		public void Stop()
		{
			if (_timer != null)
			{
				_timer.Stop();
				_timer.Dispose();
				_timer = null;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AqCameraFactory
{
	//计时器
    public class TimeTicker : IDisposable
    {
        double _dStartTime = 0.0;    ///< 开始时间
        double _dStopTime = 0.0;     ///< 停止时间 
		string _title = "TimeTicker";

        public TimeTicker(string title)
        {
            _title = title;
            Start();
        }

		public TimeTicker()
		{
			Start();
		}

		/// <summary>
		/// 开始计数
		/// </summary>
		public void Start()
        {
            _dStartTime = Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// 停止计数
        /// </summary>
        /// <returns>时间差单位ms</returns>
        public double Stop()
        {
            _dStopTime = Stopwatch.GetTimestamp();
            double theElapsedTime = ElapsedTime();

            _dStartTime = _dStopTime;
            return theElapsedTime;
        }

        /// <summary>
        /// 获取时间差
        /// </summary>
        /// <returns>时间差单位ms</returns>
        public double ElapsedTime()
        {
            _dStopTime = Stopwatch.GetTimestamp();
            double dTimeElapsed = (_dStopTime - _dStartTime) * 1000.0;
            return dTimeElapsed / Stopwatch.Frequency;
        }

        public void Dispose()
        {
            double elapsedTime = Stop();
            string sMsg = _title + ": " + elapsedTime.ToString();
            OutputDebugString(sMsg);
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern void OutputDebugString(string sMsg);
    }
}

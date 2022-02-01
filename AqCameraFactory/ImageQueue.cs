using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AqCameraFactory
{
	//未使用，仅作为保留示例
	//此队列可结合TimTask使用
	public class ImageQueue
	{
		public static ConcurrentQueue<Bitmap> DataQueue { get; set; } = new ConcurrentQueue<Bitmap>();

		static ImageQueue()
		{
			TimeTask.Instance().ExecuteTask += new System.Timers.ElapsedEventHandler(ExecuteTask);
			TimeTask.Instance().Interval = 20;
		}

		#region 队列操作
		/// <summary>
		/// 入队
		/// </summary>
		public static void DataEnqueue(Bitmap image)
		{
			DataQueue.Enqueue(image);
		}

		/// <summary>
		/// 定时执行出队操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void ExecuteTask(object sender, System.Timers.ElapsedEventArgs e)
		{
			DataDequeue();
		}

		/// <summary>
		/// 出队
		/// </summary>
		public static void DataDequeue()
		{
			if (DataQueue.Count > 0)
			{
				bool dequeueSuccesful = false;
				bool peekSuccesful = false;
				Bitmap workItem;

				peekSuccesful = DataQueue.TryPeek(out workItem);

				if (peekSuccesful)
				{
					dequeueSuccesful = DataQueue.TryDequeue(out workItem);//出队
					//Show frame count
				}
			}
			else
			{
				//Show wait status
			}
		}
		#endregion
	}
}

[Icon("timer"),Group( "Hammer" ), Title( "logic_timer" )]
public sealed class LogicTimer : Component
{
	[Property] public Action OnTimer { get; set; } // 定时器触发时调用的动作
	[Property] public Action OnTimerStart { get; set; } // 定时器开始时的动作
	[Property] public Action OnTimerStop { get; set; } // 定时器停止时的动作
	[Property] public float WaitTime { get; set; } = 5f; // 默认定时器等待时间（秒）
	[Property,Group("flag")]public bool StartOn { get; set; } = false; // 是否一开始就启动定时器
	private bool _isRunning; // 定时器是否正在运行
	private TimeSince _timeSince; // 已经过的时间
	
	protected override void OnStart()
	{
		if ( StartOn )
		{
			StartTimer();
		}
	}
	// 输入信号 - 启动定时器
	public void StartTimer()
	{
		if ( !_isRunning )
		{
			_isRunning = true;
			_timeSince = 0f; // 重置时间
			OnTimerStart?.Invoke(); // 调用开始时的回调
		}
	}
	// 输入信号 - 停止定时器
	public void StopTimer()
	{
		if ( _isRunning )
		{
			_isRunning = false;
			OnTimerStop?.Invoke(); // 调用停止时的回调
		}
	}
	// 输入信号 - 重置定时器
	public void ResetTimer()
	{
		_timeSince = 0f; // 重置时间
		if ( !_isRunning )
		{
			StartTimer(); // 如果定时器没有在运行，启动它
		}
	}
	// 时间到了执行逻辑
	protected override void OnUpdate()
	{
		if ( _isRunning )
		{ 
			if ( _timeSince >= WaitTime )
			{
				_timeSince = 0f; // 重置时间
				OnTimer?.Invoke(); // 调用定时器触发的回调
			}
		}
	}
	// 设置定时器的等待时间
	public void SetWaitTime( float timeInSeconds )
	{
		WaitTime = timeInSeconds;
	}
	// 获取当前定时器已经运行的时间
	public float GetTimeElapsed()
	{
		return _timeSince;
	}
	// 获取定时器的剩余时间
	public float GetRemainingTime()
	{
		if ( _timeSince >= WaitTime ) return 0;
		return WaitTime - _timeSince;
	}
}

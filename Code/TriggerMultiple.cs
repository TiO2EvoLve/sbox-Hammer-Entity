[Icon("view_in_ar"),Group( "Hammer" ), Title( "trigger_multiple" )]
public class TriggerMultiple : Component,Component.ITriggerListener
{
	[Property] public TagSet IncludeTags { get; set; } = new(){"Player"};
	[Property] public TagSet ExcludeTags { get; set; } = new();
	[Property] public Action OnStartTouch { get; set; }
	[Property] public Action OnStartTouchAll { get; set; }
	[Property] public Action OnEndTouch { get; set; }
	[Property] public Action OnEndTouchAll { get; set; }

	//当前服务器人数
	[Property,ReadOnly,Title("PlayerCount")]int CheckPlayerNumber => Connection.All.Count;
	
	private HashSet<GameObject> PlayerCount = new ();
	
	//当开始触发时
	public void OnTriggerEnter(GameObject other)
	{
		if (CheckCanTrigger(other) && !PlayerCount.Contains(other))
		{
			PlayerCount.Add(other);
			OnStartTouch?.Invoke();
		}
		if (PlayerCount.Count == CheckPlayerNumber) 
			OnStartTouchAll?.Invoke();
	}
	//当离开触发区域时
	public void OnTriggerExit(GameObject other)
	{
		if (CheckCanTrigger(other) && PlayerCount.Contains(other))
		{
			PlayerCount.Remove(other);
			OnEndTouch?.Invoke();
		}
		if (PlayerCount.Count == 0) 
			OnEndTouchAll?.Invoke();
	}
	//检查能否触发
	bool CheckCanTrigger(GameObject other)
	{
		if ( other.Tags.HasAny( IncludeTags ) )
		{
			return true;
		}
		if(other.Tags.HasAny( ExcludeTags ))
		{
			return false;
		}
		return true;
	}
}

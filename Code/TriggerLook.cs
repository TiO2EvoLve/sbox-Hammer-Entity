
[Icon("visibility"),Group( "Hammer" ), Title( "trigger_look" )]
public class TriggerLook : Component,Component.IPressable
{
	[Property,Group("OutPut")] public Action OnLook { get; set; } 
	[Property,Group("OutPut")] public Action OnStartLook { get; set; } 
	[Property,Group("OutPut")] public Action OnStopLook { get; set; } 
	[Property] public float LookTime { get; set; }
	private TimeSince time;
	public void Hover( IPressable.Event e )
	{
		OnStartLook?.Invoke();
		time = 0;
	}

	public void Look( IPressable.Event e )
	{
		if ( time >= LookTime )
		{
			OnLook?.Invoke();
		}
	}

	public void Blur( IPressable.Event e )
	{
		OnStopLook?.Invoke();
	}

	public bool Press( IPressable.Event e )
	{
		return false;
	}
}


[Icon("start"),Group( "Hammer" ), Title( "logic_relay" )]
public class LogicRelay : Component
{
	[Property] public Action OnTrigger { get; set; }
	
	public void Trigger()
	{
		OnTrigger?.Invoke();
	}
}

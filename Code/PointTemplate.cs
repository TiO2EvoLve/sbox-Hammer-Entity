[Icon("radio_button_checked"),Group( "Hammer" ), Title( "point_template" )]
public class PointTemplate : Component
{
	[Property] public GameObject Template { get; set; }
	[Property] public float SpawnTime { get; set; }
	[Property] public float DestroyTime { get; set; }
	[Property,Group( "Output" )] public Action<GameObject> OnSpawn { get; set; }
	[Property,Group( "Output" )] public Action OnKill { get; set; }
	[Property,Group( "Output" )] public Action OnStarted { get; set; }
	[Property,Group( "Output" )] public Action OnStoped { get; set; }
	TimeSince time = 0;
	[Property, Group( "flag" )] public bool StartOn { get; set; }
	//[Property, Group( "flag" )] public bool RandomSpwan {  get; set; }

	protected override void OnStart()
	{
		if(StartOn) OnStarted?.Invoke();
	}

	protected override async void OnUpdate()
	{
		base.OnUpdate();
		if ( StartOn && time >= SpawnTime )
		{
			time = 0;
			GameObject obj = Template.Clone(WorldPosition);
			OnSpawn?.Invoke(obj);
			await Task.DelayRealtimeSeconds( DestroyTime );
			obj.Destroy();
			OnKill?.Invoke();
		}
	}
	public void Start()
	{
		StartOn = true;
		time = 0;
		OnStarted?.Invoke();
	}
	public void Stop()
	{
		StartOn = false;
		OnStoped?.Invoke();
	}
}

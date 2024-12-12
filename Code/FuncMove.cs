
using System.Reflection.Metadata.Ecma335;

[Icon("arrow_forward"),Group( "Hammer" ), Title( "func_move" )]
public class FuncMove : Component
{
	public enum _MoveStyle
	{
		Oneway,PingPang,Rotation,Revolution
	}
	[Property] public _MoveStyle MoveStyle = _MoveStyle.Oneway;//移动方式
	[Property] public Vector3 MoveDirection { get; set; } = Vector3.Forward;// 移动方向
	[Property] public float MoveSpeed { get; set; } = 100f;//移动速度
	[Property,ShowIf("MoveStyle",_MoveStyle.PingPang)] public GameObject Target{ get; set; }//移动目标
	[Property, ShowIf( "MoveStyle", _MoveStyle.Rotation )]
	public Vector3 RotateAxis { get; set; } = Vector3.Up;//旋转轴
	[Property, ShowIf( "MoveStyle", _MoveStyle.Rotation )]
	public float Angle { get; set; } = 100;//旋转角度
	[Property,ShowIf("MoveStyle",_MoveStyle.Revolution)] public GameObject AxisTarget; // 公转中心物体
	[Property,ShowIf("MoveStyle",_MoveStyle.Revolution)] public float Radius = 200; // 公转中心物体
	[Property,Group("Output")] public Action OnMoveStart { get; set; }// 移动开始时的回调
	[Property,Group("Output")] public Action OnMoveStop { get; set; }// 移动停止时的回调
	[Property, Group( "flag" )] public bool StartMove { get; set; }// 是否一开始就移动
	private TimeSince time;
	private Vector3 StartPosition;
	protected override void OnStart()
	{
		StartPosition = WorldPosition;
	}
	protected override void OnUpdate()
	{
		if(!StartMove) return;
		switch ( MoveStyle )
		{
			case _MoveStyle.Oneway:MoveOneWay(); break;
			case _MoveStyle.PingPang:MoveReciprocate(); break;
			case _MoveStyle.Rotation:Rotate(); break;
			case _MoveStyle.Revolution:RotateWithTarget(); break;
		};
	}
	//停止移动
	public void Stop()
	{
		StartMove = false;
		OnMoveStop?.Invoke();
	}
	//开始移动
	public void Start()
	{
		StartMove = true;
		OnMoveStart?.Invoke();
	}
	//始终朝一个方向运动
	private void MoveOneWay()
	{
		WorldPosition += MoveDirection * MoveSpeed * Time.Delta;
	}

	private float Value;
	
	//往复运动
	private void MoveReciprocate()
	{
		Value = (MathF.Sin(time * MoveSpeed / 100 * MathF.PI) + 1f) / 2f;
		Log.Info( Value );
		WorldPosition = Vector3.Lerp( StartPosition, Target.WorldPosition, Value);
		
	}
	//自转
	private void Rotate()
	{
		WorldRotation = Rotation.FromAxis( RotateAxis, Angle * time);
	}

	private float angle = 1f; // 当前公转的角度
	//围绕物体公转
	private void RotateWithTarget()
	{
		if ( AxisTarget == null ) return;
		// 增加角度（根据速度和时间）
		angle += MoveSpeed / 100 * Time.Delta;
		// 计算新的位置
		float x = AxisTarget.WorldPosition.x + MathF.Cos(angle) * Radius;
		float y = AxisTarget.WorldPosition.y + MathF.Sin(angle) * Radius;
		// 更新物体位置
		WorldPosition = new Vector3(x, y, WorldPosition.z);
	}
}

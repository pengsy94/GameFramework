using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFramework.Input;

public class MouseInfo
{
    /// <summary>
    /// 上一个周期中鼠标的状态.
    /// </summary>
    public MouseState PreviousState { get; private set; }

    /// <summary>
    /// 当前周期中鼠标的状态.
    /// </summary>
    public MouseState CurrentState { get; private set; }

    public MouseInfo()
    {
        PreviousState = new MouseState();
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    /// 更新与鼠标输入相关的状态信息.
    /// </summary>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    /// 获取或设置鼠标光标在屏幕空间中的当前位置
    /// </summary>
    public Point Position
    {
        get => CurrentState.Position;
        set => SetPosition(value.X, value.Y);
    }

    /// <summary>
    /// 获取或设置鼠标光标在屏幕空间中的当前 x 坐标位置.
    /// </summary>
    public int X
    {
        get => CurrentState.X;
        set => SetPosition(value, CurrentState.Y);
    }

    /// <summary>
    /// 获取或设置鼠标光标在屏幕空间中的当前 y 坐标位置.
    /// </summary>
    public int Y
    {
        get => CurrentState.Y;
        set => SetPosition(CurrentState.X, value);
    }

    /// <summary>
    /// 获取当前帧与前一帧鼠标光标位置的差异值.
    /// </summary>
    public Point PositionDelta => CurrentState.Position - PreviousState.Position;

    /// <summary>
    /// 获取当前帧与前一帧鼠标光标 x 坐标的差值.
    /// </summary>
    public int XDelta => CurrentState.X - PreviousState.X;

    /// <summary>
    /// 获取当前帧与前一帧鼠标光标 y 坐标的差值.
    /// </summary>
    public int YDelta => CurrentState.Y - PreviousState.Y;

    /// <summary>
    /// 判断光标是否在帧间移动.
    /// </summary>
    public bool WasMoved => PositionDelta != Point.Zero;

    /// <summary>
    /// 获取自游戏开始以来累计的滚动总值.
    /// </summary>
    public int ScrollWheel => CurrentState.ScrollWheelValue;

    /// <summary>
    /// 获取当前帧与前一帧之间滚动轮的变化值.
    /// </summary>
    public int ScrollWheelDelta => CurrentState.ScrollWheelValue - PreviousState.ScrollWheelValue;

    /// <summary>
    /// 返回指定的鼠标按钮当前是否处于按下状态
    /// </summary>
    /// <param name="button">指定的鼠标按钮.</param>
    /// <returns>按下 ture, 反之则 false.</returns>
    public bool IsButtonDown(MouseButton button) => button switch
    {
        MouseButton.Left => CurrentState.LeftButton == ButtonState.Pressed,
        MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Pressed,
        MouseButton.Right => CurrentState.RightButton == ButtonState.Pressed,
        MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Pressed,
        MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Pressed,
        _ => false,
    };

    /// <summary>
    /// 返回指定的鼠标按钮当前是否处于弹起状态.
    /// </summary>
    /// <param name="button">指定的鼠标按钮.</param>
    /// <returns>弹起 ture, 反之则 false..</returns>
    public bool IsButtonUp(MouseButton button) => button switch
    {
        MouseButton.Left => CurrentState.LeftButton == ButtonState.Released,
        MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Released,
        MouseButton.Right => CurrentState.RightButton == ButtonState.Released,
        MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Released,
        MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Released,
        _ => false,
    };

    /// <summary>
    /// 判断指定按钮是否刚刚被按下(上一帧没有按下).
    /// </summary>
    /// <param name="button">指定的按钮.</param>
    /// <returns>刚按下 返回true, 反之则 false.</returns>
    public bool WasButtonJustPressed(MouseButton button) => button switch
    {
        MouseButton.Left => CurrentState.LeftButton == ButtonState.Pressed &&
                            PreviousState.LeftButton == ButtonState.Released,
        MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Pressed &&
                              PreviousState.MiddleButton == ButtonState.Released,
        MouseButton.Right => CurrentState.RightButton == ButtonState.Pressed &&
                             PreviousState.RightButton == ButtonState.Released,
        MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Pressed &&
                                PreviousState.XButton1 == ButtonState.Released,
        MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Pressed &&
                                PreviousState.XButton2 == ButtonState.Released,
        _ => false
    };

    /// <summary>
    /// 判断指定按钮是否刚刚被释放(上一帧按下).
    /// </summary>
    /// <param name="button">指定的按钮.</param>
    /// <returns>刚释放 返回true, 反之则 false.</returns>
    public bool WasButtonJustReleased(MouseButton button) => button switch
    {
        MouseButton.Left => CurrentState.LeftButton == ButtonState.Released &&
                            PreviousState.LeftButton == ButtonState.Pressed,
        MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Released &&
                              PreviousState.MiddleButton == ButtonState.Pressed,
        MouseButton.Right => CurrentState.RightButton == ButtonState.Released &&
                             PreviousState.RightButton == ButtonState.Pressed,
        MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Released &&
                                PreviousState.XButton1 == ButtonState.Pressed,
        MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Released &&
                                PreviousState.XButton2 == ButtonState.Pressed,
        _ => false
    };

    /// <summary>
    /// 将鼠标光标的当前位置设置为屏幕空间中的位置，并使用新的位置更新“当前状态”值.
    /// </summary>
    /// <param name="x">鼠标光标在屏幕空间中的 x 坐标位置.</param>
    /// <param name="y">鼠标光标在屏幕空间中的 y 坐标位置.</param>
    public void SetPosition(int x, int y)
    {
        Mouse.SetPosition(x, y);
        CurrentState = new MouseState(
            x,
            y,
            CurrentState.ScrollWheelValue,
            CurrentState.LeftButton,
            CurrentState.MiddleButton,
            CurrentState.RightButton,
            CurrentState.XButton1,
            CurrentState.XButton2
        );
    }
}
using Microsoft.Xna.Framework.Input;

namespace GameFramework.Input;

/// <summary>
/// 键盘状态检测
/// </summary>
public class KeyboardInfo
{
    /// <summary>
    /// 获取上一次更新周期内键盘输入的状态.
    /// </summary>
    public KeyboardState PreviousState { get; private set; }

    /// <summary>
    /// 获取当前输入周期内键盘输入的状态.
    /// </summary>
    public KeyboardState CurrentState { get; private set; }

    /**
     * 初始化确保在第一帧内有状态可供比较,避免检查输入变化时出现空引用问题
     */
    public KeyboardInfo()
    {
        PreviousState = new KeyboardState(); // 先创建一个空状态,因为在此之前是没有输入的
        CurrentState = Keyboard.GetState(); // 以当前键盘状态作为 CurrentState 的起点
    }

    /**
     * 更新有关键盘输入的状态信息
     */
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Keyboard.GetState();
    }

    /// <summary>
    /// 判断指定按键当前是否处于按下状态
    /// </summary>
    /// <param name="key">检查的按键</param>
    /// <returns>指定按钮处于按下状态，则返回 true，反之则返回 false</returns>
    public bool IsKeyDown(Keys key)
    {
        return CurrentState.IsKeyDown(key);
    }

    /// <summary>
    /// 判断指定按键当前是否处于弹起状态
    /// </summary>
    /// <param name="key">检查的按键</param>
    /// <returns>指定按钮处于弹起状态，则返回 true，反之则返回 false</returns>
    public bool IsKeyUp(Keys key)
    {
        return CurrentState.IsKeyUp(key);
    }

    /// <summary>
    /// 判断指定按键在当前帧是否刚按下(上一帧没有按下)
    /// </summary>
    /// <param name="key">指定的按键</param>
    /// <returns>当前帧按下，则返回 true，反之则返回 false</returns>
    public bool WasKeyJustPressed(Keys key)
    {
        return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
    }

    /// <summary>
    /// 判断指定按键在当前帧是否释放(上一帧按下状态) 
    /// </summary>
    /// <param name="key">指定的按键</param>
    /// <returns>当前帧释放，则返回true，反之则返回 false</returns>
    public bool WasKeyJustReleased(Keys key)
    {
        return CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
    }
}
using Microsoft.Xna.Framework;

namespace GameFramework.Input;

public class InputManager
{
    /// <summary>
    /// 获取键盘输入的状态信息.
    /// </summary>
    public KeyboardInfo Keyboard { get; private set; }

    /// <summary>
    /// 获取鼠标输入的状态信息.
    /// </summary>
    public MouseInfo Mouse { get; private set; }

    /// <summary>
    /// 获取游戏手柄的状态信息.
    /// </summary>
    public GamePadInfo[] GamePads { get; private set; }

    public InputManager()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();

        GamePads = new GamePadInfo[4];
        for (var i = 0; i < 4; i++)
        {
            GamePads[i] = new GamePadInfo((PlayerIndex)i);
        }
    }
    
    /// <summary>
    /// 更新键盘、鼠标和游戏手柄输入的状态信息.
    /// </summary>
    /// <param name="gameTime">当前帧.</param>
    public void Update(GameTime gameTime)
    {
        Keyboard.Update();
        Mouse.Update();

        for (var i = 0; i < 4; i++)
        {
            GamePads[i].Update(gameTime);
        }
    }
}
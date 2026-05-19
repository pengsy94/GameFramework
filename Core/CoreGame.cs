using System;
using GameFramework.Audio;
using GameFramework.Input;
using GameFramework.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFramework.Core;

public abstract class CoreGame : Game
{
    internal static CoreGame s_instance;

    /// <summary>
    /// 获取对 Core 实例的引用
    /// </summary>
    public static CoreGame Instance => s_instance;

    // 当前正在运行的场景.
    private static Scene s_activeScene;

    // 接下来要切换到的场景（如果有的话）.
    private static Scene s_nextScene;

    /// <summary>
    /// 获取图形设备管理器以控制图形的表示
    /// </summary>
    public static GraphicsDeviceManager Graphics { get; private set; }

    /// <summary>
    /// 获取用于创建图形资源和执行基本渲染的图形设备
    /// </summary>
    public static new GraphicsDevice GraphicsDevice { get; private set; }

    /// <summary>
    /// 获取用于所有 2D 渲染的 sprite 批处理.
    /// </summary>
    public static SpriteBatch SpriteBatch { get; private set; }

    /// <summary>
    /// 获取用于加载全局资源的内容管理器
    /// </summary>
    public static new ContentManager Content { get; private set; }

    /// <summary>
    /// 输入管理系统的引用.
    /// </summary>
    public static InputManager Input { get; private set; }

    /// <summary>
    /// 获取或设置一个值，该值用于指示当键盘上的“esc”键被按下时游戏是否应退出.
    /// </summary>
    public static bool ExitOnEscape { get; set; }

    /// <summary>
    /// 音频控制
    /// </summary>
    public static AudioController Audio { get; private set; }

    protected CoreGame(string title, int width, int height, bool fullScreen = false)
    {
        // 确保创建单一实例
        if (s_instance != null)
            throw new InvalidOperationException($"Only a single Core instance can be created");

        // 保证全局仅有一个实例
        s_instance = this;

        // 创建一个新的图形设备管理器
        Graphics = new GraphicsDeviceManager(this);

        // 设置图形默认设置
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;

        // 应用图形展示的更改
        Graphics.ApplyChanges();

        // 设置这个窗口的窗口
        Window.Title = title;

        // 将核心内容管理器设置为基础游戏内容管理器的引用
        Content = base.Content;

        // 设置内容的根目录
        Content.RootDirectory = "Content";
        // 鼠标图标是否显示
        IsMouseVisible = true;

        // 默认开启 ESC 退出功能
        ExitOnEscape = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        // 将核心的图形设备设置为基础游戏图形设备的引用版本
        GraphicsDevice = base.GraphicsDevice;

        // 创建批次创建精灵实例
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        // 创建新的input管理器
        Input = new InputManager();

        // 创建新的音频控制器
        Audio = new AudioController();
    }

    protected override void UnloadContent()
    {
        // 释放音频控制器
        Audio.Dispose();

        base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        // 更新 Input管理器
        Input.Update(gameTime);

        // 更新 音频控制器
        Audio.Update();

        if (ExitOnEscape && Input.Keyboard.WasKeyJustPressed(Keys.Escape))
        {
            Exit();
        }

        // 如果有下一场景等待切换到，那就切换到那个场景
        if (s_nextScene != null)
        {
            TransitionScene();
        }

        // 如果有正在进行的场景，就对其进行更新
        if (s_activeScene != null)
        {
            s_activeScene.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (s_activeScene != null)
        {
            s_activeScene.Draw(gameTime);
        }

        base.Draw(gameTime);
    }

    public static void ChangeScene(Scene next)
    {
        if (s_activeScene != next)
        {
            s_nextScene = next;
        }
    }

    private static void TransitionScene()
    {
        // 如果有正在进行的场景，就将其结束掉
        if (s_activeScene != null)
        {
            s_activeScene.Dispose();
        }

        // 强制垃圾回收器进行清理，以确保内存得以释放
        GC.Collect();

        // 将当前活动的场景更改为新场景
        s_activeScene = s_nextScene;

        // 将下一个场景的值设为无效，以防止其反复触发变化
        s_nextScene = null;

        // 如果当前活动场景不为空，则对其进行初始化
        if (s_activeScene != null)
        {
            s_activeScene.Initialize();
        }
    }

    protected override void LoadContent()
    {
        // 初始化纹理管理器
        TextureManager.Initialize(Content);

        // 初始化字体管理器
        FontsManager.Initialize(GraphicsDevice);

        base.LoadContent();
    }

    protected int ScreenWidth => base.GraphicsDevice.Viewport.Width;

    protected int ScreenHeight => base.GraphicsDevice.Viewport.Height;

    protected Vector2 ScreenCenter => new(ScreenWidth / 2f, ScreenHeight / 2f);
}
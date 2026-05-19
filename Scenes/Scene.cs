using System;
using GameFramework.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameFramework.Scenes;

/// <summary>
/// 场景基类 
/// </summary>
public abstract class Scene : IDisposable
{
    /// <summary>
    /// 获取用于加载场景特定资源的 内容管理器
    /// </summary>
    /// <remarks>
    /// 通过此内容管理器加载的资源在当前场景结束时会自动卸载.
    /// </remarks>
    protected ContentManager Content { get; }

    /// <summary>
    /// 获取场景是否已被销毁
    /// </summary>
    public bool IsDisposed { get; private set; }

    public Scene()
    {
        // 为场景创建一个内容管理者
        Content = new ContentManager(CoreGame.Content.ServiceProvider);

        // 设置为与游戏内容的根目录相同
        Content.RootDirectory = CoreGame.Content.RootDirectory;
    }

    // 终结器, 垃圾回收器清理对象时所调用的函数.
    ~Scene() => Dispose(false);

    /// <summary>
    /// 初始化场景.
    /// </summary>
    /// <remarks>
    /// 重写Initialize方法,确保仍然调用 base.Initialize(),这就是 LoadContent 方法被调用的情况.
    /// </remarks>
    public virtual void Initialize()
    {
        LoadContent();
    }

    /// <summary>
    /// 重写以提供用于加载场景内容的逻辑
    /// </summary>
    public virtual void LoadContent()
    {
    }

    /// <summary>
    /// 卸载特定场景的相关内容
    /// </summary>
    public virtual void UnloadContent()
    {
        Content.Unload();
    }

    /// <summary>
    /// 更新此场景
    /// </summary>
    /// <param name="gameTime">当前帧的时间值.</param>
    public virtual void Update(GameTime gameTime)
    {
    }

    /// <summary>
    /// 绘制场景.
    /// </summary>
    /// <param name="gameTime">当前帧的时间值.</param>
    public virtual void Draw(GameTime gameTime)
    {
    }

    /// <summary>
    /// 结束这一场景.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 结束这一场景.
    /// </summary>
    /// <param name="disposing">'
    /// 表示是否应释放管理资源.  仅在从主的 Dispose 方法调用时为真. 当从终结器中调用时，此值将为假.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            UnloadContent();
            Content.Dispose();
        }

        IsDisposed = true;
    }
}
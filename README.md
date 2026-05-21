# GameFramework

一个基于 **MonoGame** 构建的轻量级游戏开发框架，提供了核心游戏循环、图形渲染、输入处理、音频管理和场景系统等基础功能。

## 简介

MonoGame 是一个跨平台的游戏开发框架，继承了 Microsoft XNA 的精神，允许开发者使用 C# 语言创建高性能的 2D 和 3D 游戏。本框架基于 MonoGame 官方教程构建，提供了模块化的架构设计，便于快速开发各类游戏项目。

## 特性

### 核心功能

- **CoreGame** - 抽象的游戏核心类，提供统一的游戏循环和全局资源管理
- **Content Management** - 纹理和字体资源的集中管理
- **Scene System** - 场景切换和生命周期管理

### 图形系统

- **Sprite** - 2D 精灵渲染，支持旋转、缩放、颜色遮罩等变换
- **AnimatedSprite** - 动画精灵，支持帧动画播放
- **TextureAtlas** - 纹理图集管理
- **Tilemap / Tileset** - 瓦片地图系统

### 输入系统

- **Keyboard** - 键盘输入检测（支持按键状态追踪）
- **Mouse** - 鼠标输入检测（支持按钮状态和位置追踪）
- **GamePad** - 游戏手柄输入支持（最多4个手柄）

### 音频系统

- **AudioController** - 音效和背景音乐管理

## 项目结构

```
GameFramework/
├── Core/                  # 核心模块
│   ├── CoreGame.cs        # 游戏核心类
│   ├── FontsManager.cs    # 字体管理器
│   └── TextureManager.cs  # 纹理管理器
├── Graphics/              # 图形模块
│   ├── AnimatedSprite.cs  # 动画精灵
│   ├── Animation.cs       # 动画类
│   ├── Sprite.cs          # 精灵类
│   ├── TextureAtlas.cs    # 纹理图集
│   ├── TextureRegion.cs   # 纹理区域
│   ├── Tilemap.cs         # 瓦片地图
│   └── Tileset.cs         # 瓦片集
├── Input/                 # 输入模块
│   ├── GamePadInfo.cs     # 游戏手柄信息
│   ├── InputManager.cs    # 输入管理器
│   ├── KeyboardInfo.cs    # 键盘信息
│   ├── MouseButton.cs     # 鼠标按钮枚举
│   └── MouseInfo.cs       # 鼠标信息
├── Audio/                 # 音频模块
│   └── AudioController.cs # 音频控制器
├── Scenes/                # 场景模块
│   └── Scene.cs           # 场景基类
├── Circle.cs              # 圆形工具类
└── GameFramework.csproj   # 项目文件
```

## 快速开始

### 环境要求

- .NET 9 或更高版本
- MonoGame 3.8 或更高版本
- MonoGame DesktopGL

### 安装环境

验证:

```bash
dotnet --version
```

输出实例:

```bash
10.0.203
```

### 安装 MonoGame 模板

```bash
dotnet new install MonoGame.Templates.CSharp
```

### 创建解决方案

```bash
mkdir MonoGameDevelop
cd MonoGameDevelop

dotnet new sln -n MonoGameDevelop
```

### 直接 clone 游戏框架, 并添加到 Solution

```bash
git clone https://github.com/pengsy94/GameFramework.git

dotnet sln add GameFramework/GameFramework.csproj
```

### 如何使用(多个游戏的时候,按一下方法就好)

在MonoGameDevelop文件夹中创建的你的游戏:

```bash
dotnet new mgdesktopgl -o MyGame
```

继续将新游戏加入到Solution:

```bash
dotnet sln add MyGame/MyGame.csproj
```

建立新游戏对游戏框架的引用:

```bash
dotnet add MyGame/MyGame.csproj reference GameFramework/GameFramework.csproj
```

### 使用游戏项目

1. **继承 CoreGame**

```csharp
using GameFramework.Core;
using Microsoft.Xna.Framework;

public class MyGame : CoreGame
{
    public MyGame() : base("My Game", 800, 480)
    {
        // 游戏初始化配置
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        
        // 加载游戏资源
        // ChangeScene(new MainMenuScene());
    }
}
```

2. **创建场景**

```csharp
using GameFramework.Scenes;
using Microsoft.Xna.Framework;

public class MainMenuScene : Scene
{
    public override void Initialize()
    {
        // 初始化场景资源
    }

    public override void Update(GameTime gameTime)
    {
        // 更新场景逻辑
        if (CoreGame.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
        {
            CoreGame.ChangeScene(new GameScene());
        }
    }

    public override void Draw(GameTime gameTime)
    {
        // 绘制场景内容
        CoreGame.GraphicsDevice.Clear(Color.CornflowerBlue);
        
        CoreGame.SpriteBatch.Begin();
        // 绘制代码...
        CoreGame.SpriteBatch.End();
    }
}
```

3. **使用精灵**

```csharp
var texture = TextureManager.Load("player");
var region = new TextureRegion(texture);
var sprite = new Sprite(region);
sprite.CenterOrigin();
sprite.Color = Color.White;
sprite.Rotation = 0f;
sprite.Scale = Vector2.One;

// 绘制精灵
sprite.Draw(CoreGame.SpriteBatch, new Vector2(400, 240));
```

## 输入处理示例

```csharp
// 键盘输入
if (CoreGame.Input.Keyboard.IsKeyDown(Keys.Left))
{
    playerPosition.X -= speed;
}

if (CoreGame.Input.Keyboard.WasKeyJustPressed(Keys.Space))
{
    Jump();
}

// 鼠标输入
if (CoreGame.Input.Mouse.WasButtonJustPressed(MouseButton.Left))
{
    var mousePos = CoreGame.Input.Mouse.Position;
    // 处理鼠标点击
}

// 游戏手柄输入
var gamePad = CoreGame.Input.GamePads[0];
if (gamePad.IsButtonDown(Buttons.A))
{
    // 手柄按键处理
}
```

## 场景切换

```csharp
// 切换到新场景
CoreGame.ChangeScene(new NextScene());
```

## 核心 API

### CoreGame 静态属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Instance` | `CoreGame` | 当前游戏实例 |
| `Graphics` | `GraphicsDeviceManager` | 图形设备管理器 |
| `GraphicsDevice` | `GraphicsDevice` | 图形设备 |
| `SpriteBatch` | `SpriteBatch` | 精灵批处理器 |
| `Content` | `ContentManager` | 内容管理器 |
| `Input` | `InputManager` | 输入管理器 |
| `Audio` | `AudioController` | 音频控制器 |
| `ExitOnEscape` | `bool` | 是否按 ESC 退出游戏 |

### 输入检测方法

| 方法 | 说明 |
|------|------|
| `IsKeyDown(Keys key)` | 按键是否持续按下 |
| `WasKeyJustPressed(Keys key)` | 按键是否刚刚按下 |
| `WasKeyJustReleased(Keys key)` | 按键是否刚刚释放 |

## 参考资料

- [MonoGame 官方文档](https://docs.monogame.net/)
- [MonoGame 官方教程](https://docs.monogame.net/articles/tutorials/building_2d_games/index.html)
- [MonoGame GitHub](https://github.com/MonoGame/MonoGame)

## 最常用命令速查

```bash
# 创建 solution
dotnet new sln -n MonoGameLearning

# 创建类库
dotnet new mgnetstandardlibrary -o GameFramework

# 创建游戏
dotnet new mgdesktopgl -o FirstGame

# 添加到 solution
dotnet sln add GameFramework/GameFramework.csproj

# 添加引用
dotnet add FirstGame/FirstGame.csproj reference GameFramework/GameFramework.csproj

# 构建
dotnet build

# 运行
dotnet run --project FirstGame
```

## LICENSE

This project is licensed under the [MIT License](LICENSE) — see the [LICENSE](LICENSE) file for details.


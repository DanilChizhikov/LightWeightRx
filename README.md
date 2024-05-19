# LightWeightRx
![](https://img.shields.io/badge/unity-2022.3+-000.svg)

## Description
This package is a lightweight implementation for reactive programming.

## Table of Contents
- [Performance Tests](#Performance-Tests)
    - [Simple Subscribes](#Simple-Subscribes) 
    - [Subscribes With Set Value](#Subscribes-With-Set-Value) 
- [Getting Started](#Getting-Started)
    - [Install manually (using .unitypackage)](#Install-manually-(using-.unitypackage))
    - [Install via UPM (using Git URL)](#Install-via-UPM-(using-Git-URL))
- [How to use](#How-to-use)
    - [Usage examples](#Usage-examples)
- [License](#License)

## Performance Tests
Testing Code:
```csharp
for (int i = 0; i < 1000; i++)
{
    int includedValue = int.MinValue;
    property.Subscribe(value => includedValue = value);
}
```
Result:
```
Output: 
Time 5.10 Milliseconds
Subscribe Group in Milliseconds
Min:		0.00 ms
Median:		0.00 ms
Max:		0.01 ms
Avg:		0.00 ms
StdDev:		0.00 ms
SampleCount:	999
Sum:		0.85 ms
First samples:	▂▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▃▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▃▁▃▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▃▁▁▁▁▁▁▁▁▄▁▁▁▃▁▁▁▁▁▁▁▁▁▁▁▁▁▁
```
### Simple Subscribes
### Subscribes With Set Value
Testing code:
```csharp
for (int i = 0; i < 1000; i++)
{
    int includedValue = int.MinValue;
    property.Subscribe(value => includedValue = value);
}

for(int i = 0; i < 1000; i++)
{
    property.Value = Random.Range(0, int.MaxValue);
}
```
Result:
```
Output: 
Time 28.19 Milliseconds
Set Group in Milliseconds
Min:		0.02 ms
Median:		0.02 ms
Max:		0.08 ms
Avg:		0.03 ms
StdDev:		0.00 ms
SampleCount:	999
Sum:		25.04 ms
First samples:	▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▂▁▁▁▁▁▁▁▁▁▂▁▁▁▁▁▁▁▁▁▁▁▂▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▄▂▁▂▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁▁
```

## Getting Started
Prerequisites:
- [GIT](https://git-scm.com/downloads)
- [Unity](https://unity.com/releases/editor/archive) 2022.3+

### Install manually (using .unitypackage)
1. Download the .unitypackage from [releases](https://github.com/DanilChizhikov/LightWeightRx/releases/) page.
2. Open LightWeightRx.x.x.x.unitypackage

### Install via UPM (using Git URL)
1. Navigate to your project's Packages folder and open the manifest.json file.
2. Add this line below the "dependencies": { line
    - ```json title="Packages/manifest.json"
      "com.danilchizhikov.lwrx": "https://github.com/DanilChizhikov/LightWeightRx.git?path=Assets/LightWeightRx",
      ```
UPM should now install the package.

## How to use
You can use both conventional implementations and special serialized ones.
1) Simple:
```csharp
public class Example
{
    private readonly CallbackProperty<float> _speed;
    private readonly CallbackProperty<int> _health;

    public IReadOnlyCallbackProperty<float> Speed => _speed;
    public IReadOnlyCallbackProperty<int> Health => _health;
    
    public Example()
    {
        _speed = new CallbackProperty<float>();
        _health = new CallbackProperty<int>();
    }
}
```
1. 1. Single Subscribes
```csharp
public class Entity : IDisposable
{
    private readonly IDisposable _speedDisposable;

    public Entity(Example example)
    {
        //With such a subscription, the last assigned value to the field will be called immediately after the subscription
        _speedDisposable = example.Speed.Subscribe(SpeedChangedCallback);
        //With such a subscription, the last assigned value to the field will not be called immediately after the subscription
        _speedDisposable = example.Speed.SkipLastValueSubscribe(SpeedChangedCallback);
    }

    public void Dispose()
    {
        _speedDisposable.Dispose();
    }
    
    private void SpeedChangedCallback(float speed)
    {
        Debug.Log($"Speed: {speed}");
    }
}
```

1. 2. Multi-subscribe
```csharp
public class Entity : IDisposable
{
    private readonly CompositeDisposable _exampleDisposable;

    public Entity(Example example)
    {
        _exampleDisposable = new CompositeDisposable();
        _exampleDisposable.Add(example.Speed.Subscribe(SpeedChangedCallback));
        _exampleDisposable.Add(example.Health.Subscribe(HealthChangedCallback));
    }

    public void Dispose()
    {
        _exampleDisposable.Dispose();
    }
    
    private void SpeedChangedCallback(float speed)
    {
        Debug.Log($"Speed: {speed}");
    }

    private void HealthChangedCallback(int health)
    {
        Debug.Log($"Health: {health}");
    }
}
```
2) Unity Serialized
It includes all the same use cases, only the class name changes from CallbackProperty to SerializedCallbackProperty.
```csharp
public class Example : MonoBehaviour
{
    [SerailizeField] private SerializedCallbackProperty<float> _speed;
    [SerailizeField] private SerializedCallbackProperty<int> _health;

    public IReadOnlyCallbackProperty<float> Speed => _speed;
    public IReadOnlyCallbackProperty<int> Health => _health;
}
```

## License

MIT
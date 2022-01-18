<img align="right" width="100" height="100" src="https://user-images.githubusercontent.com/37786733/41541140-71602302-731a-11e8-9434-79b3a57292b6.png">

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/ArtemBelaev-ural/XMonoNode/master/LICENSE.md)
[![GitHub Wiki](https://img.shields.io/badge/wiki-available-brightgreen.svg)](https://github.com/Siccity/xNode/wiki)

<!-- [Downloads](https://github.com/Siccity/xNode/releases) / [Asset Store](http://u3d.as/108S) / -->
[Documentation](https://github.com/Siccity/xNode/wiki)

### xMonoNode

xMonoNode based on [Siccity/xNode](https://github.com/Siccity/xNode) and [exAntares/FlowNodes](https://github.com/exAntares/FlowNodes)

xMonoNode supports nodes and graphs based on Unity MonoBehaviour class. That allows you to use prefabs and instantiate the logic of nodes in the game without saving any data in scriptable objects. 
XMonoNode contains a ready-made extensible node library that includes rich features for working with sound effects

### Key features
* Lightweight in runtime
* Very little boilerplate code
* Strong separation of editor and runtime code
* No runtime reflection (unless you need to edit/build node graphs at runtime. In this case, all reflection is cached.)
* Does not rely on any 3rd party plugins
* Custom node inspector code is very similar to regular custom inspector code
* Undo/redo support
* MonoBehaviour nodes support
* Working whith prefabs
* Node library that includes:
    * sound nodes (Play, Source, Pitch), 
    * tween animation nodes,
	* math nodes,
	* unity event nodes (OnStart, OnUpdate, ...)
	* utilities (Wait, Log, Random, Branch, ...)


### Wiki
* [Getting started](https://github.com/Siccity/xNode/wiki/Getting%20Started) - create your very first node node and graph
* [Examples branch](https://github.com/Siccity/xNode/tree/examples) - look at other small projects

### Installation
<details><summary>Instructions</summary>

### Installing with Unity Package Manager
***Via Git URL***
*(Requires Unity version 2018.3.0b7  or above)*

To install this project as a [Git dependency](https://docs.unity3d.com/Manual/upm-git.html) using the Unity Package Manager,
add the following line to your project's `manifest.json`:

```
"com.github.artembelaev-ural.xmononode": "https://github.com/ArtemBelaev-ural/XMonoNode.git"
```

You will need to have Git installed and available in your system's PATH.

If you are using [Assembly Definitions](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html) in your project, you will need to add `XMonoNode` and/or `XMonoNodeEditor` as Assembly Definition References.

### Installing with git
***Via Git Submodule***

To add xNode as a [submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules) in your existing git project,
run the following git command from your project root:

```
git submodule add git@github.com:ArtemBelaev-ural/XMonoNode.git Assets/Submodules/xMonoNode
```

### Installing 'the old way'
If no source control or package manager is available to you, you can simply copy/paste the source files into your assets folder.

</details>

### Node example:
```csharp
// public classes deriving from Node are registered as nodes for use within a graph
public class MathNode : Node 
{
    // Adding [Input] or [Output] is all you need to do to register a field as a valid port on your node 
    [Input] public float a;
    [Input] public float b;
    // The value of an output node field is not used for anything, but could be used for caching output results
    [Output] public float result;
    [Output] public float sum;

    // The value of 'mathType' will be displayed on the node in an editable format, similar to the inspector
    public MathType mathType = MathType.Add;
    public enum MathType { Add, Subtract, Multiply, Divide}
    
    // GetValue should be overridden to return a value for any specified output port
    public override object GetValue(NodePort port) 
    {
        // Get new a and b values from input connections. Fallback to field values if input is not connected
        float a = GetInputValue<float>("a", this.a);
        float b = GetInputValue<float>("b", this.b);

        // After you've gotten your input values, you can perform your calculations and return a value
        if (port.fieldName == "result")
            switch(mathType) 
            {
                case MathType.Add: default: return a + b;
                case MathType.Subtract: return a - b;
                case MathType.Multiply: return a * b;
                case MathType.Divide: return a / b;
            }
        else if (port.fieldName == "sum") return a + b;
        else return 0f;
    }
}
```


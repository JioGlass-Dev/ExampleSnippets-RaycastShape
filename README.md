# Raycast Shape
Example to change length and width of raycast

## Scripts 

### `ChangeRaycastShape.cs`
Control and change the raycast shape - length and width.</br>
- Changing length of JMR Laser Raycast
```cs
JMRPointerManager _jmrPointerManager = FindObjectOfType<JMRPointerManager>();
_jmrPointerManager.MaxPointerCollisionDistance = value;
```
- Changing width of JMR Laser Raycast
```cs
PointerLaserUnity _pointerLaserUnity = FindObjectOfType<PointerLaserUnity>();
_pointerLaserUnity.WidthMultiplier = value;
```

## How to use?
1. Download and unzip this project.
2. Open the project using Unity Hub.
3. Download and import the latest version of JMRSDK package.
4. Open and play the ChangeRaycastShape scene from Assets/Scenes folder.

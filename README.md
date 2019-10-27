# Dungeon-Crawler-Map-Gen

This is a example project of random generated maps using RogueElements in Unity, only for educational purposes. It is based of the examples provided by the library, showing the results (map, stairs and object) in the scene using Tilemaps.

## How to open the project

Download this repository or clone it, then open it with Unity. The prefered version is **2018.3.9f1**. You can open it with other Unity versions, **but beware of the dependencies(list below)** Open the SampleScene in `Assets/Scenes` and press play to create the map. In the scene there is a GameObject called `MapCreation` which generates the maps using a script named `DungeonGenerator`. That script and others based on the original examples are in `Assets/Scripts/Test`.

## Dependencies

+ [RogueElements](https://github.com/audinowho/RogueElements): Version used in this project is from commit [43c29c2d9b58b386749fe15b2665ea4ef5af5f2a](https://github.com/audinowho/RogueElements/tree/43c29c2d9b58b386749fe15b2665ea4ef5af5f2a). If you want to update it, download or clone the lastest commit and compile the solution. The DLL generated has to be added to the project, in this case it's in `Assets/RogueElements`.

+ [Tilemaps](https://docs.unity3d.com/Manual/class-Tilemap.html): This feature was added in Unity in 2017.2, **so trying to open the project below this version will have issues.**

+ [2d-extras](https://github.com/Unity-Technologies/2d-extras): Contains some extra features for tilemaps, some of them are used in here. **If you want to change the Unity version of the project remember to check this, as there are different branches for different Unity versions, and it may break.**

+ [PixelPerfectCamera](https://docs.unity3d.com/Packages/com.unity.2d.pixel-perfect@1.0/manual/index.html): Not 100% necessary to the project to work (it's only visual), but if you want to keep it, **this feature was added in Unity 2018.2.**
 

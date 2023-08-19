# dynamic-unity-avatar-generator
Dynamically generates humanoid `UnityEngine.Avatar` from skeleton bones at runtime.

## How to import by Unity Package Manager

Add following dependencies to your `/Packages/manifest.json`.

```json
{
    "dependencies": {
        "com.mochineko.dynamic-unity-avatar-generator": "https://github.com/mochi-neko/dynamic-unity-avatar-generator.git?path=/Assets/Mochineko/DynamicUnityAvatarGenerator#0.2.1",
        "com.mochineko.relent": "https://github.com/mochi-neko/Relent.git?path=/Assets/Mochineko/Relent#0.2.0",
        "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
        ...
    }
}
```

## Features

Generates `UnityEngine.Avatar` from humanoid 3D model hierarchy at runtime
 with specifying skeleton root bone retrieval logic (`IRootBoneRetriever`),
 human bone retrieval logic (`IHumanBoneRetriever`)
 and other parameters of `HumanDescription` (`HumanDescriptionParameters`).

- `UnityEngine.Avatar` generator
- Root bone retriever (`IRootBoneRetriever`)
  - [x] Regular expression matching
  - [x] String comparison rule
  - [x] Specifying root bone instance
  - [ ] Has most children bones
- Human bone retriever (`IHumanBoneRetriever`)
  - [x] Regular expression matching
  - [x] String comparison rule
- Human bone transform map creator from `UnityEngine.Avatar`.

## How to use

See [AvatarGeneratorTest](./Assets/Mochineko/DynamicUnityAvatarGenerator.Tests/AvatarGeneratorTest.cs).

Regular expression pattern samples are in [RegularExpressionHumanBoneRetrieverTest](./Assets/Mochineko/DynamicUnityAvatarGenerator.Tests/RegularExpressionHumanBoneRetrieverTest.cs).

## Presets

Provides some presets of `IRootBoneRetriever`, `IHumanBoneRetriever` and `HumanDescriptionParameters`.

- Root bone retriever (`IRootBoneRetriever`)
  - Mixamo and Biped preset 
- Human bone retriever (`IHumanBoneRetriever`)
  - Mixamo and Biped preset 
- Human description parameters (`HumanDescriptionParameters`)
  - Sample preset 

See [presets](./Assets/Mochineko/DynamicUnityAvatarGenerator/Presets).

## Change log

See [CHANGELOG](./CHANGELOG.md).

## 3rd party notices

See [NOTICE](./NOTICE.md).

## License

Licensed under the [MIT](./LICENSE) license.

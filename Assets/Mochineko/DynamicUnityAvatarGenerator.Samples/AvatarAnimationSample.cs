#nullable enable
using System;
using System.Net.Http;
using System.Threading;
using Cysharp.Threading.Tasks;
using Mochineko.DynamicUnityAvatarGenerator.Presets;
using Mochineko.Relent.Result;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace Mochineko.DynamicUnityAvatarGenerator.Samples
{
    internal sealed class AvatarAnimationSample : MonoBehaviour
    {
        [SerializeField]
        private RuntimeAnimatorController? animatorController = null;

        private readonly CancellationTokenSource cancellationTokenSource = new();
        private IDisposable? disposable;

        private async void Start()
        {
            var cancellationToken = cancellationTokenSource.Token;

            var binary = await DownloadSampleModelAsync(cancellationToken);

            var instance = await LoadGLTFAsync(binary, "sample_glTF", cancellationToken);
            disposable = instance;
            foreach (var renderer in instance.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }

            var (avatar, map) = AvatarGenerator.GenerateHumanoidAvatar(
                    instance.gameObject,
                    MixamoAndBipedRootBoneRetriever.Preset,
                    MixamoAndBipedHumanBoneRetrievers.Preset,
                    HumanDescriptionParametersPreset.Preset)
                .Unwrap();

            var animator = instance.gameObject.AddComponent<Animator>();
            animator.avatar = avatar;
            animator.runtimeAnimatorController = animatorController;
        }

        private void OnDestroy()
        {
            cancellationTokenSource.Dispose();
            disposable?.Dispose();
        }

        private async UniTask<byte[]> DownloadSampleModelAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var requestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://models.readyplayer.me/62209f5170f4fcd07809c611.glb");

            using var httpClient = new HttpClient();
            using var responseMessage = await httpClient.SendAsync(requestMessage, cancellationToken);

            return await responseMessage.Content.ReadAsByteArrayAsync();
        }

        private async UniTask<RuntimeGltfInstance> LoadGLTFAsync(
            byte[] binary,
            string name,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var glTF = new GlbBinaryParser(binary, name)
                .Parse();

            using var loader = new ImporterContext(
                data:glTF,
                materialGenerator:null,
                externalObjectMap:null,
                textureDeserializer:null
            );
            loader.InvertAxis = Axes.X;

            return await loader.LoadAsync(new RuntimeOnlyAwaitCaller());
        }
    }
}

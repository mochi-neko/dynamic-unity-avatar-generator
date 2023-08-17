#nullable enable
using System.Threading.Tasks;
using FluentAssertions;
using Mochineko.DynamicUnityAvatarGenerator.Presets;
using Mochineko.Relent.Result;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Mochineko.DynamicUnityAvatarGenerator.Tests
{
    internal sealed class AvatarGeneratorTest
    {
        [Test]
        [RequiresPlayMode(true)]
        public async Task GenerateHumanoidAvatarTest()
        {
            var gameObject = DummySkeletonCreator.CreateDummyHumanoidHierarchy();
            var rootBoneRetriever = new RegularExpressionRootBoneRetriever(@".*(?i)Hips$");
            var humanBoneRetrievers = DummySkeletonCreator.CreateDummyHumanBoneRetrievers();

            var (avatar, map) = (await AvatarGenerator.GenerateHumanoidAvatar(
                    gameObject,
                    rootBoneRetriever,
                    humanBoneRetrievers,
                    new HumanDescriptionParameters()
                )
                ).Unwrap();

            avatar.isValid.Should().BeTrue();
            avatar.isHuman.Should().BeTrue();
            avatar.humanDescription.skeleton.Length.Should().Be(22);
            avatar.humanDescription.human.Length.Should().Be(21);
            avatar.humanDescription.human.Length.Should().Be(map.Count);

            map[HumanBodyBones.Hips].transform.name.Should().Be("bone.Hips");

            var mappedFromAvatar = HumanBoneTransformMapCreator
                .MapFromAvatar(avatar, gameObject)
                .Unwrap();

            foreach (var fromAvatar in mappedFromAvatar)
            {
                fromAvatar.Value.Should().Be(map[fromAvatar.Key]);
            }

            Object.Destroy(avatar);
            Object.Destroy(gameObject);
        }

        /// <summary>
        /// See https://docs.readyplayer.me/ready-player-me/api-reference/avatars/full-body-avatars
        /// </summary>
        [Test]
        [RequiresPlayMode(true)]
        public async Task ReadyPlayerMeAvatarGenerationTest()
        {
            var gameObject = DummySkeletonCreator.CreateReadyPlayerMeHumanoidHierarchy();

            var (avatar, map) = (await AvatarGenerator.GenerateHumanoidAvatar(
                    gameObject,
                    MixamoAndBipedRootBoneRetriever.Preset,
                    MixamoAndBipedHumanBoneRetrievers.Preset,
                    HumanDescriptionParametersPreset.Preset
                ))
                .Unwrap();

            avatar.isValid.Should().BeTrue();
            avatar.isHuman.Should().BeTrue();
            avatar.humanDescription.skeleton.Length.Should().Be(33);
            avatar.humanDescription.human.Length.Should().Be(32);
            avatar.humanDescription.human.Length.Should().Be(map.Count);

            map[HumanBodyBones.Hips].transform.name.Should().Be("Hips");

            var mappedFromAvatar = HumanBoneTransformMapCreator
                .MapFromAvatar(avatar, gameObject)
                .Unwrap();

            foreach (var fromAvatar in mappedFromAvatar)
            {
                fromAvatar.Value.Should().Be(map[fromAvatar.Key]);
            }

            Object.Destroy(avatar);
            Object.Destroy(gameObject);
        }
    }
}

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Settings store by `ScriptableObject`.

## [0.2.0] - 2023-08-05

### Added
- Add transform map of human bones to result.
- Add transform map creator from existing `UnityEngine.Avatar`.

## [0.1.1] - 2023-08-04

### Fixed
- Fix to add parent of root bone to human bones.

## [0.1.0] - 2023-08-04

### Added
- Humanoid `UnityEngine.Avatar` generator.
- A root bone retriever by regular expression matching.
- A root bone retriever by specifying root bone instance.
- A root bone retriever by string comparison rule.
- A human bone retriever by regular expression matching.
- A human bone retriever by string comparison rule.
- A preset of root bone retriever for Mixamo and Biped.
- A preset of human bone retrievers for Mixamo and Biped.
- A preset of human description parameters.

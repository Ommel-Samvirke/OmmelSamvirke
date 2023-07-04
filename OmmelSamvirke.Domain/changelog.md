# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2023-07-04
### Changed
- To better accommodate the concept of page layouts, a new class has been introduced.
The class ContentBlockLayoutConfiguration contains data about the position and size of a ContentBlock.
These properties have been moved from ContentBlock to the new class. Instead, ContentBlock
now contains a reference to three ContentBlockLayoutConfiguration objects. These references
are used to describe the position and size of the ContentBlock in three different layouts:
  - Mobile
  - Tablet
  - Desktop

This change is a breaking change. All tests that depended on the old implementation of ContentBlock
have been updated to reflect the new implementation.

## [0.8.0] - 2023-07-03
### Changed
- Added interface IContentBlockData which can be used when a collection
of ContentBlockData elements is needed.

## [0.7.0] - 2023-07-01
### Added
- Domain classes for the Pages feature
- Domain class for sharing content to Facebook

### Changed
- Namespace and return types of interfaces for the Newsletters feature

## [0.6.0] - 2023-06-22
### Added
- Repository interfaces for the Newsletter feature.

## [0.5.0] - 2023-06-11
### Added
- Service interfaces for the Newsletter feature.

## [0.4.0] - 2023-06-10
### Added
- Likes counter for the Newsletter model.
- NewsletterComment model for adding comments to newsletters.

## [0.3.0] - 2023-06-09
### Added
- Models for the Newsletters feature
- Common ModelIdValidator class for validating model ids

### Changed
- Refactored the BaseModel class to include a non-parameterless constructor.
- Refactored the BaseModel class to implement the interface IEquatable.
- Update this changelog file to the newest changes appear at the top of the file.

## [0.2.0] - 2023-06-07
### Added
- Model templates for the Newsletter feature.
- The NewsletterCommunity model has been completed.

## [0.1.0] - 2023-06-07
### Added
- BaseModel class, containing basic model fields. All model classes must inherit
  from BaseModel.

### Changed
- Updated README to provide more context about the Domain layer.

## [0.0.1] - 2023-05-31
### Added
- The project was generated and none of the default classes have been modified.
- This changelog file to document future changes.
- The _Docs_ directory. This should be used for requirements and documentation.

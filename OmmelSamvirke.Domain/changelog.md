# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

# vNext

## 0.13.0

- Added multi partition read.

## 0.12.1

- Fixed github action CI.

## 0.12.0

- Updated references
- Bumped full framework to 4.8

## 0.11.x

- Added ability to use a readonly connection on MongoDb persistence layer.

### Breaking Changes

- IPersistence does not support anymore the AppendAsync method, you should use a Stream class to have this behavior. This is part of removing the support of IPersistence for generation of Index value.

## 0.10.4

- Disabled parallelism on tests.

## 0.10.3

- Updated mongodb driver to latest version

## 0.10.2

- Updated mongodb driver to latest version

## 0.10.1

- Updated mongodb driver to latest version
- Fixed cake build.

## 0.9.0

- Minor fixes

## 0.8.2

- Fix wrong merge.

## 0.8.1

- Updated mongodb Driver 
- Updated to latest version of Newtonsoft.Json

## 0.8.0

- Minor modification to Sql Persistence.

## ...

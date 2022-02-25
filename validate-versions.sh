#!/usr/bin/env bash

YQ_VERSION=v4.20.2
YQ_OS=linux
YQ_ARCH=amd64
YQ_BINARY="yq_${YQ_OS}_${YQ_ARCH}"

wget --quiet "https://github.com/mikefarah/yq/releases/download/${YQ_VERSION}/${YQ_BINARY}" --output-document=/usr/bin/yq

echo "::notice Downloaded yq"

chmod +x /usr/bin/yq

echo "::notice Made yq executable"

CSPROJ_VERSION=$(yq eval  --input-format=xml ".Project.PropertyGroup[0].Version" CorundumGames.Codegen/CorundumGames.Codegen.csproj)

echo ::notice file="CorundumGames.Codegen/CorundumGames.Codegen.csproj"::Version given in CorundumGames.Codegen.csproj is ${CSPROJ_VERSION}

PACKAGE_JSON_VERSION=$(yq eval ".version" CorundumGames.Codegen/package.json)

echo "::notice::Version given in package.json is ${PACKAGE_JSON_VERSION}"


if [[ "$GITHUB_REF_TYPE" = "tag" && "$GITHUB_REF_NAME" = v*.*.* ]]; then
  echo "::notice::Version tag is $GITHUB_REF_NAME"
  # If this job was run from a pushed version tag...
  # The first condition ensures that this job was run due to a tag.
  # The second condition ensures that this is a version tag.
  if [[ "$GITHUB_REF_NAME" != "v$CSPROJ_VERSION" ]]; then
    exit 1
  elif [[ "$GITHUB_REF_NAME" != "v$PACKAGE_JSON_VERSION" ]]; then
    exit 1
  fi
elif [[ "$CSPROJ_VERSION" != "$PACKAGE_JSON_VERSION" ]]; then
  # If the version listed in the project file and the UPM package aren't the same...
  # This wasn't from a pushed tag, but we still want to validate the versions
  echo "::error::Files do not specify the same versions"
  exit 1
fi

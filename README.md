# TheBeRealProject

Blazor WASM app to display 1 years of [BeReal](https://bereal.com/) photos

[![.NET build](https://github.com/aguacongas/TheBeRealProject/actions/workflows/dotnet.yml/badge.svg)](https://github.com/aguacongas/TheBeRealProject/actions/workflows/dotnet.yml) [![Deploy to Pages](https://github.com/aguacongas/TheBeRealProject/actions/workflows/deploy.yml/badge.svg)](https://github.com/aguacongas/TheBeRealProject/actions/workflows/deploy.yml)

[Visit the result](https://aguacongas.github.io/TheBeRealProject/)

## Want yours ?

- Save your [BeReal](https://bereal.com/) photos to a GitHub repo
- Generate a [Fine-grained personal access token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token#fine-grained-personal-access-tokens) to this repo with *Contents read-only* permission
- Fork this repo
- Set an [action secret](https://docs.github.com/en/actions/security-guides/encrypted-secrets#creating-encrypted-secrets-for-a-repository) named *ASSETS_TOKEN* with the generated Fine-grained personal access token
- Set an [action variable](https://docs.github.com/en/actions/learn-github-actions/variables#creating-configuration-variables-for-a-repository) named *API_URL* with API URL to access to your photos. It should look likes *https://api.github.com/repos/{owner}/{repo}/contents/*. ex: `https://api.github.com/repos/aguacongas/TheBeRealProjectAssets/contents/`
- Run the **Deploy to Pages** action



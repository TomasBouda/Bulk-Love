# Thanks.NET  [![NuGet](https://img.shields.io/nuget/v/Thanks.NET.svg)](https://www.nuget.org/packages/Thanks.NET/)

Thanks.NET is utility that allows you to easily thank authors of packages you are using in your projects by staring their github repository.

## Installation

**.NET Core Global Tool**

Run from console:

```shell
dotnet tool install --global Thanks.NET
```

## Usage

Once installed you can run following from console/powershell:

```shell
thanks.net -s C:\Path\To\Solution -t ***YOUR_GIRHUB_TOKEN***
```

<img src="https://github.com/TomasBouda/Thanks.NET/blob/master/images/thanksnet.png">

You can also run just `thanks.net` and application will ask you for parameters.

### How to get github personal access token

- Go to https://github.com/settings/tokens
- Click on `Generate new token`
- Enter token description e.g. `Thanks.NET`
- Check `public_repo`
- Click on `Generate token`
- Copy the newly generated token. Looks like this `433fb4204cced0b285d3a29f72870495198bc29b`
- Save your token in secure place. For example https://keepass.info/

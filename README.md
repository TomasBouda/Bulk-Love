# Thanks.NET  [![NuGet](https://img.shields.io/nuget/v/Thanks.NET.svg)](https://www.nuget.org/packages/Thanks.NET/)

Thanks.NET is utility that allows you to easily thank authors of packages you are using in your projects by staring their github repository.

## Installation

**.NET Core Global Tool**

Run from console/powershell:

```shell
dotnet tool install --global Thanks.NET
```

## Usage

Once installed you can run following from console/powershell:

```shell
thanks.net -s C:\Path\To\Solution -t YOUR_GIRHUB_TOKEN
```
*Note: if you don't know how to get github access token, see section below*

You can also run just `thanks.net` and application will ask you for parameters(solution directory and github token).

<img src="https://github.com/TomasBouda/Thanks.NET/blob/master/images/thanksnet.png">

Applicaton will provide output containing information about packages found in given directory.
Succesfully stared package is marked in console output by `*` character on start of line.

<img src="https://github.com/TomasBouda/Thanks.NET/blob/master/images/stared.PNG">

Unfortunately not all packages have filled their projectUrl information on <a href="nuget.org">nuget.org</a> or their project url is not on github.com.
In case package has at least some project url(e.g. <a href="https://www.nuget.org/packages/Select2.js">select2</a>), app will try to parse github repository url from there.

If application cannot star package repositoy, there will be `x` character

<img src="https://github.com/TomasBouda/Thanks.NET/blob/master/images/not_stared.PNG">

or none

<img src="https://github.com/TomasBouda/Thanks.NET/blob/master/images/not_stared2.PNG">
<img src="https://github.com/TomasBouda/Thanks.NET/blob/master/images/not_stared3.PNG">

### How to get github personal access token

- Go to https://github.com/settings/tokens
- Click on `Generate new token`
- Enter token description e.g. `Thanks.NET`
- Check `public_repo` *(see <a href="https://github.com/TomasBouda/Thanks.NET/blob/master/images/github_token.PNG">here</a>)*
- Click on `Generate token`
- Copy the newly generated token. Looks like this `433fb4204cced0b285d3a29f72870495198bc29b`
- Save your token in secure place. For example https://keepass.info/

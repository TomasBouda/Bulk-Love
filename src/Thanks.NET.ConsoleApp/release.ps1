## Releases standalone win10-x64 application

dotnet publish -c Release -r win10-x64 -v m

if(!(Test-Path -Path .\warp-packer.exe)){
	[Net.ServicePointManager]::SecurityProtocol = "tls12, tls11, tls" ; Invoke-WebRequest https://github.com/dgiagio/warp/releases/download/v0.3.0/windows-x64.warp-packer.exe -OutFile warp-packer.exe
}

New-Item -ItemType Directory -Force -Path bin/Release/netcoreapp2.2/win10-x64/SelfContained

.\warp-packer --arch windows-x64 --input_dir bin/Release/netcoreapp2.2/win10-x64/publish --exec ThanksNET.ConsoleApp.exe --output bin/Release/netcoreapp2.2/win10-x64/SelfContained/ThanksNET.exe
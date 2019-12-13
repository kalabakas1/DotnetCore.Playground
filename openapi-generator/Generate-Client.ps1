param(
	[String] $swaggerJsonPath,
	[String] $projectName,
	[String] $srcFolderPath,
    [bool] $cleanUp = $true
)

if(!(Test-Path .\temp))
{
    New-Item -ItemType 'directory' -Name temp
}

cd .\temp

openapi-generator generate -g csharp-netcore -i $swaggerJsonPath -p="optionalAssemblyInfo=false,optionalProjectFile=false,packageName=$projectName,sourceFolder=..\..\"

cd ..

if($cleanUp -eq $true)
{
    Remove-Item temp -Force -Confirm:$false -Recurse
}

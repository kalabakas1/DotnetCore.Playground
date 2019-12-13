$swaggerPath = "http://localhost:5000/swagger/v1/swagger.json"
$projectName = "Playground.Api.Client"
$apiProjectName = "Playground.Api"

Get-Process -Name dotnet | Stop-Process -Confirm:$false -Force

cd ..\$apiProjectName
Start-Process dotnet run
cd ..\openapi-generator

Start-Sleep -Seconds 5

$swaggerResponse = Invoke-WebRequest $swaggerPath

.\Generate-Client.ps1 -swaggerJsonPath $swaggerPath -projectName $projectName -srcFolderPath ..\


Get-Process -Name dotnet | Stop-Process -Confirm:$false -Force
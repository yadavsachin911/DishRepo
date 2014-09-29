param(
      [string]$project = $null,
      [switch]$notests = $false
      )

$originalTitle = [System.Console]::Title
$start = [System.DateTime]::Now


$currentFolder = new-object System.IO.DirectoryInfo -argumentlist $MyInvocation.MyCommand.Definition
$branchFolder  = $currentFolder.Parent;
$parentFolder  = [string]($branchFolder.Parent.FullName).ToLower()
$Dish_DevRoot  = [string]($env:Dish_DevRoot).ToLower()

"CurrentFolder : $currentFolder,BranchFolder : $branchFolder,ParentFolder : $parentFolder,RootFolder : $Dish_DevRoot"
if($parentFolder -ne $Dish_DevRoot)
{
  "This path is not nested one level under the Dish Dev Root,please run"
  "from the branch folder so that the script may auto-detect your branch"
   return
}

$branch = $branchFolder.Name 
#& $SpgMo_DevRoot\$branch\Vs2013.ps1

"Performing all build on $branch"
$projects=@()

#-- Always clean bins before starting
function CleanBinaries()
{
"Removing BIN and OBJ folder"
#.\DirSeeknDestroy.exe -path . -dirs "bin;obj"
  Get-ChildItem .\ -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }
}

function RunUnitTest($unitTestAssembly)
{
if($notests -eq $True){return $True}
$fs = New-Object -ComObject Scripting.FileSystemObject
$f = $fs.GetFile("C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\mstest.exe")
$mstestPath = $f.shortpath   
$arguments = " /testcontainer:" + $unitTestAssembly
Write-Host "Running Unit tests"
Write-Host "$mstestPath $arguments"
invoke-expression "$mstestPath $arguments" | Write-Host
if($LASTEXITCODE -ne 0)
{
Write-Host "Unit tests failed" -BackgroundColor Red -ForegroundColor White
return $False
}
return $True
}

$projects += "Dish_Application"
function Dish_Application
{
   Set-Variable -Name All_Build_Current_Project -Value "Dish_Application" -Scope 1
   [System.Console]::Title = "All_Build_Current_Project"
   C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSbuild /m $Dish_DevRoot\$branch\Dish\DishesSolution\DishesApplication.sln /t:clean /p:Configuration=Debug | out-host
   if($LASTEXITCODE -ne 0) {return $False}
   C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSbuild /m $Dish_DevRoot\$branch\Dish\DishesSolution\DishesApplication.sln /t:build /p:Configuration=Debug | out-host
   if($LASTEXITCODE -ne 0) {return $False}
   C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSbuild /m $Dish_DevRoot\$branch\Dish\DishesSolution\DishesApplication.sln /t:clean /p:Configuration=Release | out-host
   if($LASTEXITCODE -ne 0) {return $False}
   C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSbuild /m $Dish_DevRoot\$branch\Dish\DishesSolution\DishesApplication.sln /t:build /p:Configuration=Release | out-host
   if($LASTEXITCODE -ne 0) {return $False}
    
   $success = RunUnitTest "$Dish_DevRoot\$branch\Dish\DishesSolution\DishesApplicationTest\bin\Debug\DishesApplicationTest.dll"

   if($success -ne $True) {return $False}

   return $True;

}

#--------------------Main Body-----------------------------
#CleanBinaries

$allSuccessful = $True
if($project -eq "")
{
foreach($projectFunction in $projects)
{
    "Building $projectFunction"
    $allSuccessful = (invoke-expression $projectFunction);
    if($allSuccessful -eq $False)
    {
       "-------------------------------------------"
       "Error building $All_Build_Current_Project"
       "ErrorLavel $ErrorLevel"
        break
    }
}
}
else
{
    $allSuccessful = (invoke-expression $project)
}

if($allSuccessful -eq $True)
{
    "--------------------------------------------------"
    "Successfully Built all projects"
}
else
{
    "-------------------------------------------------------------"
    "Failed to build all projects"
}

$buildDuration = ([System.Datetime]::Now - $start).toString()
"Build duration: $buildDuration"
""
[System.Console]::Title = $originalTitle
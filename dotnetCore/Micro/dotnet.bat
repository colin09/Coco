:: dotnet core micro
@echo off
cls
echo ####################################################################################################################
echo #####
set rootPath=D:\repository\mine\Coco\dotnetCore\Micro

echo ;注释行,这是临时文件,用完删除 >test.txt
echo service-center >> test.txt
echo service-configs >> test.txt
echo service-gateway >> test.txt

::echo service-core\configs >> test.txt
::echo service-core\identity >> test.txt
::echo service-core\order >> test.txt
::echo service-core\user >> test.txt
::echo service-core\S01 >> test.txt
::echo service-core\S02 >> test.txt
::echo service-core\S02-2 >> test.txt



echo ----[micro service project]----------------------------------------------------------------------

FOR /F "eol=;delims=, " %%i in (test.txt) do (
echo ==========================================================================
echo [ %%i ] 
echo ============
cd %rootPath%\%%i
	if not exist pom.xml (
		::dotnet build
		dotnet publish 
		cd %rootPath%\%%i\bin\Debug\netcoreapp2.1
		for %%j in (*.dll) do (
			cd %rootPath%\%%i\bin\Debug\netcoreapp2.1\publish
			start powershell.exe -Command "dotnet %%j" -NoExit -WindowStyle "Minimized"
		)	
	)
	
cd %rootPath%\%%i	
	if exist pom.xml (
		mvn compile clean package
		cd %rootPath%\%%i\target
		for %%j in (*.jar) do (
			::java -jar %%j
			start powershell.exe -Command "java -jar %%j" -NoExit -WindowStyle "Minimized"
		)
	)




)
echo ==========================================================================[over]


cd %rootPath%
Del test.txt




::mvn compile
::mvn test 
::mvn clean package
::mvn install
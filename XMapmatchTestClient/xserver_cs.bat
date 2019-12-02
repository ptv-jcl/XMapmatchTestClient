:: when doing the call to set the visual studio environment variables please uncomment the correct version
:: if the VSxxxCOMNTOOLS is not defined in your environment please replace it to the path to vsvars32.bat
:: this is usually something like C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\

:: Visual Studio 2015
call "%VS140COMNTOOLS%\vsvars32.bat"
:: Visual Studio 2013
:: call "%VS120COMNTOOLS%\vsvars32.bat"
:: Visual Studio 2012
:: call "%VS110COMNTOOLS%\vsvars32.bat"
:: Visual Studio 2010
:: call "%VS100COMNTOOLS%\vsvars32.bat"


:: uncomment the correct line for which components you have active
:: you may want to replace localhost by a different host/IP if the xServer are not running on the same machine as where you excecute this script

:: xRoute - xMap
 :: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50010/xmap/ws/XMap?WSDL
:: xLocate - xMap
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL
:: xLocate - xRoute
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL
:: xLocate - xMap - xRoute
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL
:: xLocate - xMap - xRoute - xTour
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL http://localhost:50090/xtour/ws/XTour?WSDL
:: xLocate - xMap - xRoute - xTour - xDima
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL http://localhost:50090/xtour/ws/XTour?WSDL http://localhost:50070/xdima/ws/XDima?WSDL 
:: xLocate - xMap - xRoute - xMapMatch
wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL http://localhost:50040/xmapmatch/ws/XMapmatch?WSDL
:: xMap - xRoute - xMapMatch
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50040/xmapmatch/ws/XMapmatch?WSDL
:: xLocate - xMap - xRoute - xTour - xDima - xMapMatch - xTerritory
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL http://localhost:50090/xtour/ws/XTour?WSDL http://localhost:50070/xdima/ws/XDima?WSDL  http://localhost:50040/xmapmatch/ws/XMapmatch?WSDL http://localhost:50060/xterritory/ws/XTerritory?WSDL
:: xTerritory - xTour - xLocate - xMap - xRoute 
:: wsdl /namespace:XServer  /o:XServer.cs /sharetypes http://localhost:50060/xterritory/ws/XTerritory?WSDL http://localhost:50090/xtour/ws/XTour?WSDL http://localhost:50010/xmap/ws/XMap?WSDL http://localhost:50030/xroute/ws/XRoute?WSDL http://localhost:50020/xlocate/ws/XLocate?WSDL 
@PAUSE 
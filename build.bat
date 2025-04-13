cd YachtingBot.Tests
dotnet test
cd ../YachtingBot
dotnet publish -c release --self-contained --runtime linux-x64 -p:PublishSingleFile=true 
cd ..
pause
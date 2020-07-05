echo "Cleaning"
rm -f latextools-osx-x64.zip

echo "Building"
dotnet publish -c Release -r osx-x64 --self-contained=true -p:PublishTrimmed=true

echo "Removing debug symbols"
rm -f bin/Release/netcoreapp3.1/osx-x64/publish/*.pdb

echo "Zipping"
zip -j latextools-osx-x64 bin/Release/netcoreapp3.1/osx-x64/publish/*

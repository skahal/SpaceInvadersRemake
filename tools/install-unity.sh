echo 'Downloading from http://netstorage.unity3d.com/unity/2046fc06d4d8/MacEditorInstaller/Unity.pkg'
curl -O http://netstorage.unity3d.com/unity/2046fc06d4d8/MacEditorInstaller/Unity.pkg

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
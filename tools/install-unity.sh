echo 'Downloading from http://netstorage.unity3d.com/unity/afd2369b692a/MacEditorInstaller/Unity-5.1.2f1.pkg'
curl -O http://netstorage.unity3d.com/unity/afd2369b692a/MacEditorInstaller/Unity-5.1.2f1.pkg

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity-5.1.2f1.pkg -target /
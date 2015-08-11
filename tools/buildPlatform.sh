echo "Building $1 ..."
mkdir $(pwd)/../build/SpaceInvadersRemake.$2 
/Applications/Unity/Unity.app/Contents/MacOS/Unity -$1 $(pwd)/../build/SpaceInvadersRemake.$2/SpaceInvadersRemake.$2.$3 -projectPath $(pwd)/../src/SpaceInvadersRemake/ -quit -batchmode -nographics -silent-crashes -logFile $(pwd)/$1.log
cd $(pwd)/../build

echo "Zipping SpaceInvadersRemake.$2.zip ..."
zip -r SpaceInvadersRemake.$2.zip SpaceInvadersRemake.$2

echo "Done."
#!/bin/bash

#get latest version of nuget
curl -S -L  http://nuget.org/nuget.exe > .nuget/nuget.exe

mono --runtime=v4.0 .nuget/NuGet.exe install NUnit.Runners -Version 2.6.3 -o packages

target="$*"

runTest(){
    mono --runtime=v4.0 packages/NUnit.Runners.2.6.3/tools/nunit-console.exe -noxml -nodots -labels -stoponerror "$target"
   if [ $? -ne 0 ]
   then   
     exit 1
   fi
}

runTest $1 -exclude=Performance

exit $?

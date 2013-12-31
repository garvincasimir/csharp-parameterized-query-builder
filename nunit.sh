#!/bin/bash

#Setup database
if [ "$DbServer" = "MariaDB" ]; then
   wget http://ftp.osuosl.org/pub/mariadb/mariadb-5.3.12/kvm-tarbake-jaunty-x86/mariadb-5.3.12.tar.gz
   tar -xvzf mariadb-5.3.12.tar.gz
   cd mariadb-5.3.12
   ./configure 
   make
   sudo make install
   cd ..
else
    wget http://cdn.mysql.com/Downloads/MySQL-5.1/mysql-5.1.73-osx10.6-x86_64.dmg
    hdiutil attach -quiet "mysql-5.1.73-osx10.6-x86_64.dmg"
    sudo installer -pkg "/Volumes/mysql-5.1.73-osx10.6-x86_64/mysql-5.1.73-osx10.6-x86_64.pkg" -target /
    sudo /usr/local/mysql/bin/mysqld_safe &
fi

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

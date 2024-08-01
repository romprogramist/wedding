cd C:/Users/admin/RiderProjects/AlikAndFlorasWedding/AlikAndFlorasWedding/bin/Release/net7.0/publish
Compress-Archive -Path ./* -DestinationPath ./alikandfloraswedding.zip
scp alikandfloraswedding.zip roman@95.163.236.186:~
rm alikandfloraswedding.zip

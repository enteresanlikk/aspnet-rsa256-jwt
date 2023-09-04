# aspnet-rsa256-jwt
	NOTE: This project was developed as a solution to the error "IDX10634: Unable to create the SignatureProvider. Algorithm: 'RS256'" error.

## Creating private.pem and public.pem files - [Documentation](https://www.ibm.com/docs/en/rpa/21.0?topic=keys-generating-private-public-key-pair)
1. ```openssl genrsa -out private.pem 4096```
2. ```openssl rsa -pubout -in private.pem -out public.pem```
3. Move the created files to the ***Credentials*** folder in the project.

**Note**: If you get openssl not found error, you should install openssl. Or you can start CMD with Administrator permission in ```C:\Program Files\Git\usr\bin``` folder and use openssl.

## Required packages
- PemUtils - [https://www.nuget.org/packages/PemUtils/](https://www.nuget.org/packages/PemUtils/)
- Microsoft.IdentityModel.Tokens - [https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens/](https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens/)
# Encrypt Decrypt Plugin for Xamarin, UWP and .NET

>
> Library version 2.0 has been published. This nuget contais .Net Standard version of this EncryptDecrypt logic.
> New code at src.NetStandard.
>
> Nuget version 2.0.x
>
> Pending to update this README to reference the new code.
>

A simple way to encrypt and decrypt a string. This version uses AES,ECD,PKCS7 for symmetric encryption, SHA256 for the password Hashing and UTF8 for encoding.

Great to secure data when saving to disk on a mobile devices without using vault storages or  sending data securely to a backend without relying on network protocols.

Another scenario is the use of session less backend user info tokens. A backend creates user data tokens and send them to the client encrypted. On every client call to backend it may add the token on the header request. This will allow the backend to receive client information to be be used to satisfy the request, with the garanty that he has created the token and the information is acureate. This avoids the backend to querya database for client information on each request based on user Id or session coockie. 

### Setup
* Available on NuGet: https://www.nuget.org/packages/BiT21.CrossPlatform.Plugin.EncryptDecrypt/ [![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.DeviceInfo.svg?label=NuGet)](https://www.nuget.org/packages/TBD/)
* Install into your PCL/netstandard project, Client adn .NET projects.

**Platform Support**

|Platform|TFM|Version|
| ------------------- | ------------------: | ------------------: |
|Xamarin.Android|MonoAndroid10|API 10+|
|Xamarin.iOS|Xamarin.iOS10|iOS 7+|
|Windows 10 UWP|UAP10|10+|
|.NetFramework|net45|4.5+|

### API Usage
To gain access to the EncryptDecrypt class use this methord.
```csharp
IEncryptDecrypt encryptDecrypt = EncryptDecrypt.CrossEncryptDecrypt.Current;
```

Thefore the initialization will stand:
```csharp
IEncryptDecrypt encryptDecrypt = EncryptDecrypt.CrossEncryptDecrypt.Current;

string pwd = "password1234";

string data = "This is the string that we need to encrypt to store or send securely.";

string encryptedData = await encryptDecrypt.EncryptStringAsync(pwd, data);

// Store or send the data on will.
// When needed you can recrypt using the same pwd used on encryption

string decryptedData = await encryptDecrypt.DecryptStringAsync(pwd, encryptedData);

Assert.AreEqual(data, decryptedData);
```

### Roadmap
Planning to extend the plugin with
* Export hashed Key to allow  Decrypt using Key. Recovery process for password loss.
* Use SecureString to hold the password.
 
### Contributions
Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

### License
Under MIT, see LICENSE file.

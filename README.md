# Encrypt Decrypt Plugin for Xamarin and Windows

A simple way to encrypt and decrypt a string.

### Setup
* Available on NuGet: http://www.nuget.org/packages/TBD [![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.DeviceInfo.svg?label=NuGet)](https://www.nuget.org/packages/TBD/)
* Install into your PCL/netstandard project and Client projects.

**Platform Support**

|Platform|TFM|Version|
| ------------------- | ------------------: | ------------------: |
|Xamarin.Android|MonoAndroid10|API 10+|
|Xamarin.iOS|Xamarin.iOS10|iOS 7+|
|Windows 10 UWP|UAP10|10+ (Pending development)|
|.NetFramework|net45|4.5+

### API Usage
To gain access to the FileService class use this methord.
```csharp
IEncryptDecrypt encryptDecrypt = EncryptDecrypt.CrossEncryptDecrypt.Current;
```

Thefore the initialization will stand:
```csharp
IEncryptDecrypt encryptDecrypt = EncryptDecrypt.CrossEncryptDecrypt.Current;

string pwd = "password1234";

string data = "This is the string that we need to encrypt to store or send securely.";

string e = await encryptDecrypt.EncryptStringAsync(pwd, data);

// Store or send the data on will.
// When needed you can recrypt using the same pwd used on encryption

string d = await encryptDecrypt.DecryptStringAsync(pwd, e);

Assert.AreEqual(data, d);
```

#### Roadmap
Planning to extend the plugin with

* UWP Support. 
* Export Key from password and Decrypt with Key.
* Use SecureString to hold the password.
* Use diferent alhorithms.
 
#### Contributions
Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

#### License
Under MIT, see LICENSE file.


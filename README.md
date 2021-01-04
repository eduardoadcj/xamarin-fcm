# Xamarin FCM Integration Sample
##### An example of how to integrate Firebase Cloud Messaging with Xamarin Android Applications.

### About Firebase Cloud Messaging
Firebase Cloud Messaging (FCM) provides a connection between your server and devices that
allows you to deliver and receive messages and notifications on iOS, Android,
and the web. See more details on [Google Documentations](https://firebase.google.com/docs/cloud-messaging).

### Objective
This project implements the server and mobile part of an application. The main goal is to create
an way of receive and send messages using Firebase Cloud Messaging and Xamarin Mobile Apps. 
On the implemented example, the server side send a message to the Xamarin application that calls an Android foreground service.

### About
In order to create the server, was implemented a NodeJS project that uses Firebase Admin SDK. But, there is not the only way to do that.
On reality, any language that supports Firebase Admin SDK can be used. And, in cases that is not an server available, a simple 
HTTP request to Firebase's server can do the trick.

The most problematic part is integrating the firebase in Xamarin Android project. To make it easier, the way to do that can be
separated in a fell steps:

* Update the Xamarin Forms and dependencies to the last stable version available.
* Install last stable version of [Xamarin.Firebase.Messaging](https://www.nuget.org/packages/Xamarin.Firebase.Messaging/121.0.1?_src=template) package.
* Install last stable version of [Xamarin.Google.Dagger](https://www.nuget.org/packages/Xamarin.Google.Dagger/2.27.0?_src=template) package.
* Create a Firebase project and add the Android package name correctly.
* Download the generated credentials json to root android project directory.
* On Visual Studio, find the downloaded file, go to properties and defines the Compile Action to GoogleServiceJson. 

With all the steps done, the next part is to identify the different messages that will come from Firebase and act accordingly.

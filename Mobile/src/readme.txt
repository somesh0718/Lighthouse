Ionic Generate =>
This command uses the Angular CLI to generate features such as pages, components, directives, services, etc.

You can specify a path to nest your feature within any number of subdirectories. 
For example, specify a name of "pages/New Page" to generate page files at src/app/pages/new-page/.

Type =>	The type of generator (e.g. component, directive, page, pipe, provider, tabs)

$ ionic generate [<type>] [<name>]
$ ionic generate page Login
$ ionic generate page Detail --no-module
$ ionic generate page About --constants
$ ionic generate component Produnct
$ ionic generate component contact/form
$ ionic generate component login-form --change-detection=OnPush
$ ionic generate directive ripple --skip-import
$ ionic generate service api/user
$ ionic generate provider
$ ionic generate tabs
$ ionic generate pipe MyFilterPipe

--no-module	Do not generate an NgModule for the component
--constants	Generate a page constant file for lazy-loaded pages

Ionic 6 Cordova | Create project - Run in device - Generate APK file | Complete guide for Beginners
https://ionicframework.com/docs/developing/android
https://www.youtube.com/watch?v=gElmlw6PoNA&ab_channel=CodingTechnyks

Project Types => The Ionic CLI works with a variety of Ionic project types.
ionic-angular: Ionic Angular
ionic1: Ionic 1

Ionic Angular => Ionic Angular (ionic-angular) uses Angular 5 and @ionic/app-scripts for tooling.
    You can start a new Ionic Angular app with the following command:
    $ ionic start myNewProject --type=ionic-angular 

Ionic 1 => Ionic 1 (ionic-v1) uses AngularJS.
    You can start a new Ionic 1 app with the following command:
    $ ionic start OasysV1n --type=ionic1  --cordova --capacitor
    $ ionic start LHMobile blank --type=angular --cordova --capacitor

https://docs.npmjs.com/try-the-latest-stable-version-of-npm
npm install -g npm@latest

https://stackoverflow.com/questions/32132434/set-adb-vendor-keys
adb kill-server && adb start-server

$ npm install cordova-plugin-android-permissions 
$ npm install @awesome-cordova-plugins/android-permissions 
$ ionic cap sync
 
The package-lock.json file was created with an old version of npm, so supplemental metadata must be fetched from the registry.

npm config set python C:\Users\rakes\AppData\Local\Programs\Python\Python310\python.exe

There are three templates available:
    tabs: A tabs based layout
    sidemenu: A sidemenu based layout
    blank: An empty project with a single page

Ionic CLI => The Ionic Command Line Interface (CLI) is your go-to tool for developing Ionic apps.
    $ npm install -g ionic@latest

Using Cordova => Integrate Ionic with Cordova to bring native capabilities to your app.
    $ npm install -g cordova@latest

Manage Cordova platform targets
    $ ionic cordova platform add ios
    
    $ ionic cordova platform remove android
    $ ionic cordova platform add android@10.1.2
    $ ionic cordova prepare android
    $ ionic cordova build android --warning-mode all
    $ gradle wrapper --gradle-version=4.4

    $ ionic cordova run android --prod --release -- -- --minSdkVersion=21
    $ ionic cordova run android --prod --release -- -- --versionCode=55

Clean Project Build
    $ cordova clean

Android Publishing
    $ ionic cordova build --release android   
    $ ionic cordova build android --prod --release
    # ionic cordova run android --prod --release 

Use Cordova to build for Android and iOS platform targets
    $ ionic cordova build [options]    

Examples​
    $ ionic cordova build android
    $ ionic cordova build android --buildConfig=build.json
    $ ionic cordova build android --prod --release -- -- --gradleArg=-PcdvBuildMultipleApks=true
    $ ionic cordova build android --prod --release -- -- --keystore=filename.keystore --alias=myalias
    $ ionic cordova build android --prod --release -- -- --minSdkVersion=21
    $ ionic cordova build android --prod --release -- -- --versionCode=55
    $ ionic cordova build android --prod --release --buildConfig=build.json
    $ ionic cordova build ios
    $ ionic cordova build ios --buildConfig=build.json
    $ ionic cordova build ios --prod --release
    $ ionic cordova build ios --prod --release -- --developmentTeam="ABCD" --codeSignIdentity="iPhone Developer" --packageType="app-store"
    $ ionic cordova build ios --prod --release --buildConfig=build.json
	
	
Once you installed the latest NodeJS and NPM software, then delete your node_modules folder and package-lock.json file:

rm -rf node_modules && rm package-lock.json
Once both are removed, then clean your NPM cache using the following command:

npm cache clean --force
Finally, try installing your dependencies again:

npm install
You should be able to install all dependencies without the maximum call stack size exceeded error now.	


You uploaded an APK or Android App Bundle which has an activity, activity alias, service or broadcast receiver with intent filter, but without 'android:exported' property set. This file can't be installed on Android 12 or higher. See: developer.android.com/about/versions/12/behavior-changes-12#exported
Your APK or Android App Bundle needs to have the package name aws.lighthouse.com.

Release for Android 12 users
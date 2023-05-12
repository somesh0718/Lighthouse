export const environment = {
  hmr: false,
  aws: false,
  lahi: false,
  gj: false,
  mh: true,
  jh: false,
  od: false,
  production: true,
  target: "mh",
  email: "support.mh.lighthouse@lend-a-hand-India.org",
  version: "2.21.1.15",
  //ApiBaseUrl: "https://maharashtra.lighthouse.net.in/LighthouseServices/"
  ApiBaseUrl: "https://stg-mh.lighthouse.net.in/LighthouseServices/"
};

//ApiBaseUrl: "http://localhost:61246/LighthouseServices/"
//ApiBaseUrl: "http://192.168.98.30:6345/LighthouseServices/"

// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

//Build Project => ionic build --prod --c/configuration=prod  lahi | aws | gj | mh  
// ionic serve --configuration=mh
// ionic cordova build android --release --configuration=mh
// ionic cordova build android --dev --configuration=mh
// ionic cordova run android --dev --configuration=mh

/*
 * 	"appId": "gj.lighthouse.com",
 * 	"appName": "Lighthouse GJ",
 * 	"versionCode": "10108",
 * 	"android_version": "1.1.08"
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 *
 * cordova platform rm/add android
 * config.xml   android.json    gradle.properties     settings.gradle
 *
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.

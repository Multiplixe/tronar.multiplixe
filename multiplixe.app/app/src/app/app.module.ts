import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic"

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

import { AngularFireModule } from '@angular/fire';
import { AngularFireMessagingModule } from '@angular/fire/messaging';
import { AngularFireStorageModule } from '@angular/fire/storage';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Injector } from '@angular/core';
import { ServiceWorkerModule, SwRegistrationOptions } from '@angular/service-worker';
import { environment, firebaseEnvironment } from '../environments/environment';

import { AppRestrictedComponent } from './app.restricted.component';
import { setAppInjector } from './app-injector';
import { SharedModule } from './shared-module/shared.module';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { ComponentModule } from './components/component.module';

import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { DataTransferService } from './services/data-transfer.service';

registerLocaleData(localePt);

@NgModule({
  declarations: [AppComponent, AppRestrictedComponent],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    AngularFireModule.initializeApp(firebaseEnvironment),
    ServiceWorkerModule.register('./ngsw-worker.js', { enabled: environment.production }),
    AngularFireMessagingModule,
    AngularFireStorageModule,
    ComponentModule
  ],
  providers: [
    StatusBar,
    SplashScreen,
    DataTransferService,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(injector: Injector) {
    setAppInjector(injector);
  }


}

import { BrowserModule } from '@angular/platform-browser';
import { Injector, LOCALE_ID, NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { setAppInjector } from './app-injector';
import { AuthInterceptor } from './interceptors/auth.interceptors';
import { ListItemsComponent } from './components/list-items/list-items.component';
import { LoaderComponent } from './components/loader/loader.component';
import { SetupPage } from './pages/webview/setup/setup.page';
import { TwitchCallbackPage } from './pages/webview/social-media-callback/twitch-callback.page';
import { TwitterCallbackPage } from './pages/webview/social-media-callback/twitter-callback.page';
import { YoutubeCallbackPage } from './pages/webview/social-media-callback/youtube-callback.page';
import { SocialMediaConnectionPage } from './pages/webview/social-media-connection/social-media-connection.page';
import { PasswordResetPage } from './pages/password-reset/password-reset.page';
import { ToastrModule } from 'ngx-toastr';
import { FacebookModule } from 'ngx-facebook';
import { AngularFireModule } from '@angular/fire';
import { firebaseEnvironment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    SetupPage,
    SocialMediaConnectionPage,
    ListItemsComponent,
    TwitterCallbackPage,
    TwitchCallbackPage,
    YoutubeCallbackPage,
    PasswordResetPage,
    LoaderComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    FacebookModule.forRoot(),
    AngularFireModule.initializeApp(firebaseEnvironment),
    ToastrModule.forRoot({
      positionClass : 'toast-bottom-center'
    })
  ],
  providers: [
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

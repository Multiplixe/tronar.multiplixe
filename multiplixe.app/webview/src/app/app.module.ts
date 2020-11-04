import { BrowserModule } from '@angular/platform-browser';
import { Injector, LOCALE_ID, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { SetupPage } from './pages/setup/setup.page';
import { SocialMediaConnectionPage } from './pages/social-media-connection/social-media-connection.page';
import { setAppInjector } from './app-injector';
import { ListItemsComponent } from './components/list-items/list-items.component';
import { FacebookModule } from 'ngx-facebook';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth.interceptors';
import { TwitterCallbackPage } from './pages/social-media-callback/twitter-callback.page';
import { LoaderComponent } from './components/loader/loader.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    SetupPage,
    SocialMediaConnectionPage,
    ListItemsComponent,
    TwitterCallbackPage,
    LoaderComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    FacebookModule.forRoot(),
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

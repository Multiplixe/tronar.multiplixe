import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PasswordResetPage } from './pages/password-reset/password-reset.page';
import { SetupPage } from './pages/webview/setup/setup.page';
import { FacebookCallbackPage } from './pages/webview/social-media-callback/facebook-callback.page';
import { TwitchCallbackPage } from './pages/webview/social-media-callback/twitch-callback.page';
import { TwitterCallbackPage } from './pages/webview/social-media-callback/twitter-callback.page';
import { YoutubeCallbackPage } from './pages/webview/social-media-callback/youtube-callback.page';
import { SocialMediaConnectionPage } from './pages/webview/social-media-connection/social-media-connection.page';

const routes: Routes = [
  {
    path: 'webview/social-media-connection/:socialmedia',
    component: SocialMediaConnectionPage
  },
  {
    path: 'webview/twitter-callback/:username',
    component: TwitterCallbackPage
  },
  {
    path: 'webview/twitch-callback',
    component: TwitchCallbackPage
  },
  {
    path: 'webview/youtube-callback',
    component: YoutubeCallbackPage
  },
  {
    path: 'webview/facebook-callback/:empresaId/:username',
    component: FacebookCallbackPage
  },  
  {
    path: 'webview',
    component: SetupPage
  },
  {
    path: 'webview/:socialmedia',
    component: SetupPage
  },  
  {
    path: 'redefinir-senha',
    component: PasswordResetPage
  }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

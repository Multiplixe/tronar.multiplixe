import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SetupPage } from './pages/setup/setup.page';
import { TwitchCallbackPage } from './pages/social-media-callback/twitch-callback.page';
import { TwitterCallbackPage } from './pages/social-media-callback/twitter-callback.page';
import { YoutubeCallbackPage } from './pages/social-media-callback/youtube-callback.page';
import { SocialMediaConnectionPage } from './pages/social-media-connection/social-media-connection.page';

const routes: Routes = [
  {
    path: 'setup/:socialmedia',
    component: SetupPage
  },
  {
    path: 'social-media-connection/:socialmedia',
    component: SocialMediaConnectionPage
  },
  {
    path: 'twitter-callback',
    component: TwitterCallbackPage
  },
  {
    path: 'twitch-callback',
    component: TwitchCallbackPage
  },
  {
    path: 'youtube-callback',
    component: YoutubeCallbackPage
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
